using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

/**
 * A very basic WinForm version of Select2 jQuery plugin)
 * Use to Form1 as demo
 */

namespace SelectSearch
{
    class SelectSearch
    {

        private Panel panel = new Panel();
        private MaskedTextBox filter = new MaskedTextBox();
        private ListBox list = new ListBox();
        private Object[] items = null;
        private TextBoxBase source;
        private Boolean showing;

        // Accepts Control that extends TextBoxBase
        public SelectSearch(TextBoxBase s, Object[] data)
        {
            source = s;
            items = data;

            // Change properties of source
            source.ReadOnly = true;
            source.BackColor = Color.White;

            // Grab the Coordinates of the source element
            int x = source.Location.X;
            int y = source.Location.Y + source.Height + 1;

            // Filter Properties
            filter.Mask = "D00000";
            filter.MouseClick += new MouseEventHandler(filter_MouseClick); // move cursor to start of text
            filter.TextChanged += new EventHandler(filter_TextChanged);
            filter.SetBounds(2, 2, source.Width - 4, source.Height); // subtract 2 * the x position of this, which is 2, from width
            panel.Controls.Add(filter);

            // ListBox Properties
            list.Items.AddRange(items.ToArray());
            list.SelectedIndexChanged += new EventHandler(list_SelectedIndexChanged);
            list.Click += new EventHandler(list_Click);
            list.SetBounds(2, 4 + source.Height, source.Width - 4, filter.Width - filter.Height);

            // Panel Properties
            panel.SetBounds(x, y, source.Width, source.Width);
            panel.BackColor = source.Parent.BackColor;
            panel.Controls.Add(list);
            panel.Click += new EventHandler(panel_Click);

            // Finally
            source.Parent.Controls.Add(panel);
            filter.SelectionStart = 1;
            filter.Focus();
        }

        // Hide panel if the user clicks in the spacing between elements of panel
        void panel_Click(object sender, EventArgs e)
        {
            if (isShowing())
            {
                hide();
            }
        }

        // When the user clicks the list
        void list_Click(object sender, EventArgs e)
        {
            // Go back to filter if the list is empty
            if (list.Items.Count == 0)
            {
                filter.Focus();
            }
        }

        // When the user selects an item from the list
        void list_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (list.SelectedIndex != -1)
            {
                source.Text = list.SelectedItem.ToString();
                hide();
            }
        }

        // Hide the panel
        public void hide()
        {
            panel.Hide();
            showing = false;
        }

        // When the filter's text changes
        void filter_TextChanged(object sender, EventArgs e)
        {
            list.Items.Clear();

            list.Items.AddRange(
                items.Where(i => i.ToString().Contains(filter.Text.Replace('D', ' ').Trim())).ToArray()
            );
        }

        // Change cursor position when there is no text entered
        void filter_MouseClick(object sender, EventArgs e)
        {
            if (filter.Text == "D")
            {
                filter.SelectionStart = 0;
            }
        }

        // Show the panel
        internal void show()
        {
            panel.Show();
            showing = true;
            filter.Focus();
        }

        // Is the panel visible?
        public bool isShowing()
        {
            return showing;
        }
    }
}
