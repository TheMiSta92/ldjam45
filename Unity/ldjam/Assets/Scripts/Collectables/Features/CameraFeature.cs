using UnityEngine;

[AddComponentMenu("LDJAM/Collectables/Features/Camera Follow")]
public class CameraFeature : ACollectable {

    protected override void applyEffect() {
        Camera.main.gameObject.GetComponent<CameraMovement>().DoFollow();
    }

    protected override void undoEffect() {
        Camera.main.gameObject.GetComponent<CameraMovement>().DoFollow(false);
    }

}