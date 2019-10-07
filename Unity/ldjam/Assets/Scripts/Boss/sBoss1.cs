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
    [SerializeField] protected AudioClip dash;
    [SerializeField] protected AudioClip attack;
    [SerializeField] protected AudioClip damage;
    protected AudioSource audioSource;

    private void Start() {
        sGameEventManager.Access().OnCollected += checkDamage;
        this.animator = this.gameObject.GetComponent<Animator>();
        this.DoSequence();
    }



    protected void checkDamage(ACollectable feature) {
        if (feature is DamageBoss1) this.gotDamage();
    }

    protected void gotDamage() {
        // TODO remove health
        this.playSound(this.damage);
    }

    protected void playSound(AudioClip clip) {
        if (this.audioSource == null) this.audioSource = this.gameObject.GetComponent<AudioSource>();
        this.audioSource.Stop();
        this.audioSource.clip = clip;
        this.audioSource.Play();
    }

    protected void playDashSound() {
        this.playSound(this.dash);
    }

    protected void playAttackSound() {
        this.playSound(this.attack);
    }



    public void DoSequence() {
        this.playIdleOnRight();
        Invoke("playAttackToLeft", 6f);
        Invoke("playIdleOnRight", 7.5f);
        Invoke("playDoubleDashFromRight", 9f);
        Invoke("playIdleOnRight", 11.5f);
        Invoke("playDashToLeft", 18f);
        Invoke("playIdleOnLeft", 19.5f);
        Invoke("playAttackToRight", 20f);
        Invoke("playAttackToRight", 21.5f);
        Invoke("playDashToRight", 23f);
        Invoke("playDoubleDashFromRight", 24.5f);
        Invoke("playAttackToLeft", 27f);
        Invoke("DoSequence", 28.5f);
    }



    protected void playIdleOnLeft() {
        this.animator.Play("Idle_On_Left");
    }

    protected void playIdleOnRight() {
        this.animator.Play("Idle_On_Right");
    }

    protected void playAttackToLeft() {
        this.animator.Play("Attack_To_Left");
        Invoke("playAttackSound", .5f);
    }

    protected void playAttackToRight() {
        this.animator.Play("Attack_To_Right");
        Invoke("playAttackSound", .5f);
    }

    protected void playDashToLeft() {
        this.animator.Play("Dash_To_Left");
        Invoke("playDashSound", .2f);
    }

    protected void playDashToRight() {
        this.animator.Play("Dash_To_Right");
        Invoke("playDashSound", .2f);
    }

    protected void playDoubleDashFromRight() {
        this.animator.Play("Double_Dash_From_Right");
        Invoke("playDashSound", .2f);
        Invoke("playDashSound", 1.4f);
    }

}