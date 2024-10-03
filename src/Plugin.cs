using System;
using BepInEx;

namespace UnshadedCitadel;

[BepInPlugin(PluginGUID, PluginName, PluginVersion)]
public partial class UnshadedCitadel : BaseUnityPlugin
{
    public const string PluginGUID = "alextabitha.unshadedcitadel";
    public const string PluginName = "Unshaded Citadel";
    public const string PluginVersion = "1.0.1";

    private UnshadedCitadelOptions Options;
    private bool IsInit;

    public UnshadedCitadel()
    {
        try
        {
            Options = new UnshadedCitadelOptions(this, Logger);
        }
        catch (Exception e)
        {
            Logger.LogError(e);
            throw;
        }
    }

    private void OnEnable()
    {
        On.RainWorld.OnModsInit += ModInit;
    }

    private void ModInit(On.RainWorld.orig_OnModsInit orig, RainWorld self)
    {
        orig(self);
        try
        {
            if (IsInit)
                return;

            On.RainWorldGame.ShutDownProcess += ModShutdown;
            On.GameSession.ctor += GameSessionOnctor;

            MachineConnector.SetRegisteredOI(PluginGUID, Options);
            IsInit = true;
        }
        catch (Exception e)
        {
            Logger.LogError(e);
            throw;
        }
    }

    private void ModShutdown(On.RainWorldGame.orig_ShutDownProcess orig, RainWorldGame self)
    {
        orig(self);
    }

    private void GameSessionOnctor(
        On.GameSession.orig_ctor orig,
        GameSession self,
        RainWorldGame game
    )
    {
        orig(self, game);
    }
}
