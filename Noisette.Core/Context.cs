using dnlib.DotNet;
using dnlib.DotNet.Writer;
using Noisette.Core.Interfaces;
using ILogger = Noisette.Core.Interfaces.ILogger;

namespace Noisette.Core
{
    internal class Context : IContext
    {
        public IOptions Options { get; }
        public ILogger Logger { get; set; }
        public AssemblyDef assemblyDef { get; set; }
        public TypeDef typeDef { get; set; }
        public ModuleDef moduleDef { get; set; }
        public ModuleDefMD moduleDefMD { get; set; }
        public MethodDef cctor { get; set; }

        public Context(IOptions options, ILogger logger)
        {
            Options = options;
            Logger = logger;
        }

        public bool ImportAssembly()
        {
            if (string.IsNullOrEmpty(Options.InputPath))
            {
                Logger.Warn("No valid assembly has been found.");
                return false;
            }

            try
            {
                assemblyDef = AssemblyDef.Load(Options.InputPath);
                moduleDef = assemblyDef.ManifestModule;
                moduleDefMD = ModuleDefMD.Load(Options.InputPath);

                Logger.Success($"Assembly \"{Options.InputFileName}\" has been loaded.");
                return true;
            }
            catch (Exception ex)
            {
                Logger.Success($"Assembly \"{Options.InputFileName}\" has failed to load.");
                return false;
            }
        }

        public void ExportAssembly()
        {
            try
            {
                ModuleWriterOptions saveOptions = new ModuleWriterOptions(assemblyDef.ManifestModule)
                {
                    Logger = DummyLogger.NoThrowInstance
                };

                assemblyDef.Write(Path.Combine(Options.OutputPath, Options.OutputFileName), saveOptions);

                Logger.Info($"Saved to: {Options.OutputPath}");
            }
            catch (Exception ex)
            {
                Logger.Error($"Error writing output file: {ex.Message}.");
            }
        }
    }
}