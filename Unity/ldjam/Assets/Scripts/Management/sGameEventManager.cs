using System;
using UnityEditor;
using UnityEngine;

[AddComponentMenu("LDJAM/Management/Game Event Manager")]
public class sGameEventManager : MonoBehaviour
{

    protected static sGameEventManager singleton;



    private void Awake()
    {
        sGameEventManager.singleton = this;
    }

    public static sGameEventManager Access(bool ignoreError = false)
    {
        if (sGameEventManager.singleton == null) {
            if (!ignoreError) throw new System.Exception("Game Event Manager singleton wasn't instanced, add it to the Singleton-GO!");
        }
        return sGameEventManager.singleton;
    }



    /**
     * Events
     **/
    public event Action<float> OnBossHit;
    public void Trigger_BossHit(float damage) {
        this.OnBossHit?.Invoke(damage);
    }

    public event Action OnBossKilled;
    public void Trigger_BossKilled() {
        this.OnBossKilled?.Invoke();
    }

    public event Action<float> OnCharacterHurt;
    public event Action<float> AfterCharacterHurt;
    public void Trigger_CharacterHurt(float damage) {
        this.OnCharacterHurt?.Invoke(damage);
        this.AfterCharacterHurt?.Invoke(damage);
    }

    public event Action<ACollectable> OnCollected;
    public event Action<ACollectable> AfterCollected;
    public void Trigger_Collected(ACollectable collected)
    {
        this.OnCollected?.Invoke(collected);
        this.AfterCollected?.Invoke(collected);
    }

    public event Action OnDeath;
    public event Action AfterDeath;
    public void Trigger_Death()
    {
        this.OnDeath?.Invoke();
        this.AfterDeath?.Invoke();
    }

    public event Action OnGameStart;
    public void Trigger_GameStart()
    {
        this.OnGameStart?.Invoke();
    }

    public event Action OnGameOver;
    public void Trigger_GameOver() {
        this.OnGameOver?.Invoke();
    }

    public event Action OnInput;
    public void Trigger_Input()
    {
        this.OnInput?.Invoke();
    }

    public event Action OnLanding;
    public event Action AfterLanding;
    public void Trigger_Landing()
    {
        this.OnLanding?.Invoke();
        this.AfterLanding?.Invoke();
    }

    public event Action<int> OnStageSwitch;
    public void Trigger_StageSwitch(int sceneId)
    {
        this.OnStageSwitch?.Invoke(sceneId);
    }
    /**
     * // Events
     **/

}