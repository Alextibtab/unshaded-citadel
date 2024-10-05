using System;
using System.Reflection;
using System.Security.Permissions;
using BepInEx;
using UnshadedCitadel.Utils;

#pragma warning disable CS0618
[assembly: SecurityPermission(SecurityAction.RequestMinimum, SkipVerification = true)]


#pragma warning restore CS0618


namespace UnshadedCitadel;

[BepInPlugin(PluginGUID, PluginName, PluginVersion)]
public class UnshadedCitadel : BaseUnityPlugin
{
    public const string PluginGUID = "alextabitha.unshadedcitadel";
    public const string PluginName = "Unshaded Citadel";
    public const string PluginVersion = "2.0.0";

    private UnshadedCitadelOptions Options;

    private bool IsInit;

    public UnshadedCitadel()
    {
        try
        {
            Log.Init(Logger);
            Options = new UnshadedCitadelOptions(this);
        }
        catch (Exception e)
        {
            Log.Error(e);
            throw;
        }
    }

    private void OnEnable()
    {
        On.RainWorld.OnModsInit += ModInit;
    }

    private void ApplyAllHooks()
    {
        try
        {
            Type[] types = Assembly.GetExecutingAssembly().GetTypes();
            foreach (Type type in types)
            {
                if (
                    typeof(Hooks.IHook).IsAssignableFrom(type)
                    && !type.IsInterface
                    && !type.IsAbstract
                )
                {
                    Log.Info($"Applying hook: {type.Name}");
                    var hook = Activator.CreateInstance(type) as Hooks.IHook;
                    hook?.Apply();
                    Log.Info($"Hook applied: {type.Name}");
                }
            }
        }
        catch (Exception e)
        {
            Log.Error($"Error in ApplyAllHooks: {e.Message}\n{e.StackTrace}");
        }
    }

    private void ModInit(On.RainWorld.orig_OnModsInit orig, RainWorld self)
    {
        orig(self);
        try
        {
            if (IsInit)
                return;

            ApplyAllHooks();

            On.RainWorldGame.ShutDownProcess += ModShutdown;

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
}
