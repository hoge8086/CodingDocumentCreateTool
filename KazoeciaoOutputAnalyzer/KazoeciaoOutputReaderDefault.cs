using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Text.RegularExpressions;

namespace KazoeciaoOutputAnalyzer
{
    public class KazoeciaoOutputReaderDefault : IKazoeciaoOutputReader
    {
        public SourcesDifference Read(string csv_path)
        {
            List<FunctionDifference> functions = new List<FunctionDifference>();

            using(var parser = new TextFieldParser(csv_path, Encoding.GetEncoding("shift_jis"))) {
                parser.Delimiters = new string[] { "," };
                while(!parser.EndOfData) {
                    var fields = parser.ReadFields();
                    if (fields != null && fields.Count() == 9 && !string.IsNullOrEmpty(fields[2]))
                    {
                        try
                        {
                            functions.Add(new FunctionDifference(
                                fields[2],
                                RemoveNewOrOldPathPrefix(!string.IsNullOrEmpty(fields[0]) ?  fields[0] : fields[1]),
                                int.Parse(fields[6]),
                                int.Parse(fields[4]),
                                int.Parse(fields[8]),
                                int.Parse(fields[5]),
                                int.Parse(fields[7])));
                        }
                        catch { }
                    }
                }
            }
            return new SourcesDifference(functions);
        }

        private string RemoveNewOrOldPathPrefix(string path)
        {
            //return Regex.Replace(path, @".*(new|old)\\?", string.Empty, RegexOptions.IgnoreCase);
            return Regex.Replace(path, @".*(new|old)(?=\\)", string.Empty, RegexOptions.IgnoreCase);
        }
    }
}
