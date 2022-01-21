using System.Collections.Generic;

namespace Croher.Actions
{
    internal interface IAction
    {
        bool Execute(IDictionary<string, string> parameters);
        ActionHelpMessages HelpMessages { get; }
    }
}
