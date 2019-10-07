using System;
using UnityEngine;

[AddComponentMenu("LDJAM/Fights/Player Health System")]
public class PlayerHealthSystem : AHealthSystem {

    protected override void setEventListenerFor_onDamage() {
        sGameEventManager.Access().OnCharacterHurt += this.onDamage;
    }

    protected override void onDeath() {
        sGameEventManager.Access().Trigger_GameOver();
    }

}