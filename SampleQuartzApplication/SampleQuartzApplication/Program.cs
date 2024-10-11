// See https://aka.ms/new-console-template for more information

using Data.Misc;
using SampleQuartzApplication;
using System.Configuration;
using Topshelf;
using Topshelf.Logging;

var exitCode = HostFactory.Run(hostConfigurator =>
{
    hostConfigurator.Service<Service>(serviceConfigurator =>
    {
        serviceConfigurator.ConstructUsing(service => new Service());
        serviceConfigurator.WhenStarted(service => service.Start());
        serviceConfigurator.WhenStopped(service => service.Stop());
    });

    hostConfigurator.SetInstanceName("SampleQuartzApplication");

    hostConfigurator.SetServiceName("SampleQuartzApplication");
    hostConfigurator.SetDisplayName("Sample Quartz Service");
    hostConfigurator.SetDescription("This is a Sample Quartz Application");

    hostConfigurator.RunAsLocalSystem();

    //hostConfigurator.DependsOn("SomeOtherService"); // For Manual
    //hostConfigurator.DependsOnMsSql(); // Microsoft SQL Server
    //hostConfigurator.DependsOnEventLog(); // Windows Event Log

    hostConfigurator.StartAutomaticallyDelayed();

    hostConfigurator.EnableServiceRecovery(r =>
    {
        // Has no corresponding setting in the Recovery dialogue.
        // OnCrashOnly means the service will not restart if the application returns
        // a non-zero exit code.  By convention, an exit code of zero means ‘success’.
        r.OnCrashOnly();
        // Corresponds to ‘First failure: Restart the Service’
        // Note: 0 minutes delay means restart immediately
        r.RestartService(0);
        // Corresponds to ‘Second failure: Restart the Service’
        // Note: TopShelf will configure a 1-minute delay before this restart, but the
        // Recovery dialogue only shows the first restart delay (0 minutes)
        r.RestartService(1);
        // Corresponds to ‘Subsequent failures: Restart the Service’
        r.RestartService(2);
        // Corresponds to ‘Reset fail count after: 1 days’
        r.SetResetPeriod(1);
    });

    hostConfigurator.EnableShutdown();

    hostConfigurator.OnException((ex) =>
    {
        HostLogger.Get<Service>().Info("Exception occured Main : " + ex.ToString());
    });
});

int exitCodeValue = (int)Convert.ChangeType(exitCode, exitCode.GetTypeCode());

Environment.ExitCode = exitCodeValue;