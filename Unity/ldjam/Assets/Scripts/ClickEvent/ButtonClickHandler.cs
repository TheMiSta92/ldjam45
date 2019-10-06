using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonClickHandler : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private void OnMouseDown()
    {
        sGameEventManager.Access().Trigger_Click();
    }

}
