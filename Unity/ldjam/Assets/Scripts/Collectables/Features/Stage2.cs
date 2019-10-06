using UnityEngine;

[AddComponentMenu("LDJAM/Collectables/Features/Stage 2")]
public class Stage2 : ACollectable {

    protected override void applyEffect() {
        sGameEventManager.Access().Trigger_StageSwitch(2);
    }

    protected override void undoEffect() {
        sGameEventManager.Access().Trigger_StageSwitch(1);
    }

}