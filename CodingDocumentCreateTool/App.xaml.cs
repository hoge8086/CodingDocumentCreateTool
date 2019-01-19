using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

using CodingDocumentCreater.DomainService;
using CodingDocumentCreater.Infrastructure;

namespace CodingDocumentCreateTool
{
    /// <summary>
    /// App.xaml の相互作用ロジック
    /// </summary>
    public partial class App : Application
    {
        static public CodingDocumentCreateService DocumentCreateService = null;
        static public KazoeciaoQueryService QueryService = null;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var reader = new KazoeciaoOutputAnalyzer.KazoeciaoDefault.KazoeciaoOutputReaderDefault();
            DocumentCreateService = new CodingDocumentCreateService(new OutputFactory(), reader);
            QueryService = new KazoeciaoQueryService(reader);
        }
    }
}
