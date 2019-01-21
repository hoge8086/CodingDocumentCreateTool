using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CodingDocumentCreater.DomainService;
using KazoeciaoOutputAnalyzer;

namespace CodingDocumentCreater.Infrastructure
{

    public class CodingDocumentOutputWord : ICodingDocumentOutput
    {
        public void WriteModuleDiffList(List<CodingDocumentCreateService.ModuleDifferrenceList> moduleDiffList)
        {
            using (var word = new OperateWord())
            {

                word.Open(@"Template\内部仕様書_Template.docx");

                var total = moduleDiffList[moduleDiffList.Count - 1];
                for(int i=0; i<total.ModulesDiff.Count; i++)
                {
                    string[] values = {
                        total.ModulesDiff[i].Name,
                        total.ModulesDiff[i].Difference.NewAddedStepNum.ToString(),
                        total.ModulesDiff[i].Difference.ModifiedStepNum.ToString(),
                        total.ModulesDiff[i].Difference.DeletedStepNum.ToString(),
                        total.ModulesDiff[i].Difference.MeasuredStepNum().ToString(),
                        total.ModulesDiff[i].Difference.DiversionStepNum.ToString(),
                    };
                    word.Write(3, values);
                }
                if(!System.IO.Directory.Exists(@".\out"))
                    System.IO.Directory.CreateDirectory(@".\out");
                word.Save(@"out\内部仕様書.docx");
            }
        }

    }
}
