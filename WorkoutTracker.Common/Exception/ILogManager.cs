using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutTracker.Common.Exception
{
    public interface ILogManager
    {
        void WriteLog(System.Exception ex);
    }
}
