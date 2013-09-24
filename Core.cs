using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;

namespace HCI3
{
    class Core
    {
        protected static String hfile = "history.dat";
        protected static String bfile = "bookmarks.dat";
        protected List<Link> history = new List<Link>();
        protected List<Link> bookmarks = new List<Link>();
        protected static Core instance = new Core();
        public static Core Instance
        {
            get { return instance; }
        }

        protected Core()
        {
            ReadFiles();
        }

        ~Core()
        {
            WriteFiles();
        }

        protected void ReadFiles()
        {
            BinaryReader br = new BinaryReader(File.Open(Core.hfile, FileMode.OpenOrCreate));
            while (br.BaseStream.Position < br.BaseStream.Length)
            {
                this.history.Add(new Link(br));
            }
            br.Close();
            br = new BinaryReader(File.Open(Core.bfile, FileMode.OpenOrCreate));
            while (br.BaseStream.Position < br.BaseStream.Length)
            {
                this.bookmarks.Add(new Link(br));
            }
            br.Close();
        }

        protected void WriteFiles()
        {
            BinaryWriter bw = new BinaryWriter(File.Open(Core.hfile, FileMode.OpenOrCreate));
            foreach (Link l in this.history)
            {
                l.WriteToFile(bw);
            }
            bw.Close();
            bw = new BinaryWriter(File.Open(Core.bfile, FileMode.OpenOrCreate));
            foreach (Link l in this.bookmarks)
            {
                l.WriteToFile(bw);
            }
            bw.Close();
        }

        public List<Link> History
        {
            get { return this.history; }
        }
        public List<Link> Bookmarks
        {
            get { return this.bookmarks; }
        }
        public void addToHistory(Link l)
        {
            this.history.Add(l);
        }

        public Form1 Form { get; set; }

        public void AddBookmark(Link l)
        {
            bookmarks.Add(l);
        }

        internal void NewPage(Link l)
        {
            Form.addNewPage();
            int last = Form.tabControl1.TabPages.Count - 1;
            MyPage page = Form.tabControl1.TabPages[last] as MyPage;
            page.Goto(l);
        }
    }
}
