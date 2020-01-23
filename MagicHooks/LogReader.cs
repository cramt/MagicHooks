using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MagicHooks {
    public class LogReader {
        private string path;
        private StringBuilder data = new StringBuilder();
        private EventHandler<string> onLogLine;
        public LogReader(string path) {
            this.path = path;
            StreamReader stream = new StreamReader(File.Open(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite));
            Task.Factory.StartNew(() => {
                while (true) {
                    string str = stream.ReadLine();
                    if (str == null) {
                        continue;
                    }
                    onLogLine?.Invoke(this, str);
                }
            });
        }
    }
}
