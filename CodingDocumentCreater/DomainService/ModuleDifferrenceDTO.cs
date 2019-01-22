using KazoeciaoOutputAnalyzer;

namespace CodingDocumentCreater.DomainService
{
    /// <summary>
    /// モジュール差分
    /// </summary>
    public class ModuleDifferrenceDTO
    {
        public string Name { get; private set; }
        public SourcesDifference Difference { get; private set; }

        public ModuleDifferrenceDTO(string name, SourcesDifference difference)
        {
            this.Name = name;
            this.Difference = difference;
        }
    }
}
