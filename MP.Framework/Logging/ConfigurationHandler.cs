using System;
using System.Configuration;
using System.Xml;
using MP.Framework.Logger;
using MP.Framework.Reflection;

namespace MP.Framework.Logging
{
    public class ConfigurationHandler : IConfigurationSectionHandler
    {
        public static readonly string MachineConfiguration = "machineConfiguration";
        public static readonly string MachineName = "machineName";

        public static readonly string DefaultAttribute = "default";
        public static readonly string ThresholdAttribute = "threshold";
        public static readonly string TypesNodeName = "Types";
        public static readonly string TypeNodeName = "Type";
        public static readonly string AssembliesNodeName = "Assemblies";
        public static readonly string AssemblyNodeName = "Assembly";

        public static readonly string LoggersNodeName = "Loggers";
        public static readonly string NameNodeName = "Name";

        public static readonly string Log4NetNodeName = "Log4Net";

        public object Create(object parent, object configContext, XmlNode section)
        {
            PopulateConfiguration(section);

            return null;
        }

        private void PopulateConfiguration(XmlNode section)
        {
            string defaultLogger = null;
            if (section.Attributes[DefaultAttribute] != null)
            {
                defaultLogger = section.Attributes[DefaultAttribute].Value;
            }

            #region is this machine specific?
            bool machineSpecific = false;
            if (section.Attributes[MachineConfiguration] != null && (String.Compare(section.Attributes[MachineConfiguration].Value, "enabled", true) == 0 || String.Compare(section.Attributes[MachineConfiguration].Value, "true", true) == 0))
            {
                machineSpecific = true;
            }
            #endregion

            System.Collections.Specialized.StringDictionary processed = new System.Collections.Specialized.StringDictionary();
            foreach (XmlNode node in section.ChildNodes)
            {
                #region this section will only load nodes that match this machines configuration
                string machineName = "default";
                if (machineSpecific)
                {
                    if (node.Attributes != null && node.Attributes[MachineName] != null)
                    {
                        if (String.Compare(System.Environment.MachineName, node.Attributes[MachineName].Value, true) == 0)
                        {
                            if (processed.ContainsKey(node.Name))
                            {
                                if (!processed[node.Name].Equals(machineName))
                                {
                                    continue;
                                }
                                else
                                {
                                    machineName = node.Attributes[MachineName].Value;
                                    processed.Remove(node.Name);
                                    processed.Add(node.Name, machineName);
                                }
                            }
                            else
                            {
                                machineName = node.Attributes[MachineName].Value;
                                processed.Add(node.Name, machineName);
                            }
                        }
                        else
                        {
                            continue;
                        }
                    }
                    else
                    {
                        if (processed.ContainsKey(node.Name))
                        {
                            continue;
                        }

                        processed.Add(node.Name, machineName);
                    }
                }
                else
                {
                    if (node.Attributes != null && node.Attributes[MachineName] != null)
                    {
                        continue;
                    }
                }
                #endregion

                InternalLogger logger = new InternalLogger();
                if (node.Attributes[ThresholdAttribute] != null)
                {
                    LoggerSeverity threshold = (LoggerSeverity)Enum.Parse(typeof(LoggerSeverity), node.Attributes[ThresholdAttribute].Value, true);
                    logger.SetThreshold(threshold);
                }

                LoggerFactory.AddLogger(node.Name, logger);
                AssignLogger(logger, node);

                LoggerFactory.SetDefault(defaultLogger);
            }
        }

        private void AssignLogger(InternalLogger logger, XmlNode section)
        {
            foreach (XmlNode node in section.ChildNodes)
            {
                if (String.Compare(node.Name, AssembliesNodeName, true) == 0)
                {
                    foreach (XmlNode child in node.ChildNodes)
                    {
                        LoggerFactory.AddLogger(AssemblyUtilities.LoadAssembly(child.FirstChild.Value), logger);
                    }

                    continue;
                }

                if (String.Compare(node.Name, TypesNodeName, true) == 0)
                {
                    foreach (XmlNode child in node.ChildNodes)
                    {
                        LoggerFactory.AddLogger(AssemblyUtilities.Locate(child.FirstChild.Value), logger);
                    }

                    continue;
                }

                if (String.Compare(node.Name, LoggersNodeName, true) == 0)
                {
                    PopulateLogger(logger, node);
                }
            }
        }

        private void PopulateLogger(InternalLogger logger, XmlNode section)
        {
            foreach (XmlNode node in section.ChildNodes)
            {
                if (String.Compare(node.Name, Log4NetNodeName, true) == 0)
                {
                    Log4NetLogger tmp = new Log4NetLogger(node.FirstChild.Value);
                    logger.AddLogger(tmp);
                }
            }
        }
    }
}
