using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using KazoeciaoOutputAnalyzer;

namespace CodingDocumentCreateTool
{
    public class ModifiedFunctionListWindowViewModel : INotifyPropertyChanged
    {

        public class Function
        {
            public string Module { get; set; }
            public string FileName { get; set; }
            public string FunctionName { get; set; }
            public string ModifiedType{ get; set; }

            public Function(FunctionDifference funcDiff)
            {
                this.Module = funcDiff.DirectoryPath;
                this.FileName = funcDiff.FileName;
                this.FunctionName = funcDiff.FunctionName;
                this.ModifiedType = (funcDiff.IsDeleted() ? "削除" : (funcDiff.IsNewAdded() ? "新規" : (funcDiff.IsModified() ? "修正" : "-")));
            }
        }

        private List<Function> functions;

        public ModifiedFunctionListWindowViewModel()
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
