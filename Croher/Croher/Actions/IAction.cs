using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Croher.Actions
{
    interface IAction
    {
        bool Execute(IDictionary<string, string> parameters);
        ActionHelpMessages HelpMessages { get; }
    }
}
