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
using System.Windows.Navigation;
using System.Windows.Shapes;

using CodingDocumentCreater.DomainService;
using Microsoft.Win32;

namespace CodingDocumentCreateTool
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowViewModel viewModel = new MainWindowViewModel();
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }

        private void LoadKazoeciaoOutput(object sender, RoutedEventArgs e)
        {
            try
            {
                var funcs = App.QueryService.QueryFunctionDifferencess(textboxKazoeciaoOutputFilePath.Text);
                viewModel.Functions = funcs.Select((x) => new MainWindowViewModel.Function(x)).ToList();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, Properties.Resources.ToolName, MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void CreateModifiedFunctionList(object sender, RoutedEventArgs e)
        {
            var win = new ModifiedFunctionListWindow(textboxKazoeciaoOutputFilePath.Text);
            win.Owner = this;
            win.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            win.ShowDialog();
        }

        private void CreateCordingDocument(object sender, RoutedEventArgs e)
        {
            var win = new CodingDocumentCreateWindow(textboxKazoeciaoOutputFilePath.Text);
            win.Owner = this;
            win.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            win.ShowDialog();
        }

        private void SelectFile(object sender, RoutedEventArgs e)
        {
            //OpenFileDialogクラスのインスタンスを作成
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            ofd.Filter = "かぞえチャオ出力ファイル(*.csv)|*.csv;|すべてのファイル(*.*)|*.*";
            //[ファイルの種類]ではじめに選択されるものを指定する
            ofd.FilterIndex = 1;
            ofd.Title = "かぞえチャオの出力ファイルを選択してください";

            //ダイアログを表示する
            if (ofd.ShowDialog() ?? false)
            {
                //OKボタンがクリックされたとき、選択されたファイル名を表示する
                textboxKazoeciaoOutputFilePath.Text = ofd.FileName;
            }
        }

        //---
        // 参考URL:<https://qiita.com/Go-zen-chu/items/c6dbd4c472909118fad0>
        private void textBox_PreviewDragOver(object sender, System.Windows.DragEventArgs e) {
            if (e.Data.GetDataPresent(System.Windows.DataFormats.FileDrop, true)) {
                e.Effects = System.Windows.DragDropEffects.Copy;
            } else {
                e.Effects = System.Windows.DragDropEffects.None;
            }
            e.Handled = true;
        }
        private void textBox_Drop(object sender, System.Windows.DragEventArgs e) {
            var dropFiles = e.Data.GetData(System.Windows.DataFormats.FileDrop) as string[];
            if (dropFiles == null) return;
            textboxKazoeciaoOutputFilePath.Text = dropFiles[0];
        }
        //---
    }
}
