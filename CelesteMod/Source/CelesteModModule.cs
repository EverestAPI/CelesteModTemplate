using System;
using Microsoft.Xna.Framework;
#if Exports
using MonoMod.ModInterop;
#endif

namespace Celeste.Mod.CelesteMod;

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
        // TODO: apply any hooks that should always be active
    }

    public override void Unload() {
            // TODO: unapply any hooks applied in Load()
        }
}