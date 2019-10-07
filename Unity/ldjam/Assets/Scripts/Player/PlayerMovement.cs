using System;
using UnityEngine;

//Class for player movement
[AddComponentMenu("LDJAM/Player/Movement")]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private static int layerDuckObstacles = 26;

    [SerializeField] private Transform spawnPoint;
    private float accelerationNow = 0f;
    [SerializeField] [Range(0.1f, 0.8f)] private float controllerDeadZone;
    private Rigidbody2D body;
    [SerializeField] protected soPlayerParams parameters;
    private bool jumpAvailable = true;
    [Header("Features")]
    [SerializeField] private bool canJump = false;
    [SerializeField] private bool canDuck = false;
    [SerializeField] private bool canWalkLeft = false;
    [SerializeField] [Range(.1f, .95f)] private float lowestDuck = .5f;
    [SerializeField] [Range(.1f, 1f)] private float timeForDuckAnimation = 1f;
    private float passedDuckAnimationTime = 0f;
    private bool shouldDuck = false;
    private float moveX = 0f;
    [SerializeField] private bool doubleJumpActive = false;
    private bool FirstJump = false;
    private float timeSinceLastJump = 0.5f;
    [SerializeField] [Range(0f, 1f)] float timeBetweenJumps = 0;
    [SerializeField] private bool reverse = false;

    private Transform playerT;

    // Start is called before the first frame update
    void Start()
    {
        body = this.gameObject.GetComponent<Rigidbody2D>();
        this.playerT = this.gameObject.transform;
        sGameEventManager.Access().OnDeath += Respawn;
        sGameEventManager.Access().OnLanding += ResetDoubleJump;
    }

    private void ResetDoubleJump()
    {
        if (doubleJumpActive)
        {
            FirstJump = true;
        }

    }

    // Update is called once per frame
    void Update()
    {
        this.moveX = Input.GetAxisRaw("Horizontal");

        if (reverse)
        {
            moveX = moveX * -1;
        }

        if (this.canDuck && !this.shouldDuck && !this.IsAirborne() && Input.GetAxisRaw("Vertical") < -controllerDeadZone)
        {
            this.passedDuckAnimationTime = 0f;
            this.shouldDuck = true;
        }
        else if (this.shouldDuck && this.playerT.localScale.y < 1f && Input.GetAxisRaw("Vertical") > -controllerDeadZone)
        {
            if (this.canStandUp())
            {
                this.shouldDuck = false;
            }
        }
        if (!this.canWalkLeft && this.moveX < 0f)
        {
            this.moveX = 0f;
        }
        if (Math.Abs(moveX) > controllerDeadZone || this.shouldDuck)
        {
            sGameEventManager.Access().Trigger_Input();
        }
        if (this.shouldDuck && this.playerT.localScale.y > this.lowestDuck)
        {
            this.passedDuckAnimationTime += Time.deltaTime;
            float scaleY = 1f - (1f - this.lowestDuck) * this.passedDuckAnimationTime / this.timeForDuckAnimation;
            if (scaleY < this.lowestDuck)
            {
                scaleY = this.lowestDuck;
                this.passedDuckAnimationTime = 0f;
            }
            this.playerT.localScale = new Vector3(1f, scaleY, 1f);
        }
        else if (!this.shouldDuck && this.playerT.localScale.y < 1f)
        {
            this.passedDuckAnimationTime += Time.deltaTime;
            float scaleY = this.lowestDuck + (1f - this.lowestDuck) * this.passedDuckAnimationTime / this.timeForDuckAnimation;
            if (scaleY > 1f)
            {
                scaleY = 1f;
                this.passedDuckAnimationTime = 0f;
            }
            this.playerT.localScale = new Vector3(1f, scaleY, 1f);
        }
    }

    private void FixedUpdate()
    {
        // check fell down
        if (this.gameObject.transform.position.y < -8f)
        {
            sGameEventManager.Access().Trigger_Death();
        }

        float maxVelSituational = this.parameters.maxVelocity;
        if (this.IsDucking())
        {
            maxVelSituational *= .7f;
        }
        float accelerationGainSituational = this.parameters.accelerationGain;
        if (this.IsDucking())
        {
            accelerationGainSituational *= .7f;
        }

        if (moveX < -controllerDeadZone || moveX > controllerDeadZone)
        {
            this.accelerationNow += accelerationGainSituational * this.moveX;
            if (Mathf.Abs(this.accelerationNow) > Mathf.Abs(this.parameters.maxAcceleration))
            {
                if (this.accelerationNow < -this.parameters.maxAcceleration)
                {
                    this.accelerationNow = -this.parameters.maxAcceleration;
                }
                else
                {
                    this.accelerationNow = this.parameters.maxAcceleration;
                }
            }
            float newVel = body.velocity.x + this.accelerationNow * Time.fixedDeltaTime;
            if (Mathf.Abs(newVel) > Mathf.Abs(maxVelSituational))
            {
                if (newVel < -maxVelSituational)
                {
                    newVel = -maxVelSituational;
                }
                else
                {
                    newVel = maxVelSituational;
                }
            }
            body.velocity = new Vector2(newVel, body.velocity.y);
        }
        if (Input.GetButton("Jump"))
        {
            if (timeSinceLastJump > timeBetweenJumps)
            {
                if (jumpAvailable && this.canJump && !this.shouldDuck)
                {

                    if (!doubleJumpActive)
                    {
                        jumpAvailable = false;
                        getBody().AddForce(new Vector2(0, this.parameters.jumpHeight));
                        timeSinceLastJump = 0;
                    }
                    else
                    {
                        if (FirstJump)
                        {
                            jumpAvailable = true;
                            FirstJump = false;
                            getBody().AddForce(new Vector2(0, this.parameters.jumpHeight));
                            timeSinceLastJump = 0;
                        }
                        else
                        {
                            jumpAvailable = false;
                            getBody().AddForce(new Vector2(0, this.parameters.jumpHeight));
                            timeSinceLastJump = 0;
                        }
                    }
                }
                sGameEventManager.Access().Trigger_Input();
            }
        }
        timeSinceLastJump += Time.deltaTime;
    }

    protected bool canStandUp()
    {
        UnityEngine.Object[] os = FindObjectsOfType(typeof(GameObject));
        float midX = this.gameObject.transform.position.x;
        float offset = this.gameObject.GetComponent<Collider2D>().bounds.size.x / 2f;
        float topYOnMaxScale = this.gameObject.transform.position.y + this.gameObject.GetComponent<Collider2D>().bounds.size.y;
        Vector2 pointToTestLeft = new Vector2(midX - offset, topYOnMaxScale);
        Vector2 pointToTestRight = new Vector2(midX + offset, topYOnMaxScale);
        foreach (UnityEngine.Object o in os)
        {
            GameObject go = (GameObject)o;
            if (go.layer == PlayerMovement.layerDuckObstacles)
            {
                Collider2D coll = go.GetComponent<Collider2D>();
                Vector2 closestLeft = coll.ClosestPoint(pointToTestLeft);
                if (closestLeft == pointToTestLeft) return false;
                Vector2 closestRight = coll.ClosestPoint(pointToTestRight);
                if (closestRight == pointToTestRight) return false;
            }
        }
        return true;
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

    public bool IsDucking()
    {
        return this.shouldDuck;
    }

    /// <summary>
    /// Returns the horizontal speed of the player
    /// </summary>
    /// <returns>Horizontal speed</returns>
    public float GetHorizontalSpeed()
    {
        return getBody().velocity.x;
    }

    /// <summary>
    /// Returns the vertical speed of the player
    /// </summary>
    /// <returns>Vertical speed</returns>
    public float GetVerticalSpeed()
    {
        return getBody().velocity.y;
    }

    public void Respawn()
    {
        this.gameObject.transform.position = this.spawnPoint.transform.position;
        this.gameObject.transform.rotation = this.spawnPoint.transform.rotation;
        getBody().velocity = Vector2.zero;
        getBody().angularVelocity = 0f;
    }

    public void SetCanWalkLeft(bool able = true)
    {
        this.canWalkLeft = able;
    }

    public void SetCanJump(bool able = true)
    {
        this.canJump = able;
    }

    public void SetCanDuck(bool able = true)
    {
        this.canDuck = able;
    }


    protected Rigidbody2D getBody()
    {
        if (this.body == null) this.body = this.gameObject.GetComponent<Rigidbody2D>();
        return this.body;
    }
    public void SetCanDoubleJump(bool able = true)
    {
        this.doubleJumpActive = able;
    }
    public void SetReverse(bool able = true)
    {
        this.reverse = able;
    }
}
