using UnityEngine;

[AddComponentMenu("LDJAM/Collectables/Features/Jump")]
public class Jump : ACollectable {

    protected GameObject player;



    private void Start() {
        sGameEventManager.Access().OnLanding += findPlayer;
    }



    protected override void applyEffect() {
        this.player.GetComponent<PlayerMovement>().SetCanJump();
    }

    protected override void undoEffect() {
        this.player.GetComponent<PlayerMovement>().SetCanJump(false);
    }

    protected void findPlayer() {
        this.player = GameObject.FindGameObjectWithTag("Player");
    }

}