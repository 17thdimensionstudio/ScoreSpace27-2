using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float range;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.AddRelativeForce(new Vector2(speed, 0));
        gameObject.GetComponent<AudioSource>().pitch = Random.Range(.8f, 1.2f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Mathf.Abs(transform.position.magnitude - FindObjectOfType<PlayerControler>().transform.position.magnitude) > range)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
    }
}
