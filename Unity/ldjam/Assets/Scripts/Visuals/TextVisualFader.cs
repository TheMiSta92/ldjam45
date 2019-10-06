using UnityEngine;

[AddComponentMenu("LDJAM/Visuals/Fader")]
[RequireComponent(typeof(MeshRenderer))]
public class TextVisualFader : MonoBehaviour
{

    [SerializeField] [Range(0f, 1f)] protected float initialAlpha = 0f;

    protected MeshRenderer mr;

    protected float currentTime = 0f;
    protected float targetTime;
    protected float targetAlpha = 0f;
    protected float currentAlpha;
    protected bool doFade = false;

    protected float changeAlphaLeft;



    private void Start()
    {
        this.searchForSpriteRenderer();
    }

    private void Update()
    {
        if (this.doFade)
        {
            this.currentTime += Time.deltaTime;
            float timeLeft = this.targetTime - this.currentTime;
            this.setAlpha(this.currentAlpha + this.changeAlphaLeft / timeLeft);
            this.changeAlphaLeft = this.targetAlpha - this.currentAlpha;
            if (Mathf.Abs(this.changeAlphaLeft) < .01f)
            {
                this.setAlpha(this.targetAlpha);
                this.doFade = false;
            }
        }
    }



    protected void searchForSpriteRenderer()
    {
        this.mr = this.gameObject.GetComponent<MeshRenderer>();
        this.mr.material.EnableKeyword("_ALPHABLEND_ON");   // change shader-mode to "Fade"
        this.currentAlpha = this.initialAlpha;
        this.setAlpha(this.currentAlpha);
    }

    protected void setAlpha(float a)
    {
        if (this.mr == null) this.searchForSpriteRenderer();
        Color c = this.mr.material.color;
        this.currentAlpha = a;
        c.a = a;
        this.mr.material.color = c;
    }

    public void FadeIn(float time)
    {
        this.currentTime = 0f;
        this.targetTime = time;
        this.currentAlpha = this.mr.material.color.a;
        this.targetAlpha = 1f;
        this.changeAlphaLeft = this.targetAlpha - this.currentAlpha;
        this.doFade = true;
    }

    public void FadeOut(float time)
    {
        this.currentTime = 0f;
        this.targetTime = time;
        this.currentAlpha = 1f;
        this.targetAlpha = 0f;
        this.changeAlphaLeft = this.targetAlpha - this.currentAlpha;
        this.doFade = true;
    }

}