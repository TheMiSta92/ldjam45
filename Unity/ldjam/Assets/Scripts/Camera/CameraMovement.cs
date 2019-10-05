using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        body = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //if player is in left border and moves right
        if (this.player.transform.position.x < (this.transform.position.x - border + 0.5 * borderWidth) && this.player.transform.position.x > (this.transform.position.x - border - 0.5 * borderWidth) && this.player.GetComponent<PlayerMovement>().GetHorizontalSpeed() > 0)
        {
            this.accelerationNow = 0;
            this.body.velocity = new Vector2(this.player.GetComponent<Rigidbody2D>().velocity.x, this.body.velocity.y);
        }
        //if player is in right border and moves left
        else if (this.player.transform.position.x < (this.transform.position.x + border + 0.5 * borderWidth) && this.player.transform.position.x > (this.transform.position.x + border - 0.5 * borderWidth) && this.player.GetComponent<PlayerMovement>().GetHorizontalSpeed() < 0)
        {
            this.accelerationNow = 0;
            this.body.velocity = new Vector2(this.player.GetComponent<Rigidbody2D>().velocity.x, this.body.velocity.y);
        }
        //if player is in left border and moves left
        else if (this.player.transform.position.x < (this.transform.position.x - border + 0.5 * borderWidth) && this.player.transform.position.x > (this.transform.position.x - border - 0.5 * borderWidth) && this.player.GetComponent<PlayerMovement>().GetHorizontalSpeed() < 0)
        {
            if (accelerationNow + accelerationStep < maxAcceleration)
            {
                accelerationNow = accelerationNow + accelerationStep;
            }
            else
            {
                accelerationNow = maxAcceleration;
            }
            this.body.velocity = new Vector2(this.player.GetComponent<Rigidbody2D>().velocity.x - accelerationNow, this.body.velocity.y);
        }
        //if player is in right border and moves right
        else if (this.player.transform.position.x < (this.transform.position.x + border + 0.5 * borderWidth) && this.player.transform.position.x > (this.transform.position.x + border - 0.5 * borderWidth) && this.player.GetComponent<PlayerMovement>().GetHorizontalSpeed() > 0)
        {
            if (accelerationNow + accelerationStep < maxAcceleration)
            {
                accelerationNow = accelerationNow + accelerationStep;
            }
            else
            {
                accelerationNow = maxAcceleration;
            }
            this.body.velocity = new Vector2(this.player.GetComponent<Rigidbody2D>().velocity.x + accelerationNow, this.body.velocity.y);
        }
        //if player is in the middle
        else
        {
            //if player moves left
            if (this.player.GetComponent<PlayerMovement>().GetHorizontalSpeed() < 0)
            {
                if (accelerationNow + accelerationStep < maxAcceleration)
                {
                    accelerationNow = accelerationNow + accelerationStep;
                }
                else
                {
                    accelerationNow = maxAcceleration;
                }
                this.body.velocity = new Vector2(this.player.GetComponent<Rigidbody2D>().velocity.x - accelerationNow, this.body.velocity.y);
            }
            //if player moves right
            if (this.player.GetComponent<PlayerMovement>().GetHorizontalSpeed() > 0)
            {
                if (accelerationNow + accelerationStep < maxAcceleration)
                {
                    accelerationNow = accelerationNow + accelerationStep;
                }
                else
                {
                    accelerationNow = maxAcceleration;
                }
                this.body.velocity = new Vector2(this.player.GetComponent<Rigidbody2D>().velocity.x + accelerationNow, this.body.velocity.y);
            }
            //if player does not move
            if (this.player.GetComponent<PlayerMovement>().GetHorizontalSpeed() == 0)
            {
                accelerationNow = 0;
                this.body.velocity = new Vector2(0, 0);
            }
        }

        this.body.position = new Vector2(this.body.position.x, this.player.GetComponent<Rigidbody2D>().position.y + verticalOffset);

    }
}
