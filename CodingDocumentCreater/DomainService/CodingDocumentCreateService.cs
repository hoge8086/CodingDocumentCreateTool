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
        IKazoeciaoOutputReader kazoeciaoReader = null;

        public CodingDocumentCreateService(IOutputFactory outputFactory, IKazoeciaoOutputReader kazoeciaoReader)
        {
            this.outputFactory = outputFactory;
            this.kazoeciaoReader = kazoeciaoReader;
        }
        
        /// <summary>
        /// モジュール差分
        /// </summary>
        public class ModuleDifferrence
        {
            public string Name { get; private set; }
            public SourcesDifference Difference { get; private set; }

            public ModuleDifferrence(string name, SourcesDifference difference)
            {
                this.Name = name;
                this.Difference = difference;
            }
        }

        /// <summary>
        /// モジュール差分リスト
        /// </summary>
        public class ModuleDifferrenceList
        {
            public string Name { get; private set; }
            public List<ModuleDifferrence> ModulesDiff{ get; private set; }
            public ModuleDifferrenceList(string name, List<ModuleDifferrence> modulesDiff)
            {
                this.Name = name;
                this.ModulesDiff = new List<ModuleDifferrence>(modulesDiff);
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
            var soucesDiff = kazoeciaoReader.Read(kazoeciaoOutputPath)
                                .SelectModefied().
                                ChangeDiversionCoefficient(diversionCoefficient);

            var modules = new List<ModuleDifferrenceList>();
            var total = new List<ModuleDifferrence>();
            
            foreach (var dir in directoryPaths)
            {
                total.Add(new ModuleDifferrence(dir, soucesDiff.SelectByDirectoryPath(dir)));

                var module = new List<ModuleDifferrence>();
                foreach (var file in soucesDiff.FileNames())
                {
                    module.Add(new ModuleDifferrence(file, soucesDiff.SelectByFileName(file)));
                }
                module.Add(new ModuleDifferrence("合計", soucesDiff));
                modules.Add(new ModuleDifferrenceList(dir, module));
            }
            total.Add(new ModuleDifferrence("合計", soucesDiff.SelectByDirectoryPath(directoryPaths.ToArray())));
            modules.Add(new ModuleDifferrenceList("全体", total));

            outputFactory.CreateCodingDocumentOutput().WriteModuleDiffList(modules);
        }

    }
}
