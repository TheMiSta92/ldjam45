using UnityEngine;

public class HealthUp : ACollectable {

    protected override bool canBeCollected() {
        PlayerHealthSystem health = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealthSystem>();
        return health.GetHealth() < health.GetMaxHealth();
    }

    protected override void applyEffect() {
        PlayerHealthSystem health = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealthSystem>();
        sHealthGui.Access().SetHealthPlayer(health.GetHealth() + 1f, health.GetHealth());
    }

    protected override void undoEffect() {
        // nothing
    }

}