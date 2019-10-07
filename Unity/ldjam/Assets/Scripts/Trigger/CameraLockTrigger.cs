using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLockTrigger : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            this.UnlockCamera();
        }
    }
    private IEnumerator ResetYAfterAirborne(CameraMovement move)
    {
        yield return new WaitWhile(()=>GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().IsAirborne());
        move.DoFollowY(false);
    }

    public void UnlockCamera() {
        Camera cam = GameObject.FindObjectOfType<Camera>();
        CameraMovement move = cam.gameObject.GetComponent<CameraMovement>();
        move.DoFollow(true);
        move.DoFollowX(true);
        StartCoroutine(ResetYAfterAirborne(move));
    }
}
