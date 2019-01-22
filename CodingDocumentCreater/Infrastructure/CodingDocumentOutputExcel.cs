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
        public void WriteModuleDiffList(List<ModuleDifferrenceListDTO> moduleDiffList, double diversionCoefficient)
        {
            int rowIndex = 1;
            using (var excel = new OperateExcel())
            {
                excel.Open(@"template\内部仕様書_Template.xlsx");

                excel.Write(0, 8, diversionCoefficient);
                for(int i=0; i<moduleDiffList.Count; i++)
                {
                    excel.Write(rowIndex, 1, moduleDiffList[i].Name);
                    rowIndex++;
                    excel.CopyAndInsertRow(rowIndex, 1, 1, "template");
                    rowIndex++;
                    for (int j = 0; j < moduleDiffList[i].ModulesDiff.Count; j++)
                    {
                        object[] values = {
                            moduleDiffList[i].ModulesDiff[j].Name,
                            moduleDiffList[i].ModulesDiff[j].Difference.NewAddedStepNum,
                            moduleDiffList[i].ModulesDiff[j].Difference.ModifiedStepNum,
                            moduleDiffList[i].ModulesDiff[j].Difference.DeletedStepNum,
                            moduleDiffList[i].ModulesDiff[j].Difference.MeasuredStepNum(),
                            moduleDiffList[i].ModulesDiff[j].Difference.DiversionStepNum,
                            moduleDiffList[i].ModulesDiff[j].Difference.MeasuredStepNumWithDiversion(),
                        };
                        excel.CopyAndInsertRow(rowIndex, 2, 1, "template");
                        excel.Write(rowIndex, 1, values);
                        rowIndex++;
                    }
                    rowIndex++;
                }
                excel.DeleteSheet("template");

                if(!System.IO.Directory.Exists(Setting.OutputDirectory))
                    System.IO.Directory.CreateDirectory(Setting.OutputDirectory);
                excel.Save(System.IO.Path.Combine(Setting.OutputDirectory, "内部仕様書.xlsx"));
            }
        }
    }
}
