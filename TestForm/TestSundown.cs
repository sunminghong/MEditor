using System;
using System.Windows.Forms;
using MEditor.Sundownlib;

namespace MEditor.TestForm
{
    public partial class TestSundown : Form
    {
        private readonly Sundown sundown;

        public TestSundown()
        {
            InitializeComponent();
            sundown = new Sundown();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            webBrowser1.DocumentText = sundown.Render("**llkllll**");
        }
    }
}