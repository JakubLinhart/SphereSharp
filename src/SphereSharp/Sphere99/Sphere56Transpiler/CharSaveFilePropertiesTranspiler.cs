using System;
using System.Collections.Generic;

namespace SphereSharp.Sphere99.Sphere56Transpiler
{
    internal sealed class CharSaveFilePropertiesTranspiler : SaveFilePropertiesTranspiler
    {
        private static HashSet<string> forbiddenProperties = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "Flag_running" };

        public CharSaveFilePropertiesTranspiler(SourceCodeBuilder builder, Sphere56TranspilerVisitor parentVisitor, MultiValueDictionary<string, string> invalidPropertyValues)
            : base(builder, parentVisitor, forbiddenProperties, invalidPropertyValues)
        {
        }
    }
}
