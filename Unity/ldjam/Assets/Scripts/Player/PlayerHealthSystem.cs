using System;
using UnityEngine;

[AddComponentMenu("LDJAM/Fights/Player Health System")]
public class PlayerHealthSystem : AHealthSystem {

    protected override void setEventListenerFor_onDamage() {
        sGameEventManager.Access().OnCharacterHurt += this.onDamage;
    }

    protected override void removeEventListener() {
        sGameEventManager.Access(true).OnCharacterHurt -= this.onDamage;
    }

    protected override void onDeath() {
        sGameEventManager.Access().Trigger_GameOver();
        this.gameObject.GetComponent<PlayerAnimation>().PlayDeathAnimation();
    }

    protected override void playDamageAnimation() {
        this.gameObject.GetComponent<PlayerAnimation>().PlayDamageAnimation();
    }

    protected override void refreshHealthGui(float health, float before) {
        sHealthGui.Access().SetHealthPlayer(health, before);
    }

}