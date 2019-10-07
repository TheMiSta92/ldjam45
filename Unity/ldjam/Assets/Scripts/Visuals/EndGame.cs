using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class EndGame : MonoBehaviour
{
    [SerializeField] [Range(0.001f, 0.1f)] private float fadeOutVelocity;
    private bool isEndTriggered;
    private MeshRenderer mesh;
    private bool firstWhite = true;
    private Color col;
    private float lastAlpha=0;
    private bool fadeFinished = false;
    private bool fadeRunning = false;
    [SerializeField] [Range(1f, 5f)] private float waitUntilfadeOut;
    private FadeOutAudioOnGameEnd fadeAudio;
    private AudioSource mainAudio;
    [SerializeField] private AudioClip titleClip;


    private void Update()
    {
        if (isEndTriggered)
        {
            Invoke("fadeOut", waitUntilfadeOut);
            fadeAudio.fadeOut();
            this.isEndTriggered = false;
        }
        if (this.fadeRunning)
        {
            fadeOut();
        }
        if (this.fadeFinished)
        {
            playTitleMusic();
            playCredits();
            Invoke("fadeOutMusic", 27f); 
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Animator anim=GameObject.FindGameObjectWithTag("Endpoint_Text").GetComponent<Animator>();
            anim.enabled = true;
            collision.gameObject.GetComponent<PlayerMovement>().enabled = false;
            mesh = GameObject.FindGameObjectWithTag("Fader").GetComponent<MeshRenderer>();
            mesh.material.EnableKeyword("_ALPHABLEND_ON");   // change shader-mode to "Fade"
            isEndTriggered = true;
            fadeAudio = Camera.main.GetComponentInChildren<FadeOutAudioOnGameEnd>();
            mainAudio= Camera.main.GetComponentInChildren<AudioSource>();
        }
    }
    private void playCredits()
    {
        this.fadeFinished = false;
        Debug.Log("Playing Credits");
        Camera.main.gameObject.GetComponent<VideoPlayer>().Play();
        Camera.main.gameObject.GetComponent<VideoPlayer>().loopPointReached += EndGame_loopPointReached;
    }

    private void EndGame_loopPointReached(VideoPlayer source)
    {
        goToTitleScreen();
    }

    private void fadeOut()
    {
        this.fadeRunning = true;
        if (firstWhite)
        {
            col = mesh.material.color;
        }
        if (mesh.material.color.a + fadeOutVelocity > 1)
        {
            col.a = 1f;
            mesh.material.color = col;
            if (firstWhite)
            {
                firstWhite = false;
                fadeFinished = true;
            }
        }
        col.a = col.a + fadeOutVelocity;
        mesh.material.color = col;
        if (lastAlpha != col.a)
        {
            Debug.Log("Setting Alpha to" + col.a.ToString());
            lastAlpha = col.a;
        }
    }
    private void goToTitleScreen()
    {
        Debug.Log("Switching to tile screen");
        SceneManager.LoadScene(0);
    }
    
    private void playTitleMusic()
    {
        this.mainAudio.Stop();
        this.mainAudio.clip = titleClip;
        this.mainAudio.volume = 1f;
        this.mainAudio.Play();
    }
    private void fadeOutMusic()
    {
        fadeAudio.fadeOut(3);
    }

}
