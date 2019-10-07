using UnityEngine;

[AddComponentMenu("LDJAM/Collectables/Features/Stage 2")]
public class Stage3 : ACollectable {

    [SerializeField] protected GameObject playerStage2;
    [SerializeField] protected GameObject playerStage3;


    protected override void applyEffect() {
        sGameEventManager.Access().Trigger_StageSwitch(3);
        this.stopPhysics();
        this.doTransformationAnimation();
        Invoke("swapToNextStage", 2.5f);
        Invoke("startPhysics", 2.8f);
    }

    protected override void undoEffect() {
        sGameEventManager.Access().Trigger_StageSwitch(2);
    }



    protected Vector2 conservedVel;

    protected void stopPhysics() {
        this.playerStage2.GetComponent<PlayerMovement>().enabled = false;
        this.conservedVel = new Vector2(this.playerStage2.GetComponent<Rigidbody2D>().velocity.x, this.playerStage2.GetComponent<Rigidbody2D>().velocity.y);
        this.playerStage2.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
    }

    protected void doTransformationAnimation() {
        this.playerStage2.GetComponent<PlayerAnimation>().StopChecking();
        this.playerStage2.GetComponent<PlayerAnimation>().PlayAnimation("Evolve");
    }

    protected float healthToSet;
    protected float maxHealthToSet;

    protected void swapToNextStage() {
        this.playerStage3.AddComponent<PlayerHealthSystem>();
        this.healthToSet = this.playerStage2.GetComponent<PlayerHealthSystem>().GetHealth();
        this.maxHealthToSet = this.playerStage2.GetComponent<PlayerHealthSystem>().GetMaxHealth();
        this.playerStage2.tag = "Untagged";
        Destroy(this.playerStage2);

        this.playerStage3.transform.position = new Vector3(this.playerStage2.transform.position.x, this.playerStage2.transform.position.y, this.playerStage2.transform.position.z);
        this.playerStage3.SetActive(true);
        this.playerStage3.tag = "Player";
        Camera.main.gameObject.GetComponent<CameraMovement>().SearchAgainForPlayer();
        this.playerStage3.GetComponent<PlayerAnimation>().enabled = true;
        this.playerStage3.GetComponent<PlayerAnimation>().StopChecking();
        
        this.playerStage3.GetComponent<Rigidbody2D>().velocity = new Vector2(this.conservedVel.x, this.conservedVel.y);
    }

    protected void startPhysics() {
        this.playerStage3.GetComponent<PlayerMovement>().enabled = true;
        this.playerStage3.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        this.playerStage3.GetComponent<PlayerAnimation>().StartCheckingAgain();
        this.playerStage3.GetComponent<PlayerHealthSystem>().SetMaxHealth(this.maxHealthToSet);
        this.playerStage3.GetComponent<PlayerHealthSystem>().SetHealth(this.healthToSet);
    }

}