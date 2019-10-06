using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioCollected : MonoBehaviour
{
    [SerializeField] AudioClip clipScript;
    [SerializeField] AudioClip clipCan;
    private AudioSource source;
    // Start is called before the first frame update
    void Start()
    {
        
        sGameEventManager.Access().OnCollected += AddAudioSource_OnCollected;
    }

    private void AddAudioSource_OnCollected(ACollectable obj)
    {
        if (obj.tag == "Script")
        {
            if (this.source == null) searchForSource();
            if (source.clip != clipScript)
            {
                source.clip = clipScript;
            }
            source.Play();
            return;
        }
        
        if (obj.tag == "Can")
        {
            if (this.source == null) searchForSource();
            if (source.clip != clipCan)
            {
                source.clip = clipCan;
            }
            source.Play();
        }
    }
    private void searchForSource()
    {
        this.source = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<AudioSource>();
    }
}

