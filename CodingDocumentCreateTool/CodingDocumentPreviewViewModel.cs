using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using KazoeciaoOutputAnalyzer;
using CodingDocumentCreater.DomainService;

namespace CodingDocumentCreateTool
{
    public class CodingDocumentPreviewViewModel : INotifyPropertyChanged
    {
        public class Module
        {
            public string ModuleName { get; set; }
            public string NewAddedStepNum { get; set; }
            public string ModifiedStepNum { get; set; }
            public string DeletedStepNum { get; set; }
            public string DiversionStepNum { get; set; }

            public Module(KazoeciaoQueryService.ModuleDifferrenceDTO moduleDiff)
            {
                this.ModuleName = moduleDiff.Name;
                this.NewAddedStepNum = moduleDiff.Defference.NewAddedStepNum.ToString();
                this.ModifiedStepNum = moduleDiff.Defference.ModifiedStepNum.ToString();
                this.DeletedStepNum = moduleDiff.Defference.DeletedStepNum.ToString();
                this.DiversionStepNum = moduleDiff.Defference.DiversionStepNum.ToString();
            }
        }

        private List<Module> modules;

        public CodingDocumentPreviewViewModel()
        {
            modules = new List<Module>();
        }

        public List<Module> Modules
        {
            get { return modules; }
            set
            {
                modules = value;
                if(PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(nameof(Modules)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }

}
