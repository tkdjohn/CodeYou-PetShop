using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities {
    public static class SimpleLogger {
        public static void LogException(Exception ex) {
            // for now we will just write it to error console
            Console.Error.WriteLine(ex.ToString());
        }
    }
}
