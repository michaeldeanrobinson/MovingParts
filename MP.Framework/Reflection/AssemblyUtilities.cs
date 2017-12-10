using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using MP.Framework.Logger;

namespace MP.Framework.Reflection
{
    public static class AssemblyUtilities
    {
        public static Assembly LoadAssembly(string file)
        {
            if (!file.EndsWith(".dll", StringComparison.OrdinalIgnoreCase))
            {
                file += ".dll";
            }

            try
            {
                FileInfo info = new FileInfo(file);
                Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

                for (int i = 0; i < assemblies.Length; i++)
                {
                    if (String.Compare(info.Name, assemblies[i].GetName().Name + ".dll", true) == 0)
                    {
                        return assemblies[i];
                    }
                }

                try
                {
                    return Assembly.Load(Path.Combine(info.DirectoryName, info.Name));
                }
                catch (Exception ex)
                {
                    LoggerFactory.DefaultLogger.LogException(ex);
                }

                try
                {
                    if (!AppDomain.CurrentDomain.ShadowCopyFiles)
                    {
                        return Assembly.Load(info.FullName);
                    }
                }
                catch (Exception ex)
                {
                    LoggerFactory.DefaultLogger.LogException(ex);
                }

                try
                {
                    return AppDomain.CurrentDomain.Load(info.Name);
                }
                catch (Exception ex)
                {
                    LoggerFactory.DefaultLogger.LogException(ex);
                }

                try
                {
                    return AppDomain.CurrentDomain.Load(new AssemblyName(info.Name));
                }
                catch (Exception e)
                {
                    LoggerFactory.DefaultLogger.LogException(e);
                }

                try
                {
                    return AppDomain.CurrentDomain.Load(info.Name);
                }
                catch (Exception e)
                {
                    LoggerFactory.DefaultLogger.LogException(e);
                }

                try
                {
                    return AppDomain.CurrentDomain.Load(info.FullName);
                }
                catch (Exception e)
                {
                    LoggerFactory.DefaultLogger.LogException(e);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Write("Error loading Assembly " + file + " - " + ex.Message);
            }

            return null;
        }

        public static Type[] Locate(Assembly assembly, Type attribute)
        {
            List<Type> plugins = new List<Type>();

            Locate(assembly, attribute, plugins);

            return plugins.ToArray();
        }

        public static void Locate(Assembly assembly, Type attribute, List<Type> plugins)
        {
            Type[] assemblyTypes = assembly.GetTypes();

            for (int j = 0; j < assemblyTypes.Length; j++)
            {
                Attribute attrib = Attribute.GetCustomAttribute(assemblyTypes[j], attribute, false);

                if (attrib == null)
                {
                    continue;
                }

                plugins.Add(assemblyTypes[j]);
            }
        }

        public static Type Locate(string name)
        {
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

            for (int i = 0; i < assemblies.Length; i++)
            {
                Type type = assemblies[i].GetType(name, false, true);

                if (type != null)
                {
                    return type;
                }
            }

            return null;
        }

        public static string CreateStringFromResource(Type type, string resourceName)
        {
            using (TextReader reader = new StreamReader(type.Assembly.GetManifestResourceStream(type, resourceName)))
            {
                return reader.ReadToEnd();
            }
        }

        public static Stream CreateStreamFromResource(Type type, string resourceName)
        {
            Stream stream = type.Assembly.GetManifestResourceStream(type, resourceName);

            return stream;
        }

        public static byte[] CreateByteArrayFromResource(Type type, string resourceName)
        {
            using (Stream stream = type.Assembly.GetManifestResourceStream(type, resourceName))
            {
                byte[] buffer = new byte[1024];
                using (MemoryStream ms = new MemoryStream())
                {
                    while (true)
                    {
                        int read = stream.Read(buffer, 0, buffer.Length);
                        if (read <= 0)
                        {
                            return ms.ToArray();
                        }

                        ms.Write(buffer, 0, read);
                    }
                }
            }
        }

        public static string GetAssemblyName(Assembly assembly)
        {
            return assembly.FullName.Substring(0, assembly.FullName.IndexOf(","));
        }

        public static string GetAssemblyVersion(Assembly assembly)
        {
            foreach (string part in assembly.FullName.Split(','))
            {
                string trimmed = part.Trim();

                if (trimmed.StartsWith("Version="))
                {
                    return trimmed.Substring(8);
                }
            }

            return "0.0.0.0";
        }

        public static Type GetType(string assembly, string className)
        {
            return LoadAssembly(assembly).GetType(className);
        }

        public static void LoadAssemblies(string workingDirectory, string mask)
        {
            List<Assembly> loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies().Where(o => !o.IsDynamic).ToList();
            string[] loadedPaths = loadedAssemblies.Select(a => a.Location).ToArray();

            string[] referencedPaths = Directory.GetFiles(workingDirectory, mask);
            List<string> toLoad = referencedPaths.Where(r => !loadedPaths.Contains(r, StringComparer.InvariantCultureIgnoreCase)).ToList();
            toLoad.ForEach(path => loadedAssemblies.Add(AppDomain.CurrentDomain.Load(AssemblyName.GetAssemblyName(path))));
        }

        public static List<Type> GetTypes(string mask, string startsWith)
        {
            string workingDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            LoadAssemblies(workingDirectory, mask);

            List<Assembly> assemblies = AppDomain.CurrentDomain.GetAssemblies().Where(o => o.FullName.StartsWith("MP.")).ToList();
            List<Type> types = new List<Type>();

            foreach (Assembly item in assemblies)
            {
                types.AddRange(item.GetTypes());
            }

            return types;
        }
    }
}
