using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOutAudioSourceOnGameStart : MonoBehaviour {

    [SerializeField] [Range(.3f, 5f)] protected float fadeTime = 1f;

    protected bool doFade = false;
    protected float currentVol = 1f;
    protected float currentTime = 0f;



    private void Start() {
        sGameEventManager.Access().OnGameStart += fadeOut;
    }

    private void Update() {
        if (this.doFade) {
            this.currentTime += Time.deltaTime;
            float vol = 1f - this.currentTime / this.fadeTime;
            if (vol <= 0f) {
                this.doFade = false;
                vol = 0f;
            }
            this.gameObject.GetComponent<AudioSource>().volume = vol;
        }
    }



    private void fadeOut() {
        this.doFade = true;
        this.currentTime = 0f;
    }

}