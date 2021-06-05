using System.Collections.Generic;

namespace Mancala
{
    public class TreeNode
    {
        public int _value = -50;
        public int[] _board { get; set; }
        public int _type = 2; //1 - player(minimizer) 2 - AI (maximizer), default 2 for root
        public int _pos = -1;

        public List<TreeNode> _children { get; set; }

        public TreeNode(int[] board)
        {
            this._children = new List<TreeNode>();
            _board = board;
        }

        public TreeNode(int[] board, int pos, int type)
        {
            this._children = new List<TreeNode>();
            _board = board;
            _value = board[13] - board[6];
            _pos = pos;
            _type = type;
        }

        public void AddChild(int[] board, int pos, int type)
        {
            var treeNode = new TreeNode(board, pos, type);
            _children.Add(treeNode);
        }
    }
}
