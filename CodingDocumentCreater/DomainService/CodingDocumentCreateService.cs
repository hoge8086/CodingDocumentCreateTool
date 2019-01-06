using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KazoeciaoOutputAnalyzer;

namespace CodingDocumentCreater.DomainService
{
    /// <summary>
    /// 内部仕様書を生成するサービス
    /// </summary>
    public class CodingDocumentCreateService
    {
        IOutputFactory outputFactory = null;

        public CodingDocumentCreateService(IOutputFactory outputFactory)
        {
            this.outputFactory = outputFactory;
        }
        
        /// <summary>
        /// 修正関数一覧を出力する
        /// </summary>
        /// <param name="kazoeciaoOutputPath"></param>
        public void CreateModifiedFunctionList(string kazoeciaoOutputPath)
        {
            var reader = new KazoeciaoOutputReader();
            var soucesDiff = reader.Read(kazoeciaoOutputPath).SelectModefied();
            using (var output = outputFactory.CreateFunctionListOutput())
            {
                output.Open();
                foreach (var func in soucesDiff.Functions())
                {
                    output.Write(func);
                }
                output.Close();
            }
        }

        /// <summary>
        /// 内部仕様書を出力する
        /// </summary>
        /// <param name="kazoeciaoOutputPath"></param>
        /// <param name="directoryPaths"></param>
        /// <param name="diversionCoefficient"></param>
        public void CreateCodingDocument(string kazoeciaoOutputPath, List<string> directoryPaths, double diversionCoefficient)
        {
            var reader = new KazoeciaoOutputReader();
            var sourcesDiff = reader.Read(kazoeciaoOutputPath).SelectModefied().ChangeDiversionCoefficient(diversionCoefficient);
            using (var output = outputFactory.CreateCodingDocumentOutput())
            {
                output.Open();

                output.WriteTotalDiff(sourcesDiff);
                foreach(var dir in directoryPaths)
                {
                    var moduleDiff = sourcesDiff.SelectByDirectoryPath(dir);
                    output.WriteModuleDiff(dir, moduleDiff);

                    foreach (var file in moduleDiff.FileNames())
                    {
                        var fileDiff = moduleDiff.SelectByFileName(file);
                        output.WriteFileDiff(file, fileDiff);
                    }
                }
                output.Close();
            }
        }
    }
}
