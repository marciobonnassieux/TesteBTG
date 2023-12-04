using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqsMessageConsummer
{
    public interface ILoggerService
    {
        void Log(string message);
    }
}
