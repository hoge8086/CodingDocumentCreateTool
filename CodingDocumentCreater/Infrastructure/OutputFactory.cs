
using CodingDocumentCreater.DomainService;
using CodingDocumentCreater.Infrastructure;

namespace CodingDocumentCreater.Infrastructure
{
    public class OutputFactory : IOutputFactory
    {
        public ICodingDocumentOutput CreateCodingDocumentOutput()
        {
            return new CodingDocumentMultiOutput(
                new CodingDocumentOutputExcel(),
                new CodingDocumentOutputWord());
        }
    }
}
