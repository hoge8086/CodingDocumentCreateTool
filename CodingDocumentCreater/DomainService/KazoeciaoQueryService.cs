using System.Collections.Generic;
using KazoeciaoOutputAnalyzer;

namespace CodingDocumentCreater.DomainService
{
    public class KazoeciaoQueryService
    {
        /// <summary>
        /// 関数一覧を取得する
        /// </summary>
        /// <param name="kazoeciaoOutputPath"></param>
        /// <returns></returns>
        public IEnumerable<FunctionDifference> QueryFunctionDifferencess(string kazoeciaoOutputPath)
        {
            var reader = new KazoeciaoOutputReaderDefault();
            return reader.Read(kazoeciaoOutputPath).Functions();
        }

        /// <summary>
        /// 修正関数一覧を取得する
        /// </summary>
        /// <param name="kazoeciaoOutputPath"></param>
        /// <returns></returns>
        public IEnumerable<FunctionDifference> QueryModifiedFunctions(string kazoeciaoOutputPath)
        {
            var reader = new KazoeciaoOutputReaderDefault();
            return reader.Read(kazoeciaoOutputPath).SelectModefied().Functions();
        }

        /// <summary>
        /// モジュール差分
        /// </summary>
        public class ModuleDifferrenceDTO
        {
            public string Name { get; private set; }
            public SourcesDifference Defference { get; private set; }
            public ModuleDifferrenceDTO(string name, SourcesDifference defference)
            {
                this.Name = name;
                this.Defference = defference;
            }
        }
        /// <summary>
        /// 指定モジュール群のモジュール単位の差分一覧を取得する
        /// </summary>
        /// <param name="kazoeciaoOutputPath"></param>
        /// <param name="directoryPaths"></param>
        /// <param name="diversionCoefficient"></param>
        /// <returns></returns>
        public IEnumerable<ModuleDifferrenceDTO> QueryModuleDifferences(string kazoeciaoOutputPath, List<string> directoryPaths, double diversionCoefficient)
        {
            var reader = new KazoeciaoOutputReaderDefault();
            var soucesDiff = reader.Read(kazoeciaoOutputPath)
                                .SelectModefied().
                                ChangeDiversionCoefficient(diversionCoefficient);

            var modules = new List<ModuleDifferrenceDTO>();
            foreach(var dir in directoryPaths)
            {
                modules.Add(new ModuleDifferrenceDTO(dir, soucesDiff.SelectByDirectoryPath(dir)));
            }
            modules.Add(new ModuleDifferrenceDTO("合計", soucesDiff.SelectByDirectoryPath(directoryPaths.ToArray())));
            return modules;
        }

        /// <summary>
        /// 指定モジュールのファイル単位の差分一覧を取得する
        /// </summary>
        /// <param name="kazoeciaoOutputPath"></param>
        /// <param name="directoryPath"></param>
        /// <param name="diversionCoefficient"></param>
        /// <returns></returns>
        public IEnumerable<ModuleDifferrenceDTO> QueryModuleFileDifferences(string kazoeciaoOutputPath, string directoryPath, double diversionCoefficient)
        {
            var reader = new KazoeciaoOutputReaderDefault();
            var soucesDiff = reader.Read(kazoeciaoOutputPath)
                                    .SelectModefied()
                                    .SelectByDirectoryPath(directoryPath)
                                    .ChangeDiversionCoefficient(diversionCoefficient);

            var modules = new List<ModuleDifferrenceDTO>();
            foreach(var file in soucesDiff.FileNames())
            {
                modules.Add(new ModuleDifferrenceDTO(file, soucesDiff.SelectByFileName(file)));
            }
            modules.Add(new ModuleDifferrenceDTO("合計", soucesDiff));
            return modules;
        }

        /// <summary>
        /// デフォルトの修正モジュール一覧を取得する
        /// </summary>
        /// <param name="kazoeciaoOutputPath"></param>
        /// <returns></returns>
        public List<string> QueryModuleList(string kazoeciaoOutputPath)
        {
            var reader = new KazoeciaoOutputReaderDefault();
            var sourcesDiff = reader.Read(kazoeciaoOutputPath).SelectModefied();
            return sourcesDiff.DirectoryPaths();
        }
    }
}
