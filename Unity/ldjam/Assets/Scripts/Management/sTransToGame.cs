using UnityEngine;
using UnityEngine.SceneManagement;

public class sTransToGame : MonoBehaviour {

    protected static sTransToGame singleton;

    private void Awake() {
        sTransToGame.singleton = this;
    }

    public static sTransToGame Access() {
        if (sTransToGame.singleton == null) throw new System.Exception("TransToGame singleton wasn't instanced, add it to the Singleton-GO!");
        return sTransToGame.singleton;
    }


    private void Start() {
        sGameEventManager.Access().OnGameStart += waitAndDo;
    }



    protected void waitAndDo() {
        Invoke("DoTransition", 1f);
    }

    public void DoTransition() {
        SceneManager.LoadScene(1);
    }

}