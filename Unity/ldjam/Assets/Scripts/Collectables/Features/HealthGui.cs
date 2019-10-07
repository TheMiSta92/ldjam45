using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthGui : ACollectable {

    protected override void applyEffect() {
        sHealthGui.Access().ShowHealthPlayer();
        sHealthGui.Access().ShowHealthBoss();
    }

    protected override void undoEffect() {
        sHealthGui.Access().ShowHealthPlayer(false);
        sHealthGui.Access().ShowHealthBoss(false);
    }

}