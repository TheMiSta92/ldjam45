using UnityEngine;

public class TriggerParticleSystemOnGameStart : MonoBehaviour {

    private void Start() {
        this.gameObject.GetComponent<ParticleSystem>().Stop();
        sGameEventManager.Access().OnGameStart += triggerParticles;
    }

    protected void triggerParticles() {
        this.gameObject.GetComponent<ParticleSystem>().Play();
    }

}