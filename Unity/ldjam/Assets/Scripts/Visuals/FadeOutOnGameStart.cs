using UnityEngine;

[AddComponentMenu("LDJAM/Visuals/Fade Out on Game Start")]
public class FadeOutOnGameStart : VisualFader {

    private void Start() {
        sGameEventManager.Access().OnGameStart += fadeOutStuff;
    }


    private void fadeOutStuff() {
        this.FadeOut(12f);
    }

}