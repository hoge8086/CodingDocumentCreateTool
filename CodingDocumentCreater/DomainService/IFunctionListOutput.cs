using KazoeciaoOutputAnalyzer;
using System;

namespace CodingDocumentCreater.DomainService
{
    /// <summary>
    /// 修正関数一覧出力インタフェース
    /// </summary>
    public interface IFunctionListOutput: IDisposable
    {
        void Open();
        void Write(FunctionDifference funcDiff);
        void Close();
    }
}
