using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;

namespace SphereSharp.Syntax
{
    public class MemberAccessSyntax
    {
        public static MemberAccessSyntax GlobalObject { get; } = new MemberAccessSyntax("[global]", null);

        public string MemberName
        {
            get
            {
                if (MemberNameSyntax.Segments.Length == 1 && MemberNameSyntax.Segments[0] is TextSegmentSyntax textSegment)
                {
                    return textSegment.Text;
                }

                throw new NotImplementedException();
            }
        }

        public SymbolSyntax MemberNameSyntax { get; }
        public MemberAccessSyntax Object { get; }

        public MemberAccessSyntax(string memberName, MemberAccessSyntax obj)
            : this(new SymbolSyntax(memberName), obj)
        {
        }

        public MemberAccessSyntax(SymbolSyntax memberName, MemberAccessSyntax obj)
        {
            MemberNameSyntax = memberName;
            Object = obj;
        }
    }
}
