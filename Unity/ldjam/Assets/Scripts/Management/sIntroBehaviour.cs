using System;
using UnityEngine;

[AddComponentMenu("LDJAM/Management/Intro Behaviour")]
public class sIntroBehaviour : MonoBehaviour {

    protected static sIntroBehaviour singleton;



    [SerializeField] protected GameObject fakeAnimator;
    protected Transform fakeAnimatorT;
    protected bool doFakeAnimation = false;
    protected float fakeAnimationSpeed = 15f;

    [SerializeField] protected GameObject[] firstFeatures;


    protected GameObject player;



    private void Awake() {
        sIntroBehaviour.singleton = this;
    }

    public static sIntroBehaviour Access() {
        if (sIntroBehaviour.singleton == null) throw new System.Exception("Intro Behaviour singleton wasn't instanced, add it to the Singleton-GO!");
        return sIntroBehaviour.singleton;
    }



    private void Start() {
        this.player = GameObject.FindGameObjectWithTag("Player");
        this.fakeAnimatorT = this.fakeAnimator.transform;
        this.hideFirstFeatures();
        this.StartFakeAnimation();
    }



    public void StartFakeAnimation() {
        this.showPlayer(false);
        sConsoleTextWriter.Access().SetNormalDelay(Time.deltaTime);
        sConsoleTextWriter.Access().SetDeviation(Time.deltaTime);
        sConsoleTextWriter.Access().ShowText("Log(\"Hello World!\");");
        this.doFakeAnimation = true;
    }

    private void Update() {
        if (this.doFakeAnimation) {
            float organicer = UnityEngine.Random.Range(-10f, 10f);
            this.fakeAnimatorT.Translate((this.fakeAnimationSpeed + organicer) * Time.deltaTime, 0f, 0f);
            if (this.fakeAnimatorT.position.x > 19f) {
                this.doFakeAnimation = false;
                Destroy(this.fakeAnimator);
                this.showPlayer();
                sConsoleTextWriter.Access().ResetSpeed();
                sConsoleTextWriter.Access().ShowText("player.Spawn();");
                Invoke("showFirstFeature", 1.2f);
            }
        }
    }

    protected void showPlayer(bool show = true) {
        this.player.SetActive(show);
    }

    protected void hideFirstFeatures() {
        foreach (GameObject feature in this.firstFeatures) {
            VisualFader fader = feature.GetComponent<VisualFader>();
            if (fader == null) {
                feature.SetActive(false);
            }
        }
    }

    protected void showFirstFeature() {
        foreach (GameObject feature in this.firstFeatures) {
            VisualFader fader = feature.GetComponent<VisualFader>();
            if (fader != null) {
                fader.FadeIn(5f);
            } else {
                feature.SetActive(true);
            }
        }
    }

}