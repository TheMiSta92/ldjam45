using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("LDJAM/Visuals/Camera Background")]
public class CameraBackgroundOnStage : MonoBehaviour {

    [SerializeField] protected Color[] backgroundColors;



    private void Start() {
        sGameEventManager.Access().OnStageSwitch += changeBg;
    }



    protected void changeBg(int stage) {
        Camera.main.backgroundColor = this.backgroundColors[stage - 1];
    }

}