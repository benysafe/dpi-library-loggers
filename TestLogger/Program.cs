
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using InterfaceLibraryLogger;
using NLog;

namespace TestLogger
{
    class Program
    {
        public static IGenericLogger _genericLogger;
        public static Logger _logger;

        static void Main(string[] args)
        {
            string pathLibrary = @"E:\WORK\D_VISION\SRC\Release\netcoreapp3.1\NLogLogging";
            string logConfigPath = @"E:\WORK\D_VISION\SRC\LibraryLogger\TestLogger\logger.config";
            _genericLogger = (IGenericLogger)Assembly_Load_metho<IGenericLogger>(pathLibrary);
            _genericLogger.loadConfig(logConfigPath);
            _logger = (Logger)_genericLogger.init("TestLogger");

            Console.WriteLine("Test of logger process...");

            // logging
            _logger.Trace("Trace message");
            _logger.Debug("Debug message");
            _logger.Info("Info message");
            _logger.Warn("Warning message");
            _logger.Error("Error message");
            _logger.Fatal("Critical message");
            
            Console.ReadLine();
        }

        public static T Assembly_Load_metho<T>(string path)
        {
            Assembly miExtensionAssembly = Assembly.LoadFile(path);
            List<Type> types = miExtensionAssembly.GetExportedTypes().ToList();

            if (!types.Any(eleTypes => eleTypes.GetTypeInfo().GetInterfaces().ToList().Any(eleInter => eleInter.FullName.Equals(typeof(T).FullName))))
            {
                throw new Exception($"no se encontro implementacion de la interfas '{typeof(T).FullName}', en el ensamblado {path}");
            }

            string typeFullName = types.Find(eleTypes => eleTypes.GetTypeInfo().GetInterfaces().ToList().Any(eleInter => eleInter.FullName.Equals(typeof(T).FullName))).FullName;

            Type miExtensionType = miExtensionAssembly.GetType(typeFullName);
            object miExtensionObjeto = Activator.CreateInstance(miExtensionType);

            return (T)miExtensionObjeto;
        }
    }
}
