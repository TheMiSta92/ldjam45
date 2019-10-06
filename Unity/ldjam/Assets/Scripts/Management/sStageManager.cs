using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("LDJAM/Management/Stage Manager")]
public class sStageManager : MonoBehaviour {

    protected static sStageManager singleton;



    private void Awake() {
        sStageManager.singleton = this;
    }

    public static sStageManager Access() {
        if (sStageManager.singleton == null) throw new System.Exception("Stage Manager singleton wasn't instanced, add it to the Singleton-GO!");
        return sStageManager.singleton;
    }



    [SerializeField] protected GameObject[] stages;



    private void Start() {
        sGameEventManager.Access().OnStageSwitch += doSwitch;
        for (int i = 2; i <= this.stages.Length; i++) {
            this.instantHide(i);
        }
        this.instantShow(1);
    }

    private void doSwitch(int stage) {
        switch (stage) {
            case 1: { break; }
            case 2: {
                    this.hideStage(1);
                    this.showStage(2);
                    break;
                }
        }
    }

    protected void instantHide(int stage) {
        this.stages[stage - 1].SetActive(false);
    }

    protected void instantShow(int stage) {
        this.stages[stage - 1].SetActive(true);
    }

    protected void hideStage(int stage) {
        this.stages[stage - 1].SetActive(false);
    }

    protected void showStage(int stage) {
        this.stages[stage - 1].SetActive(true);
    }

}
