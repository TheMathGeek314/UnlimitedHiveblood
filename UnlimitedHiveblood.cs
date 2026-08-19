using Modding;
using System.Collections.Generic;
using UnityEngine;
using HutongGames.PlayMaker;
using Satchel;

namespace UnlimitedHiveblood {
    public class UnlimitedHiveblood: Mod {
        new public string GetName() => "UnlimitedHiveblood";
        public override string GetVersion() => "1.0.0.3";
        public override void Initialize(Dictionary<string, Dictionary<string, GameObject>> preloadedObjects) {
            On.PlayMakerFSM.OnEnable += editFSM;
        }

        private void editFSM(On.PlayMakerFSM.orig_OnEnable orig, PlayMakerFSM self) {
            orig(self);
            if(self.gameObject.name == "Health" && self.FsmName == "Hive Health Regen") {
                whiteHiveSynergy whiteHiveAction = new();
                FsmState idleState = self.GetValidState("Idle");
                idleState.AddAction(whiteHiveAction);
                idleState.AddTransition("HIVE SYNERGY", "Start Recovery");
            }
        }
    }

    public class whiteHiveSynergy: FsmStateAction {
        public override void OnEnter() {
            PlayerData pd = PlayerData.instance;
            int rcs = pd.GetInt(nameof(PlayerData.royalCharmState));
            bool e36 = pd.GetBool(nameof(PlayerData.equippedCharm_36));
            bool e29 = pd.GetBool(nameof(PlayerData.equippedCharm_29));
            int h = pd.GetInt(nameof(PlayerData.health));
            int mh = pd.GetInt(nameof(PlayerData.maxHealth));
            if(rcs >= 3 && e36 && e29 && h < mh) {
                base.Fsm.Event("HIVE SYNERGY");
            }
        }
    }
}