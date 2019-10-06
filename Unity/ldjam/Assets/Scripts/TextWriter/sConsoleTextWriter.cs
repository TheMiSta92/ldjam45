using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[AddComponentMenu("LDJAM/Visuals/Console Text Writer")]
public class sConsoleTextWriter : MonoBehaviour
{
    protected static sConsoleTextWriter singleton;



    private void Awake() {
        sConsoleTextWriter.singleton = this;
    }

    public static sConsoleTextWriter Access() {
        if (sConsoleTextWriter.singleton == null) throw new System.Exception("Console Text Writer singleton wasn't instanced, add it to the Singleton-GO!");
        return sConsoleTextWriter.singleton;
    }


    public enum ConsoleTyperStyle { 
        [InspectorName("Cleaning and typing from empty")] FROM_CLEAN,
        [InspectorName("Overwriting current text char-by-char")] OVERWRITE
    }

    [SerializeField] ConsoleTyperStyle style;
    private GameObject text;
    private TextMesh mesh;
    [SerializeField] [Range(0f, 0.2f)] private float normalDelay=0.065f;
    [SerializeField] [Range(0f,0.2f)] private float deviation=0.065f;

    private float default_normal;
    private float default_deviation;

    // Start is called before the first frame update
    void Start()
    {
        this.default_normal = this.normalDelay;
        this.default_deviation = this.deviation;
        text = this.gameObject;
        mesh = text.GetComponent<TextMesh>();
        DeleteText();
    }

    public void DeleteText()
    {
        mesh.text = "";
    }

    public void ShowText(string str) {
        StartCoroutine(WriteText(str));
    }

    public void ResetSpeed() {
        this.normalDelay = this.default_normal;
        this.deviation = this.default_deviation;
    }

    public void SetNormalDelay(float delay) {
        this.normalDelay = delay;
    }

    public void SetDeviation(float dev) {
        this.deviation = dev;
    }

    protected IEnumerator WriteText(string t)
    {
        if (this.style == ConsoleTyperStyle.FROM_CLEAN) {
            DeleteText();
            foreach (char c in t) {
                mesh.text = mesh.text + c;
                yield return new WaitForSeconds(normalDelay + UnityEngine.Random.Range(0f, 1f) * deviation);
            }
        } else {
            throw new NotImplementedException("Atze dua moi wos!!!!");
        }
    }
}
