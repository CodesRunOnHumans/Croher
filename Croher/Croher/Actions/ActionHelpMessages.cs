using System.Collections.Generic;

namespace Croher.Actions
{
    internal record ActionHelpMessages(string Summary, IDictionary<string, string> Parameters);
}
