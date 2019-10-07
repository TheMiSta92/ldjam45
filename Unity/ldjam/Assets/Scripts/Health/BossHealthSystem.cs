
using UnityEngine;

[AddComponentMenu("LDJAM/Fights/Player Health System")]
public class BossHealthSystem : AHealthSystem {

    protected override void setEventListenerFor_onDamage() {
        sGameEventManager.Access().OnBossHit += this.onDamage;
    }

    protected override void removeEventListener() {
        sGameEventManager.Access(true).OnBossHit -= this.onDamage;
    }

    protected override void onDeath() {
        sGameEventManager.Access().Trigger_BossKilled();
    }

    protected override void playDamageAnimation() {
        // do nothing (handled in sBoss1)
    }

    protected override void refreshHealthGui(float health, float before) {
        sHealthGui.Access().SetHealthBoss(health, before);
    }

}