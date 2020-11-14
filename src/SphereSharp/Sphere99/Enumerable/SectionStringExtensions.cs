using System.Collections.Generic;

namespace SphereSharp.Sphere99.Enumerable
{
    public static class SectionStringExtensions
    {
        public static IEnumerable<SectionParsingResult<sphereScript99Parser.SaveFileSectionContext>> ToSaveSections(this string content)
        {
            var parser = new Sphere99.Parser();
            return new SectionEnumerable<sphereScript99Parser.SaveFileSectionContext>(content, parser.ParseSaveFileSection);
        }
    }
}
