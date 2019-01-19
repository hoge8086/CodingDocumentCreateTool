using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CodingDocumentCreater.DomainService;
using KazoeciaoOutputAnalyzer;
using OperrateExcel;

namespace CodingDocumentCreater.Infrastructure
{
    public class CodingDocumentOutputExcel : OperateExcel, ICodingDocumentOutput
    {
        int rowIndex = 1;

        public void Open()
        {
            Open(@"template\内部仕様書_Template.xlsx");
            rowIndex = 1;
        }

        public new void Close()
        {
            if(!System.IO.Directory.Exists(@".\out"))
                System.IO.Directory.CreateDirectory(@".\out");

            Save(@"out\内部仕様書.xlsx");
            base.Close();
        }

        public void WriteTotalDiff(IStepDifference funcDiff)
        {
            rowIndex++;
        }

        public void WriteModuleDiff(string moduleName, IStepDifference moduleDiff)
        {
            Write(rowIndex, 1, moduleName);
            rowIndex++;
        }

        public void WriteFileDiff(string fileName, IStepDifference fileDiff)
        {
            string[] values = {
                fileName,
                fileDiff.NewAddedStepNum.ToString(),
                fileDiff.ModifiedStepNum.ToString(),
                fileDiff.DeletedStepNum.ToString(),
                //fileDiff.MeasuredStepNum().ToString(),
                fileDiff.DiversionStepNum.ToString(),
            };
            Write(rowIndex, 1, values);
            rowIndex++;
        }
    }
}
