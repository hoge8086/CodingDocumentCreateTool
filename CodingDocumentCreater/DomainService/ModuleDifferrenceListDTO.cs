using System.Collections.Generic;

namespace CodingDocumentCreater.DomainService
{
    /// <summary>
    /// モジュール差分リスト
    /// </summary>
    public class ModuleDifferrenceListDTO
    {
        public string Name { get; private set; }
        public List<ModuleDifferrenceDTO> ModulesDiff{ get; private set; }
        public ModuleDifferrenceListDTO(string name, List<ModuleDifferrenceDTO> modulesDiff)
        {
            this.Name = name;
            this.ModulesDiff = new List<ModuleDifferrenceDTO>(modulesDiff);
        }
    }
}
