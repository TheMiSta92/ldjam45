
using UnityEngine;

[AddComponentMenu("LDJAM/Fights/Player Health System")]
public class BossHealthSystem : AHealthSystem {

    protected override void setEventListenerFor_onDamage() {
        sGameEventManager.Access().OnBossHit += this.onDamage;
    }

    protected override void onDeath() {
        sGameEventManager.Access().Trigger_BossKilled();
    }

}