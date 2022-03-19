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
        public CelesteModModule() {
            Instance = this;
#if ReducedLogging
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
#if HookHelpers
            On.Celeste.LevelLoader.ctor += LevelLoader_ctor;
            On.Celeste.OverworldLoader.ctor += OverworldLoader_ctor;

#endif
            // TODO: apply any hooks that should always be active
        }

        public override void Unload() {
#if HookHelpers
            On.Celeste.LevelLoader.ctor -= LevelLoader_ctor;
            On.Celeste.OverworldLoader.ctor -= OverworldLoader_ctor;

#endif
            // TODO: unapply any hooks applied in Load()
        }
#if HookHelpers

        public void LoadBeforeLevel() {
            On.Celeste.Mod.AssetReloadHelper.ReloadLevel += AssetReloadHelper_ReloadLevel;

            // TODO: apply any hooks that should only be active while a level is loaded
        }

        public void UnloadAfterLevel() {
            On.Celeste.Mod.AssetReloadHelper.ReloadLevel -= AssetReloadHelper_ReloadLevel;

            // TODO: unapply any hooks applied in LoadBeforeLevel()
        }

        private void AssetReloadHelper_ReloadLevel(On.Celeste.Mod.AssetReloadHelper.orig_ReloadLevel orig) {
            orig();

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