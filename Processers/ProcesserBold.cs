using System.Text.RegularExpressions;
using ICSharpCode.AvalonEdit;

namespace MEditor.Processers
{
    public class ProcesserBold : IProcesser
    {
        #region IProcesser 成员

        private EMark[] _marks = new[] {EMark.boldanditail, EMark.bold, EMark.itail};

        public EMark[] ProcessMarks
        {
            get { return _marks; }
            set { _marks = value; }
        }

        public void Process(TextEditor rtb, EMark mark)
        {
            //粗体和斜体：用星号”*”或者下划线”_”
            //一个表示斜体；
            //两个表示粗体；
            //三个表示粗斜体。
            switch (mark)
            {
                case EMark.bold:
                    Bold(rtb);
                    return;
                case EMark.itail:
                    iterator(rtb);
                    return;
                case EMark.boldanditail:
                    Bold(rtb);
                    iterator(rtb);
                    return;
            }
        }

        private void iterator(TextEditor rtb)
        {
        }

        private void Bold(TextEditor rtb)
        {
            //int startPos = rtb.SelectionStart + rtb.SelectedText.Length;

            string source = rtb.SelectedText;

            var regex = new Regex(@"^\*\*(.*)\*\*$");
            Match mat = regex.Match(source);
            if (mat.Success)
            {
                //manager.SelectedText = mat.Groups[1].Value;//what here doing ??????
                return;
            }
            //manager.SelectedText = "**" + manager.SelectedText + "**";
            rtb.SelectedText = "**" + source + "**";
        }

        #endregion
    }
}