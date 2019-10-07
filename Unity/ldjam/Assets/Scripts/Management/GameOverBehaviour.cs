using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverBehaviour : MonoBehaviour
{
    private MeshRenderer mesh;
    private Color col;
    [SerializeField] Color wantedCol;
    // Start is called before the first frame update
    void Start()
    {
        mesh = gameObject.GetComponent<MeshRenderer>();
        wantedCol.a = 0;
        mesh.material.color = wantedCol;
        sGameEventManager.Access().OnGameOver += ShowGameOver;
    }

private void ShowGameOver()
    {
        col = mesh.material.color;
        col.a = 1;
        mesh.material.color = col;
        Invoke("goToTitle", 5f);
    }
    private void goToTitle()
    {
        SceneManager.LoadScene(0);
    }
}
