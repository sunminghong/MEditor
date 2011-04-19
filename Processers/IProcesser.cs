using System;
using System.Collections.Generic;
using System.Text;

namespace MEditor.Processers
{
    public interface IProcesser
    {
        /// <summary>
        /// 可以处理的标签类型
        /// </summary>
        EMark[] ProcessMarks { get; set; }

        /// <summary>
        /// 处理并返回处理后的文本
        /// </summary>
        /// <param name="sourceText">待处理的文本</param>
        /// <param name="mark">标签类型</param>
        /// <returns>处理后的返回</returns>
        void Process(System.Windows.Forms.RichTextBox rtb, EMark mark);
    }
}
