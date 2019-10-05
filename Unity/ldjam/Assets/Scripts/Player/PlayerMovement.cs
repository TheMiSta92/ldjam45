using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class for player movement
[AddComponentMenu("LDJAM/Player/Movement")]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] [Range(10f, 50f)] private float accelerationGain;
    [SerializeField] [Range(1f, 50f)] private float maxAcceleration;
    private float accelerationNow = 0f;
    [SerializeField] [Range(20f, 100f)] private float maxVelocity;
    [SerializeField] [Range(0.1f, 0.8f)] private float controllerDeadZone;
    private Rigidbody2D body;
    [SerializeField] private bool jumpAvailable=true;
    [SerializeField] private bool canWalkLeft = false;
    [SerializeField][Range(200f,2000f)] private float jumpHeight=500f;
    private float moveX = 0f;
    // Start is called before the first frame update
    void Start()
    {
        body = this.gameObject.GetComponent<Rigidbody2D>();
        sGameEventManager.Access().OnDeath += Respawn;
    }

    // Update is called once per frame
    void Update()
    {
        this.moveX = Input.GetAxisRaw("Horizontal");
        if (!this.canWalkLeft && this.moveX < 0f) {
            this.moveX = 0f;
        }
        if (Math.Abs(moveX) > controllerDeadZone) {
            sGameEventManager.Access().Trigger_Input();
        }
    }

    private void FixedUpdate()
    {
        // check fell down
        if (this.gameObject.transform.position.y < -8f) {
            sGameEventManager.Access().Trigger_Death();
        }

        if (moveX < -controllerDeadZone||moveX>controllerDeadZone)
        {
            if (accelerationNow + accelerationGain < maxAcceleration)
            {
                accelerationNow = accelerationNow + accelerationGain;
            }
            else
            {
                accelerationNow = maxAcceleration;
            }
            if (body.velocity.x + accelerationNow < maxVelocity)
            {
                body.velocity = new Vector2(body.velocity.x + accelerationNow * Time.fixedDeltaTime * moveX, body.velocity.y);
            }
            else
            {
                body.velocity = new Vector2(maxVelocity * moveX * Time.fixedDeltaTime, body.velocity.y);
            }

            accelerationNow = accelerationNow - accelerationGain;

        }
        if (Input.GetButton("Jump"))
        {
            if (jumpAvailable)
            {
                body.AddForce(new Vector2(0, jumpHeight));
                jumpAvailable = false;
                sGameEventManager.Access().Trigger_Input();
            }
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 25)
        {
            sGameEventManager.Access().Trigger_Landing();
            jumpAvailable = true;
        }
    }

    /// <summary>
    /// Is the player actively walking (incl. user input)
    /// </summary>
    /// <returns>0 ... no, -1 ... to the left, +1 ... to the right</returns>
    public int IsWalking()
    {
        if (Math.Abs(this.moveX) > this.controllerDeadZone)
        {
            if (this.moveX > 0f) return 1;
            return -1;
        }
        return 0;
    }

    /// <summary>
    /// Is the player sliding (no user input, but moving)
    /// </summary>
    /// <returns>0 ... no, -1 ... to the left, +1 ... to the right</returns>
    public int IsSliding()
    {
        if (this.IsWalking() == 0 && Math.Abs(this.GetHorizontalSpeed()) > 0f)
        {
            if (this.GetHorizontalSpeed() > 0f) return 1;
            return -1;
        }
        return 0;
    }

    /// <summary>
    /// Is the player airborn?
    /// </summary>
    /// <returns>True if jump is not available</returns>
    public bool IsAirborne()
    {
        return !this.jumpAvailable;
    }

    /// <summary>
    /// Is the player falling?
    /// </summary>
    /// <returns>True if airborne and negative velocity in y</returns>
    public bool IsFalling()
    {
        return this.IsAirborne() && this.GetVerticalSpeed() < 0f;
    }

    /// <summary>
    /// Returns the horizontal speed of the player
    /// </summary>
    /// <returns>Horizontal speed</returns>
    public float GetHorizontalSpeed()
    {
        return body.velocity.x;
    }

    /// <summary>
    /// Returns the vertical speed of the player
    /// </summary>
    /// <returns>Vertical speed</returns>
    public float GetVerticalSpeed()
    {
        return body.velocity.y;
    }

    public void Respawn() {
        this.gameObject.transform.position = this.spawnPoint.transform.position;
        this.gameObject.transform.rotation = this.spawnPoint.transform.rotation;
        body.velocity = Vector2.zero;
        body.angularVelocity = 0f;
    }

    public void SetCanWalkLeft(bool able = true) {
        this.canWalkLeft = able;
    }

}
}
