using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("LDJAM/Collectables/Damage to Boss 1")]
public class DamageBoss1 : ACollectable {
    protected override void applyEffect() {
        // don't do anything (done via event)
    }

    protected override void undoEffect() {
        // don't do anything
    }
}