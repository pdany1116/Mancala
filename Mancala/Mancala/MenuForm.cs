using System.Windows.Forms;

namespace Mancala
{
    public partial class MenuForm : Form
    {
        public MenuForm()
        {
            InitializeComponent();
        }

        private void newGameBtn_Click(object sender, System.EventArgs e)
        {
            newGameBtn.Hide();
            newGameAIBtn.Show();
            newGamePvPBtn.Show();
        }

        private void newGameAIBtn_Click(object sender, System.EventArgs e)
        {
            AIGameForm aIGameForm = new AIGameForm(this);
            this.Hide();
            aIGameForm.ShowDialog();

        }

        private void newGamePvPBtn_Click(object sender, System.EventArgs e)
        {
            PvPGameForm pvPGameForm = new PvPGameForm(this);
            this.Hide();
            pvPGameForm.ShowDialog();
        }

        private void aboutBtn_Click(object sender, System.EventArgs e)
        {
            AboutForm aboutForm = new AboutForm();
            aboutForm.ShowDialog();
        }

        private void exitBtn_Click(object sender, System.EventArgs e)
        {
            Application.Exit();
        }
    }
}
