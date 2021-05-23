using System.Windows.Forms;

namespace Mancala
{
    public partial class MenuForm : Form
    {
        GameForm _gameForm;
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
            _gameForm = new GameForm(this);
            this.Hide();
            _gameForm.ShowDialog();

        }

        private void newGamePvPBtn_Click(object sender, System.EventArgs e)
        {
            _gameForm = new GameForm(this);
            this.Hide();
            _gameForm.ShowDialog();
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
