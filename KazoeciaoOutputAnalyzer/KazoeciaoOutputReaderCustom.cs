using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Text.RegularExpressions;

namespace KazoeciaoOutputAnalyzer
{
    public class KazoeciaoOutputReaderCustom : IKazoeciaoOutputReader
    {
        public SourcesDifference Read(string csv_path)
        {
            List<FunctionDifference> functions = new List<FunctionDifference>();

            using(var parser = new TextFieldParser(csv_path, Encoding.GetEncoding("shift_jis"))) {
                parser.Delimiters = new string[] { "," };
                for(int i=0; !parser.EndOfData; i++) {
                    var fields = parser.ReadFields();
                    if(i >= 3 && fields != null)
                    {
                        try
                        {
                            functions.Add(new FunctionDifference(
                                RemoveClassNameFromMethod(fields[1]),
                                System.IO.Path.GetDirectoryName(fields[0]),
                                int.Parse(fields[5]),
                                int.Parse(fields[3]),
                                int.Parse(fields[6]),
                                int.Parse(fields[2]),
                                int.Parse(fields[9])));
                        }
                        catch { }
                    }
                }
            }
            return new SourcesDifference(functions);
        }

        private string RemoveClassNameFromMethod(string method)
        {
            return Regex.Replace(method, @"\w+(::)", string.Empty, RegexOptions.IgnoreCase);
        }
    }
}
