namespace Noisette.Core.Interfaces
{
    internal interface ILogger
    {
        void Success(string message);

        void Error(string message);

        void Info(string message);

        void Warn(string message);

        void AsciiArt();
    }
}