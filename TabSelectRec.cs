using System.Collections.Generic;

namespace MEditor
{
    public class TabSelectRec
    {
        /// <summary>
        ///     浏览文档的顺序记录队列，用于关闭tab时激活页面的顺序
        /// </summary>
        private static readonly Stack<int> _selectSeque = new Stack<int>();

        public static void Rec(int ind)
        {
            _selectSeque.Push(ind);
        }

        /// <summary>
        ///     返回-1 说明没有记录
        /// </summary>
        /// <returns></returns>
        public static int GetLast()
        {
            if (_selectSeque.Count > 0)
                return _selectSeque.Pop();

            return -1;
        }
    }
}