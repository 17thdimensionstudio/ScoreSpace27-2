using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float range;
    [SerializeField] Animator anim;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] LayerMask enemyLayer;
    private Rigidbody2D rb;
    public bool isExplosive;
    public Rigidbody2D playerRB;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        if (isExplosive)
        {
            rb.AddRelativeForce(new Vector2(speed/1.5f, 0));
        }
        else
        {
            rb.AddRelativeForce(new Vector2(speed, 0));
        }
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
        if (!collision.CompareTag("Player"))
        {
            if (isExplosive)
            {
                StartCoroutine(ExplodeRoutine());
            }
        }
    }

    IEnumerator ExplodeRoutine()
    {
        Destroy(gameObject.GetComponent<Collider2D>());
        Destroy(gameObject.GetComponent<Rigidbody2D>());
        if (Physics2D.CircleCast(transform.position, 2, Vector2.zero, 0, playerLayer))
        {
            RaycastHit2D player = Physics2D.CircleCast(transform.position, 2, Vector2.zero, 0, playerLayer);
            player.collider.gameObject.GetComponent<Rigidbody2D>().AddForce(-(transform.position - playerRB.transform.position).normalized * 1000);
        }
        if (Physics2D.CircleCast(transform.position, 2, Vector2.zero, 0, enemyLayer))
        {
            RaycastHit2D[] enemies = Physics2D.CircleCastAll(transform.position, 2, Vector2.zero, 0, enemyLayer);
            foreach (RaycastHit2D enemy in enemies)
            {
                enemy.collider.gameObject.GetComponent<EnemyScript>().health = 0;
            }
        }
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
