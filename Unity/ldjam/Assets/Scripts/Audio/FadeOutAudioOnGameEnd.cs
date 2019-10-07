using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOutAudioOnGameEnd : MonoBehaviour
{
    [SerializeField] [Range(.3f, 5f)] protected float fadeTime = 1f;

    protected bool doFade = false;
    protected float currentVol = 1f;
    protected float currentTime = 0f;



    private void Start()
    {
       
    }

    private void Update()
    {
        if (this.doFade)
        {
            this.currentTime += Time.deltaTime;
            float vol = 1f - this.currentTime / this.fadeTime;
            if (vol <= 0f)
            {
                this.doFade = false;
                vol = 0f;
            }
            this.gameObject.GetComponent<AudioSource>().volume = vol;
        }
    }



    public void fadeOut()
    {
        this.doFade = true;
        this.currentTime = 0f;
    }
    public void fadeOut(float time)
    {
        fadeTime = time;
        this.doFade = true;
        this.currentTime = 0f;
    }
}
