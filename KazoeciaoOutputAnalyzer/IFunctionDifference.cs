namespace KazoeciaoOutputAnalyzer
{
    public interface IFunctionDifference : IStepDifference
    {
        /// <summary>
        /// ディレクトリパス(モジュール名)
        /// </summary>
        string DirectoryPath { get; }
        /// <summary>
        /// ファイル名(クラス名)
        /// </summary>
        string FileName { get; }
        /// <summary>
        /// 関数名
        /// </summary>
        string FunctionName { get; }

        /// <summary>
        /// 関数自体が削除されたか?
        /// </summary>
        /// <returns></returns>
        bool IsDeleted();
        /// <summary>
        /// 関数が修正されたか?(修正/新規追加/削除)
        /// </summary>
        /// <returns></returns>
        bool IsModified();
        /// <summary>
        /// 新規追加関数かどうか?
        /// </summary>
        /// <returns></returns>
        bool IsNewAdded();
    }
}