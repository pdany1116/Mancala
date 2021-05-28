using System;
using System.Windows.Forms;

namespace Mancala.Game.Board
{
    public class Store
    {
        Label _label;
        public int _count;
        public Store(Label label)
        {
            _label = label;
            _count = 0;
            _label.Text = _count.ToString();
            _label.Click += new System.EventHandler(labelClick);
        }

        void labelClick(object sender, EventArgs e)
        {
            _count = 0;
            _label.Text = _count.ToString();
        }

        void SetCount(int count)
        {
            _count = count;
            _label.Text = _count.ToString();
        }

        void AddCount()
        {
            _count++;
            _label.Text = _count.ToString();
        }
    }
}
