namespace CC_Functions.W32.Forms
{
    internal partial class SelectBox<T> : Form
    {
        public T result;

        public SelectBox(T[] Options, string title = "")
        {
            InitializeComponent();
            Text = title;
            listBox1.Items.AddRange(Options.Select(s => (object) s).ToArray());
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            result = (T) listBox1.SelectedItem;
            DialogResult = DialogResult.OK;
            Close();
        }
    }

    public static class SelectBox
    {
        public static T Show<T>(T[] Options, string title = "")
        {
            SelectBox<T> sb = new SelectBox<T>(Options, title);
            return sb.ShowDialog() == DialogResult.OK ? sb.result : default;
        }
    }
}