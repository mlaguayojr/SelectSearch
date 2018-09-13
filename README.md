# SelectSearch
A WinForms version of the Select2 jQuery plugin

## Example of how to use

```CSharp
  // When the user enters the textbox
  private void textBox1_Enter(object sender, EventArgs e)
  {
    // if the object was not set
    if (search == null)
    {
      // create a new SelectSearch
      search = new SelectSearch(textBox1, new Object[] { "a", "b", "aab", "bbaa" });
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
```
