using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptText : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private float offsetY;
    [SerializeField] private Color color_stage1;
    [SerializeField] private Color color_stage2;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < gameObject.transform.childCount; ++i)
        {
            Transform script = gameObject.transform.GetChild(i);
            ACollectable test = script.gameObject.GetComponent<ACollectable>();
            GameObject newGo = Instantiate(prefab);
            newGo.transform.parent = script;
            newGo.transform.localPosition = new Vector3(0f, offsetY, 0f);
            newGo.GetComponent<TextMesh>().text = test.GetFilename();
            newGo.AddComponent<TextVisualFader>();
        }
        sGameEventManager.Access().OnStageSwitch += ScriptText_OnStageSwitch;
    }

    private void ScriptText_OnStageSwitch(int sceneId)
    {

        switch (sceneId)
        {
            case 1:
                {
                    for (int i = 0; i < gameObject.transform.childCount; ++i)
                    {
                        Transform script = gameObject.transform.GetChild(i);
                        TextMesh[] childMeshes = script.GetComponentsInChildren<TextMesh>();
                        foreach (TextMesh mesh in childMeshes)
                        {
                            mesh.color = color_stage1;
                        }
                    }
                    break;

                }
            case 2:
                {
                    for (int i = 0; i < gameObject.transform.childCount; ++i)
                    {
                        Transform script = gameObject.transform.GetChild(i);
                        TextMesh[] childMeshes = script.GetComponentsInChildren<TextMesh>();
                        foreach (TextMesh mesh in childMeshes)
                        {
                            mesh.color = color_stage2;
                        }
                    }
                    break;
                }
        }
    }
}
