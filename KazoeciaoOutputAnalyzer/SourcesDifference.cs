using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KazoeciaoOutputAnalyzer
{
    public class SourcesDifference : StepDifference
    {
        private List<FunctionDifference> functions;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="functions"></param>
        public SourcesDifference(List<FunctionDifference> functions, double diversionCoefficient = 0.0)
        {
            this.functions = functions;
            this.DiversionCoefficient = diversionCoefficient;
        }

        /// <summary>
        /// 関数一覧を取得
        /// </summary>
        /// <returns></returns>
        public IEnumerable<FunctionDifference> Functions()
        {
            return new List<FunctionDifference>(functions);
        }

        /// <summary>
        /// 修正箇所の差分のみを取得
        /// </summary>
        /// <returns></returns>
        public SourcesDifference SelectModefied()
        {
            var funcs = functions.Where(x => x.IsModified());
            return new SourcesDifference(funcs.ToList(), DiversionCoefficient);
        }

        /// <summary>
        /// ディレクトリパス一覧
        /// </summary>
        /// <returns></returns>
        public List<string> DirectoryPaths()
        {
            return functions
                    .Select(x => x.DirectoryPath)
                    .Distinct()
                    .ToList();
        }

        /// <summary>
        /// 指定したディレクトリのみの差分を取得
        /// </summary>
        /// <param name="dirPath"></param>
        /// <returns></returns>
        public SourcesDifference SelectByDirectoryPath(params string[] dirPaths)
        {
            var funcs = functions.Where((func) =>
                {
                    var mn= func.DirectoryPath + @"\";
                    return dirPaths.Any((dirPath) =>
                    {
                        if (!dirPath.EndsWith(@"\"))
                            dirPath += @"\";
                        return mn.StartsWith(dirPath, StringComparison.OrdinalIgnoreCase);
                    });
                });
            return new SourcesDifference(funcs.ToList(), DiversionCoefficient);
        }

        /// <summary>
        /// ファイル名一覧
        /// </summary>
        /// <returns></returns>
        public List<string> FileNames()
        {
            return functions
                    .Select(x => x.FileName)
                    .Distinct()
                    .ToList();
        }
        
        /// <summary>
        /// 指定したファイルのみの差分を取得
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public SourcesDifference SelectByFileName(string fileName)
        {
            var funcs = functions.Where((x) => x.FileName == fileName);
            return new SourcesDifference(funcs.ToList(), DiversionCoefficient);
        }

        // 修正行数
        public override int ModifiedStepNum {
            get { return functions.Sum(x => x.ModifiedStepNum); }
        }
        // 追加行数
        public override int NewAddedStepNum {
            get { return functions.Sum(x => x.NewAddedStepNum); }
        }
        // 削除行数
        public override int DeletedStepNum {
            get { return functions.Sum(x => x.DeletedStepNum); }
        }
        // 修正前の関数全体の行数
        public override int OldTotalStepNum {
            get { return functions.Sum(x => x.OldTotalStepNum); }
        }
        // 流用行数
        public override int DiversionStepNum {
            get { return functions.Sum(x => x.DiversionStepNum); }
        }
        // 流用行数
        public double DiversionCoefficient { get; private set; }

        /// <summary>
        /// 流用係数を変更する
        /// </summary>
        /// <param name="diversionStepNum"></param>
        /// <returns></returns>
        public SourcesDifference ChangeDiversionCoefficient(double diversionStepNum)
        {
            return new SourcesDifference(functions, diversionStepNum);
        }
        /// <summary>
        /// 流用付き新規換算ステップ数
        /// </summary>
        /// <returns></returns>
        public int MeasuredStepNumWithDiversion()
        {
            return MeasuredStepNum() + (int)(DiversionStepNum * DiversionCoefficient);
        }
    }
}
