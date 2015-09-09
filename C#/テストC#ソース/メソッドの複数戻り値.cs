
	private void button1_Click(object sender, EventArgs e)
    {
      var packed = GetHoge ();
      if (packed.IsOK)
      {
        textBox1.Text = packed.Name;
      }
    }

    private dynamic GetHoge ()
    {
	  // ñ≥ñºä÷êîÇ≈
      return new { Name = "ÇŸÇ∞", IsOK = true };
    }
    
