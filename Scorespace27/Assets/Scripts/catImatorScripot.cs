using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class catImatorScripot : MonoBehaviour
{
    Rigidbody2D rb;
    Animator myAnim;
    private void Start()
    {
        myAnim = gameObject.GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        if(rb.velocity.y < -.1f)
        {
            myAnim.SetBool("falling", true);
        }
        else if (rb.velocity.y > .1f)
        {
            myAnim.SetBool("Acending", true);
        }
        else
        {
            myAnim.SetBool("falling", false);
            myAnim.SetBool("Acending", false);
            if (Mathf.Abs(rb.velocity.x) > .3f)
            {
                myAnim.SetBool("Walking", true);
            }
            else
            {
                myAnim.SetBool("Walking", false);
            }
        }
    }
}
