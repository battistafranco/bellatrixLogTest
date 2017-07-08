using System;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;

namespace bellatrixLogTest
{
    public class JobLogger
    {
        private static bool _logToFile;
        private static bool _logToConsole;
        private static bool _logMessage;
        private static bool _logWarning;
        private static bool _logError;
        private static bool _logToDataBase;

        public JobLogger(bool logToFile, bool logToConsole,
            bool logToDataBase, bool logMessage, bool logWarning, bool logError)
        {
            _logError = logError;
            _logMessage = logMessage;
            _logWarning = logWarning;
            _logToDataBase = logToDataBase;
            _logToFile = logToFile;
            _logToConsole = logToConsole;
        }

        public void LogMessage(string message, bool isMessage, bool isWarning, bool isError)
        {
            string checkMessage = String.Empty;

            if (message != null)
            {
                checkMessage = message.Trim();
            }

            if (checkMessage == String.Empty || checkMessage.Length == 0)
            {
                return;
            }

            if (!_logToConsole && !_logToFile && !_logToDataBase)
            {
                throw new Exception("Invalid Configuration");
            }

            if ((!_logError && !_logMessage && !_logWarning) || (!isMessage && !isWarning && !isError))
            {
                throw new Exception("Error or Warning or Message must be specified");
            }

            try
            {

                int logType = 0;

                if (isMessage && _logMessage)
                {
                    logType = 1;
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else if (isError && _logError)
                {
                    logType = 2;
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                else if (isWarning && _logWarning)
                {
                    logType = 3;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                }

                if (_logToDataBase)
                    LogToDatabase(message, logType);             

                if (_logToFile)
                    LogToFile(message);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.StackTrace);
            }
        }

        private static void LogToDatabase(string message, int logType)
        {
            var conn = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            SqlConnection connection = new SqlConnection(conn);
            connection.Open();
            string query = String.Format("Insert into log Values('{0}','{1}')", message, logType.ToString()); //"Insert into log Values('" + message + "'," + logType.ToString() + ")";
            SqlCommand command = new SqlCommand(query, connection);
            command.ExecuteNonQuery();
        }

        private static void LogToFile(string message)
        {
            string logFilePath = String.Format("{0}LogFile{1}.txt", ConfigurationManager.AppSettings["LogFileDirectory"], DateTime.Now.ToString("yyyyMMdd"));
            string loggedText = string.Empty;
            if (!File.Exists(logFilePath))
            {
                File.Create(logFilePath).Close();
            }
            else if (File.Exists(logFilePath))
            {
                loggedText = System.IO.File.ReadAllText(logFilePath);
            }

            var newLine = String.Format("{0} - {1}", DateTime.Now.ToShortDateString(), message);

            StringBuilder builder = new StringBuilder();
            builder.AppendLine(loggedText);
            builder.AppendLine(newLine);

            File.WriteAllText(logFilePath, builder.ToString());
            Console.WriteLine(newLine);
        }
    }
}