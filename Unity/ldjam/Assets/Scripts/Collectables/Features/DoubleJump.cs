using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("LDJAM/Collectables/Features/Double Jump")]
public class DoubleJump : ACollectable
{

    protected GameObject player;



    private void Start()
    {
        sGameEventManager.Access().OnLanding += findPlayer;
    }



    protected override void applyEffect()
    {
        this.player.GetComponent<PlayerMovement>().SetCanDoubleJump();
    }

    protected override void undoEffect()
    {
        this.player.GetComponent<PlayerMovement>().SetCanDoubleJump(false);
    }

    protected void findPlayer()
    {
        this.player = GameObject.FindGameObjectWithTag("Player");
    }

}