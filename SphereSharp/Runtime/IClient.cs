using SphereSharp.Interpreter;
using System;
using System.Collections.Generic;
using System.Text;

namespace SphereSharp.Runtime
{
    public interface IClient : IHoldTags
    {
        void Dialog(string defName, Arguments args);
        void CloseDialog(string defName, int buttonId);
        void SysMessage(string message);
    }
}
