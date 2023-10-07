using Noisette.Core.Interfaces;
using Console = Colorful.Console;
using System.Drawing;

namespace Noisette.Core
{
    internal class Logger : ILogger
    {
        public void AsciiArt()
        {
            string[] asciiArt =
            {
                "",
                "███████╗██╗  ██╗██████╗ ██╗███████╗██╗  ██╗ █████╗ ██╗     ██╗",
                "██╔════╝██║ ██╔╝██╔══██╗██║╚══███╔╝██║  ██║██╔══██╗██║     ██║",
                "███████╗█████╔╝ ██████╔╝██║  ███╔╝ ███████║███████║██║     ██║",
                "╚════██║██╔═██╗ ██╔══██╗██║ ███╔╝  ██╔══██║██╔══██║██║     ██║",
                "███████║██║  ██╗██║  ██║██║███████╗██║  ██║██║  ██║███████╗██║",
                "╚══════╝╚═╝  ╚═╝╚═╝  ╚═╝╚═╝╚══════╝╚═╝  ╚═╝╚═╝  ╚═╝╚══════╝╚═╝",
                "Noisette Unpacker by Team Skrizhali",
                ""
            };

            Array.ForEach(asciiArt, asciiLine =>
                Console.WriteLine(asciiLine.PadLeft((Console.WindowWidth - asciiLine.Length) / 2 + asciiLine.Length), Color.White));
        }

        private void LogWithColor(string message, Color textColor)
        {
            Console.Write("[", textColor);
            Console.Write(DateTime.Now.ToString("HH:mm:ss"), Color.White);
            Console.Write("] ", textColor);
            Console.WriteLine(message, textColor);
        }

        public void Error(string message) => LogWithColor(message, Color.OrangeRed);

        public void Info(string message) => LogWithColor(message, Color.Aqua);

        public void Success(string message) => LogWithColor(message, Color.LimeGreen);

        public void Warn(string message) => LogWithColor(message, Color.DarkOrange);
    }
}