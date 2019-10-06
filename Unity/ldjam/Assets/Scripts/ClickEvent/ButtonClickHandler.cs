using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonClickHandler : MonoBehaviour
{
    private Animator animator;


    private void Start() {
        this.animator = this.gameObject.GetComponent<Animator>();
    }

    private void OnMouseDown() {
        sGameEventManager.Access().Trigger_GameStart();
    }

}
