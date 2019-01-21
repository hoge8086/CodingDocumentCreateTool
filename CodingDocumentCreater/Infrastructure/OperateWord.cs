using System;
using System.Linq;

using System.Runtime.InteropServices;
using Microsoft.Office.Interop.Word;

namespace CodingDocumentCreater.Infrastructure
{
    public class OperateWord : IDisposable
    {

        // Word操作用オブジェクト
        protected Application xlApp = null;
        protected Document xlDoc= null;

        public void Open(string filePath)
        {
            xlApp = new Application();
            xlApp.DisplayAlerts = WdAlertLevel.wdAlertsNone;
            xlApp.Visible = false;
            xlDoc = xlApp.Application.Documents.Open(CalcPath(filePath));
        }

        public void Save(string filePath)
        {
            xlDoc.SaveAs2(CalcPath(filePath));
        }

        public void Close()
        {
            try
            {
                // xDocl
                if (xlDoc != null)
                {
                    Marshal.ReleaseComObject(xlDoc);
                    xlDoc = null;
                }
         
                // xlApp解放
                if (xlApp != null)
                {
                    try
                    {
                        // アラートを戻して終了
                        xlApp.DisplayAlerts = WdAlertLevel.wdAlertsAll;
                        xlApp.Quit();
                    }
                    finally
                    {
                        Marshal.ReleaseComObject(xlApp);
                        xlApp = null;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        public void Write(int tableIndex, params string[] values)
        {
            var row = xlDoc.Tables[tableIndex].Rows.Add();
            for(int i=0; i<values.Count(); i++)
            {
                xlDoc.Tables[tableIndex].Cell(row.Index, i + 1).Range.Text = values[i];
            }
        }

        private string CalcPath(string path)
        {
            if (System.IO.Path.IsPathRooted(path))
                return path;
            return System.IO.Path.Combine(new System.IO.FileInfo(System.Reflection.Assembly.GetExecutingAssembly().Location).Directory.FullName, path);
        }

        #region IDisposable Support
        private bool disposedValue = false; // 重複する呼び出しを検出するには

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: マネージ状態を破棄します (マネージ オブジェクト)。
                }

                // TODO: アンマネージ リソース (アンマネージ オブジェクト) を解放し、下のファイナライザーをオーバーライドします。
                // TODO: 大きなフィールドを null に設定します。
                Close();

                disposedValue = true;
            }
        }

        // TODO: 上の Dispose(bool disposing) にアンマネージ リソースを解放するコードが含まれる場合にのみ、ファイナライザーをオーバーライドします。
         ~OperateWord() {
           // このコードを変更しないでください。クリーンアップ コードを上の Dispose(bool disposing) に記述します。
           Dispose(false);
         }

        // このコードは、破棄可能なパターンを正しく実装できるように追加されました。
        public void Dispose()
        {
            // このコードを変更しないでください。クリーンアップ コードを上の Dispose(bool disposing) に記述します。
            Dispose(true);
            // TODO: 上のファイナライザーがオーバーライドされる場合は、次の行のコメントを解除してください。
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
