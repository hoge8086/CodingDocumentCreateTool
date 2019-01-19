using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodingDocumentCreater.DomainService;
using KazoeciaoOutputAnalyzer;

namespace CodingDocumentCreater.Infrastructure
{
    public class MockConsoleOutputFactory : IOutputFactory
    {
        public ICodingDocumentOutput CreateCodingDocumentOutput()
        {
            return new OutputConsole();
        }

        public IFunctionListOutput CreateFunctionListOutput()
        {
            return new OutputConsole();
        }
    }

    public class OutputConsole : IFunctionListOutput, ICodingDocumentOutput
    {
        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        private static extern bool AllocConsole();

        private const UInt32 StdOutputHandle = 0xFFFFFFF5;

        public OutputConsole()
        {
        }

        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        private static extern IntPtr GetStdHandle(UInt32 nStdHandle);
        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        private static extern void SetStdHandle(UInt32 nStdHandle, IntPtr handle);

        public void Close()
        {
        }

        public void Open()
        {
            AllocConsole();

            //// stdout's handle seems to always be equal to 7
            //IntPtr defaultStdout = new IntPtr(7);
            //IntPtr currentStdout = GetStdHandle(StdOutputHandle);

            //if (currentStdout != defaultStdout)
            //    // reset stdout
            //    SetStdHandle(StdOutputHandle, defaultStdout);

            //// reopen stdout
            //TextWriter writer = new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = true };
            //Console.SetOut(writer);
        }

        public void Write(IFunctionDifference funcDiff)
        {
            Console.Write(funcDiff.ToString());
            System.Diagnostics.Debug.WriteLine(funcDiff.ToString());
        }

        public void WriteFileDiff(string fileName, IStepDifference fileDiff)
        {
            Console.Write(fileName +  ":" + fileDiff.ToString());
            System.Diagnostics.Debug.WriteLine(fileName +  ":" + fileDiff.ToString());
        }

        public void WriteModuleDiff(string moduleName, IStepDifference moduleDiff)
        {
            Console.Write(moduleName +  ":" + moduleDiff.ToString());
            System.Diagnostics.Debug.WriteLine(moduleName +  ":" + moduleDiff.ToString());
        }

        public void WriteTotalDiff(IStepDifference totalDiff)
        {
            Console.Write("Total:" + totalDiff.ToString());
            System.Diagnostics.Debug.WriteLine("Total:" + totalDiff.ToString());
        }

        public void Dispose()
        {
        }
    }
}
