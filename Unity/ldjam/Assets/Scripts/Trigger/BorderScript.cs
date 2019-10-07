using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderScript : MonoBehaviour
{
    [SerializeField] [Range(1f, 5f)] private float centerspeed;
    [SerializeField] private GameObject centerObj;
    [SerializeField] private bool isLeftBorder;

    protected bool isLocked = false;
    protected float lastLockedXPos = 0f;

    private void Start() {
        
    }

    protected void tryRecenter() {
        if (this.isLocked) {
            Camera.main.gameObject.transform.position = new Vector3(this.lastLockedXPos, Camera.main.gameObject.transform.position.y, Camera.main.gameObject.transform.position.z);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (isLeftBorder)
                if (collision.GetComponent<Rigidbody2D>().velocity.x < 0)
                    this.UnlockCamera();
                else
                    this.LockCamera();
            else
                if (collision.GetComponent<Rigidbody2D>().velocity.x > 0)
                this.UnlockCamera();
            else
                this.LockCamera();
        }
    }
    private IEnumerator ResetYAfterAirborne(CameraMovement move)
    {
        yield return new WaitWhile(() => GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().IsAirborne());
        move.DoFollowY(false);
    }

    public void UnlockCamera()
    {
        sGameEventManager.Access().AfterDeath -= tryRecenter;
        this.isLocked = false;
        Camera cam = GameObject.FindObjectOfType<Camera>();
        CameraMovement move = cam.gameObject.GetComponent<CameraMovement>();
        move.DoFollow(true);
        move.DoFollowX(true);
        StartCoroutine(ResetYAfterAirborne(move));
    }
    public void LockCamera()
    {
        sGameEventManager.Access().AfterDeath += tryRecenter;
        this.isLocked = true;
        this.lastLockedXPos = centerObj.transform.position.x;
        Camera cam = GameObject.FindObjectOfType<Camera>();
        CameraMovement move = cam.gameObject.GetComponent<CameraMovement>();
        move.DoFollow(false);
        move.DoFollowY(true);
        move.DoFollowX(false);
        Rigidbody2D camBody = cam.GetComponent<Rigidbody2D>();
        StartCoroutine(WaitTillCentered(camBody));
    }
    IEnumerator WaitTillCentered(Rigidbody2D camBody)
    {
        if (camBody.position.x < centerObj.transform.position.x)
        {
            camBody.velocity = new Vector2(centerspeed, 0);
        }
        else
        {
            camBody.velocity = new Vector2(-centerspeed, 0);
        }
        yield return new WaitWhile(() => camBody.position.x <= centerObj.transform.position.x);
        camBody.velocity = new Vector2(0, camBody.velocity.y);
    }
}
