using System;

namespace bellatrixLogTest
{
    class Program
    {
        static void Main(string[] args)
        {
            bool logToFile = true;
            bool logToConsole = true;
            bool logToDatabase = true;
            bool logMessage = false;
            bool logWarning = true;
            bool logError = false;
            string message = "Mensaje de prueba - Bellatrix Test";
            bool isMessage = false;
            bool isWarning = true;
            bool isError = false;


            JobLogger log = new JobLogger(logToFile, logToConsole, logToDatabase, logMessage, logWarning, logError);
            log.LogMessage(message, isMessage, isWarning, isError);
            Console.ReadKey();
        }
    }
}
