using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    [SerializeField][Range(0.001f,0.5f)] private float fadeOutVelocity;
    private bool isEndTriggered;
    private MeshRenderer mesh;
    private bool firstWhite=true;

    private void Update()
    {
        if (isEndTriggered)
        {
            if (mesh.material.color.a < 1)
            {
                Color col = mesh.material.color;
                if (mesh.material.color.a + fadeOutVelocity > 1)
                {
                    col.a = 1f;
                    mesh.material.color = col;
                    if (firstWhite)
                    {
                        firstWhite = false;
                        StartCoroutine(PlayCreditsAndGoToStart());
                    }
                }
                col.a = col.a+fadeOutVelocity;
                mesh.material.color = col;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
           collision.gameObject.GetComponent<PlayerMovement>().enabled = false;
            mesh = GameObject.FindGameObjectWithTag("Fader").GetComponent<MeshRenderer>();
            isEndTriggered = true;
        }
    }
    private IEnumerator PlayCreditsAndGoToStart()
    {
        yield return new WaitForSeconds(1f);
        //play Credits
        yield return new WaitForSeconds(30f);
        SceneManager.LoadScene(0);
    }

}
