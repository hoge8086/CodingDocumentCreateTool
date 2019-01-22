using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KazoeciaoOutputAnalyzer;

namespace CodingDocumentCreater.DomainService
{
    /// <summary>
    /// 内部仕様書のマルチ出力
    /// </summary>
    public class CodingDocumentMultiOutput : ICodingDocumentOutput
    {
        ICodingDocumentOutput[] outputs = null;

        public CodingDocumentMultiOutput(params ICodingDocumentOutput[] outputs)
        {
            this.outputs = outputs;
        }

        public void WriteModuleDiffList(List<ModuleDifferrenceListDTO> moduleDiffList)
        {
            foreach(var output in outputs)
            {
                output.WriteModuleDiffList(moduleDiffList);
            }
        }

    }
}
