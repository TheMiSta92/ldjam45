using UnityEngine;

[AddComponentMenu("LDJAM/Collectables/Features/Go Left")]
public class GoLeft : ACollectable {

    protected GameObject player;



    private void Start() {
        sGameEventManager.Access().OnLanding += findPlayer;
    }



    protected override void applyEffect() {
        this.player.GetComponent<PlayerMovement>().SetCanWalkLeft();
    }

    protected override void undoEffect() {
        this.player.GetComponent<PlayerMovement>().SetCanWalkLeft(false);
    }

    protected void findPlayer() {
        this.player = GameObject.FindGameObjectWithTag("Player");
    }

}