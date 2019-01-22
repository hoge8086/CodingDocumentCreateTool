using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CodingDocumentCreater.Infrastructure
{
    public class Setting
    {
        public static readonly string ExeDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        public static readonly string OutputDirectory =  Path.Combine(ExeDirectory, "out");
    }
}
