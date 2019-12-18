using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace CC_Functions.Misc
{
    partial class SelectBox<T> : Form
    {
        public T result;
        public SelectBox(T[] Options, string title = "")
        {
            InitializeComponent();
            Text = title;
            listBox1.Items.AddRange(Options.Select(s => (object)s).ToArray());
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            result = (T)listBox1.SelectedItem;
            DialogResult = DialogResult.OK;
            Close();
        }
    }

    public static class SelectBox
    {
        public static T Show<T>(T[] Options, string title = "")
        {
            SelectBox<T> sb = new SelectBox<T>(Options, title);
            return sb.ShowDialog() == DialogResult.OK ? sb.result : default(T);
        }
    }
}
