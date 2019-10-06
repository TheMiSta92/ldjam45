using UnityEngine;

[AddComponentMenu("LDJAM/Collectables/Features/Duck")]
public class Duck : ACollectable {

    protected GameObject player;



    private void Start() {
        sGameEventManager.Access().OnLanding += findPlayer;
    }



    protected override void applyEffect() {
        this.player.GetComponent<PlayerMovement>().SetCanDuck();
    }

    protected override void undoEffect() {
        this.player.GetComponent<PlayerMovement>().SetCanDuck(false);
    }

    protected void findPlayer() {
        this.player = GameObject.FindGameObjectWithTag("Player");
    }

}