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
    /// ModifiedFunctionListWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class ModifiedFunctionListWindow : Window
    {
        private string kazoecioOutputFilePath;
        private ModifiedFunctionListWindowViewModel viewModel = new ModifiedFunctionListWindowViewModel();

        public ModifiedFunctionListWindow(string kazoecioOutputFilePath)
        {
            InitializeComponent();
            this.kazoecioOutputFilePath = kazoecioOutputFilePath;
            this.DataContext = viewModel;
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            try
            {
                var funcs = App.QueryService.QueryModifiedFunctions(kazoecioOutputFilePath);
                viewModel.Functions = funcs.Select((x) => new ModifiedFunctionListWindowViewModel.Function(x)).ToList();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, Properties.Resources.ToolName, MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }

        }

        private void ClickButton(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
