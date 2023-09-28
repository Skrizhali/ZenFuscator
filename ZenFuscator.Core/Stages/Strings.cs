using dnlib.DotNet.Emit;
using System.Text;
using ZenFuscator.Core.Interfaces;

namespace ZenFuscator.Core.Stages
{
    internal class Strings : IStage
    {
        public bool IsDetected { get; set; }

        public void Execute(IContext ctx)
        {
            ctx.Logger.Info("Searching Strings Decrypter.");

            foreach (var method in ctx.moduleDef.GetTypes().SelectMany(type => type.Methods).ToList())
            {
                if (method.Body is not null)
                {
                    var instructions = method.Body.Instructions;
                    var remInstructions = new List<Instruction>();

                    for (int i = 0; i < instructions.Count; i++)
                    {
                        var instruction = instructions[i];

                        if (instruction.Operand != null &&
                            instruction.Operand.ToString().Contains("System.Text.Encoding System.Text.Encoding::get_UTF8()") &&
                            i + 2 < instructions.Count && instructions[i + 2].Operand != null &&
                            instructions[i + 2].Operand.ToString().Contains("System.Byte[] System.Convert::FromBase64String(System.String)") &&
                            i + 3 < instructions.Count && instructions[i + 3].Operand != null &&
                            instructions[i + 3].Operand.ToString().Contains("System.String System.Text.Encoding::GetString(System.Byte[])"))
                        {
                            if (i + 1 < instructions.Count && instructions[i + 1].OpCode == OpCodes.Ldstr && instructions[i + 1].Operand is string base64String)
                            {
                                IsDetected = true;

                                var decryptedString = Encoding.UTF8.GetString(Convert.FromBase64String(base64String));
                                ctx.Logger.Success($"Decrypting: {base64String}");
                                instructions[i + 1].OpCode = OpCodes.Ldstr;
                                instructions[i + 1].Operand = decryptedString;
                                remInstructions.Add(instructions[i]);
                                remInstructions.Add(instructions[i + 2]);
                                remInstructions.Add(instructions[i + 3]);
                            }
                        }

                        foreach (var rem in remInstructions)
                        {
                            instructions.Remove(rem);
                        }
                    }
                }
            }

            if (!IsDetected)
                ctx.Logger.Error("No decryptable strings have been found.");
        }
    }
}