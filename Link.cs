using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace HCI3
{
    public class Link
    {
        public String Name;
        public String Url;
        public DateTime dateTime;
        public Link()
        {
            dateTime = DateTime.Now;
        }

        public Link(BinaryReader rd)
        {
            this.Name = rd.ReadString();
            this.Url = rd.ReadString();
            this.dateTime = new DateTime(rd.ReadInt64());
        }

        public override string ToString()
        {
            return dateTime.ToShortTimeString() + this.Name + "  " + this.Url;
        }

        public void WriteToFile(BinaryWriter wr)
        {
            wr.Write(this.Name);
            wr.Write(this.Url);
            wr.Write(this.dateTime.Ticks);
        }
    }
}
