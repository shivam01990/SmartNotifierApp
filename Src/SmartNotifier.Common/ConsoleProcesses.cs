using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartNotifier.Common
{
    public class ConsoleProcesses
    {
        public string ProcessName { get; set; }
        public string ActualProcessName { get; set; }
        public ProcessStatus Status { get; set; } = ProcessStatus.NA;

        public static List<ConsoleProcesses> GetConsoleProcesses()
        {

            return new List<ConsoleProcesses>()
            {
                new ConsoleProcesses(){ ProcessName= "CanBusSimulator", ActualProcessName="CanBusSimulator", Status= ProcessStatus.NA},
                new ConsoleProcesses(){ ProcessName= "CardiacServer", ActualProcessName="CardiacServer", Status= ProcessStatus.NA},
                new ConsoleProcesses(){ ProcessName= "ExamApplication", ActualProcessName="ExamApplication", Status= ProcessStatus.NA},
                new ConsoleProcesses(){ ProcessName= "GantryWCFSelfHostedServices", ActualProcessName="GantryWCFSelfHostedServices", Status= ProcessStatus.NA},
                new ConsoleProcesses(){ ProcessName= "ScannerFrameworkWinApp", ActualProcessName="ScannerFrameworkWinApp", Status= ProcessStatus.NA},
                new ConsoleProcesses(){ ProcessName= "ReconServer", ActualProcessName="ReconServer", Status= ProcessStatus.NA},
                new ConsoleProcesses(){ ProcessName= "ExamManager", ActualProcessName="ExamManager", Status= ProcessStatus.NA},
                new ConsoleProcesses(){ ProcessName= "DataAcquisitionBridge", ActualProcessName="DataAcquisitionBridge", Status= ProcessStatus.NA},
                new ConsoleProcesses(){ ProcessName= "DataProcessingBridge", ActualProcessName="DataProcessingBridge", Status= ProcessStatus.NA},
                new ConsoleProcesses(){ ProcessName= "Patient Directory", ActualProcessName="PatientDirectory", Status= ProcessStatus.NA},
                new ConsoleProcesses(){ ProcessName= "Workflow", ActualProcessName="Workflow", Status= ProcessStatus.NA},
                new ConsoleProcesses(){ ProcessName= "GantryServices", ActualProcessName="GantryServices", Status= ProcessStatus.NA},
                new ConsoleProcesses(){ ProcessName= "GantryLoggerServiceHost", ActualProcessName="GantryLoggerServiceHost", Status= ProcessStatus.NA},
                new ConsoleProcesses(){ ProcessName= "PmsCT Logger Service", ActualProcessName="LoggerSvc", Status= ProcessStatus.NA},
                new ConsoleProcesses(){ ProcessName= "PmsCT Messaging Service", ActualProcessName="MessageService", Status= ProcessStatus.NA},
                new ConsoleProcesses(){ ProcessName= "PmsCT Printing Service", ActualProcessName="PmsPrintingService", Status= ProcessStatus.NA},
                new ConsoleProcesses(){ ProcessName= "PmsCT Remoting Service", ActualProcessName="PmsRemotingService", Status= ProcessStatus.NA},
                new ConsoleProcesses(){ ProcessName= "SQL Server (SQLEXPRESS)", ActualProcessName="sqlservr", Status= ProcessStatus.NA},
                new ConsoleProcesses(){ ProcessName= "SQL Server VSS Writer", ActualProcessName="sqlwriter", Status= ProcessStatus.NA},
            };
        }

        public static ProcessStatus GetProcessStatus(string processName, Process[] processes)
        {
            //Iterate to every process to check if it is our required process
            var process = processes.FirstOrDefault(x => x.ProcessName == processName);
            if (process != null)
            {
                if (process.Responding)
                {
                    return ProcessStatus.Running;
                }
                else
                {
                    return ProcessStatus.NotResponding;
                }
            }
            else
            {
                return ProcessStatus.Stopped;
            }
        }
    }
}
