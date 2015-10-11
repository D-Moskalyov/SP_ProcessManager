using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessManager
{
    class ProcInfo
    {
        public string ProcessName;
        public int ID;

        public ProcInfo(string name, int id)
        {
            this.ProcessName = name;
            this.ID = id;
        }


        public override string ToString()
        {
           // return string.Format("Name: {0}, ID: {1}", ProcessName, ID);
            return ProcessName;
        }
    }
}
