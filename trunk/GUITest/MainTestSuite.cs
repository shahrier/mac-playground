﻿using System;
using System.Windows.Forms;
using System.Threading;
using NUnit.Framework;

namespace GUITest
{
    [TestFixture]
    public class MainTestSuite
	{
		[Test]
        public void BrowserTest()
		{
			var button = (Button)UI.GetMember("MainForm.button2", typeof(Button));
            UI.Perform(() => button.PerformClick());

			var webForm = UI.WaitForForm("WebForm");
			ThrowIfNull(webForm, "WebForm not found");

            Thread.Sleep(1000);
            UI.Perform(() => webForm.Close());
		}

        [Test]
        public void DialogTest()
		{
			var button = (Button)UI.GetMember("MainForm.button3", typeof(Button));
            UI.Delay(1.0, () => { SendKeys.SendWait("{ESC}");  });
            UI.Perform(() => button.PerformClick());
		}

        [Test]
        public void OpenFileDialogTest()
        {
            var combo = (ComboBox)UI.GetMember("MainForm.dialogTypeCombo", typeof(ComboBox));
            UI.Perform(() => combo.Focus());
            SendKeys.SendWait("{F4}");
            SendKeys.SendWait("{DOWN}");
            SendKeys.SendWait("{ENTER}");
            Thread.Sleep(500);

            var button = (Button)UI.GetMember("MainForm.button3", typeof(Button));
            UI.Delay(1.5, () => { SendKeys.SendWait("{ESC}"); });
            UI.Perform(() => button.PerformClick());
            Thread.Sleep(2000);
        }
        
		void ThrowIfNull(object instance, string message = "Instance not found")
		{
			if (instance == null)
				throw new ApplicationException(message);
		}
	}
}