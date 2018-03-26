using SphereSharp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphereSharp.Interpreter
{
    public class Binder : IBinder
    {
        private readonly CodeModel model;
        private BuildInFunctionBindings functionBindings = new BuildInFunctionBindings();
        private BuildInPropertyBindings propertyBindinds = new BuildInPropertyBindings();

        public Binder(CodeModel model)
        {
            this.model = model;
        }

        public Function GetFunction(object targetObject, string name)
        {
            Function function;

            if (!model.TryGetFunction(name, out FunctionDef functionDef))
            {
                function = this.functionBindings.GetFunction(targetObject, name);
            }
            else
                function = new UserDefinedFunction(name, functionDef);

            return function;
        }

        public void SetProperty(object targetObject, string name, object value)
        {
            propertyBindinds.SetProperty(targetObject, name, value);
        }
    }
}
