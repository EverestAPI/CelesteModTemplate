using System;
using Microsoft.Xna.Framework;
#if Exports
using MonoMod.ModInterop;
#endif

namespace Celeste.Mod.CelesteMod {
    public class CelesteModModule : EverestModule {
        public static CelesteModModule Instance { get; private set; }

#if Settings
        public override Type SettingsType => typeof(CelesteModModuleSettings);
        public static CelesteModModuleSettings Settings => (CelesteModModuleSettings) Instance._Settings;

#endif
#if Session
        public override Type SessionType => typeof(CelesteModModuleSession);
        public static CelesteModModuleSession Session => (CelesteModModuleSession) Instance._Session;

#endif
#if SaveData
        public override Type SaveDataType => typeof(CelesteModModuleSaveData);
        public static CelesteModModuleSaveData SaveData => (CelesteModModuleSaveData) Instance._SaveData;

#endif
        public CelesteModModule() {
            Instance = this;
#if Logging
//-:cnd:noEmit
#if DEBUG
            // debug builds use verbose logging
            Logger.SetLogLevel(nameof(CelesteModModule), LogLevel.Verbose);
#else
            // release builds use info logging to reduce spam in log files
            Logger.SetLogLevel(nameof(CelesteModModule), LogLevel.Info);
#endif
//+:cnd:noEmit
#endif
        }

        public override void Load() {
#if Exports
            typeof(CelesteModExports).ModInterop(); // TODO: delete this line if you do not need to export any functions

#endif
#if Hooks
            On.Celeste.LevelLoader.ctor += LevelLoader_ctor;
            On.Celeste.OverworldLoader.ctor += OverworldLoader_ctor;

#endif
            // TODO: apply any hooks that should always be active
        }

        public override void Unload() {
#if Hooks
            On.Celeste.LevelLoader.ctor -= LevelLoader_ctor;
            On.Celeste.OverworldLoader.ctor -= OverworldLoader_ctor;

#endif
            // TODO: unapply any hooks applied in Load()
        }
#if Hooks

        public void LoadBeforeLevel() {
#if Core
            Everest.Events.AssetReload.OnAfterReload += AssetReload_OnAfterReload;
#else
            On.Celeste.Mod.AssetReloadHelper.ReloadLevel += AssetReloadHelper_ReloadLevel;
#endif

            // TODO: apply any hooks that should only be active while a level is loaded
        }

        public void UnloadAfterLevel() {
#if Core
            Everest.Events.AssetReload.OnAfterReload -= AssetReload_OnAfterReload;
#else
            On.Celeste.Mod.AssetReloadHelper.ReloadLevel -= AssetReloadHelper_ReloadLevel;
#endif

            // TODO: unapply any hooks applied in LoadBeforeLevel()
        }

#if Core
        private void AssetReload_OnAfterReload(bool silent) {
#else
        private void AssetReloadHelper_ReloadLevel(On.Celeste.Mod.AssetReloadHelper.orig_ReloadLevel orig) {
            orig();

#endif
            // TODO: anything that should happen after assets are reloaded with F5
        }

        private void OverworldLoader_ctor(On.Celeste.OverworldLoader.orig_ctor orig, OverworldLoader self, Overworld.StartMode startmode, HiresSnow snow) {
            orig(self, startmode, snow);
            if (startmode != (Overworld.StartMode) (-1)) {
                UnloadAfterLevel();
            }
        }

        private void LevelLoader_ctor(On.Celeste.LevelLoader.orig_ctor orig, LevelLoader self, Session session, Vector2? startposition) {
            orig(self, session, startposition);
            LoadBeforeLevel();
        }
#endif
    }
}
