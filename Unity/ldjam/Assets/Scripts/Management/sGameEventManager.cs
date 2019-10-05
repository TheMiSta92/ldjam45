using System;
using UnityEditor;
using UnityEngine;

[AddComponentMenu("LDJAM/Management/Game Event Manager")]
public class sGameEventManager : MonoBehaviour {

    protected static sGameEventManager singleton;



    private void Awake() {
        sGameEventManager.singleton = this;
    }

    public static sGameEventManager Access() {
        if (sGameEventManager.singleton == null) throw new System.Exception("Game Event Manager singleton wasn't instanced, add it to the Singleton-GO!");
        return sGameEventManager.singleton;
    }



    /**
     * Events
     **/
    public event Action<ACollectable> OnCollected;
    public event Action<ACollectable> AfterCollected;
    public void Trigger_Collected(ACollectable collected) {
        this.OnCollected?.Invoke(collected);
        this.AfterCollected?.Invoke(collected);
    }

    public event Action OnInput;
    public void Trigger_Input() {
        this.OnInput?.Invoke();
    }
    /**
     * // Events
     **/

}



[CustomEditor(typeof(sGameEventManager))]
public class sGameEventManagerEditor : Editor {
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        EditorGUILayout.HelpBox("OnCollected" + Environment.NewLine + "AfterCollected", MessageType.Info);
        EditorGUILayout.HelpBox("OnInput", MessageType.Info);
    }
}