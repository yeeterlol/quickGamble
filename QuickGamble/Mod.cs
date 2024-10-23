using GDWeave;

namespace QuickGamble;

public class Mod : IMod {
    public Config Config;

    public Mod(IModInterface modInterface) {
        this.Config = modInterface.ReadConfig<Config>();
        modInterface.Logger.Information("QuickGamble has loaded!");
		modInterface.RegisterScriptMod(new QuickGamble());

    }

    public void Dispose() {
        // Cleanup anything you do here
    }
}
