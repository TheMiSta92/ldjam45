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
                this.animator.Play("Idle_Left");
                this.runningAni = "Idle_Left";
            } else {
                this.animator.Play("Idle_Right");
                this.runningAni = "Idle_Right";
            }
        }
    }



    protected void doCheck() {
        if (!this.doChecks) return;

        if (this.pm.IsAirborne() && !this.runningAni.StartsWith("Jump_")) {
            if (this.pm.GetHorizontalSpeed() > 0f) {
                this.animator.Play("Jump_Right");
                this.runningAni = "Jump_Right";
            } else {
                this.animator.Play("Jump_Left");
                this.runningAni = "Jump_Left";
            }
            return;
        }

        if (this.pm.IsAirborne()) return;

        if (this.pm.IsWalking() == 1) {
            this.animator.Play("Walk_Right");
            this.runningAni = "Walk_Right";
            return;
        }

        if (this.pm.IsWalking() == -1) {
            this.animator.Play("Walk_Left");
            this.runningAni = "Walk_Left";
            return;
        }

        if (this.pm.IsSliding() == 1) {
            this.animator.Play("Idle_Right");
            this.runningAni = "Idle_Right";
            return;
        }

        if (this.pm.IsSliding() == -1) {
            this.animator.Play("Idle_Left");
            this.runningAni = "Idle_Left";
            return;
        }
    }

    protected void playLanding() {
        this.doChecks = false;
        this.animator.Play("Landing");
        this.runningAni = "Landing";
        Invoke("startCheckingAgain", 10f / 30f);
    }

    protected void startCheckingAgain() {
        this.doChecks = true;
    }

}