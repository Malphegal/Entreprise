using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_AIA_Console_Version
{
    public class FunctionsNotFound : Exception
    {
        public FunctionsNotFound(string message) : base(message)
        { }
    }
}
