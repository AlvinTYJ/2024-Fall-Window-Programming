using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace e94115011_practice_7_2
{
    public partial class 尋找與取代 : Form
    {
        private text _textForm;
        public 尋找與取代(text textForm)
        {
            InitializeComponent();
            _textForm = textForm;
            this.FormClosed += 尋找與取代_FormClosed;
        }

        private void btnFindNext_Click(object sender, EventArgs e)
        {
            _textForm.FindNext(textBox1.Text);
        }

        private void btnReplace_Click(object sender, EventArgs e)
        {
            _textForm.Replace(textBox1.Text, textBox2.Text);
        }

        private void btnReplaceAll_Click(object sender, EventArgs e)
        {
            _textForm.ReplaceAll(textBox1.Text, textBox2.Text);
        }

        private void 尋找與取代_FormClosed(object sender, FormClosedEventArgs e)
        {
            _textForm.richTextBox1.SelectionBackColor = _textForm.richTextBox1.BackColor;
            _textForm.richTextBox1.SelectionColor = _textForm.richTextBox1.ForeColor;
        }
    }
}
