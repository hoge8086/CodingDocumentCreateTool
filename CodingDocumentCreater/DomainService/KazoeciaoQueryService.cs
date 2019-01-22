using System.Collections.Generic;
using KazoeciaoOutputAnalyzer;

namespace CodingDocumentCreater.DomainService
{
    public class KazoeciaoQueryService
    {
        IKazoeciaoOutputReader kazoeciaoReader = null;

        public KazoeciaoQueryService(IKazoeciaoOutputReader kazoeciaoReader)
        {
            this.kazoeciaoReader = kazoeciaoReader;
        }
        /// <summary>
        /// 関数一覧を取得する
        /// </summary>
        /// <param name="kazoeciaoOutputPath"></param>
        /// <returns></returns>
        public IEnumerable<IFunctionDifference> QueryFunctionDifferencess(string kazoeciaoOutputPath)
        {
            return kazoeciaoReader.Read(kazoeciaoOutputPath).Functions();
        }

        /// <summary>
        /// 修正関数一覧を取得する
        /// </summary>
        /// <param name="kazoeciaoOutputPath"></param>
        /// <returns></returns>
        public IEnumerable<IFunctionDifference> QueryModifiedFunctions(string kazoeciaoOutputPath)
        {
            return kazoeciaoReader.Read(kazoeciaoOutputPath).SelectModefied().Functions();
        }

        /// <summary>
        /// デフォルトの修正モジュール一覧を取得する
        /// </summary>
        /// <param name="kazoeciaoOutputPath"></param>
        /// <returns></returns>
        public List<string> QueryModuleList(string kazoeciaoOutputPath)
        {
            var sourcesDiff = kazoeciaoReader.Read(kazoeciaoOutputPath).SelectModefied();
            return sourcesDiff.DirectoryPaths();
        }

        /// <summary>
        /// モジュール差分のレポートを取得する
        /// </summary>
        /// <param name="kazoeciaoOutputPath"></param>
        /// <param name="directoryPaths"></param>
        /// <param name="diversionCoefficient"></param>
        public List<ModuleDifferrenceListDTO> QueryModuleDifferrenceList(string kazoeciaoOutputPath, List<string> directoryPaths, double diversionCoefficient)
        {
            var soucesDiff = kazoeciaoReader.Read(kazoeciaoOutputPath)
                                .SelectModefied()
                                .SelectByDirectoryPath(directoryPaths.ToArray())
                                .ChangeDiversionCoefficient(diversionCoefficient);

            var moduleList = new List<ModuleDifferrenceListDTO>();
            var total = new List<ModuleDifferrenceDTO>();
            
            foreach (var dir in directoryPaths)
            {
                var moduleDiff = soucesDiff.SelectByDirectoryPath(dir);
                total.Add(new ModuleDifferrenceDTO(dir, moduleDiff));

                var module = new List<ModuleDifferrenceDTO>();
                foreach (var file in moduleDiff.FileNames())
                {
                    module.Add(new ModuleDifferrenceDTO(file, moduleDiff.SelectByFileName(file)));
                }
                module.Add(new ModuleDifferrenceDTO("合計", moduleDiff));
                moduleList.Add(new ModuleDifferrenceListDTO(dir, module));
            }
            total.Add(new ModuleDifferrenceDTO("合計", soucesDiff));
            moduleList.Insert(0, new ModuleDifferrenceListDTO("全体", total));

            return moduleList;
        }
    }
}
