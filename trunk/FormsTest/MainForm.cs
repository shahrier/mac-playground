﻿using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
using MonoMac.AppKit;

namespace FormsTest
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
			InitializeComponent();

			ListFontFamilies(this.listBox1);
		}

		protected override CreateParams CreateParams {
			get {
				var cp = base.CreateParams;
				cp.ExStyle |= 0x02000000;  // Turn on WS_EX_COMPOSITED
				return cp;
			}
		}		

        void ListFontFamilies(ListBox listBox)
        {
            var fonts = new InstalledFontCollection();
            var families = fonts.Families;
            foreach (var family in families)
            {
                listBox.Items.Add(family.Name);
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            try {
                string name = listBox1.SelectedItem as string;
                label2.Font = new Font(name, label2.Font.Size);
                label3.Font = new Font(name, label3.Font.Size);
                label4.Font = new Font(name, label4.Font.Size);
                label5.Font = new Font(name, label5.Font.Size);
                label7.Font = new Font(name, label7.Font.Size);
            }
            catch
            {
            }
        }

//		protected override void OnPaint (PaintEventArgs e)
//		{
//			NSView nsView = (NSView)MonoMac.ObjCRuntime.Runtime.GetNSObject(this.Handle);
//			if (nsView != null && nsView.LockFocusIfCanDraw ()) {
//				base.OnPaint (e);
//				nsView.UnlockFocus ();
//			}
//		}

		protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
		{
			base.OnClosing(e);
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			Application.Exit();
		}

		private void button2_Click(object sender, System.EventArgs e)
		{
			var form = new Form();
			form.Show();
		}

		private void button3_Click(object sender, System.EventArgs e)
		{
			MessageBox.Show (loremIpsum);
		}

		static readonly string loremIpsum = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum";
    }
}
