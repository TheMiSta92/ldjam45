using System;
using UnityEngine;

public abstract class AHealthSystem : MonoBehaviour {

    [SerializeField] protected float maxHealth = 5f;
    [SerializeField] protected float health;


    protected abstract void setEventListenerFor_onDamage();
    protected abstract void removeEventListener();
    protected abstract void onDeath();
    protected abstract void playDamageAnimation();
    protected abstract void refreshHealthGui(float health, float healthBefore);



    private void Start() {
        this.health = this.maxHealth;
        this.setEventListenerFor_onDamage();
    }


    protected void onDamage(float damage) {
        float healthBefore = this.health;
        this.health -= damage;
        this.refreshHealthGui(this.health, healthBefore);
        if (this.health <= 0f) {
            this.onDeath();
        } else {
            this.playDamageAnimation();
        }
    }



    public float GetHealth() {
        return this.health;
    }

    public float GetMaxHealth() {
        return this.maxHealth;
    }

    public void SetHealth(float health) {
        this.health = health;
        if (this.health > this.maxHealth) {
            this.health = this.maxHealth;
        }
    }

    public void SetMaxHealth(float maxHealth) {
        this.maxHealth = maxHealth;
        if (this.health > this.maxHealth) {
            this.health = this.maxHealth;
        }
    }

    private void OnDestroy() {
        this.removeEventListener();
    }

}