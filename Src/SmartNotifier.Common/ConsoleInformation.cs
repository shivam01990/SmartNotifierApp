using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartNotifier.Common
{
    public class ConsoleInformation
    {
        public string Scanner { get; set; } = "NA";
        public string ScannerVersion { get; set; } = "NA";
        public string Modality { get; set; } = "NA";
        public string Model { get; set; } = "NA";
        public string CouchType { get; set; } = "NA";

        public static string GetMachineUniqueDescriptionFilePath()
        {
            return @"C:\Pms\Config\ScannerFramework\System\MachineUniqueDescription.cfg";
        }

        public static string GetSystemFilePath()
        {
            return @"C:\Pms\Config\ScannerFramework\System\System.cfg";
        }
    }
}
