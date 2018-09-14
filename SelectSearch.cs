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

        private Panel panel = new Panel();                      // The container for our filter and list
        private MaskedTextBox filter = new MaskedTextBox();     // The search filter
        private ListBox list = new ListBox();                   // The visible container of our items
        private Object[] items = null;                          // This holds all of the items to show in the ListBox
        private TextBoxBase source;                             // So long as the Control uses this class, it will work
        private List<Control> controls = new List<Control>();   // List of Controls to hide/show when Select is visible/not visible

        // Constructor
        public SelectSearch(TextBoxBase s, Object[] data)
        {
            source = s;     // The TextBoxBase Control the user has selected
            items = data;   // The items to use in the listbox
            
            // Change properties of source
            source.ReadOnly = true;
            source.BackColor = Color.White;

            // Grab the Coordinates of the source element
            int x = source.Location.X;
            int y = source.Location.Y + source.Height + 1;

            // Filter Properties
            filter.Mask = "D00000";
            filter.MouseClick += new MouseEventHandler(filter_MouseClick);      // move cursor to start of text
            filter.TextChanged += new EventHandler(filter_TextChanged);         // Do something when the filter's text has changed
            filter.SetBounds(2, 2, source.Width - 4, source.Height);            // subtract 2 * the x position of this, which is 2, from width
            filter.SelectionStart = 1;
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
            panel.Visible = false;

            // Finally
            source.Parent.Controls.Add(panel);
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
            toggleControls();
        }

        // Filter our list of items to the text of the filter
        void filter_TextChanged(object sender, EventArgs e)
        {
            // Remove any existing items from the ListBox
            list.Items.Clear();

            // Add the following items
            list.Items.AddRange(

                // For every object in the items[], add it to items[] if it contains the text of the filter
                items.Where(i => i.ToString().Contains(filter.Text.Replace('D', ' ').Trim())).ToArray()
            );
        }

        // Change cursor position when there is no text entered
        void filter_MouseClick(object sender, EventArgs e)
        {
            if (filter.Text == "D")
            {
                filter.SelectionStart = 1;
            }

        }

        // Show the panel
        internal void show()
        {
            panel.Show();
            panel.BringToFront();
            filter.Focus();
            toggleControls();
        }

        // Get the visibility of the Select panel
        public bool isShowing()
        {
            return panel.Visible;
        }

        // Change the visibility of elements that get in the way
        internal void toggleControls()
        {
            if (controls != null)
            {
                foreach (Control c in controls)
                {
                    c.Visible = !isShowing();
                }
            }
        }

        // Add a Control to be hidden later
        public void hideControl(Control c)
        {
            if (!controls.Contains(c))
            {
                controls.Add(c);
            }
            c.Hide();
        }

        // Hide multiple Controls
        public void hideControls(Control[] controls)
        {
            foreach (Control c in controls)
            {
                hideControl(c);
            }
        }
    }
}
