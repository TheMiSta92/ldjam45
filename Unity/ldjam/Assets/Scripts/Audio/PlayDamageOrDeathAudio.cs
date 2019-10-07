using UnityEngine;

public class PlayDamageOrDeathAudio : MonoBehaviour {

    [SerializeField] protected AudioClip damage;
    [SerializeField] protected AudioClip death;
    protected AudioSource source;

    private void Start() {
        sGameEventManager.Access().OnCharacterHurt += playDamage;
        sGameEventManager.Access().OnGameOver += playDeath;
        this.source = this.gameObject.GetComponent<AudioSource>();
    }

    protected void playDamage(float dmg) {
        this.source.Stop();
        this.source.clip = this.damage;
        this.source.Play();
    }

    protected void playDeath() {
        this.source.Stop();
        this.source.clip = this.death;
        this.source.Play();
    }

    private void OnDestroy() {
        sGameEventManager.Access().OnCharacterHurt -= playDamage;
        sGameEventManager.Access().OnGameOver -= playDeath;
    }

}
