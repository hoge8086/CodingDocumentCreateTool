using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using CsvSerializer;

namespace KazoeciaoOutputAnalyzer.KazoeciaoDefault
{
    public class FunctionDifferenceDefault : IFunctionDifference
    {
        // Csvのフィールド一覧 (使用しないフィールドは定義不要)
        [CsvSerializer.FieldAttribute("変更後パス名", true)]
        public string PathAfter {get; set;}
        [CsvSerializer.FieldAttribute("変更前パス名", true)]
        public string PathBefore {get; set;}
        [CsvSerializer.FieldAttribute("新規ステップ数")]
        public int NewAddedStepNum { get; set; }
        [CsvSerializer.FieldAttribute("モジュール名")]
        public string FunctionName { get; set; }
        [CsvSerializer.FieldAttribute("修正元ステップ数")]
        public int OldTotalStepNum { get; set; }
        [CsvSerializer.FieldAttribute("修正ステップ数")]
        public int ModifiedStepNum { get; set; }
        [CsvSerializer.FieldAttribute("流用ステップ数")]
        public int DiversionStepNum { get; set; }
        [CsvSerializer.FieldAttribute("削除ステップ数")]
        public int DeletedStepNum { get; set; }

        private string CalcFilePath()
        {
            var path = !string.IsNullOrEmpty(PathAfter) ? PathAfter : PathBefore;
            return Regex.Replace(path, @".*(new|old)(?=\\)", string.Empty, RegexOptions.IgnoreCase);
        }

        public string FileName {
            get { return System.IO.Path.GetFileNameWithoutExtension(CalcFilePath()); }
        }

        public string DirectoryPath {
            get { return System.IO.Path.GetDirectoryName(CalcFilePath()); }
        }

        public bool IsNewAdded()
        {
            return (OldTotalStepNum == 0) && (NewAddedStepNum > 0);
        }

        public bool IsModified()
        {
            return (NewAddedStepNum > 0) ||
                   (ModifiedStepNum > 0) ||
                   (DeletedStepNum > 0);
        }

        public bool IsDeleted()
        {
            return (OldTotalStepNum > 0) &&
                   (OldTotalStepNum == DeletedStepNum);
        }

        public override string ToString()
        {
            return string.Format("{0}({1}):{2}",
                            DirectoryPath + @"\" + FileName + @"\" + FunctionName,
                            (IsDeleted() ? "Del" : (IsNewAdded() ? "Add" : (IsModified() ? "Modify" : "None"))),
                            base.ToString());
        }

        public FunctionDifferenceDefault() { }

        /// <summary>
        /// ユニットテスト用コンストラクタ
        /// </summary>
        public FunctionDifferenceDefault(
            string FunctionName,
            string Path,
            int ModifiedStepNum,
            int NewAddedStepNum,
            int DeletedStepNum,
            int OldTotalStepNum,
            int DiversionStepNum)
        {
            this.PathAfter = @"old\" + Path;
            this.PathBefore = @"new\" + Path;;
            this.NewAddedStepNum = NewAddedStepNum;
            this.FunctionName = FunctionName;
            this.OldTotalStepNum = OldTotalStepNum;
            this.ModifiedStepNum = ModifiedStepNum;
            this.DiversionStepNum = DiversionStepNum;
            this.DeletedStepNum = DeletedStepNum;
        }

        
    }
}
