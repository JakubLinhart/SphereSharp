using Sprache;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SphereSharp.Syntax
{
    public class EventsStatementSyntax : StatementSyntax
    {
        public EventsStatementSyntax(string name, EventsOperationKind kind)
        {
            EventName = name;
            Kind = kind;
        }

        public string EventName { get; }
        public EventsOperationKind Kind { get; }

        public static EventsStatementSyntax Parse(string src) => EventsStatementParser.Events.Parse(src);
    }
}
