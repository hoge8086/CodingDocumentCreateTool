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
        public void Close()
        {
            for(int i=0; i<outputs.Count(); i++)
            {
                if(outputs[i] != null)
                {
                    try
                    {
                        outputs[i].Close();
                    }finally
                    {
                        outputs[i] = null;
                    }
                }
            }
        }

        public void Dispose()
        {
            Close();
        }

        public void Open()
        {
            for(int i=0; i<outputs.Count(); i++)
            {
                if(outputs[i] != null)
                {
                    outputs[i].Open();
                }
            }
        }

        public void WriteFileDiff(string fileName, StepDifference fileDiff)
        {
            for(int i=0; i<outputs.Count(); i++)
            {
                if(outputs[i] != null)
                {
                    outputs[i].WriteFileDiff(fileName, fileDiff);
                }
            }
        }

        public void WriteModuleDiff(string moduleName, StepDifference moduleDiff)
        {
            for(int i=0; i<outputs.Count(); i++)
            {
                if(outputs[i] != null)
                {
                    outputs[i].WriteModuleDiff(moduleName, moduleDiff);
                }
            }
        }

        public void WriteTotalDiff(StepDifference funcDiff)
        {
            for(int i=0; i<outputs.Count(); i++)
            {
                if(outputs[i] != null)
                {
                    outputs[i].WriteTotalDiff(funcDiff);
                }
            }
        }
    }
}
