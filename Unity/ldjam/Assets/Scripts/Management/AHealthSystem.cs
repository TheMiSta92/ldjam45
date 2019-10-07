using System;
using UnityEngine;

public abstract class AHealthSystem : MonoBehaviour {

    [SerializeField] protected float maxHealth = 5f;
    protected float health;


    protected abstract void setEventListenerFor_onDamage();
    protected abstract void onDeath();



    private void Start() {
        this.health = this.maxHealth;
        this.setEventListenerFor_onDamage();
    }


    protected void onDamage(float damage) {
        this.health -= damage;
        if (this.health <= 0) {
            this.onDeath(); ;
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

}