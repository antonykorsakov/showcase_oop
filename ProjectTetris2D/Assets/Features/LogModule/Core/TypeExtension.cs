using System;

namespace Features.LogModule.Core
{
    internal static class TypeExtension
    {
        internal static string GetModuleName(this Type type)
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            var namespaceName = type.Namespace;
            if (namespaceName != null)
            {
                var parts = namespaceName.Split('.', StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length > 1)
                    return parts[1];
            }
#endif

            return "UnknownModule";
        }
    }
}