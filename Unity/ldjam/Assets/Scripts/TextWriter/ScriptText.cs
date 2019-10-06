using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptText : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private float offsetY;

    // Start is called before the first frame update
    void Start()
    {
        for(int i=0;i < gameObject.transform.childCount; ++i)
        {
            Transform script = gameObject.transform.GetChild(i);
            ACollectable test = script.gameObject.GetComponent<ACollectable>();
            GameObject newGo = Instantiate(prefab);
            newGo.transform.parent = script;
            newGo.transform.localPosition = new Vector3(0f, offsetY, 0f);
            newGo.GetComponent<TextMesh>().text = test.GetFilename();
        }
    }
}
