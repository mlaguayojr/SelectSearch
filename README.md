# SelectSearch
A WinForms version of the Select2 jQuery plugin

## Example of how to use

```CSharp
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace SelectSearch
{
    public partial class Form1 : Form
    {
        private SelectSearch search;
        private List<Object> items = new List<Object>();

        public Form1()
        {
            InitializeComponent();

            // Add items to the example
            for (int i = 10000; i <= 99999; i++)
            {
                items.Add(i);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label1.Focus();
        }

        // When the user enters the textbox
        private void textBox1_Enter(object sender, EventArgs e)
        {
            // if the object was not set
            if (search == null)
            {
                // create a new filterSearch
                search = new SelectSearch(textBox1, items.ToArray());

                // Hide these controls because they are near textBox1
                search.hideControls(new Control[] { textBox2, label2 });
            }

            search.show();
        }

        // If the user clicks anywhere else on the form's background
        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            if (search.isShowing())
            {
                search.hide();
            }
        }

    }

}

```
