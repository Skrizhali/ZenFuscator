using dnlib.DotNet;
using dnlib.DotNet.Emit;
using Noisette.Core.Interfaces;

namespace Noisette.Core.Stages
{
    /// <summary>
    /// Proxy stage contains metadata aswell since it proxies either true or false.
    /// </summary>
    internal class Proxy : IStage
    {
        public bool IsDetected { get; set; }

        public void Execute(IContext ctx)
        {
            ctx.Logger.Info("Searching Proxy Fixer.");

            foreach (var type in ctx.moduleDef.Types.ToList())
            {
                foreach (var method in type.Methods.ToList())
                {
                    if (method.HasBody)
                    {
                        var instructions = method.Body.Instructions;

                        for (int i = 0; i < instructions.Count; i++)
                        {
                            if (instructions[i].OpCode == OpCodes.Call && instructions[i].Operand is MethodDef)
                            {
                                MethodDef methodDef = (MethodDef)instructions[i].Operand;

                                switch (methodDef.Body.Instructions[1].OpCode.Code)
                                {
                                    case Code.Ldstr:
                                        instructions[i].OpCode = OpCodes.Ldstr;
                                        instructions[i].Operand = methodDef.Body.Instructions[1].Operand.ToString();
                                        IsDetected = true;
                                        ctx.Logger.Success($"Resolved: {methodDef.Name}");
                                        type.Methods.Remove(methodDef);
                                        break;

                                    case Code.Ldc_I4:
                                        instructions[i].OpCode = OpCodes.Ldc_I4;
                                        instructions[i].Operand = (int)methodDef.Body.Instructions[1].Operand;
                                        IsDetected = true;
                                        ctx.Logger.Success($"Resolved: {methodDef.Name}");
                                        type.Methods.Remove(methodDef);
                                        break;

                                    case Code.Ldc_I4_0:
                                        instructions[i].OpCode = OpCodes.Ldc_I4_0;
                                        Console.WriteLine(methodDef.Body.Instructions[1].Operand);
                                        IsDetected = true;
                                        ctx.Logger.Success($"Resolved: {methodDef.Name}");
                                        type.Methods.Remove(methodDef);
                                        break;
                                }
                            }
                        }
                    }
                }
            }

            if (!IsDetected)
                ctx.Logger.Error("None proxied values found.");
        }
    }
}