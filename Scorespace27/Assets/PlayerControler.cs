using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    [SerializeField] float maxWalkSpeed;
    [SerializeField] float jumpPower;
    public float speed;
    [SerializeField] LayerMask groundLayer;

    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (rb.velocity.x < maxWalkSpeed)
            {
                rb.AddForce(Vector2.right * speed);
                if (rb.velocity.x < 0)
                {
                    rb.AddForce(Vector2.right * speed * 1.2f);
                }
            }
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (rb.velocity.x > -maxWalkSpeed)
            {
                rb.AddForce(Vector2.left * speed);
                if (rb.velocity.x > 0)
                {
                    rb.AddForce(Vector2.left * speed * 1.2f);
                }
            }
        }
        if (Input.GetKey(KeyCode.Space) && IsGrounded())
        {
            rb.AddForce(Vector2.up * jumpPower);
        }

    }

    private bool IsGrounded()
    {
        if (Physics2D.Linecast(transform.position, new Vector2(transform.position.x, transform.position.y - 0.55f), groundLayer))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
