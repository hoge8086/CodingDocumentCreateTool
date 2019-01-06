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

            comboBoxSelectedModule.Items.Add("全てのモジュール");
            for(int i=directoryPaths.Count()-1; i>=0; i--)
                comboBoxSelectedModule.Items.Add(directoryPaths[i]);
            comboBoxSelectedModule.SelectedIndex = 0;
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            try
            {
                LoadModule();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, Properties.Resources.ToolName, MessageBoxButton.OK, MessageBoxImage.Exclamation);
                Close();
            }
        }

        private void LoadModule()
        {
            if(comboBoxSelectedModule.SelectedIndex == 0)
            {
                var modules = App.QueryService.QueryModuleDifferences(kazoeciaoOutputPath, directoryPaths, diversionCoefficient);
                viewModel.Modules = modules.Select((x) => new CodingDocumentPreviewViewModel.Module(x)).ToList();
            }else
            {
                string directory = comboBoxSelectedModule.SelectedValue as string;
                var modules = App.QueryService.QueryModuleFileDifferences(kazoeciaoOutputPath, directory, diversionCoefficient);
                viewModel.Modules = modules.Select((x) => new CodingDocumentPreviewViewModel.Module(x)).ToList();
            }
        }

        private void ClickButton(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void SelectModuleChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadModule();
        }
    }
}
