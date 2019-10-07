using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitFadeOutObject : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Color init = gameObject.GetComponent<MeshRenderer>().material.color;
        init.r = 1f;
        init.g = 1f;
        init.b = 1f;
        init.a = 0;
        gameObject.GetComponent<MeshRenderer>().material.color = init;
    }
}
