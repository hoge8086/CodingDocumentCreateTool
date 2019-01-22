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
        KazoeciaoQueryService query = null;

        public CodingDocumentCreateService(IOutputFactory outputFactory, IKazoeciaoOutputReader kazoeciaoReader)
        {
            this.outputFactory = outputFactory;
            this.kazoeciaoReader = kazoeciaoReader;
            this.query = new KazoeciaoQueryService(kazoeciaoReader);
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
            var report = this.query.QueryModuleDifferrenceList(kazoeciaoOutputPath, directoryPaths, diversionCoefficient);
            outputFactory.CreateCodingDocumentOutput().WriteModuleDiffList(report);
        }

    }
}
