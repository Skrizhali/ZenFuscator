using ZenFuscator.Core.Interfaces;

namespace ZenFuscator.Core.Stages
{
    internal class Attributes : IStage
    {
        public bool IsDetected { get; set; }

        public void Execute(IContext ctx)
        {
            ctx.Logger.Info("Searching Attributes Remover.");

            foreach (var type in ctx.moduleDef.Types.ToList())
            {
                if (!type.Fields.Any() && !type.Methods.Any(method => !method.IsStaticConstructor))
                {
                    if (type != ctx.moduleDef.GlobalType)
                    {
                        IsDetected = true;
                        ctx.moduleDef.Types.Remove(type);
                        ctx.Logger.Success($"Removed attribute: {type.Name}");
                    }
                }
            }

            if (!IsDetected)
                ctx.Logger.Error("None invalid attributes found.");
        }
    }
}