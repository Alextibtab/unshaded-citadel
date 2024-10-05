using UnshadedCitadel.Utils;

namespace UnshadedCitadel.Hooks;

public class RoomSettingsHook
{
    private Region Region;

    public void Apply()
    {
        On.RoomSettings.ctor += RoomSettingsCtor;
    }

    private void RoomSettingsCtor(
        On.RoomSettings.orig_ctor orig,
        RoomSettings self,
        string name,
        Region region,
        bool template,
        bool firstTemplate,
        SlugcatStats.Name playerChar
    )
    {
        Log.Info($"RoomSettingsConstructor called for {name}");
        orig(self, name, region, template, firstTemplate, playerChar);

        self.pal = 33;

        int fadeScreens = self.fadePalette.fades.Length;
        self.fadePalette = new RoomSettings.FadePalette(12, fadeScreens);
        for (int i = 0; i < fadeScreens; i++)
        {
            self.fadePalette.fades[i] = 0.15f;
        }
    }
}
