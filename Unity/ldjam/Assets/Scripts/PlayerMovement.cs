using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class for player movement
[AddComponentMenu("LDJAM/Player/Movement")]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] [Range(100f, 1000f)] private float speed;
    [SerializeField] [Range(0.1f,0.8f)]private float controllerDeadZone;
    private Rigidbody2D body;
    [SerializeField] private bool jumpAvailable=true;
    [SerializeField][Range(200f,2000f)] private float jumpHeight=500f;
    private float moveX = 0f;
    // Start is called before the first frame update
    void Start()
    {
        body = this.gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        moveX = Input.GetAxisRaw("Horizontal");
    }

    private void FixedUpdate()
    {
        if (moveX < -controllerDeadZone||moveX>controllerDeadZone)
        {
            body.velocity = new Vector2(transform.right.x * moveX * speed * Time.fixedDeltaTime,body.velocity.y);
        }
        if (Input.GetButton("Jump"))
        {
            if (jumpAvailable)
            {
                body.AddForce(new Vector2(0, jumpHeight));
                jumpAvailable = false;
            }
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer==25)
        {
            jumpAvailable = true;
        }
    }
}
