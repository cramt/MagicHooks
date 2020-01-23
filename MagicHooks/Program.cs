using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Permissions;

namespace MagicHooks {
    class Program {
        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        static void Main(string[] args) {
            const string path = @"C:\Users\madsc\AppData\LocalLow\Wizards Of The Coast\MTGA\output_log.txt";
            File.ReadAllLines(path).ToList().ForEach(x => {
                JObject j = null;
                try {
                    j = JObject.Parse(x);
                }
                catch (JsonReaderException) {
                    return;
                }
                List<int> hps = new List<int>();
                j.SelectToken("greToClientEvent")?.SelectToken("greToClientMessages")?.Values<JObject>().ToList().ForEach(y => {
                    int? i = y.SelectToken("gameStateMessage")?.SelectToken("players")?.Values<JObject>().FirstOrDefault(z => z.SelectToken("mulliganCount") != null)?.SelectToken("lifeTotal")?.Value<int>();
                    if (i.HasValue) {
                        hps.Add(i.Value);
                    }
                });
                hps.ForEach(y => Console.WriteLine(y));
            });
            Console.WriteLine("done");
            /*
            LogReader reader = null;
            
            
            if (File.Exists(path)) {
                reader = new LogReader(path);
            }
            using (FileSystemWatcher watcher = new FileSystemWatcher()) {
                watcher.Path = new FileInfo(path).DirectoryName;

                // Watch for changes in LastAccess and LastWrite times, and
                // the renaming of files or directories.
                watcher.NotifyFilter = NotifyFilters.LastAccess
                                     | NotifyFilters.LastWrite
                                     | NotifyFilters.FileName
                                     | NotifyFilters.DirectoryName;

                // Only watch text files.
                watcher.Filter = new FileInfo(path).Name;

                // Add event handlers.
                watcher.Changed += (object source, FileSystemEventArgs e) => {
                    if(e.ChangeType == WatcherChangeTypes.Changed) {
                        reader?.Read();
                    }
                };
                watcher.Created += (object source, FileSystemEventArgs e) => {
                    reader = new LogReader(path);
                };
                watcher.Deleted += (object source, FileSystemEventArgs e) => {
                    reader = null;
                };
                watcher.Renamed += (object source, RenamedEventArgs e) => {
                    if (e.FullPath == path) {
                        reader = new LogReader(path);
                    }
                };

                // Begin watching.
                watcher.EnableRaisingEvents = true;

                // Wait for the user to quit the program.
                Console.WriteLine("Press 'q' to quit the sample.");
                while (Console.Read() != 'q') ;
            }
            */
        }
    }
}
