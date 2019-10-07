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
            this.ShowFileName(script);
        }

        sGameEventManager.Access().OnStageSwitch += ScriptText_OnStageSwitch;
        ScriptText_OnStageSwitch(1);
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
                        TextMesh mesh = script.GetComponentInChildren<TextMesh>();
                        if(mesh!=null)
                            mesh.color = color_stage1;
                    }
                    break;

                }
            case 2:
                {
                    for (int i = 0; i < gameObject.transform.childCount; ++i)
                    {
                        Transform script = gameObject.transform.GetChild(i);
                        TextMesh mesh = script.GetComponentInChildren<TextMesh>();
                        if (mesh != null)
                            mesh.color = color_stage2;
                    }
                    break;
                }
        }

   
    }

    public void ShowFileName(Transform script) {
        ACollectable test = script.gameObject.GetComponent<ACollectable>();
        if (test != null) {
            if (test.ShouldShowFileName()) {
                GameObject newGo = Instantiate(prefab);
                newGo.transform.parent = script;
                newGo.transform.localPosition = new Vector3(0f, offsetY, 0f);
                newGo.GetComponent<TextMesh>().text = test.GetFilename();
                if (test.transform.name == "GoLeft" || test.transform.name == "CameraFeature")
                    newGo.AddComponent<TextVisualFader>();
            }
        }
    }

    public void ShowFileName() {
        Transform t = this.gameObject.transform;
        this.ShowFileName(t);
    }
}
