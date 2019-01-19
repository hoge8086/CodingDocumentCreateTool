using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Text.RegularExpressions;
using CsvSerializer;

namespace KazoeciaoOutputAnalyzer.KazoeciaoDefault
{
    public class KazoeciaoOutputReaderDefault : IKazoeciaoOutputReader
    {
        public SourcesDifference Read(string csv_path)
        {
            var serializer = new CsvSerializer<FunctionDifferenceDefault>();
            var functions = serializer.Read(csv_path, Encoding.GetEncoding("shift_jis"));
            return new SourcesDifference(functions.ConvertAll<IFunctionDifference>(f => f as IFunctionDifference));
        }
    }
}
