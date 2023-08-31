using InterfaceLibraryLogger;
using NLog;
namespace NLogLogger
{
    public class NLogLogger : IGenericLogger
    {
        private LogFactory _loggerFactory = new LogFactory();
        public object init(string loggerName = null)
        {
            try
            {
                Logger _logger = _loggerFactory.GetLogger(loggerName);
                return (object)_logger;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void loadConfig(string logConfigPath)
        {
            try
            {
                _loggerFactory = LogManager.LoadConfiguration(logConfigPath);
            }
            catch (Exception ex)
            {
                throw new Exception($"No fue posible cargar el fichero de configuracion {logConfigPath}");
            }
        }
    }
}