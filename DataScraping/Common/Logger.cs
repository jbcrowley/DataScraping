namespace SeleniumFramework.Common
{
    public class Logger
    {
        /// <summary>
        /// Writes the current NUnit log to file and and attaches it to the current NUnit TestContext.
        /// </summary>
        public static void AttachLog()
        {
            string fullFilePath = $"{Utils.GenerateLogPath()}.txt";
            // TODO: Write errors to log and attach to TestContext
            using (StreamWriter outputFile = new(fullFilePath))
            {
                outputFile.WriteLine("test");
            }
            TestContext.AddTestAttachment(fullFilePath);
        }

        /// <summary>
        /// Writes a message to the current test.
        /// </summary>
        /// <param name="message">The message to be written.</param>
        public static void Log(string message)
        {
            TestContext.Out.WriteLine(message);
        }
    }
}