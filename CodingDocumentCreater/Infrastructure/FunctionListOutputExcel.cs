using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.InteropServices;

using CodingDocumentCreater.DomainService;
using KazoeciaoOutputAnalyzer;
using OperrateExcel;

namespace CodingDocumentCreater.Infrastructure
{
    public class FunctionListOutputExcel : OperateExcel, IFunctionListOutput
    {
        int functionNum = 1;

        public void Open()
        {
            Open(@"Template\修正関数一覧_Template.xlsx");
            functionNum = 1;
        }

        public new void Close()
        {
            if(!System.IO.Directory.Exists(@".\out"))
                System.IO.Directory.CreateDirectory(@".\out");
            Save(@"out\修正関数一覧.xlsx");
            base.Close();
        }

        public void Write(IFunctionDifference funcDiff)
        {
            string[] values = {
                funcDiff.IsNewAdded() ? "新規" : (funcDiff.IsDeleted() ? "削除" : "修正"),
                funcDiff.DirectoryPath,
                funcDiff.FileName,
                funcDiff.FunctionName
            };
            Write(functionNum, 1, values);
            functionNum++;
        }

    }
}
