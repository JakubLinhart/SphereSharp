using SphereSharp.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SphereSharp.Interpreter
{
    public class BuildInFunctionBindings : IFunctionBinder
    {
        private static Dictionary<string, Function> functions = new Dictionary<string, Function>(StringComparer.OrdinalIgnoreCase);

        static BuildInFunctionBindings()
        {
            Add("dialog", Dialog);
            Add("closedialog", CloseDialog);
            Add("sysmessage", SysMessage);
            Add("tag", Tag);
            Add("remove", Remove);

            Add("gumppic", GumpPic);
            Add("setlocation", SetLocation);
            Add("resizepic", ResizePic);
            Add("htmlgump", HtmlGump);
            Add("htmlgumpa", HtmlGumpA);
            Add("button", Button);
            Add("texta", TextA);
            Add("textentry", TextEntry);
            Add("settext", SetText);

            Add("argv", ArgV);
            Add("argn", ArgN);
            Add("arg", Arg);
            Add("args", ArgS);
            Add("argtxt", ArgTxt);
            Add("strlen", Strlen);
            Add("strcmpi", Strcmpi);

            Add("skill", Skill);
            Add("newitem", NewItem);
        }

        private static void Add(string name, Func<object, EvaluationContext, object> impl) =>
            functions.Add(name, new BuiltInFunction(name, impl));

        public Function GetFunction(object targetObject, string name)
        {
            if (functions.TryGetValue(name, out Function function))
                return function;

            return null;
        }

        private static object Strlen(object obj, EvaluationContext context)
        {
            string arg = context.Arguments.ArgS(0);
            if (string.IsNullOrEmpty(arg))
                return "0";

            return arg.Length.ToString();
        }

        private static object Strcmpi(object obj, EvaluationContext context)
        {
            string arg1 = context.Arguments.ArgS(0);
            string arg2 = context.Arguments.ArgS(1);

            if (arg1.Equals(arg2, StringComparison.OrdinalIgnoreCase))
                return "0";
            return "1";
        }

        public static object Arg(object obj, EvaluationContext context)
        {
            if (context.Parent == null)
                throw new NotImplementedException("Parent context not defined.");

            if (context.Arguments.Count() == 1)
                return context.Parent.Variables.Read(context.Arguments.ArgS(0));
            else if (context.Arguments.Count() == 2)
            {
                context.Parent.Variables.Set(context.Arguments.ArgS(0), context.Arguments.ArgS(1));
                return string.Empty;
            }
            else
                throw new NotImplementedException($"Argument count mismatch, {context.Arguments.Count()} expected 2,");
        }

        private static object ArgV(object obj, EvaluationContext context)
        {
            if (context.Parent != null)
            {
                return context.Parent.Arguments.ArgS(context.Arguments.ArgInt(0));
            }
            else
                throw new NotImplementedException();
        }

        private static object ArgN(object obj, EvaluationContext context)
        {
            if (context.Parent != null)
            {
                return context.Parent.Arguments.ArgN.ToString();
            }
            else
                throw new NotImplementedException();
        }

        private static object ArgS(object obj, EvaluationContext context)
        {
            if (context.Parent != null)
            {
                return context.Parent.Arguments.ArgS(0);
            }
            else
                throw new NotImplementedException();
        }

        private static object ArgTxt(object obj, EvaluationContext context)
        {
            if (context.Parent != null)
            {
                return context.Parent.Arguments.ArgTxt(context.Arguments.ArgInt(0));
            }
            else
                throw new NotImplementedException();
        }

        private static object Dialog(object targetObject, EvaluationContext context)
        {
            Arguments dialogInitArgs = new Arguments();

            if (context.Arguments.Count() > 1)
            {
                foreach (var arg in context.Arguments.Skip(1))
                {
                    dialogInitArgs.Add(arg);
                }
            }

            ((IClient)targetObject).Dialog(context.Arguments.ArgS(0), dialogInitArgs);

            return string.Empty;
        }

        private static object Skill(object targetObject, EvaluationContext context)
        {
            ((IChar)targetObject).Skill(context.Arguments.ArgInt(0));

            return string.Empty;
        }

        private static object NewItem(object targetObject, EvaluationContext context)
        {
            ((IChar)targetObject).NewItem(context.Arguments.ArgS(0));

            return string.Empty;
        }

        private static string Tag(object targetObject, EvaluationContext context)
        {
            if (context.Src is IHoldTags tagHolder)
            {
                if (context.Arguments.Count > 1)
                {
                    tagHolder.Tag(context.Arguments.ArgS(0), context.Arguments.ArgS(1));
                    return string.Empty;
                }
                else
                {
                    return tagHolder.Tag(context.Arguments.ArgS(0));
                }
            }
            else
                throw new NotImplementedException($"unhandled context.Src {context.Src.GetType().Name}");
        }

        private static object Remove(object targetObject, EvaluationContext context)
        {
            if (context.Src is IHoldTags tagHolder)
            {
                tagHolder.RemoveTag(context.Arguments.ArgS(0));
                return string.Empty;
            }
            else
                throw new NotImplementedException($"unhandled context.Src {context.Src.GetType().Name}");
        }

        private static object CloseDialog(object targetObject, EvaluationContext context)
        {
            ((IClient)targetObject).CloseDialog(context.Arguments.ArgS(0), context.Arguments.ArgInt(1));
            return string.Empty;
        }

        private static object SysMessage(object targetObject, EvaluationContext context)
        {
            ((IClient)targetObject).SysMessage(context.Arguments.ArgS(0));

            return string.Empty;
        }

        private static object GumpPic(object targetObject, EvaluationContext context)
        {
            ((IGump)targetObject).GumpPic(context.Arguments.ArgInt(0), context.Arguments.ArgInt(1), context.Arguments.ArgInt(2));
            return string.Empty;
        }

        private static object SetLocation(object targetObject, EvaluationContext context)
        {
            ((IGump)targetObject).SetLocation(context.Arguments.ArgInt(0), context.Arguments.ArgInt(1));
            return string.Empty;
        }

        private static object ResizePic(object targetObject, EvaluationContext context)
        {
            ((IGump)targetObject).ResizePic(context.Arguments.ArgInt(0), context.Arguments.ArgInt(1), context.Arguments.ArgInt(2), context.Arguments.ArgInt(3), context.Arguments.ArgInt(4));
            return string.Empty;
        }

        private static object HtmlGump(object targetObject, EvaluationContext context)
        {
            ((IGump)targetObject).HtmlGump(context.Arguments.ArgInt(0), context.Arguments.ArgInt(1), context.Arguments.ArgInt(2), context.Arguments.ArgInt(3));
            return string.Empty;
        }

        private static object HtmlGumpA(object targetObject, EvaluationContext context)
        {
            ((IGump)targetObject).HtmlGumpA(context.Arguments.ArgInt(0), context.Arguments.ArgInt(1), context.Arguments.ArgInt(2), context.Arguments.ArgInt(3), context.Arguments.ArgS(4));
            return string.Empty;
        }

        private static object Button(object targetObject, EvaluationContext context)
        {
            ((IGump)targetObject).Button(context.Arguments.ArgInt(0), context.Arguments.ArgInt(1), context.Arguments.ArgInt(2), context.Arguments.ArgInt(3), context.Arguments.ArgInt(4), context.Arguments.ArgInt(5), context.Arguments.ArgInt(6));
            return string.Empty;
        }

        private static object TextA(object targetObject, EvaluationContext context)
        {
            ((IGump)targetObject).TextA(context.Arguments.ArgInt(0), context.Arguments.ArgInt(1), context.Arguments.ArgInt(2), context.Arguments.ArgS(3));
            return string.Empty;
        }

        private static object TextEntry(object targetObject, EvaluationContext context)
        {
            ((IGump)targetObject).TextEntry(context.Arguments.ArgInt(0), context.Arguments.ArgInt(1), context.Arguments.ArgInt(2), context.Arguments.ArgInt(3), context.Arguments.ArgInt(4), context.Arguments.ArgInt(5), context.Arguments.ArgInt(6));
            return string.Empty;
        }

        private static object SetText(object targetObject, EvaluationContext context)
        {
            ((IGump)targetObject).SetText(context.Arguments.ArgInt(0), context.Arguments.ArgS(1));
            return string.Empty;
        }
    }
}
