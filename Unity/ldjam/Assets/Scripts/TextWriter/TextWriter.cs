using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class TextWriter : MonoBehaviour
{
    private GameObject text;
    private TextMesh mesh;
    [SerializeField] [Range(0f, 0.2f)] private float normalDelay=0.065f;
    [SerializeField] [Range(0f,0.2f)] private float deviation=0.065f;
    // Start is called before the first frame update
    void Start()
    {
        text = GameObject.FindGameObjectWithTag("ConsoleText");
        mesh = text.GetComponent<TextMesh>();
        DeleteText();
        StartCoroutine(WriteText("Console.WriteLine(\"Hello World\")"));

    }
    public void DeleteText()
    {
        mesh.text = "";
    }
    public IEnumerator WriteText(string t)
    {
        DeleteText();
        foreach(char c in t)
        {
            mesh.text = mesh.text + c;
            yield return new WaitForSeconds(normalDelay + UnityEngine.Random.Range(0f, 1f)*deviation);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
