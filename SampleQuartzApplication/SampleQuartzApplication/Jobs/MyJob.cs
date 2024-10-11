using Data.Misc;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleQuartzApplication.Jobs
{
    internal class MyJob:IJob
    {
        private readonly LogManager logManager = LogManager.Instance;

        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                await Console.Out.WriteLineAsync($"MyJob Started!!!");
                logManager.createLog(LogType.INFO, $"Initializing MyJob");
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync($"MyJob Started e!!!");
                logManager.createLog(LogType.ERROR, $"Error occured while initializing MyJob: {ex.Message}");
            }
        }
    }
}
