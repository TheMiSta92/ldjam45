using UnityEngine;

[AddComponentMenu("LDJAM/Collectables/Features/Camera Follow")]
public class CameraFeature : ACollectable {

    [SerializeField] protected GameObject runOutSideCamStopper;


    protected override void applyEffect() {
        Camera.main.gameObject.GetComponent<CameraMovement>().DoFollow();
        Destroy(runOutSideCamStopper);
    }

    protected override void undoEffect() {
        Camera.main.gameObject.GetComponent<CameraMovement>().DoFollow(false);
    }

}