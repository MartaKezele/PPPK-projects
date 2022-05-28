
namespace PPPK_DZ1.Forms
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.miExecute = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tbpResults = new System.Windows.Forms.TabPage();
            this.pnlResults = new System.Windows.Forms.Panel();
            this.tbpMessages = new System.Windows.Forms.TabPage();
            this.lblMessage = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lbDatabases = new System.Windows.Forms.ListBox();
            this.lbTables = new System.Windows.Forms.ListBox();
            this.lbViews = new System.Windows.Forms.ListBox();
            this.lbProcedures = new System.Windows.Forms.ListBox();
            this.lbTableColumns = new System.Windows.Forms.ListBox();
            this.lbViewColumns = new System.Windows.Forms.ListBox();
            this.lbParameters = new System.Windows.Forms.ListBox();
            this.rtbQuery = new System.Windows.Forms.RichTextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuStrip1.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tbpResults.SuspendLayout();
            this.tbpMessages.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miExecute});
            this.menuStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.menuStrip1.Size = new System.Drawing.Size(1478, 36);
            this.menuStrip1.TabIndex = 0;
            // 
            // miExecute
            // 
            this.miExecute.Name = "miExecute";
            this.miExecute.Size = new System.Drawing.Size(87, 32);
            this.miExecute.Text = "Execute";
            this.miExecute.Click += new System.EventHandler(this.MiExecute_Click);
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tbpResults);
            this.tabControl.Controls.Add(this.tbpMessages);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tabControl.Location = new System.Drawing.Point(0, 416);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1478, 528);
            this.tabControl.TabIndex = 2;
            // 
            // tbpResults
            // 
            this.tbpResults.Controls.Add(this.pnlResults);
            this.tbpResults.Location = new System.Drawing.Point(4, 29);
            this.tbpResults.Name = "tbpResults";
            this.tbpResults.Padding = new System.Windows.Forms.Padding(3);
            this.tbpResults.Size = new System.Drawing.Size(1470, 495);
            this.tbpResults.TabIndex = 0;
            this.tbpResults.Text = "Results";
            this.tbpResults.UseVisualStyleBackColor = true;
            // 
            // pnlResults
            // 
            this.pnlResults.AutoScroll = true;
            this.pnlResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlResults.Location = new System.Drawing.Point(3, 3);
            this.pnlResults.Name = "pnlResults";
            this.pnlResults.Size = new System.Drawing.Size(1464, 489);
            this.pnlResults.TabIndex = 0;
            // 
            // tbpMessages
            // 
            this.tbpMessages.Controls.Add(this.lblMessage);
            this.tbpMessages.Location = new System.Drawing.Point(4, 29);
            this.tbpMessages.Name = "tbpMessages";
            this.tbpMessages.Padding = new System.Windows.Forms.Padding(3);
            this.tbpMessages.Size = new System.Drawing.Size(1470, 495);
            this.tbpMessages.TabIndex = 1;
            this.tbpMessages.Text = "Messages";
            this.tbpMessages.UseVisualStyleBackColor = true;
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessage.ForeColor = System.Drawing.Color.Red;
            this.lblMessage.Location = new System.Drawing.Point(3, 3);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(0, 20);
            this.lblMessage.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label3, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.label5, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.label6, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.label7, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.lbDatabases, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lbTables, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.lbViews, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.lbProcedures, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.lbTableColumns, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.lbViewColumns, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.lbParameters, 1, 7);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 36);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 8;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(988, 380);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Databases:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 95);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Tables:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(497, 95);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(115, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "Table columns:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 189);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 20);
            this.label4.TabIndex = 3;
            this.label4.Text = "Views:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(497, 189);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(110, 20);
            this.label5.TabIndex = 4;
            this.label5.Text = "View columns:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(4, 283);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(94, 20);
            this.label6.TabIndex = 5;
            this.label6.Text = "Procedures:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(497, 283);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(95, 20);
            this.label7.TabIndex = 6;
            this.label7.Text = "Parameters:";
            // 
            // lbDatabases
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.lbDatabases, 2);
            this.lbDatabases.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbDatabases.FormattingEnabled = true;
            this.lbDatabases.ItemHeight = 20;
            this.lbDatabases.Location = new System.Drawing.Point(4, 25);
            this.lbDatabases.Name = "lbDatabases";
            this.lbDatabases.Size = new System.Drawing.Size(980, 66);
            this.lbDatabases.TabIndex = 7;
            this.lbDatabases.SelectedIndexChanged += new System.EventHandler(this.LbDatabases_SelectedIndexChanged);
            // 
            // lbTables
            // 
            this.lbTables.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbTables.FormattingEnabled = true;
            this.lbTables.ItemHeight = 20;
            this.lbTables.Location = new System.Drawing.Point(4, 119);
            this.lbTables.Name = "lbTables";
            this.lbTables.Size = new System.Drawing.Size(486, 66);
            this.lbTables.TabIndex = 8;
            this.lbTables.SelectedIndexChanged += new System.EventHandler(this.LbTables_SelectedIndexChanged);
            // 
            // lbViews
            // 
            this.lbViews.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbViews.FormattingEnabled = true;
            this.lbViews.ItemHeight = 20;
            this.lbViews.Location = new System.Drawing.Point(4, 213);
            this.lbViews.Name = "lbViews";
            this.lbViews.Size = new System.Drawing.Size(486, 66);
            this.lbViews.TabIndex = 10;
            this.lbViews.SelectedIndexChanged += new System.EventHandler(this.LbViews_SelectedIndexChanged);
            // 
            // lbProcedures
            // 
            this.lbProcedures.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbProcedures.FormattingEnabled = true;
            this.lbProcedures.ItemHeight = 20;
            this.lbProcedures.Location = new System.Drawing.Point(4, 307);
            this.lbProcedures.Name = "lbProcedures";
            this.lbProcedures.Size = new System.Drawing.Size(486, 69);
            this.lbProcedures.TabIndex = 12;
            this.lbProcedures.SelectedIndexChanged += new System.EventHandler(this.LbProcedures_SelectedIndexChanged);
            // 
            // lbTableColumns
            // 
            this.lbTableColumns.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbTableColumns.FormattingEnabled = true;
            this.lbTableColumns.ItemHeight = 20;
            this.lbTableColumns.Location = new System.Drawing.Point(497, 119);
            this.lbTableColumns.Name = "lbTableColumns";
            this.lbTableColumns.Size = new System.Drawing.Size(487, 66);
            this.lbTableColumns.TabIndex = 13;
            // 
            // lbViewColumns
            // 
            this.lbViewColumns.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbViewColumns.FormattingEnabled = true;
            this.lbViewColumns.ItemHeight = 20;
            this.lbViewColumns.Location = new System.Drawing.Point(497, 213);
            this.lbViewColumns.Name = "lbViewColumns";
            this.lbViewColumns.Size = new System.Drawing.Size(487, 66);
            this.lbViewColumns.TabIndex = 14;
            // 
            // lbParameters
            // 
            this.lbParameters.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbParameters.FormattingEnabled = true;
            this.lbParameters.ItemHeight = 20;
            this.lbParameters.Location = new System.Drawing.Point(497, 307);
            this.lbParameters.Name = "lbParameters";
            this.lbParameters.Size = new System.Drawing.Size(487, 69);
            this.lbParameters.TabIndex = 15;
            // 
            // rtbQuery
            // 
            this.rtbQuery.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbQuery.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbQuery.Location = new System.Drawing.Point(988, 36);
            this.rtbQuery.Name = "rtbQuery";
            this.rtbQuery.Size = new System.Drawing.Size(490, 380);
            this.rtbQuery.TabIndex = 4;
            this.rtbQuery.Text = "";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1478, 944);
            this.Controls.Add(this.rtbQuery);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.tbpResults.ResumeLayout(false);
            this.tbpMessages.ResumeLayout(false);
            this.tbpMessages.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tbpResults;
        private System.Windows.Forms.TabPage tbpMessages;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ListBox lbDatabases;
        private System.Windows.Forms.ListBox lbTables;
        private System.Windows.Forms.ListBox lbViews;
        private System.Windows.Forms.ListBox lbProcedures;
        private System.Windows.Forms.RichTextBox rtbQuery;
        private System.Windows.Forms.ListBox lbTableColumns;
        private System.Windows.Forms.ListBox lbViewColumns;
        private System.Windows.Forms.ListBox lbParameters;
        private System.Windows.Forms.ToolStripMenuItem miExecute;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.Panel pnlResults;
    }
}