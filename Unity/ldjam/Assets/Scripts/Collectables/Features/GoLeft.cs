using UnityEngine;

[AddComponentMenu("LDJAM/Collectables/Features/Go Left")]
public class GoLeft : ACollectable {

    protected GameObject player;



    private void Start() {
        this.player = GameObject.FindGameObjectWithTag("Player");
    }



    protected override void applyEffect() {
        this.player.GetComponent<PlayerMovement>().SetCanWalkLeft();
    }

    protected override void undoEffect() {
        this.player.GetComponent<PlayerMovement>().SetCanWalkLeft(false);
    }

}