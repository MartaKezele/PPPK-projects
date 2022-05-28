using PPPK_DZ1.Dal;
using System;
using System.Windows.Forms;

namespace PPPK_DZ1.Forms
{
    public partial class LogInForm : Form
    {
        public LogInForm()
        {
            InitializeComponent();
        }

        private void btnLogIn_Click(object sender, EventArgs e)
        {
            try
            {
                RepoFactory.GetRepo().LogIn(tbServer.Text.Trim(), tbUsername.Text.Trim(), tbPassword.Text.Trim());
                new MainForm().Show();
                Hide();
                // do not forget to catch Form_Closing of child to kill the application!
            }
            catch (Exception ex)
            {
                lblErrorMessage.Text = ex.Message;
                lblErrorMessage.Visible = true;
            }
        }
    }
}
