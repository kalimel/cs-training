using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAddressbookTests
{
    public class GroupData
    {
        private string name;
        private string header;
        private string footer;

        public GroupData(string name, string header, string footer)
        {
            this.name = name;
            this.header = header;
            this.footer = footer;
        }

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                this.name = name;
            }
        }

        public string Header
        {
            get
            {
                return header;
            }
            set
            {
                this.header = header;
            }
        }

        public string Footer
        {
            get
            {
                return footer;
            }
            set
            {
                this.footer = footer;
            }
        }
    }
}
