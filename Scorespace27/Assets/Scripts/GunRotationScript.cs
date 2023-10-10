using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunRotationScript : MonoBehaviour
{
    private Vector3 mousePos;
    [SerializeField] SpriteRenderer playerSprite;
    [SerializeField] SpriteRenderer gunSprite;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.right = new Vector3(mousePos.x, mousePos.y, 0) - transform.position;
        if (mousePos.x - playerSprite.transform.position.x > 0)
        {
            playerSprite.flipX = false;
            gunSprite.flipY = false;
        }
        else
        {
            playerSprite.flipX = true;
            gunSprite.flipY = true;
        }
    }
}
