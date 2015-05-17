using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Servicing.Extensions
{
    public static class RelfectionExtensions
    {
        /// <summary>
        /// Returns collection of types defined in given assembly
        /// </summary>
        public static IEnumerable<Type> EnumerateTypes(this Assembly assembly)
        {
            if (assembly == null) return Enumerable.Empty<Type>();

            try
            {
                return assembly.GetTypes();
            }
            catch (ReflectionTypeLoadException exception)
            {
                return exception.Types;
            }
        }

        /// <summary>
        /// Returns true if given type is considered controller
        /// </summary>
        public static bool IsController(this Type type)
        {
            return type != null
                   && type.IsPublic
                   && !type.IsAbstract
                   && typeof(IController).IsAssignableFrom(type)
                   && type.Name.EndsWith("Controller", StringComparison.OrdinalIgnoreCase);
        }
    }
}
