using Amazon;
using Amazon.CloudWatchLogs;
using Amazon.CloudWatchLogs.Model;
using Amazon.Runtime;
using System.Configuration;

namespace Commons.Helpers
{
    public class Logger
    {
        private IAmazonCloudWatchLogs _cliente;
        private string _grupoLog;
        private string _nombreLogStream;
        private string _siguienteToken = null;

        private Logger(string logGroup, IConfiguration configuration)
        {
            _cliente = new AmazonCloudWatchLogsClient(configuration.GetValue<string>("accessKeyCloudWatch"),
                                                      configuration.GetValue<string>("secretKeyCloudWatch"), RegionEndpoint.USEast1);
            _grupoLog = logGroup;
        }

        public static async Task<Logger> InstanciarLoggerAsync(string grupoLog, IConfiguration configuration)
        {
            var logger = new Logger(grupoLog, configuration);
            await logger.CrearGrupoLogsAsync();
            await logger.CrearRegistrosStreamAsync();
            return logger;
        }

        private async Task CrearGrupoLogsAsync()
        {

            var obtieneGrupo = await _cliente.DescribeLogGroupsAsync(new DescribeLogGroupsRequest() { LogGroupNamePrefix = _grupoLog });
            var logGroupExists = obtieneGrupo.LogGroups.Any(l => l.LogGroupName == _grupoLog);
            if (!logGroupExists)
                await _cliente.CreateLogGroupAsync(new CreateLogGroupRequest(_grupoLog));
        }

        private async Task CrearRegistrosStreamAsync()
        {
            _nombreLogStream = $"{DateTime.UtcNow:yyyyMMddHHmmss} - {Guid.NewGuid()}";
            _ = await _cliente.CreateLogStreamAsync(new CreateLogStreamRequest()
            {
                LogGroupName = _grupoLog,
                LogStreamName = _nombreLogStream
            });
        }

        public async Task agregarLogAsync(string mensaje)
        {
            var respuestaMensaje = await _cliente.PutLogEventsAsync(new PutLogEventsRequest()
            {
                LogGroupName = _grupoLog,
                LogStreamName = _nombreLogStream,
                SequenceToken = _siguienteToken,
                LogEvents = new List<InputLogEvent>()
                {
                    new InputLogEvent()
                    {
                        Message = mensaje,
                        Timestamp = DateTime.UtcNow
                    }
                }
            });
            _siguienteToken = respuestaMensaje.NextSequenceToken;
        }
    }
}
