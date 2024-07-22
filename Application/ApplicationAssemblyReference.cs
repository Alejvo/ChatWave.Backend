using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public sealed class ApplicationAssemblyReference
    {
        internal static readonly Assembly Assembly = typeof(ApplicationAssemblyReference).Assembly;
    }
}
