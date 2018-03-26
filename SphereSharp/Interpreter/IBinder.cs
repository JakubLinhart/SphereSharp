using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphereSharp.Interpreter
{
    public interface IBinder : IFunctionBinder, IPropertyBinder
    {
    }

    public interface IFunctionBinder
    {
        Function GetFunction(object targetObject, string name);
    }

    public interface IPropertyBinder
    {
        void SetProperty(object targetObject, string name, object value);
    }
}
