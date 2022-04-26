using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

Host.CreateDefaultBuilder (args)
    .UseSystemd()
    .ConfigureLogging((hostContext, builder) => {
        // if we're in fact using systemd, throw out the default console logger and only use the systemd journald
        if (Microsoft.Extensions.Hosting.Systemd.SystemdHelpers.IsSystemdService()) {
            builder.ClearProviders();
            builder.AddJournal(options => options.SyslogIdentifier = hostContext.Configuration["SyslogIdentifier"]);
        }
    })
    .ConfigureWebHostDefaults (webBuilder => {
        webBuilder.UseStartup<FuGetGallery.Startup> ();
    })
    .Build ()
    .Run ();
