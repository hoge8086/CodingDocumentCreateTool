using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using KazoeciaoOutputAnalyzer;

namespace CodingDocumentCreateTool
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public class Function
        {
            public string Module { get; set; }
            public string FileName { get; set; }
            public string FunctionName { get; set; }
            public string NewAddedStepNum { get; set; }
            public string ModifiedStepNum { get; set; }
            public string DeletedStepNum { get; set; }
            public string DiversionStepNum { get; set; }
            public string OldTotalStepNum { get; set; }

            public Function(FunctionDifference funcDiff)
            {
                this.Module = funcDiff.DirectoryPath;
                this.FileName = funcDiff.FileName;
                this.FunctionName = funcDiff.FunctionName;
                this.NewAddedStepNum = funcDiff.NewAddedStepNum.ToString();
                this.ModifiedStepNum = funcDiff.ModifiedStepNum.ToString();
                this.DeletedStepNum = funcDiff.DeletedStepNum.ToString();
                this.DiversionStepNum = funcDiff.DiversionStepNum.ToString();
                this.OldTotalStepNum = funcDiff.OldTotalStepNum.ToString();
            }
        }

        private List<Function> functions;

        public MainWindowViewModel()
        {
            functions = new List<Function>();
        }

        public List<Function> Functions
        {
            get { return functions; }
            set
            {
                functions = value;
                if(PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(nameof(Functions)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }

}
