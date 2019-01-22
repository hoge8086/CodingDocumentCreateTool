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
    public class CodingDocumentOutputExcel : ICodingDocumentOutput
    {
        public void WriteModuleDiffList(List<ModuleDifferrenceListDTO> moduleDiffList)
        {
            int rowIndex = 1;
            using (var excel = new OperateExcel())
            {
                excel.Open(@"template\内部仕様書_Template.xlsx");

                for(int i=0; i<moduleDiffList.Count; i++)
                {
                    excel.Write(rowIndex, 1, moduleDiffList[i].Name);
                    rowIndex++;
                    for (int j = 0; j < moduleDiffList[i].ModulesDiff.Count; j++)
                    {
                        string[] values = {
                            moduleDiffList[i].ModulesDiff[j].Name,
                            moduleDiffList[i].ModulesDiff[j].Difference.NewAddedStepNum.ToString(),
                            moduleDiffList[i].ModulesDiff[j].Difference.ModifiedStepNum.ToString(),
                            moduleDiffList[i].ModulesDiff[j].Difference.DeletedStepNum.ToString(),
                            moduleDiffList[i].ModulesDiff[j].Difference.MeasuredStepNum().ToString(),
                            moduleDiffList[i].ModulesDiff[j].Difference.DiversionStepNum.ToString(),
                        };
                        excel.Write(rowIndex, 1, values);
                        rowIndex++;
                    }
                    rowIndex++;
                }

                if(!System.IO.Directory.Exists(@".\out"))
                    System.IO.Directory.CreateDirectory(@".\out");
                excel.Save(@"out\内部仕様書.xlsx");
            }
        }
    }
}
