using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MagicHooks {
    public class LogReader {
        private string path;
        private Encoding encoding = Encoding.UTF8;
        private StringBuilder data = new StringBuilder();
        public LogReader(string path) {
            this.path = path;
            StreamReader stream = new StreamReader(File.Open(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite));
            Task.Factory.StartNew(() => {
                while (true) {
                    string str = stream.ReadLine();
                    if(str == null) {
                        continue;
                    }
                    Console.WriteLine(str);
                    data.Append(str);
                }
            });
        }
        public void Read() {
            
        }
        public string Data {
            get {
                return data.ToString();
            }
        }
    }
}
