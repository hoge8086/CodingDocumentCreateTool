using KazoeciaoOutputAnalyzer;
using System;
using System.Collections.Generic;

namespace CodingDocumentCreater.DomainService
{
    public interface ICodingDocumentOutput
    {
        void WriteModuleDiffList(List<ModuleDifferrenceListDTO> moduleDiffList, double diversionCoefficient);
    }
}
