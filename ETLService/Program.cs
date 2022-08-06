using System;
using System.IO;
using System.Collections.Generic;
using ETLService.Extractors.Readers;
using ETLService.Models;
using ETLService.Converters;
using ETLService.Transformers;
using ETLService.Loaders.Writers;
using ETLService.EtlServices;
using Microsoft.Extensions.Configuration;
using System.Globalization;
using ETLService.Metalog;

namespace ETLService
{
    class Program
    {
        private static EtlService _service;
        static void Main(string[] args)
        {
            string configPath = "config.json";
            if(!File.Exists(configPath))
                Console.WriteLine("Configuration file is missing");
            else
            {
                IConfigurationBuilder builder = new ConfigurationBuilder();
                builder.AddJsonFile(configPath, true, true);
                IConfiguration config = builder.Build();
                using var watcher = new FileSystemWatcher(config["inputfolder"]);
                watcher.Filters.Add("*.txt");
                watcher.Filters.Add("*.csv");
                watcher.Changed += new FileSystemEventHandler(OnChanged);
                FileService fileService = new FileService(config["outputfolder"], config["inputfolder"], config["donefolder"]);
                _service = new EtlService(fileService);
                Console.WriteLine("-------------------------------");
                Console.WriteLine("Click to get started: Y+Enter;");
                Console.WriteLine("Click to finish: N+Enter;");
                Console.WriteLine("-------------------------------");
                if (Console.Read() == 'Y')
                {
                    do
                    {
                        _service.Run();
                        watcher.EnableRaisingEvents = true;
                    } while (Console.Read() != 'N');
                }
                else Console.Clear();
            }
        }

        private static void OnChanged(object source, FileSystemEventArgs e)
        {
            _service.Run();
        }
    }
}