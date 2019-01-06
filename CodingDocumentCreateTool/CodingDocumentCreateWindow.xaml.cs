using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CodingDocumentCreateTool
{
    /// <summary>
    /// CodingDocumentCreateWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class CodingDocumentCreateWindow : Window
    {
        public class ViewModel : INotifyPropertyChanged
        {
            public class ModulePath
            {
                public string Module { get; set; }

                public ModulePath() { }
                public ModulePath(string module)
                {
                    this.Module = module;
                }
            }

            private List<ModulePath> modules;

            public ViewModel()
            {
                Modules = new List<ModulePath>();
            }

            public List<string> GetList()
            {
                return modules.Select((x) => x.Module).ToList();
            }

            public List<ModulePath> Modules
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

        private string kazoecioOutputFilePath;
        private ViewModel viewModel = new ViewModel();

        public CodingDocumentCreateWindow(string kazoecioOutputFilePath)
        {
            InitializeComponent();
            this.kazoecioOutputFilePath = kazoecioOutputFilePath;
            this.DataContext = viewModel;
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            try
            {
                var modules = App.QueryService.QueryModuleList(kazoecioOutputFilePath);
                viewModel.Modules = modules.Select((x) => new ViewModel.ModulePath(x)).ToList();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, Properties.Resources.ToolName, MessageBoxButton.OK, MessageBoxImage.Exclamation);
                Close();
            }
        }

        private void CreateDocument(object sender, RoutedEventArgs e)
        {
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;
                double diversionCoefficient = double.Parse(comboboxDiversionCoefficient.Text);
                App.DocumentCreateService.CreateCodingDocument(kazoecioOutputFilePath, viewModel.GetList(), diversionCoefficient);
                Mouse.OverrideCursor = null;
                MessageBox.Show("完了！", Properties.Resources.ToolName, MessageBoxButton.OK, MessageBoxImage.Information);
                Close();
            }
            catch(Exception ex)
            {
                Mouse.OverrideCursor = null;
                MessageBox.Show(ex.Message, Properties.Resources.ToolName, MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }

        }

        private void ShowPreview(object sender, RoutedEventArgs e)
        {
            try
            {
                double diversionCoefficient = double.Parse(comboboxDiversionCoefficient.Text);
                var win = new CodingDocumentPreview(kazoecioOutputFilePath, viewModel.GetList(), diversionCoefficient);
                win.Owner = this;
                win.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                win.ShowDialog();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, Properties.Resources.ToolName, MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }
    }
}
