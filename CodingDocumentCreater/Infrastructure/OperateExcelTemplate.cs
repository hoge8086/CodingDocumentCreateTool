using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.InteropServices;
using Microsoft.Office.Interop.Excel;

namespace CodingDocumentCreater.Infrastructure
{
    /// <summary>
    /// エクセル操作ユーティリティ
    /// </summary>
    public class OperateExcelTemplate : IDisposable
    {
        // Excel操作用オブジェクト
        protected Application xlApp = null;
        protected Workbooks xlBooks = null;
        protected Workbook xlBook = null;
        protected Sheets xlSheets = null;
        protected Worksheet xlSheet = null;

        public void Open(string excelPath)
        {
            xlApp = new Application();
            xlApp.DisplayAlerts = false;
            xlBook = xlApp.Workbooks.Open(CalcPath(excelPath));
            xlSheets = xlBook.Sheets;
            xlSheet = xlSheets[1];
        }

        public void Close()
        {
            try
            {
                // xlSheet解放
                if (xlSheet != null)
                {
                    Marshal.ReleaseComObject(xlSheet);
                    xlSheet = null;
                }
         
                // xlSheets解放
                if (xlSheets != null)
                {
                    Marshal.ReleaseComObject(xlSheets);
                    xlSheets = null;
                }
         
                // xlBook解放
                if (xlBook != null)
                {
                    try
                    {
                        xlBook.Close();
                    }
                    finally
                    {
                        Marshal.ReleaseComObject(xlBook);
                        xlBook = null;
                    }
                }
         
                // xlBooks解放
                if (xlBooks != null)
                {
                    Marshal.ReleaseComObject(xlBooks);
                    xlBooks = null;
                }
         
                // xlApp解放
                if (xlApp != null)
                {
                    try
                    {
                        // アラートを戻して終了
                        xlApp.DisplayAlerts = true;
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

        public void Save(string savePath)
        {
            xlBook.SaveAs(CalcPath(savePath));
        }

        public void SelectSheet(string sheetName)
        {
            foreach (Worksheet sh in xlSheets)
            {
                if (sheetName == sh.Name)
                {
                    xlSheet = sh;
                    return;
                }
                Marshal.ReleaseComObject(sh);
            }
            throw new ArgumentException("ブック内に指定シート名は見つかりません。");
        }

        public void Write(int row, int column, params object[] values)
        {

            Range xlRange = null;
            Range xlStartRange = null;
            Range xlEndRange = null;

            try
            {
                xlStartRange = xlSheet.Cells[row, column];
                xlEndRange = xlSheet.Cells[row, column + values.Count() - 1];
                xlRange = xlSheet.Range[xlStartRange, xlEndRange];
                xlRange.Value2 = values;
            }
            finally
            {
                if (xlRange != null)
                {
                    Marshal.ReleaseComObject(xlRange);
                    xlRange = null;
                }
                if (xlStartRange != null)
                {
                    Marshal.ReleaseComObject(xlStartRange);
                    xlStartRange = null;
                }
                if (xlEndRange != null)
                {
                    Marshal.ReleaseComObject(xlEndRange);
                    xlEndRange = null;
                }
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
                    Close();
                }

                // TODO: アンマネージ リソース (アンマネージ オブジェクト) を解放し、下のファイナライザーをオーバーライドします。
                // TODO: 大きなフィールドを null に設定します。

                disposedValue = true;
            }
        }

        // TODO: 上の Dispose(bool disposing) にアンマネージ リソースを解放するコードが含まれる場合にのみ、ファイナライザーをオーバーライドします。
        // ~FunctionListOutputExcel() {
        //   // このコードを変更しないでください。クリーンアップ コードを上の Dispose(bool disposing) に記述します。
        //   Dispose(false);
        // }

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
