using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mancala
{
    public class TreeNode
    {
        public int _value = -50;
        public int[] _board { get; set; }
        public int _round = 2; //AI player
        public int _pos = -1;

        public List<TreeNode> _children { get; set; }

        public TreeNode(int[] board)
        {
            this._children = new List<TreeNode>();
            _board = board;
        }

        public TreeNode(int[] board, int pos, int round)
        {
            this._children = new List<TreeNode>();
            _board = board;
            _value = board[13] - board[6];
            _pos = pos;
            _round = round;
        }

        public void AddChild(int[] board, int pos, int round)
        {
            var treeNode = new TreeNode(board, pos, round);
            _children.Add(treeNode);
        }
    }
}
