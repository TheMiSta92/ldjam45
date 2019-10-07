using System.Collections;
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

    protected AudioSource sfx;
    protected AudioSource ambient;

    protected bool alive = true;
    protected bool facingRight = false;

    private void Start() {
        sGameEventManager.Access().OnCollected += checkDamage;
        sGameEventManager.Access().OnBossKilled += onBossFinished;
        this.animator = this.gameObject.GetComponent<Animator>();
    }



    protected void checkDamage(ACollectable feature) {
        if (feature is DamageBoss1) this.gotDamage();
    }

    protected void gotDamage() {
        sGameEventManager.Access().Trigger_BossHit(.5f);
        if (this.gameObject.GetComponent<BossHealthSystem>().GetHealth() > 0f) {
            this.playSound(this.damage);
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
        Invoke("playFightingMusic", 12f);
        Invoke("doFightSequence", 13f);
    }

    protected void doFightSequence() {
        this.playAttackToLeft();
        Invoke("playIdleOnRight", 1.5f);
        Invoke("playDoubleDashFromRight", 3f);
        Invoke("playIdleOnRight", 5.5f);
        Invoke("playDashToLeft", 12f);
        Invoke("playIdleOnLeft", 13.5f);
        Invoke("playAttackToRight", 14f);
        Invoke("playIdleOnLeft", 15.4f);
        Invoke("playAttackToRight", 15.5f);
        Invoke("playDashToRight", 17f);
        Invoke("playDoubleDashFromRight", 18.5f);
        Invoke("playIdleOnRight", 21f);
        Invoke("playAttackToLeft", 26f);
        Invoke("playIdleOnRight", 27.4f);
        Invoke("doFightSequence", 27.5f);
    }

    protected void onBossFinished() {
        this.alive = false;
        this.playDeath();
        Invoke("playAmbientMusic", 2.5f);
        Invoke("doDestroy", 3f);
    }

    protected void doDestroy() {
        Destroy(this.gameObject);
    }



    protected void playIdleOnLeft() {
        if (this.alive) {
            this.facingRight = true;
            this.animator.Play("Idle_On_Left");
        }
    }

    protected void playIdleOnRight() {
        if (this.alive) {
            this.facingRight = false;
            this.animator.Play("Idle_On_Right");
        }
    }

    protected void playAttackToLeft() {
        if (this.alive) {
            this.facingRight = false;
            this.animator.Play("Attack_To_Left");
            Invoke("playAttackSound", .5f);
        }
    }

    protected void playAttackToRight() {
        if (this.alive) {
            this.facingRight = true;
            this.animator.Play("Attack_To_Right");
            Invoke("playAttackSound", .5f);
        }
    }

    protected void playDashToLeft() {
        if (this.alive) {
            this.facingRight = false;
            this.animator.Play("Dash_To_Left");
            Invoke("playDashSound", .2f);
        }
    }

    protected void playDashToRight() {
        if (this.alive) {
            this.facingRight = true;
            this.animator.Play("Dash_To_Right");
            Invoke("playDashSound", .2f);
        }
    }

    protected void playDoubleDashFromRight() {
        if (this.alive) {
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