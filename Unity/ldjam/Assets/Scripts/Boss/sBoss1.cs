﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("LDJAM/Fights/Boss 1")]
public class sBoss1 : MonoBehaviour {

    protected static sBoss1 singleton;



    private void Awake() {
        sBoss1.singleton = this;
    }

    public static sBoss1 Access() {
        if (sBoss1.singleton == null) throw new System.Exception("Boss 1 singleton wasn't instanced, add it to the Singleton-GO!");
        return sBoss1.singleton;
    }



    protected Animator animator;
    [Header("SFX")]
    [SerializeField] protected AudioClip dash;
    [SerializeField] protected AudioClip attack;
    [SerializeField] protected AudioClip damage;
    [SerializeField] protected AudioClip death;
    [Header("Ambient")]
    [SerializeField] protected AudioClip cave;
    [SerializeField] protected AudioClip fight;
    [SerializeField] protected AudioClip normal;
    [Header("Dialogues")]
    [SerializeField] protected GameObject dialogueStart;
    [SerializeField] protected GameObject dialogueDeath;
    [Header("Misc")]
    [SerializeField] protected GameObject newSpawnpoint;
    [SerializeField] protected GameObject prefabDamageScript;
    [SerializeField] protected GameObject entryTrigger;
    [SerializeField] protected GameObject blockSkip;

    protected AudioSource sfx;
    protected AudioSource ambient;

    protected bool alive = true;
    protected bool facingRight = false;
    protected bool idle = true;

    protected bool playerAlive = true;

    protected bool droppedScriptAlive = false;

    private void Start() {
        sGameEventManager.Access().OnCollected += checkDamage;
        sGameEventManager.Access().OnBossKilled += onBossFinished;
        sGameEventManager.Access().OnGameOver += onGameOver;
        this.animator = this.gameObject.GetComponent<Animator>();
    }



    protected void checkDamage(ACollectable feature) {
        if (feature is DamageBoss1) {
            this.gotDamage();
            this.droppedScriptAlive = false;
        }
    }

    protected void onGameOver() {
        this.playerAlive = false;
    }

    protected void gotDamage() {
        sGameEventManager.Access().Trigger_BossHit(1f);
        if (this.gameObject.GetComponent<BossHealthSystem>().GetHealth() > 0f) {
            this.playSound(this.damage);
        }
    }

    protected void dropScriptRight() {
        this.dropScript(true);
    }

    protected void dropScriptLeft() {
        this.dropScript(false);
    }

    protected void dropScript(bool right) {
        if (!this.droppedScriptAlive && this.alive && this.playerAlive) {
            this.droppedScriptAlive = true;
            float posX = 63.1f;
            if (right) {
                posX = 72.2f;
            }
            GameObject go = Instantiate(this.prefabDamageScript, this.gameObject.transform);
            go.GetComponent<ScriptText>().ShowFileName();
            go.transform.parent = this.gameObject.transform.parent.parent;
            go.transform.position = new Vector3(posX, -2.2f, 2.2f);
        }
    }

    protected void playSound(AudioClip clip) {
        if (this.sfx == null) this.sfx = this.gameObject.GetComponent<AudioSource>();
        if (this.alive || !this.alive && clip == this.death) {
            this.sfx.Stop();
            this.sfx.clip = clip;
            this.sfx.Play();
        }
    }

    protected void playAmbient(AudioClip clip) {
        if (this.ambient == null) this.ambient = Camera.main.gameObject.transform.Find("Audio_Script").gameObject.GetComponent<AudioSource>();
        this.ambient.Stop();
        this.ambient.clip = clip;
        if (clip == this.fight) {
            this.ambient.volume = .3f;
        } else {
            this.ambient.volume = 1f;
        }
        this.ambient.Play();
    }

    protected void playDashSound() {
        this.playSound(this.dash);
    }

    protected void playAttackSound() {
        this.playSound(this.attack);
    }

    protected void playDeathSound() {
        this.playSound(this.death);
    }

    protected void playCaveMusic() {
        this.playAmbient(this.cave);
    }

    protected void playFightingMusic() {
        this.playAmbient(this.fight);
    }

    protected void playAmbientMusic() {
        this.playAmbient(this.normal);
    }



    public void DoSequence() {
        this.playCaveMusic();
        this.playIdleOnRight();
        this.moveSpawnpoint();
        Invoke("freezePlayerMovement", .5f);
        Invoke("playDialogueStart", 2f);
        Invoke("playFightingMusic", 12f);
        Invoke("allowPlayerMovement", 12f);
        Invoke("doFightSequence", 13f);
    }

    protected void playDialogueStart() {
        this.dialogueStart.GetComponent<Animator>().enabled = true;
    }

