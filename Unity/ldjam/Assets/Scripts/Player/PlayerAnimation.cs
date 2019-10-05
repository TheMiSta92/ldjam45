using System;
using UnityEngine;

[AddComponentMenu("LDJAM/Player/Animation Controller")]
[RequireComponent(typeof(PlayerMovement), typeof(Animator))]
public class PlayerAnimation : MonoBehaviour {

    protected Animator animator;
    protected PlayerMovement pm;
    protected string runningAni;


    private void Start() {
        this.animator = this.gameObject.GetComponent<Animator>();
        this.pm = this.gameObject.GetComponent<PlayerMovement>();
        this.runningAni = "Idle_Right";
        sGameEventManager.Access().OnInput += doCheck;
    }

    private void FixedUpdate() {
        if (Math.Abs(this.pm.GetHorizontalSpeed()) <= .0005f && this.pm.IsWalking() == 0) {
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
        //if (this.pm.IsAirborne()) {
        //    this.animator.Play("Jump");
        //}

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

}