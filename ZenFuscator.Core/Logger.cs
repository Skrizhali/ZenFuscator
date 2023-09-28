using System.Drawing;
using ZenFuscator.Core.Interfaces;
using Console = Colorful.Console;

namespace ZenFuscator.Core
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
                "ZenFuscator Unpacker by Team Skrizhali",
                ""
            };

            Array.ForEach(asciiArt, asciiLine =>
                Console.WriteLine(asciiLine.PadLeft((Console.WindowWidth - asciiLine.Length) / 2 + asciiLine.Length), Color.White));
        }

        private void LogWithColor(string message, Color textColor)
        {
            Colorful.Console.Write("[", textColor);
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