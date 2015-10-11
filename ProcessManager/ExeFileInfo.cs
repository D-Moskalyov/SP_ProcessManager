using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ProcessManager
{
    public class ExeFileInfo
    {
        string fullName;

        public string FullName
        {
            get { return fullName; }
            set { fullName = value; }
        }
        string shortName;

        public string ShortName
        {
            get { return shortName; }
            set { shortName = value; }
        }

        public ExeFileInfo(FileSystemInfo fSI)
        {
            FullName = fSI.FullName;
            ShortName = fSI.Name;
        }

        public override string ToString()
        {
            return ShortName.ToString();
        }

    }
}
