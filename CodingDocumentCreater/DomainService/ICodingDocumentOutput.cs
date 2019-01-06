using KazoeciaoOutputAnalyzer;
using System;

namespace CodingDocumentCreater.DomainService
{
    public interface ICodingDocumentOutput : IDisposable
    {
        void Open();
        void WriteTotalDiff(StepDifference totalDiff);
        void WriteModuleDiff(string moduleName, StepDifference moduleDiff);
        void WriteFileDiff(string fileName, StepDifference fileDiff);
        void Close();
    }
}
