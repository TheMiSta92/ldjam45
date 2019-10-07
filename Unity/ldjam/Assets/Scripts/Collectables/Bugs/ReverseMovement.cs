using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("LDJAM/Collectables/Bugs/Reverse Movement")]
public class ReverseMovement : ACollectable
{

    protected GameObject player;



    private void Start()
    {
        sGameEventManager.Access().OnLanding += findPlayer;
    }



    protected override void applyEffect()
    {
        this.player.GetComponent<PlayerMovement>().SetReverse();
    }

    protected override void undoEffect()
    {
        this.player.GetComponent<PlayerMovement>().SetReverse(false);
    }

    protected void findPlayer()
    {
        this.player = GameObject.FindGameObjectWithTag("Player");
    }

}
