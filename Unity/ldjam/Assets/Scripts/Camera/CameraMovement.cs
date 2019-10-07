using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("LDJAM/Camera/Movement")]
[RequireComponent(typeof(Rigidbody2D))]
public class CameraMovement : MonoBehaviour
{
    private GameObject player;
    [SerializeField] [Range(5f, 100f)] private float border;
    [SerializeField] [Range(1f, 50f)] private float accelerationStep;
    private float accelerationNow = 0f;
    [SerializeField] [Range(1f,50f)] private float maxAcceleration = 0f;
    [SerializeField] private float borderWidth;
    [SerializeField] private float verticalOffset;
    private Rigidbody2D body;

    [SerializeField] protected bool doFollow = false;
    [SerializeField] protected bool doFollowY = false;
    [SerializeField] protected bool doFollowX = false;
    protected float lockedYVal;
    protected float lockedXVal;

    // Start is called before the first frame update
    void Start()
    {
        sGameEventManager.Access().OnGameOver += deactivate;
        player = GameObject.FindGameObjectWithTag("Player");
        body = this.GetComponent<Rigidbody2D>();
        this.lockedYVal = Camera.main.gameObject.transform.position.y;
        this.lockedXVal= Camera.main.gameObject.transform.position.x;
    }

    protected void deactivate() {
        this.enabled = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (this.doFollow && GameObject.FindGameObjectWithTag("Player") != null) {
            //if GetPlayer() is in left border and moves right
            if (this.GetPlayer().transform.position.x < (this.transform.position.x - border + 0.5 * borderWidth) && this.GetPlayer().transform.position.x > (this.transform.position.x - border - 0.5 * borderWidth) && this.GetPlayer().GetComponent<PlayerMovement>().GetHorizontalSpeed() > 0) {
                this.accelerationNow = 0;
                Vector2 newVel = new Vector2(this.GetPlayer().GetComponent<Rigidbody2D>().velocity.x, this.body.velocity.y);
                if (!this.doFollowY) {
                    newVel.y = 0f;
                }
                if (!this.doFollowX)
                {
                    newVel.x = 0f;
                }
                this.body.velocity = newVel;
            }
            //if GetPlayer() is in right border and moves left
            else if (this.GetPlayer().transform.position.x < (this.transform.position.x + border + 0.5 * borderWidth) && this.GetPlayer().transform.position.x > (this.transform.position.x + border - 0.5 * borderWidth) && this.GetPlayer().GetComponent<PlayerMovement>().GetHorizontalSpeed() < 0) {
                this.accelerationNow = 0;
                Vector2 newVel = new Vector2(this.GetPlayer().GetComponent<Rigidbody2D>().velocity.x, this.body.velocity.y);
                if (!this.doFollowY) {
                    newVel.y = 0f;
                }
                if (!this.doFollowX)
                {
                    newVel.x = 0f;
                }
                this.body.velocity = newVel;
            }
            //if GetPlayer() is in left border and moves left
            else if (this.GetPlayer().transform.position.x < (this.transform.position.x - border + 0.5 * borderWidth) && this.GetPlayer().transform.position.x > (this.transform.position.x - border - 0.5 * borderWidth) && this.GetPlayer().GetComponent<PlayerMovement>().GetHorizontalSpeed() < 0) {
                if (accelerationNow + accelerationStep < maxAcceleration) {
                    accelerationNow = accelerationNow + accelerationStep;
                } else {
                    accelerationNow = maxAcceleration;
                }
                Vector2 newVel = new Vector2(this.GetPlayer().GetComponent<Rigidbody2D>().velocity.x - accelerationNow, this.body.velocity.y);
                if (!this.doFollowY) {
                    newVel.y = 0f;
                }
                if (!this.doFollowX)
                {
                    newVel.x = 0f;
                }
                this.body.velocity = newVel;
            }
            //if GetPlayer() is in right border and moves right
            else if (this.GetPlayer().transform.position.x < (this.transform.position.x + border + 0.5 * borderWidth) && this.GetPlayer().transform.position.x > (this.transform.position.x + border - 0.5 * borderWidth) && this.GetPlayer().GetComponent<PlayerMovement>().GetHorizontalSpeed() > 0) {
                if (accelerationNow + accelerationStep < maxAcceleration) {
                    accelerationNow = accelerationNow + accelerationStep;
                } else {
                    accelerationNow = maxAcceleration;
                }
                Vector2 newVel = new Vector2(this.GetPlayer().GetComponent<Rigidbody2D>().velocity.x + accelerationNow, this.body.velocity.y);
                if (!this.doFollowY) {
                    newVel.y = 0f;
                }
                if (!this.doFollowX)
                {
                    newVel.x = 0f;
                }
                this.body.velocity = newVel;
            }
            //if GetPlayer() is in the middle
            else {
                //if GetPlayer() moves left
                if (this.GetPlayer().GetComponent<PlayerMovement>().GetHorizontalSpeed() < 0) {
                    if (accelerationNow + accelerationStep < maxAcceleration) {
                        accelerationNow = accelerationNow + accelerationStep;
                    } else {
                        accelerationNow = maxAcceleration;
                    }
                    Vector2 newVel = new Vector2(this.GetPlayer().GetComponent<Rigidbody2D>().velocity.x - accelerationNow, this.body.velocity.y);
                    if (!this.doFollowY) {
                        newVel.y = 0f;
                    }
                    if (!this.doFollowX)
                    {
                        newVel.x = 0f;
                    }
                    this.body.velocity = newVel;
                }
                //if GetPlayer() moves right
                if (this.GetPlayer().GetComponent<PlayerMovement>().GetHorizontalSpeed() > 0) {
                    if (accelerationNow + accelerationStep < maxAcceleration) {
                        accelerationNow = accelerationNow + accelerationStep;
                    } else {
                        accelerationNow = maxAcceleration;
                    }
                    Vector2 newVel = new Vector2(this.GetPlayer().GetComponent<Rigidbody2D>().velocity.x + accelerationNow, this.body.velocity.y);
                    if (!this.doFollowY) {
                        newVel.y = 0f;
                    }
                    if (!this.doFollowX)
                    {
                        newVel.x = 0f;
                    }
                    this.body.velocity = newVel;
                }
                //if GetPlayer() does not move
                if (this.GetPlayer().GetComponent<PlayerMovement>().GetHorizontalSpeed() == 0) {
                    accelerationNow = 0;
                    this.body.velocity = new Vector2(0, 0);
                }
            }

            Vector2 position = new Vector2(this.body.position.x, this.GetPlayer().GetComponent<Rigidbody2D>().position.y + verticalOffset);
            if (!this.doFollowY) {
                position.y = this.lockedYVal;
            }
            if (!this.doFollowX)
            {
                position.x = this.lockedXVal;
            }
            this.body.position = position;

            if (this.doFollowX)
            {
                if (position.x < GetPlayer().transform.position.x - 10f || position.x > GetPlayer().transform.position.x + 10f)
                {
                    this.body.position = new Vector2(GetPlayer().transform.position.x, body.position.y);
                }
            }
        }
        else
        {
            if (doFollowY)
            {
                if (GetPlayer().transform.position.y > 0)
                {
                    gameObject.transform.position = new Vector3(gameObject.transform.position.x, GetPlayer().transform.position.y, gameObject.transform.position.z);
                }
            }
        }
    }

    private void Update() {
        if (this.gameObject.transform.position.y < 0f) {
            this.gameObject.transform.position = new Vector3(gameObject.transform.position.x, 0f, gameObject.transform.position.z);
        }
    }

    public void DoFollow(bool follow = true) {
        this.doFollow = follow;
        this.doFollowX = follow;
    }

    public void DoFollowY(bool follow = true) {
        this.doFollowY = follow;
    }

    public GameObject GetPlayer() {
        if (this.player != null) return player;
        player = GameObject.FindGameObjectWithTag("Player");
        return player;
    }

    public void DoFollowX(bool follow = true)
    {
        if (follow == false)
        {
            this.lockedXVal = Camera.main.gameObject.transform.position.x;
        }
        this.doFollowX = follow;
    }

}
