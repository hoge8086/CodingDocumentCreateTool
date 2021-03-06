﻿using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CodingDocumentCreater.DomainService;
using KazoeciaoOutputAnalyzer;

namespace CodingDocumentCreater.Infrastructure
{

    public class CodingDocumentOutputWord : ICodingDocumentOutput
    {
        public void WriteModuleDiffList(List<ModuleDifferrenceListDTO> moduleDiffList, double diversionCoefficient)
        {
            using (var word = new OperateWord())
            {
                word.Open(@"Template\内部仕様書_Template.docx");

                var total = moduleDiffList[0];
                for(int i=0; i<total.ModulesDiff.Count; i++)
                {
                    string[] values = {
                        total.ModulesDiff[i].Name,
                        total.ModulesDiff[i].Difference.NewAddedStepNum.ToString(),
                        total.ModulesDiff[i].Difference.ModifiedStepNum.ToString(),
                        total.ModulesDiff[i].Difference.DeletedStepNum.ToString(),
                        total.ModulesDiff[i].Difference.MeasuredStepNum().ToString(),
                        total.ModulesDiff[i].Difference.DiversionStepNum.ToString(),
                        total.ModulesDiff[i].Difference.MeasuredStepNumWithDiversion().ToString(),
                    };
                    word.Write(3, values);
                }
                if(!System.IO.Directory.Exists(Setting.OutputDirectory))
                    System.IO.Directory.CreateDirectory(Setting.OutputDirectory);
                word.Save(System.IO.Path.Combine(Setting.OutputDirectory, "内部仕様書.docx"));
            }
        }

    }
}
