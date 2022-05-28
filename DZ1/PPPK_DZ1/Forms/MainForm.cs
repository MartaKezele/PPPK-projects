using PPPK_DZ1.Dal;
using PPPK_DZ1.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace PPPK_DZ1.Forms
{
    public partial class MainForm : Form
    {
        private const string SELECT = "select";
        private const string CREATE = "create";
        private const string ALTER = "alter";
        private const string DATABASE = "database";
        private const string TABLE = "table";
        private const string VIEW = "view";
        private const string PROC = "proc";
        private const string PROCEDURE = "procedure";
        private const string USE = "use";
        private const string INSERT = "insert";
        private const string UPDATE = "update";
        private const string DELETE = "delete";
        private const string DROP = "drop";
        private const string GO = "go";

        public MainForm()
        {
            InitializeComponent();
            Init();
        }

        private void Init() => LoadDatabases();

        private void LoadDatabases() => lbDatabases.DataSource = new List<Database>(RepoFactory.GetRepo().GetDatabases());

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
            => Application.Exit();

        private void LbDatabases_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshTablesViewsProcedures();
        }

        private void LbTables_SelectedIndexChanged(object sender, EventArgs e)
            => lbTableColumns.DataSource = (lbTables.SelectedItem as DBEntity).Columns;

        private void LbViews_SelectedIndexChanged(object sender, EventArgs e)
            => lbViewColumns.DataSource = (lbViews.SelectedItem as DBEntity).Columns;

        private void LbProcedures_SelectedIndexChanged(object sender, EventArgs e)
            => lbParameters.DataSource = (lbProcedures.SelectedItem as Procedure).Parameters;

        private void MiExecute_Click(object sender, EventArgs e)
        {
            lblMessage.Text = string.Empty;
            pnlResults.Controls.Clear();
            try
            {
                IList<KeyValuePair<bool, string>> commands = ParseCommands(rtbQuery.SelectedText.Length > 0 ? rtbQuery.SelectedText : rtbQuery.Text);
                ExecuteCommands(commands);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error parsing commands", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Clear()
        {
            lbTableColumns.DataSource = null;
            lbViewColumns.DataSource = null;
            lbParameters.DataSource = null;
        }

        private void RefreshTablesViewsProcedures()
        {
            Clear();
            lbTables.DataSource = (lbDatabases.SelectedItem as Database).Tables;
            lbViews.DataSource = (lbDatabases.SelectedItem as Database).Views;
            lbProcedures.DataSource = (lbDatabases.SelectedItem as Database).Procedures;
        }

        private IList<KeyValuePair<bool, string>> ParseCommands(string text)
        {
            IList<KeyValuePair<bool, string>> commands = new List<KeyValuePair<bool, string>>();

            string[] words = text.Split(' ', '\n');

            int position = 0;

            bool isQuery = false;
            bool isViewOrProc = false;
            
            string query = string.Empty;
            string nonQuery = string.Empty;

            while (position < words.Length)
            {
                if (words[position].Trim().ToLower().Equals(SELECT) && !isViewOrProc)
                {
                    var previousCommand = GetPreviousCommand(query, nonQuery);
                    if (GetPreviousCommand(query, nonQuery).Value != string.Empty)
                    {
                        commands.Add(previousCommand);
                    }

                    isQuery = true;
                    nonQuery = string.Empty;
                    query = words[position++].Trim() + " ";
                }
                else if (words[position].Trim().ToLower().Equals(CREATE) || words[position].Trim().ToLower().Equals(ALTER))
                {
                    var previousCommand = GetPreviousCommand(query, nonQuery);
                    if (GetPreviousCommand(query, nonQuery).Value != string.Empty)
                    {
                        commands.Add(previousCommand);
                    }

                    isQuery = false;
                    query = string.Empty;
                    nonQuery = words[position++].Trim() + " ";

                    if (words[position].Trim().ToLower().Equals(DATABASE) || words[position].Trim().ToLower().Equals(TABLE))
                    {
                        isViewOrProc = false;
                        nonQuery += words[position++].Trim() + " ";
                    }
                    else if (words[position].Trim().ToLower().Equals(VIEW) || 
                            words[position].Trim().ToLower().Equals(PROC) ||
                            words[position].Trim().ToLower().Equals(PROCEDURE))
                    {
                        isViewOrProc = true;
                        nonQuery += words[position++].Trim() + " ";
                    }
                    else
                    {
                        throw new Exception($"Unknown command: {nonQuery += words[position++].Trim()}.");
                    }
                }
                else if ((words[position].Trim().ToLower().Equals(USE) ||
                        words[position].Trim().ToLower().Equals(INSERT) || 
                        words[position].Trim().ToLower().Equals(UPDATE) ||
                        words[position].Trim().ToLower().Equals(DELETE) ||
                        words[position].Trim().ToLower().Equals(DROP)) && !isViewOrProc)
                {
                    var previousCommand = GetPreviousCommand(query, nonQuery);
                    if (GetPreviousCommand(query, nonQuery).Value != string.Empty)
                    {
                        commands.Add(previousCommand);
                    }

                    isQuery = false;
                    query = string.Empty;
                    nonQuery = words[position++].Trim() + " ";
                }
                else if (words[position].Trim().ToLower().Equals(GO))
                {
                    if (isViewOrProc)
                    {
                        isViewOrProc = false;
                    }
                    position++;
                }
                else if (isQuery)
                {
                    query += words[position].Trim() + " ";
                    if (position == words.Length - 1)
                    {
                        commands.Add(new KeyValuePair<bool, string>(true, query.Remove(query.Length - 1, 1)));
                        position++;
                    }
                    else if (words[++position].Trim().ToLower().Equals(GO))
                    {
                        commands.Add(new KeyValuePair<bool, string>(true, query.Remove(query.Length - 1, 1)));
                        query = string.Empty;
                        nonQuery = string.Empty;
                    }
                }
                else
                {
                    nonQuery += words[position].Trim() + " ";
                    if (position == words.Length - 1)
                    {
                        commands.Add(new KeyValuePair<bool, string>(false, nonQuery.Remove(nonQuery.Length - 1, 1)));
                        position++;
                    }
                    else if (words[++position].Trim().ToLower().Equals(GO))
                    {
                        commands.Add(new KeyValuePair<bool, string>(false, nonQuery.Remove(nonQuery.Length - 1, 1)));
                        query = string.Empty;
                        nonQuery = string.Empty;
                    }
                }
            }

            return commands;
        }

        private KeyValuePair<bool, string> GetPreviousCommand(string query, string nonQuery)
        {
            if (!string.IsNullOrWhiteSpace(query))
            {
                return new KeyValuePair<bool, string>(true, query.Remove(query.Length - 1, 1));
            }
            else if (!string.IsNullOrWhiteSpace(nonQuery))
            {
                return new KeyValuePair<bool, string>(false, nonQuery.Remove(nonQuery.Length - 1, 1));
            }
            else
            {
                return new KeyValuePair<bool, string>(false, string.Empty);
            }
        }

        private void ExecuteCommands(IList<KeyValuePair<bool, string>> commands)
        {
            try
            {
                foreach (var command in commands)
                {
                    if (command.Key == true)
                    {
                        DataSet ds = RepoFactory.GetRepo().ExecuteQuery(lbDatabases.SelectedItem as Database, command.Value);
                        for (int i = 0; i < ds.Tables.Count; i++)
                        {
                            pnlResults.Controls.Add(new DataGridView()
                            {
                                DataSource = ds.Tables[i],
                                Dock = DockStyle.Top,
                                BackgroundColor = Color.White
                            });
                        }

                        tabControl.SelectedTab = tbpResults;
                    }
                    else
                    {
                        if (command.Value.ToLower().StartsWith(USE + " "))
                        {
                            foreach (Database database in lbDatabases.Items)
                            {
                                if (database.Name.ToLower() == command.Value.Substring(4).ToLower())
                                {
                                    lbDatabases.SelectedItem = database;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            int result = RepoFactory.GetRepo().ExecuteNonQuery(lbDatabases.SelectedItem as Database, command.Value);

                            if (command.Value.ToLower().StartsWith($"{CREATE} {DATABASE} ") ||
                                command.Value.ToLower().StartsWith($"{ALTER} {DATABASE} ") ||
                                command.Value.ToLower().StartsWith($"{DROP} {DATABASE} "))
                            {
                                LoadDatabases();
                            }
                            else if (command.Value.ToLower().StartsWith($"{CREATE} ") ||
                                command.Value.ToLower().StartsWith($"{ALTER} ") ||
                                command.Value.ToLower().StartsWith($"{DROP} "))
                            {
                                RefreshTablesViewsProcedures();
                            }


                            lblMessage.ForeColor = Color.Black;

                            if (result != -1)
                            {
                                lblMessage.Text += $"({result}) rows affected.\n";
                            }
                            else
                            {
                                lblMessage.Text += $"Command completed successfully.\n";
                            }

                            tabControl.SelectedTab = tbpMessages;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.ForeColor = Color.Red;
                lblMessage.Text = ex.Message + "\n";
                tabControl.SelectedTab = tbpMessages;
            }
        }
    }
}
