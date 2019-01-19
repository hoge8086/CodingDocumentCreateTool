using KazoeciaoOutputAnalyzer;
using System;

namespace CodingDocumentCreater.DomainService
{
    public interface ICodingDocumentOutput : IDisposable
    {
        void Open();
        void WriteTotalDiff(IStepDifference totalDiff);
        void WriteModuleDiff(string moduleName, IStepDifference moduleDiff);
        void WriteFileDiff(string fileName, IStepDifference fileDiff);
        void Close();
    }
}
