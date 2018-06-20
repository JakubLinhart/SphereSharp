﻿using System;
using System.Collections.Generic;

namespace SphereSharp.Sphere99.Sphere56Transpiler
{
    internal sealed class ItemSaveFilePropertiesTranspiler : SaveFilePropertiesTranspiler
    {
        private static HashSet<string> forbiddenProperties = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "Age", "Changer",
            "CoOwner", "Friend" // TODO: needs to be translated for .56 somehow
        };

        public ItemSaveFilePropertiesTranspiler(SourceCodeBuilder builder, Sphere56TranspilerVisitor parentVisitor)
            : base(builder, parentVisitor, forbiddenProperties)
        {
        }
    }
}
