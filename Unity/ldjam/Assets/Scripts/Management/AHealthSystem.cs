using System;
using UnityEngine;

public abstract class AHealthSystem : MonoBehaviour {

    [SerializeField] protected float maxHealth = 5f;
    [SerializeField] protected float health;


    protected abstract void setEventListenerFor_onDamage();
    protected abstract void removeEventListener();
    protected abstract void onDeath();
    protected abstract void playDamageAnimation();



    private void Start() {
        this.health = this.maxHealth;
        this.setEventListenerFor_onDamage();
    }


    protected void onDamage(float damage) {
        this.health -= damage;
        if (this.health <= 0f) {
            this.onDeath();
        } else {
            this.playDamageAnimation();
        }
    }



    public float GetHealth() {
        return this.health;
    }

    public void SetHealth(float health) {
        this.health = health;
        if (this.health > this.maxHealth) {
            this.health = this.maxHealth;
        }
    }

    private void OnDestroy() {
        this.removeEventListener();
    }

}