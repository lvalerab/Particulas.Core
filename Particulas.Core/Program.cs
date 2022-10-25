using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Particulas.Core.Window;

namespace Particulas.Core
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //CreateHostBuilder(args).Build().Run();
            Ventana win = new Ventana(new OpenTK.Windowing.Desktop.GameWindowSettings { IsMultiThreaded = true, RenderFrequency = 60, UpdateFrequency = 60 }, new OpenTK.Windowing.Desktop.NativeWindowSettings { });
            win.Run();            
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