    protected void playDialogueDeath() {
        this.dialogueDeath.GetComponent<Animator>().enabled = true;
    }

    protected void moveSpawnpoint() {
        Vector3 oldSpawnpointPos = GameObject.FindGameObjectWithTag("Player").transform.parent.position;
        Vector3 newSpawnpointPos = this.newSpawnpoint.transform.position;
        Vector3 diff = newSpawnpointPos - oldSpawnpointPos;
        GameObject.FindGameObjectWithTag("Player").transform.parent.position = newSpawnpointPos;
        GameObject.FindGameObjectWithTag("Player").transform.Translate(-diff);
    }

    protected void freezePlayerMovement() {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().enabled = false;
        GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    protected void allowPlayerMovement() {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().enabled = true;
    }

    protected void doFightSequence() {
        this.playAttackToLeft();
        Invoke("playIdleOnRight", 1.5f);
        Invoke("playDoubleDashFromRight", 3f);
        Invoke("dropScriptRight", 3.5f);
        Invoke("playIdleOnRight", 5.5f);
        Invoke("playDashToLeft", 12f);
        Invoke("playIdleOnLeft", 13.5f);
        Invoke("playAttackToRight", 14f);
        Invoke("playIdleOnLeft", 15.4f);
        Invoke("playAttackToRight", 15.5f);
        Invoke("playDashToRight", 17f);
        Invoke("dropScriptLeft", 17.5f);
        Invoke("playDoubleDashFromRight", 18.5f);
        Invoke("playIdleOnRight", 21f);
        Invoke("playAttackToLeft", 26f);
        Invoke("playIdleOnRight", 27.4f);
        Invoke("doFightSequence", 27.5f);
    }

    protected void onBossFinished() {
        this.alive = false;
        this.removeBossColliders();
        this.playDeath();
        Invoke("playDialogueDeath", 1f);
        Invoke("playAmbientMusic", 2.5f);
        Invoke("endFight", 2.6f);
    }

    protected void removeBossColliders() {
        this.gameObject.GetComponent<Collider2D>().enabled = false;
        for (int i = 0; i < this.gameObject.transform.childCount; i++) {
            this.gameObject.transform.GetChild(i).gameObject.GetComponent<Collider2D>().enabled = false;
        }
    }

    protected void endFight() {
        this.blockSkip.GetComponent<BorderScript>().UnlockCamera();
        sHealthGui.Access().ShowHealthBoss(false);
        Destroy(this.blockSkip);
        Destroy(this.entryTrigger);
    }



    protected void playIdleOnLeft() {
        if (this.alive && this.playerAlive) {
            this.idle = true;
            this.facingRight = true;
            this.animator.Play("Idle_On_Left");
        } else if (!this.playerAlive) {
            if (!this.idle) {
                this.animator.Play("Idle_On_Left");
            }
        }
    }

    protected void playIdleOnRight() {
        if (this.alive && this.playerAlive) {
            this.idle = true;
            this.facingRight = false;
            this.animator.Play("Idle_On_Right");
        } else if (!this.playerAlive) {
            if (!this.idle) {
                this.animator.Play("Idle_On_Left");
            }
        }
    }

    protected void playAttackToLeft() {
        if (this.alive && this.playerAlive) {
            this.idle = false;
            this.facingRight = false;
            this.animator.Play("Attack_To_Left");
            Invoke("playAttackSound", .5f);
        }
    }

    protected void playAttackToRight() {
        if (this.alive && this.playerAlive) {
            this.idle = false;
            this.facingRight = true;
            this.animator.Play("Attack_To_Right");
            Invoke("playAttackSound", .5f);
        }
    }

    protected void playDashToLeft() {
        if (this.alive && this.playerAlive) {
            this.idle = false;
            this.facingRight = false;
            this.animator.Play("Dash_To_Left");
            Invoke("playDashSound", .2f);
        }
    }

    protected void playDashToRight() {
        if (this.alive && this.playerAlive) {
            this.idle = false;
            this.facingRight = true;
            this.animator.Play("Dash_To_Right");
            Invoke("playDashSound", .2f);
        }
    }

    protected void playDoubleDashFromRight() {
        if (this.alive && this.playerAlive) {
            this.idle = false;
            this.facingRight = false;
            this.animator.Play("Double_Dash_From_Right");
            Invoke("playDashSound", .2f);
            Invoke("playDashSound", 1.4f);
        }
    }

    protected void playDeath() {
        //float lastPosX = this.gameObject.transform.position.x;
        if (this.facingRight) {
            this.animator.Play("Death_Right");
        } else {
            this.animator.Play("Death_Left");
        }
        //this.gameObject.transform.position = new Vector3(lastPosX, this.gameObject.transform.position.y, this.gameObject.transform.position.z);
        this.playDeathSound();
    }

}