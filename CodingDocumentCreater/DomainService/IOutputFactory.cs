﻿namespace CodingDocumentCreater.DomainService
{
    /// <summary>
    /// 内部仕様書出力インタフェース
    /// </summary>
    public interface IOutputFactory
    {
        ICodingDocumentOutput CreateCodingDocumentOutput();
    }
}
