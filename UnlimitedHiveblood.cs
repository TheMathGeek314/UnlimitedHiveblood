using Modding;
using System.Collections.Generic;
using UnityEngine;
using HutongGames.PlayMaker;
using Satchel;

namespace UnlimitedHiveblood {
    public class UnlimitedHiveblood: Mod {
        new public string GetName() => "UnlimitedHiveblood";
        public override string GetVersion() => "1.0.0.2";
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
            if(pd.royalCharmState >= 3 && pd.equippedCharm_36 && pd.equippedCharm_29 && pd.health < pd.maxHealth) {
                base.Fsm.Event("HIVE SYNERGY");
            }
        }
    }
}