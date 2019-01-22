using CodingDocumentCreater.DomainService;
using System;
using System.Collections.Generic;
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
    /// CodingDocumentPreview.xaml の相互作用ロジック
    /// </summary>
    public partial class CodingDocumentPreview : Window
    {
        private string kazoeciaoOutputPath;
        private List<string> directoryPaths;
        private double diversionCoefficient;
        private CodingDocumentPreviewViewModel viewModel = new CodingDocumentPreviewViewModel();

        public CodingDocumentPreview(string kazoeciaoOutputPath, List<string> directoryPaths, double diversionCoefficient)
        {
            InitializeComponent();
            this.kazoeciaoOutputPath = kazoeciaoOutputPath;
            this.directoryPaths = directoryPaths;
            this.diversionCoefficient = diversionCoefficient;
            this.DataContext = viewModel;
        }

        List<ModuleDifferrenceListDTO> moduleList = null;

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            try
            {
                moduleList = App.QueryService.QueryModuleDifferrenceList(kazoeciaoOutputPath, directoryPaths, diversionCoefficient);
                for(int i=0; i<moduleList.Count; i++)
                {
                    ComboBoxItem item = new ComboBoxItem();
                    item.Content = moduleList[i].Name;
                    item.Tag = moduleList[i].ModulesDiff;
                    comboBoxSelectedModule.Items.Add(item);
                }
                comboBoxSelectedModule.SelectedIndex = 0;
                DrawGrid();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, Properties.Resources.ToolName, MessageBoxButton.OK, MessageBoxImage.Exclamation);
                Close();
            }
        }

        private void DrawGrid()
        {
            var diff = (List<ModuleDifferrenceDTO>)(((ComboBoxItem)comboBoxSelectedModule.SelectedItem).Tag);
            viewModel.Modules = diff.Select((x) => new CodingDocumentPreviewViewModel.Module(x)).ToList();
        }

        private void ClickButton(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void SelectModuleChanged(object sender, SelectionChangedEventArgs e)
        {
            DrawGrid();
        }
    }
}
