using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TheCodeKing.ActiveButtons.Controls;

namespace HCI3
{
    public partial class Form1:  Form
    {
        protected TabPage page = new TabPage();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Core.Instance.Form = this;
            addNewPage();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            IActiveMenu menu = ActiveMenu.GetInstance(this);
            ActiveButton button = new ActiveButton();
            button.BackColor = Color.Transparent;
            button.Text = "+";
            button.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            button.Click += new EventHandler(myBtnClick);
            menu.Items.Add(button);
        }
        private void myBtnClick(object sender, EventArgs e)
        {
            addNewPage();
        }

        internal void addNewPage()
        {
            MyPage tab = new MyPage();
            this.tabControl1.TabPages.Add(tab);
        }
    }
}
