using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HCI3
{
    public partial class MyPage : TabPage
    {
        protected Browser browser;
        public MyPage()
        {
            InitializeComponent();
            CreateUI();
        }
        protected void CreateUI()
        {
            this.browser = new Browser(this);
            this.Controls.Add(browser);
            this.browser.Dock = DockStyle.Fill;
        }

        internal void Goto(Link l)
        {
            browser.loadPage(l.Url);
        }
    }
}
