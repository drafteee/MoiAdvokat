using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace LawyerService.BL.Helpers
{
    public static class ResourceHelper
    {
        public static string ReadManifestData(string embeddedFileName)
        {
            var assembly = typeof(ResourceHelper).GetTypeInfo().Assembly;
            return GetManifestData(assembly, embeddedFileName);
        }

        public static string ReadManifestDataFromAssembly(string assemblyName, string embeddedFileName)
        {
            var assembly = Assembly.Load(assemblyName);
            return GetManifestData(assembly, embeddedFileName);
        }

        private static string GetManifestData(Assembly assembly, string embeddedFileName)
        {
            var resourceName = assembly.GetManifestResourceNames().First(s => s.EndsWith(embeddedFileName, StringComparison.CurrentCultureIgnoreCase));

            using (var stream = assembly.GetManifestResourceStream(resourceName))
            {
                if (stream == null)
                {
                    throw new InvalidOperationException("Could not load manifest resource stream.");
                }

                using (var reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }
}
