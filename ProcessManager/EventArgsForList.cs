using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessManager
{
    public class EventArgsForList : EventArgs
    {
        public ExeFileInfo exeFileInfo;

        public EventArgsForList(ExeFileInfo eFI)
        {
            exeFileInfo = eFI;
        }
    }
}
