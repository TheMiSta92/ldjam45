using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraUnlockTrigger : MonoBehaviour
{
    [SerializeField] [Range(1f, 5f)] private float centerspeed;
    [SerializeField] private GameObject centerObj;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Camera cam=GameObject.FindObjectOfType<Camera>();
            CameraMovement move=cam.gameObject.GetComponent<CameraMovement>();
            move.DoFollow(false);
            move.DoFollowY(true);
            move.DoFollowX(false);
            Rigidbody2D camBody = cam.GetComponent<Rigidbody2D>();
            StartCoroutine(WaitTillCentered(camBody));
        }
    }
    IEnumerator WaitTillCentered(Rigidbody2D camBody)
    {
        camBody.velocity = new Vector2(centerspeed, 0);
        yield return new WaitWhile(() => camBody.position.x <= centerObj.transform.position.x);
        camBody.velocity = new Vector2(0, camBody.velocity.y);
    }
}
