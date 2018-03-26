using SphereSharp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphereSharp.ServUO.Generator
{
    public class Generator
    {
        public string NameSpace { get; }
        public CodeModel CodeModel { get; }

        public Generator(string nameSpace, CodeModel codeModel)
        {
            NameSpace = nameSpace;
            CodeModel = codeModel;
        }

        public string Generate(ItemDef itemDef)
        {
            if (itemDef.IsBase)
            {
                var template = new BaseItemDefTemplate(NameSpace, itemDef);
                return template.TransformText();
            }
            else
            {
                var template = new InheritedItemDefTemplate(NameSpace, itemDef);
                return template.TransformText();
            }
        }

        public string Generate(CharDef charDef)
        {
            if (charDef.IsBase)
            {
                var template = new BaseCharDefTemplate(NameSpace, charDef);
                return template.TransformText();
            }
            else
            {
                var template = new InheritedCharDefTemplate(NameSpace, charDef);
                return template.TransformText();
            }
        }

        public string Generate(GumpDef gumpDef)
        {
            var template = new GumpTemplate(NameSpace, gumpDef);
            return template.TransformText();
        }
    }
}
