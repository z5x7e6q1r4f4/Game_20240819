using System;
using System.Collections.Generic;
using System.Reflection;

namespace Main
{
    public static class AssemblyUtility
    {
        public static IEnumerable<Assembly> AllAssemblies => AppDomain.CurrentDomain.GetAssemblies();
    }
}