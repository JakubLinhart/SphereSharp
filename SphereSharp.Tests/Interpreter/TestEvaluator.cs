using FluentAssertions;
using SphereSharp.Interpreter;
using SphereSharp.Model;
using SphereSharp.Runtime;
using SphereSharp.Syntax;
using SphereSharp.Tests.Runtime;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphereSharp.Tests.Interpreter
{
    internal class TestEvaluator
    {
        private Dictionary<string, NameDef> nameDefs = new Dictionary<string, NameDef>();
        private Dictionary<string, FunctionDef> functions = new Dictionary<string, FunctionDef>();
        private Dictionary<string, EventsDef> events = new Dictionary<string, EventsDef>();
        private Dictionary<string, SkillDef> skills = new Dictionary<string, SkillDef>();

        private object src;
        private object defaultTargetObject;
        private IGump argo;

        public TestChar TestChar { get; set; }
        public TestItem TestItem { get; set; } = new TestItem();
        public TestClient TestObjBase { get; } = new TestClient();
        public TestGump TestGump { get; } = new TestGump();

        public CodeModel CodeModel { get; private set; }
        public EvaluationContext Context { get; private set; }
        public Evaluator Evaluator { get; private set; }

        public TestEvaluator()
        {
            TestChar = new TestChar(name => null, (syntax, context) => Evaluator.Evaluate(syntax, context));
        }

        public TestEvaluator AddNameDef(string key, string value)
        {
            nameDefs.Add(key, new NameDef(key, value));

            return this;
        }

        public TestEvaluator AddSkillDef(SkillDef skillDef)
        {
            skills.Add(skillDef.DefName, skillDef);

            return this;
        }


        internal TestEvaluator AddEvents(EventsSectionSyntax section)
        {
            var eventsDef = CodeModelBuilder.BuildEventsDef(section);
            this.events[section.Name] = eventsDef;

            return this;
        }

        public TestEvaluator AddFunction(string src)
        {
            var syntax = SectionSyntax.Parse(src).Should().BeOfType<FunctionSectionSyntax>().Which;

            functions.Add(syntax.Name, new FunctionDef(syntax.Name, syntax.Body));

            return this;
        }

        public TestEvaluator SetSrc(object objBase)
        {
            src = objBase;
            return this;
        }

        public TestEvaluator SetArgO(IGump gump)
        {
            argo = gump;
            return this;
        }

        public TestEvaluator Create()
        {
            CodeModel = new CodeModel(Enumerable.Empty<ItemDef>(), Enumerable.Empty<GumpDef>(),
                nameDefs.Values, functions.Values, eventsDefs: events.Values, skillDefs: skills.Values);

            Context = new EvaluationContext();
            Context.ArgO = argo;
            Context.Src = src;
            Context.Default = defaultTargetObject ?? (object)src ?? argo;
            Evaluator = new Evaluator(CodeModel, new Binder(CodeModel));

            return this;
        }

        public string EvaluateCodeBlock(string src)
        {
            var syntax = CodeBlockSyntax.Parse(src);
            return Evaluator.Evaluate(syntax, Context);
        }

        public string EvaluateCall(string src)
        {
            var syntax = CallSyntax.Parse(src);
            return Evaluator.Evaluate(syntax, Context);
        }

        public int EvaluateExpression(string src)
        {
            var syntax = ExpressionSyntax.Parse(src);
            return Evaluator.Evaluate(syntax, Context);
        }

        public string EvaluateSymbol(string src)
        {
            var syntax = SymbolSyntax.Parse(src);
            return Evaluator.Evaluate(syntax, Context);
        }

        internal TestEvaluator SetDefault(object defaultObj)
        {
            defaultTargetObject = defaultObj;

            return this;
        }
    }
}
