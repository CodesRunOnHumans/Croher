using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Croher.Actions
{
    record ActionHelpMessages(string Summary, IDictionary<string, string> Parameters);
}
