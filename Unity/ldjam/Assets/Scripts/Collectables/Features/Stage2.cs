﻿using UnityEngine;

[AddComponentMenu("LDJAM/Collectables/Features/Stage 2")]
public class Stage2 : ACollectable {

    [SerializeField] protected GameObject playerStage1;
    [SerializeField] protected GameObject playerStage2;


    protected override void applyEffect() {
        sGameEventManager.Access().Trigger_StageSwitch(2);
        this.stopPhysics();
        this.doTransformationAnimation();
        Invoke("swapToNextStage", 2.5f);
        Invoke("startPhysics", 2.8f);
    }

    protected override void undoEffect() {
        sGameEventManager.Access().Trigger_StageSwitch(1);
    }



    protected Vector2 conservedVel;

    protected void stopPhysics() {
        this.playerStage1.GetComponent<PlayerMovement>().enabled = false;
        this.conservedVel = new Vector2(this.playerStage1.GetComponent<Rigidbody2D>().velocity.x, this.playerStage1.GetComponent<Rigidbody2D>().velocity.y);
        this.playerStage1.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
    }

    protected void doTransformationAnimation() {
        this.playerStage1.GetComponent<PlayerAnimation>().StopChecking();
        this.playerStage1.GetComponent<PlayerAnimation>().PlayAnimation("Evolve");
    }

    protected float healthToSet;
    protected float maxHealthToSet;

    protected void swapToNextStage() {
        this.playerStage2.AddComponent<PlayerHealthSystem>();
        this.healthToSet = this.playerStage1.GetComponent<PlayerHealthSystem>().GetHealth();
        this.maxHealthToSet = this.playerStage1.GetComponent<PlayerHealthSystem>().GetMaxHealth();
        this.playerStage1.tag = "Untagged";
        Destroy(this.playerStage1);

        this.playerStage2.transform.position = new Vector3(this.playerStage1.transform.position.x, this.playerStage1.transform.position.y, this.playerStage1.transform.position.z);
        this.playerStage2.SetActive(true);
        this.playerStage2.tag = "Player";
        Camera.main.gameObject.GetComponent<CameraMovement>().GetPlayer();
        this.playerStage2.GetComponent<PlayerAnimation>().enabled = true;
        this.playerStage2.GetComponent<PlayerAnimation>().StopChecking();
        
        this.playerStage2.GetComponent<Rigidbody2D>().velocity = new Vector2(this.conservedVel.x, this.conservedVel.y);
    }

    protected void startPhysics() {
        this.playerStage2.GetComponent<PlayerMovement>().enabled = true;
        this.playerStage2.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        this.playerStage2.GetComponent<PlayerAnimation>().StartCheckingAgain();
        this.playerStage2.GetComponent<PlayerHealthSystem>().SetMaxHealth(this.maxHealthToSet);
        this.playerStage2.GetComponent<PlayerHealthSystem>().SetHealth(this.healthToSet);
    }

}