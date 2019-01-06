using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KazoeciaoOutputAnalyzer
{
    public class FunctionDifference : StepDifference
    {
        // 関数名
        public string FunctionName { get; private set; }
        // ファイルパス
        private string filePath;
        // 修正行数
        private int modifiedStepNum;
        // 追加行数
        private int newAddedStepNum;
        // 削除行数
        private int deletedStepNum;
        // 修正前の関数全体の行数
        private int oldTotalStepNum;
        // 流用行数
        private int diversionStepNum;

        // ファイル名(クラス名)
        public string FileName {
            get { return System.IO.Path.GetFileNameWithoutExtension(filePath); }
        }
        // ディレクトリパス(モジュール名)
        public string DirectoryPath {
            get { return System.IO.Path.GetDirectoryName(filePath); }
        }
        // 修正行数
        public override int ModifiedStepNum {
            get { return modifiedStepNum; }
        }
        // 追加行数
        public override int NewAddedStepNum {
            get { return newAddedStepNum; }
        }
        // 削除行数
        public override int DeletedStepNum {
            get { return deletedStepNum; }
        }
        // 修正前の関数全体の行数
        public override int OldTotalStepNum {
            get { return oldTotalStepNum; }
        }
        // 流用行数
        public override int DiversionStepNum {
            get { return diversionStepNum; }
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="codeOfDeclaration"></param>
        /// <param name="filePath"></param>
        /// <param name="modifiedStepNum"></param>
        /// <param name="newAddedStepNum"></param>
        /// <param name="deletedStepNum"></param>
        /// <param name="oldTotalStepNum"></param>
        /// <param name="diversionStepNum"></param>
        public FunctionDifference(
            string codeOfDeclaration,
            string filePath,
            int modifiedStepNum,
            int newAddedStepNum,
            int deletedStepNum,
            int oldTotalStepNum,
            int diversionStepNum)
        {
            //正しくカウントされないときがある
            //System.Diagnostics.Debug.Assert(modifiedStepNum <= oldTotalStepNum);
            //System.Diagnostics.Debug.Assert(deletedStepNum <= oldTotalStepNum);
            //System.Diagnostics.Debug.Assert(diversionStepNum <= oldTotalStepNum);
            //System.Diagnostics.Debug.Assert((modifiedStepNum + diversionStepNum + deletedStepNum) <= oldTotalStepNum);

            this.FunctionName = codeOfDeclaration;
            this.filePath = filePath;
            this.modifiedStepNum = modifiedStepNum;
            this.newAddedStepNum = newAddedStepNum;
            this.deletedStepNum = deletedStepNum;
            this.oldTotalStepNum = oldTotalStepNum;
            this.diversionStepNum = diversionStepNum;
        }

        /// <summary>
        /// 新規追加関数かどうか?
        /// </summary>
        /// <returns></returns>
        public bool IsNewAdded()
        {
            return (OldTotalStepNum == 0) && (NewAddedStepNum > 0);
        }

        /// <summary>
        /// 関数が修正されたか?(修正/新規追加/削除)
        /// </summary>
        /// <returns></returns>
        public bool IsModified()
        {
            return (NewAddedStepNum > 0) ||
                   (ModifiedStepNum > 0) ||
                   (DeletedStepNum > 0);
        }

        /// <summary>
        /// 関数自体が削除されたか?
        /// </summary>
        /// <returns></returns>
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
    }
}
