using System;
using System.Windows.Forms;

namespace Mancala.Game.Board
{
    public class Pit
    {
        public Label _label;
        public int _count;
        EventHandler _click;
        bool _clicked = false;
        public Pit(Label label)
        {            
            _label = label;
            _count = 4;
            _label.Text = _count.ToString();
            _click = new System.EventHandler(labelClick);
            _label.Click += _click;
        }

        void labelClick(object sender, EventArgs e)
        {
            _clicked = true;
            _label.Text = _count.ToString();
        }

        public void EnableClick()
        {
            _clicked = false;
            _label.Click += _click;
        }

        public void DisableClick()
        {
            _clicked = false;
            _label.Click -= _click;
        }

        public void SetCount(int count)
        {
            _count = count;
            _label.Text = _count.ToString();
        }

        void AddCount()
        {
            _count++;
            _label.Text = _count.ToString();
        }

        public bool IsClicked()
        {
            return _clicked;
        }
    }
}
