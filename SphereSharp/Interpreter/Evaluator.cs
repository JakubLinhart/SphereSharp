using SphereSharp.Model;
using SphereSharp.Runtime;
using SphereSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace SphereSharp.Interpreter
{
    public class Evaluator
    {
        public CodeModel Model { get; }
        public IBinder Binder { get; }

        public Evaluator(CodeModel model, IBinder binder)
        {
            Model = model;
            Binder = binder;
        }
        
        public string Evaluate(MacroSyntax macroSyntax, EvaluationContext context)
        {
            return Evaluate(macroSyntax.Call, context).ToString();
        }

        public string Evaluate(LiteralSyntax literalSyntax, EvaluationContext context)
        {
            StringBuilder builder = new StringBuilder();

            foreach (var segment in literalSyntax.Segments)
            {
                switch (segment)
                {
                    case TextSegmentSyntax textSegment:
                        builder.Append(textSegment.Text);
                        break;
                    case MacroSegmentSyntax macroSegment:
                        builder.Append(Evaluate(macroSegment.Macro.Call, context));
                        break;
                    case EvalMacroSegmentSyntax evalMacroSegment:
                        builder.Append(Evaluate(evalMacroSegment.Expression, context));
                        break;
                    default:
                        throw new NotImplementedException($"Literal segment type {segment.GetType().Name}");
                }
            }

            return builder.ToString();
        }

        public string Evaluate(ArgumentSyntax syntax, EvaluationContext context)
        {
            switch (syntax)
            {
                case TextArgumentSyntax textArgument:
                    return textArgument.Text;
                case LiteralArgumentSyntax literalArgument:
                    return Evaluate(literalArgument.Literal, context);
                case MacroArgumentSyntax macroArgument:
                    var argValue = Evaluate(macroArgument.Macro, context);
                    return argValue;
                case EvalMacroArgumentSyntax evalArgument:
                    var evalArgValue = Evaluate(evalArgument.Expression, context);
                    return evalArgValue.ToString();
                default:
                    throw new NotImplementedException($"evaluation of {syntax.GetType().Name}");
            }
        }

        public EvaluationContext Evaluate(ArgumentListSyntax syntax, EvaluationContext context)
        {
            var subContext = context.CreateSubContext();

            foreach (var argSyntax in syntax.Arguments)
            {
                var argValue = Evaluate(argSyntax, context);
                subContext.Arguments.Add(argValue);
            }

            return subContext;
        }

        public void Evaluate(AssignmentSyntax assignment, EvaluationContext context)
        {
            var memberName = Evaluate(assignment.LValue.MemberNameSyntax, context);
            var value = Evaluate(assignment.RValue, context);

            if (assignment.LValue.Object.MemberName.Equals("tag", StringComparison.OrdinalIgnoreCase))
            {
                var function = Binder.GetFunction(context.Default, "tag");
                if (function != null)
                {
                    var subContext = context.CreateSubContext();
                    subContext.Arguments.Add(memberName);
                    subContext.Arguments.Add(value.ToString());
                    function.Call(context.Default, this, subContext);
                }
                else
                {
                    throw new NotImplementedException();
                }
            }
            else
                Binder.SetProperty(context.Default, memberName.ToLower(), value);
        }

        public object Evaluate(CallSyntax statement, EvaluationContext context)
        {
            object result;

            var memberName = Evaluate(statement.MemberNameSyntax, context);
            if (memberName.Equals("src", StringComparison.OrdinalIgnoreCase))
                result = context.Src;
            else if (memberName.Equals("argo", StringComparison.OrdinalIgnoreCase))
                result = context.ArgO;
            else if (statement.Arguments.Arguments.Any())
            {
                var subContext = Evaluate(statement.Arguments, context);
                var function = Binder.GetFunction(context.Default, memberName);
                if (function != null)
                {
                    result = function.Call(context.Default, this, subContext);
                }
                else
                    throw new InvalidOperationException($"Unknown function {memberName}");
            }
            else if (memberName.Equals("tag", StringComparison.OrdinalIgnoreCase))
            {
                var tagMemberName = Evaluate(statement.ChainedCall.MemberNameSyntax, context);
                var function = Binder.GetFunction(context.Src, "tag");
                if (function != null)
                {
                    if (statement.ChainedCall.Arguments.Arguments.Any())
                    {
                        var subContext = Evaluate(statement.ChainedCall.Arguments, context);
                        var tagFunction = Binder.GetFunction(context.Src, tagMemberName);
                        if (function != null)
                        {
                            return tagFunction.Call(context.Default, this, subContext);
                        }
                        else
                            throw new InvalidOperationException($"Unknown function {tagMemberName}");
                    }
                    else
                    {
                        var subContext = context.CreateSubContext();
                        subContext.Arguments.Add(tagMemberName);
                        return function.Call(context.Src, this, subContext);
                    }
                }
                else
                {
                    throw new NotImplementedException();
                }
            }
            else
            {
                var function = Binder.GetFunction(context.Default, memberName);
                if (function != null)
                    result = function.Call(context.Default, this, context.CreateSubContext());
                else if (Binder.TryGetProperty(context.Default, memberName, out object propResult))
                    result = propResult;
                else if (this.Model.TryGetSkillDef(memberName, out SkillDef skillDef))
                    result = skillDef.Id.ToString();
                else
                    result = this.Model.GetDefName(memberName).Value;
            }

            if (statement.ChainedCall != null)
            {
                var chainedContext = context.CreateSubContext();
                chainedContext.Arguments = context.Arguments;
                chainedContext.Variables = context.Variables;
                chainedContext.Default = result;

                return Evaluate(statement.ChainedCall, chainedContext);
            }
            else
                return result;


            //if (statement.Arguments.Arguments.Any())
            //{
            //    var subContext = Evaluate(statement.Arguments, context);

            //    object obj;
            //    if (statement.MemberName.Equals("src", StringComparison.OrdinalIgnoreCase))
            //        obj = subContext.Src;
            //    else if (statement.MemberName.Equals("argo", StringComparison.OrdinalIgnoreCase))
            //        obj = subContext.ArgO;
            //    else
            //        obj = subContext.Default;

            //    var function = Binder.GetFunction(obj, statement.MemberName.ToLower());
            //    if (function != null)
            //    {
            //        return function.Call(obj, this, subContext);
            //    }
            //    else
            //        throw new InvalidOperationException($"Unknown function {statement.MemberName}");
            //}
            //else if (statement.ChainedCall != null)
            //{
            //    string memberName = Evaluate(statement.MemberNameSyntax, context);

            //    var function = Binder.GetFunction(context.Default, memberName.ToLower());
            //    if (function != null)
            //        return function.Call(null, this, context.CreateSubContext());

            //    if (Binder.TryGetProperty(context.Default, memberName, out object result))
            //        return result.ToString();

            //    if (this.Model.TryGetSkillDef(memberName, out SkillDef skillDef))
            //    {
            //        return skillDef.Id.ToString();
            //    }

            //    return this.Model.GetDefName(memberName).Value;
            //}
            //else
            //{
            //    if (statement.ChainedCall.MemberName.Equals("tag", StringComparison.OrdinalIgnoreCase))
            //    {
            //        var function = Binder.GetFunction(context.Src, "tag");
            //        if (function != null)
            //        {
            //            string memberName = Evaluate(statement.MemberNameSyntax, context);

            //            var subContext = context.CreateSubContext();
            //            subContext.Arguments.Add(memberName);
            //            return function.Call(context.Src, this, subContext);
            //        }
            //        else
            //        {
            //            throw new NotImplementedException();
            //        }
            //    }
            //    else
            //    {
            //        throw new NotImplementedException();
            //    }
            //}
        }

        public string Evaluate(IEnumerable<SegmentSyntax> segments, EvaluationContext context)
        {
            if (segments.Count() == 1 && segments.First() is TextSegmentSyntax textSegment)
            {
                return textSegment.Text;
            }

            var builder = new StringBuilder();
            foreach (var segment in segments)
            {
                switch (segment)
                {
                    case TextSegmentSyntax textSegment2:
                        builder.Append(textSegment2.Text);
                        break;
                    case MacroSegmentSyntax macroSegment:
                        var macroResult = Evaluate(macroSegment.Macro, context);
                        builder.Append(macroResult);
                        break;
                    default:
                        throw new NotImplementedException($"segment type {segment.GetType().Name}");
                }
            }

            return builder.ToString();
        }

        public string Evaluate(SymbolSyntax symbolSyntax, EvaluationContext context)
        {
            var symbol = Evaluate(symbolSyntax.Segments, context);

            if (symbolSyntax is IndexedSymbolSyntax indexed)
            {
                return $"{symbol}[{Evaluate(indexed.Index, context)}]";
            }

            return symbol;
        }

        public int Evaluate(UnaryOperatorSyntax op, EvaluationContext context)
        {
            var operand = Evaluate(op.Operand, context);

            switch (op.Kind)
            {
                case UnaryOperatorKind.LogicalNot:
                    return operand == 0 ? 1 : 0;
                default:
                    throw new NotImplementedException($"Unary operator kind {op.Kind}");
            }
        }

        public int Evaluate(BinaryOperatorSyntax op, EvaluationContext context)
        {
            var operand1 = Evaluate(op.Operand1, context);
            var operand2 = Evaluate(op.Operand2, context);

            switch (op.Kind)
            {
                case BinaryOperatorKind.Add:
                    return operand1 + operand2;
                case BinaryOperatorKind.Subtract:
                    return operand1 - operand2;
                case BinaryOperatorKind.Multiply:
                    return operand1 * operand2;
                case BinaryOperatorKind.Equal:
                    return operand1 == operand2 ? 1 : 0;
                case BinaryOperatorKind.NotEqual:
                    return operand1 != operand2 ? 1 : 0;
                case BinaryOperatorKind.LogicalOr:
                    return operand1 != 0 || operand2 != 0 ? 1 : 0;
                case BinaryOperatorKind.BinaryOr:
                    return operand1 | operand2;
                case BinaryOperatorKind.MoreThan:
                    return operand1 > operand2 ? 1 : 0;
                case BinaryOperatorKind.LessThan:
                    return operand1 < operand2 ? 1 : 0;
                default:
                    throw new NotImplementedException($"Binary operator kind {op.Kind}");
            }
        }

        private int ParseInt(string str)
        {
            if (str.StartsWith("0"))
                return ParseInt(str, NumberStyles.HexNumber);
            else
                return ParseInt(str, NumberStyles.Number);
        }

        private int ParseInt(string str, NumberStyles style)
        {
            if (string.IsNullOrEmpty(str))
                return 0;

            return int.Parse(str, style);
        }

        private static Random random = new Random();

        public int Evaluate(IntervalExpressionSyntax interval, EvaluationContext context)
        {
            var min = Evaluate(interval.MinValue, context);
            var max = Evaluate(interval.MaxValue, context);

            if (min > max)
            {
                var temp = min;
                min = max;
                max = temp;
            }

            return random.Next(min, max);
        }

        public int Evaluate(IntegerConstantExpressionSyntax constant)
        {
            NumberStyles style;

            switch (constant.Kind)
            {
                case ConstantExpressionSyntaxKind.Decadic:
                    style = NumberStyles.Number;
                    break;
                case ConstantExpressionSyntaxKind.Hex:
                    style = NumberStyles.HexNumber;
                    break;
                default:
                    throw new NotImplementedException($"{constant.Kind}");
            }

            return ParseInt(constant.Value, style);
        }

        public int Evaluate(ExpressionSyntax expr, EvaluationContext context)
        {
            switch (expr)
            {
                case IntegerConstantExpressionSyntax integerConstant:
                    return Evaluate(integerConstant);
                case DecimalConstantExpressionSyntax decimalConstant:
                    return (int)Decimal.Parse(decimalConstant.Value, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture);
                case IntervalExpressionSyntax interval:
                    return Evaluate(interval, context);
                case CallExpressionSyntax call:
                    return ParseInt(Evaluate(call.Call, context).ToString());
                case UnaryOperatorSyntax unaryOperator:
                    return Evaluate(unaryOperator, context);
                case BinaryOperatorSyntax binaryOperator:
                    return Evaluate(binaryOperator, context);
                case MacroExpressionSyntax macroExpression:
                    return ParseInt(Evaluate(macroExpression.Macro, context));
                case EvalMacroExpressionSyntax evalMacroExpression:
                    return Evaluate(evalMacroExpression.Expression, context);
                default:
                    throw new NotImplementedException($"Expression type {expr.GetType().Name}");
            }
        }

        public string Evaluate(IfSyntax ifSyntax, EvaluationContext context)
        {
            var conditionValue = Evaluate(ifSyntax.Condition, context);
            if (conditionValue != 0)
            {
                return Evaluate(ifSyntax.ThenBlock, context);
            }
            else
            {
                foreach (var elseIf in ifSyntax.ElseIfs)
                {
                    var elseIfConditionValue = Evaluate(elseIf.Condition, context);
                    if (elseIfConditionValue != 0)
                        return Evaluate(elseIf.ThenBlock, context);
                }

                return Evaluate(ifSyntax.ElseBlock, context);
            }
        }

        public string Evaluate(MacroStatementSyntax macroSyntax, EvaluationContext context)
        {
            var statement = Evaluate(macroSyntax.Macro, context);
            var statementSyntax = CodeBlockSyntax.Parse(statement);
            return Evaluate(statementSyntax, context);
        }

        public string Evaluate(DoSwitchSyntax doSwitchSyntax, EvaluationContext context)
        {
            var conditionValue = Evaluate(doSwitchSyntax.Condition, context);

            if (conditionValue < 0 || conditionValue > doSwitchSyntax.Cases.Length)
                throw new NotImplementedException($"doswitch condition {conditionValue} out of range (0, {doSwitchSyntax.Cases.Length})");

            return Evaluate(doSwitchSyntax.Cases[conditionValue], context, out bool _);
        }

        private void Evaluate(EventsStatementSyntax eventsStatement, EvaluationContext context)
        {
            switch (eventsStatement.Kind)
            {
                case EventsOperationKind.Subscribe:
                    ((IHoldTriggers)context.Default).SubscribeEvents(Model.GetEventsDef(eventsStatement.EventName));
                    break;
                case EventsOperationKind.Unsubscribe:
                    ((IHoldTriggers)context.Default).UnsubscribeEvents(Model.GetEventsDef(eventsStatement.EventName));
                    break;
                default:
                    throw new NotImplementedException($"Events operation {eventsStatement.Kind}");
            }
        }

        public string Evaluate(StatementSyntax statementSyntax, EvaluationContext context, out bool terminate)
        {
            terminate = false;

            switch (statementSyntax)
            {
                case AssignmentSyntax assignment:
                    Evaluate(assignment, context);
                    break;
                case MacroStatementSyntax macroSyntax:
                    Evaluate(macroSyntax, context);
                    break;
                case CallSyntax callSyntax:
                    Evaluate(callSyntax, context);
                    break;
                case ReturnSyntax returnSyntax:
                    terminate = true;
                    return Evaluate(returnSyntax.Argument, context);
                case IfSyntax ifSyntax:
                    Evaluate(ifSyntax, context);
                    break;
                case DoSwitchSyntax doSwitch:
                    Evaluate(doSwitch, context);
                    break;
                case EventsStatementSyntax eventsStatement:
                    Evaluate(eventsStatement, context);
                    break;
                default:
                    throw new NotImplementedException($"Evaluation of {statementSyntax.GetType().Name}");
            }

            return string.Empty;
        }

        public string Evaluate(CodeBlockSyntax codeBlock, EvaluationContext context)
        {
            foreach (var statement in codeBlock.Statements)
            {
                var result = Evaluate(statement, context, out bool terminate);
                if (terminate)
                    return result;
            }

            return string.Empty;
        }
    }
}
