using System;
using UnityEngine;

[AddComponentMenu("LDJAM/Player/Animation Controller")]
[RequireComponent(typeof(PlayerMovement), typeof(Animator))]
public class PlayerAnimation : MonoBehaviour {

    protected Animator animator;
    protected PlayerMovement pm;
    protected string runningAni;
    protected bool doChecks = true;


    private void Start() {
        this.animator = this.gameObject.GetComponent<Animator>();
        this.pm = this.gameObject.GetComponent<PlayerMovement>();
        this.runningAni = "Idle_Right";
        sGameEventManager.Access().OnInput += doCheck;
        sGameEventManager.Access().OnLanding += playLanding;
        sGameEventManager.Access().AfterLanding += doCheck;
    }

    private void FixedUpdate() {
        if (!this.doChecks) return;

        if (Math.Abs(this.pm.GetHorizontalSpeed()) <= .0005f && this.pm.IsWalking() == 0 && !this.pm.IsAirborne()) {
            if (this.runningAni.EndsWith("_Left")) {
                this.PlayAnimation("Idle_Left");
            } else {
                this.PlayAnimation("Idle_Right");
            }
        }
    }

    protected void doCheck() {
        if (!this.doChecks) return;

        if (this.pm.IsAirborne() && !this.runningAni.StartsWith("Jump_") && !this.pm.IsDucking()) {
            if (this.pm.GetHorizontalSpeed() > 0f) {
                this.PlayAnimation("Jump_Right");
            } else {
                this.PlayAnimation("Jump_Left");
            }
            return;
        }

        if (this.pm.IsAirborne()) return;

        if (this.pm.IsWalking() == 1) {
            this.PlayAnimation("Walk_Right");
            return;
        }

        if (this.pm.IsWalking() == -1) {
            this.PlayAnimation("Walk_Left");
            return;
        }

        if (this.pm.IsSliding() == 1) {
            this.PlayAnimation("Idle_Right");
            return;
        }

        if (this.pm.IsSliding() == -1) {
            this.PlayAnimation("Idle_Left");
            return;
        }
    }

    protected void playLanding() {
        if (this.doChecks) {
            if (!this.pm.IsDucking()) {
                this.PlayAnimation("Landing");
            }
            this.doChecks = false;
            Invoke("StartCheckingAgain", 10f / 30f);
        }
    }

    public void StartCheckingAgain() {
        this.doChecks = true;
    }

    public void StopChecking() {
        this.doChecks = false;
    }

    public void PlayDamageAnimation() {
        this.doChecks = false;
        if (this.runningAni.EndsWith("_Left")) {
            this.PlayAnimation("Damage_Left");
        } else {
            this.PlayAnimation("Damage_Right");
        }
        Invoke("StartCheckingAgain", .3f);
    }

    public void PlayDeathAnimation() {
        this.doChecks = false;
        if (this.runningAni.EndsWith("_Left")) {
            this.PlayAnimation("Death_Left");
        } else {
            this.PlayAnimation("Death_Right");
        }
    }

    public void PlayAnimation(string name) {
        if (this.gameObject.activeSelf) {
            this.animator.Play(name);
            this.runningAni = name;
        }
    }

    private void OnDestroy() {
        sGameEventManager.Access().OnInput -= doCheck;
        sGameEventManager.Access().OnLanding -= playLanding;
        sGameEventManager.Access().AfterLanding -= doCheck;
    }

}