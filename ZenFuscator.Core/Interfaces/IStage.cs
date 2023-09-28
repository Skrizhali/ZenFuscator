namespace ZenFuscator.Core.Interfaces
{
    internal interface IStage
    {
        bool IsDetected { get; }

        void Execute(IContext context);
    }
}