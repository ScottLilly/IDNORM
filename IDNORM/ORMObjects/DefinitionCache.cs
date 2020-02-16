using System.Collections.Generic;

namespace IDNORM.ORMObjects
{
    internal static class DefinitionCache
    {
        internal static List<ClassDefinition> ClassDefinitions { get; set; }

        static DefinitionCache()
        {
            ClassDefinitions = new List<ClassDefinition>();
        }

        internal static void AddClass()
        {
            
        }
    }
}
