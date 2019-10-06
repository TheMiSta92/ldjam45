using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[AddComponentMenu("LDJAM/Visuals/Console Text Writer")]
public class sConsoleTextWriter : MonoBehaviour
{
    protected static sConsoleTextWriter singleton;
    [SerializeField] private Color color_stage1;
    [SerializeField] private Color color_stage2;


    private void Awake()
    {
        sConsoleTextWriter.singleton = this;
    }

    public static sConsoleTextWriter Access()
    {
        if (sConsoleTextWriter.singleton == null) throw new System.Exception("Console Text Writer singleton wasn't instanced, add it to the Singleton-GO!");
        return sConsoleTextWriter.singleton;
    }


    public enum ConsoleTyperStyle
    {
        [InspectorName("Cleaning and typing from empty")] FROM_CLEAN,
        [InspectorName("Overwriting current text char-by-char")] OVERWRITE
    }

    [SerializeField] ConsoleTyperStyle style;
    private GameObject text;
    private TextMesh mesh;
    [SerializeField] [Range(0f, 0.2f)] private float normalDelay = 0.065f;
    [SerializeField] [Range(0f, 0.2f)] private float deviation = 0.065f;

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
        SConsoleTextWriter_OnStageSwitch(1);
        sGameEventManager.Access().OnStageSwitch += SConsoleTextWriter_OnStageSwitch; ;
    }

    private void SConsoleTextWriter_OnStageSwitch(int sceneId)
    {
        switch (sceneId)
        {
            case 1:
                {

                    mesh.color = color_stage1;
                    break;

                }
            case 2:
                {
                    mesh.color = color_stage2;
                    break;
                }
        }
    }

    public void DeleteText()
    {
        mesh.text = "";
    }

    public void ShowText(string str)
    {
        StartCoroutine(WriteText(str));
    }

    public void ResetSpeed()
    {
        this.normalDelay = this.default_normal;
        this.deviation = this.default_deviation;
    }

    public void SetNormalDelay(float delay)
    {
        this.normalDelay = delay;
    }

    public void SetDeviation(float dev)
    {
        this.deviation = dev;
    }

    protected IEnumerator WriteText(string t)
    {
        if (this.style == ConsoleTyperStyle.FROM_CLEAN)
        {
            DeleteText();
            foreach (char c in t)
            {
                mesh.text = mesh.text + c;
                yield return new WaitForSeconds(normalDelay + UnityEngine.Random.Range(0f, 1f) * deviation);
            }
        }
        else
        {
            if (mesh.text.Length > t.Length)
            {
                int sizeDifference = mesh.text.Length - t.Length;
                int lastPos = t.Length;
                for (int i = 0; i < t.Length; ++i)
                {
                    char[] text = mesh.text.ToCharArray();
                    text[i] = t[i];
                    mesh.text = new string(text);
                    yield return new WaitForSeconds(normalDelay + UnityEngine.Random.Range(0f, 1f) * deviation);
                }
                for (int i = 0; i < sizeDifference; ++i)
                {
                    mesh.text = mesh.text.Remove(lastPos,1);
                    yield return new WaitForSeconds(normalDelay + UnityEngine.Random.Range(0f, 1f) * deviation);
                }
            }
            else if (mesh.text.Length < t.Length)
            {
                int lastPos = mesh.text.Length;
                int sizeDifference = t.Length - mesh.text.Length;
                for (int i = 0; i < mesh.text.Length; ++i)
                {
                    char[] text = mesh.text.ToCharArray();
                    text[i] = t[i];
                    mesh.text = new string(text);
                    yield return new WaitForSeconds(normalDelay + UnityEngine.Random.Range(0f, 1f) * deviation);
                }
                for (int i = 0; i < sizeDifference; ++i)
                {

                    mesh.text = mesh.text + t[lastPos + i];
                    yield return new WaitForSeconds(normalDelay + UnityEngine.Random.Range(0f, 1f) * deviation);
                }

            }
            else
            {
                for (int i = 0; i < mesh.text.Length; ++i)
                {
                    char[] text = mesh.text.ToCharArray();
                    text[i] = t[i];
                    mesh.text = new string(text);
                    yield return new WaitForSeconds(normalDelay + UnityEngine.Random.Range(0f, 1f) * deviation);
                }
            }
        }
    }

}
