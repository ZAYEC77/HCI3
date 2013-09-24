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
    public partial class Browser : UserControl
    {
        protected TabPage owner;
        protected bool mustSave;
        public Browser(TabPage owner)
        {
            InitializeComponent();
            this.owner = owner;
            this.owner.Text = "Empty page";
            this.webBrowser.ShowPropertiesDialog();
            this.webBrowser.Navigate("about:blank");
            this.webBrowser.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(browser_LoadCompleted);
            updateBookmarks();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            moveBack();
        }

        private void moveBack()
        {
            webBrowser.GoBack();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            loadPage(this.linkInput.Text);
        }

        internal void loadPage(string link)
        {
            if (IsValidUri(link))
            {
                webBrowser.Navigate(link);                
            }
            else
            {
                MessageBox.Show("Error");
            }
        }

        private void browser_LoadCompleted(object sender, EventArgs e)
        {
            if (webBrowser.Document != null)
                if (webBrowser.Url.ToString() != "about:blank")
                {
                    owner.Text = ((HtmlDocument)webBrowser.Document).Title;
                    addToHistory(owner.Text, webBrowser.Url.ToString());
                    linkInput.Text = webBrowser.Url.ToString();
                }
        }

        private void addToHistory(String title, String url)
        {
            Link l = new Link() {Name = title, Url = url};
            Core.Instance.addToHistory(l);
        }

        private void linkInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                loadPage(this.linkInput.Text);
            }
        }

        public bool IsValidUri(string url)
        {
            Uri validatedUri;
            return Uri.TryCreate(url, UriKind.RelativeOrAbsolute, out validatedUri);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            MoveNext();
        }

        private void MoveNext()
        {
            webBrowser.GoForward();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            webBrowser.ShowSaveAsDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            String name = owner.Text;
            String url = (webBrowser.Url != null)?webBrowser.Url.ToString():"";
            Link l = new Link() {Name =  name, Url = url};
            Core.Instance.AddBookmark(l);
            updateBookmarks();
        }

        private void updateBookmarks()
        {
            toolStrip1.Items.Clear();
            foreach (Link item in Core.Instance.Bookmarks)
            {
                ToolStripLabel label = new ToolStripLabel(item.Name);
                label.Click += new EventHandler(label_Click);
                toolStrip1.Items.Add(label);
            }
        }

        private void label_Click(object sender, EventArgs e)
        {
            int index = toolStrip1.Items.IndexOf(sender as ToolStripItem);
            Link l = Core.Instance.Bookmarks[index];
            Core.Instance.NewPage(l);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            showHistory();
        }

        private void showHistory()
        {
            List<Link> list = Core.Instance.History;
            if (webBrowser.Document == null) 
            {
                webBrowser.Navigate("about:blank");
            }
            HtmlDocument doc = webBrowser.Document as HtmlDocument;
            doc.Title = "History";
            doc.Body.InnerHtml = "";
            HtmlElement e = doc.CreateElement("h1");
            e.InnerText = "History:";
            doc.Body.AppendChild(e);
            foreach (var item in list)
            {
                e = doc.CreateElement("h2");
                HtmlElement ee = doc.CreateElement("a");
                ee.InnerText = item.dateTime.ToShortTimeString() + " " + item.Name;
                ee.SetAttribute("href", item.Url);
                e.AppendChild(ee);
                doc.Body.AppendChild(e);
            }

        }

    }
}
