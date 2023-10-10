using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagDisplay : MonoBehaviour
{
    [SerializeField] GunScript gunScript;
    [SerializeField] Sprite[] magDisplaySprites;
    [SerializeField] Sprite reloadSpriteOne;
    [SerializeField] Sprite reloadSpriteTwo;
    [SerializeField] Sprite reloadSpriteThree;
    [SerializeField] Sprite reloadSpriteFour;
    [SerializeField] Image spriteRenderer;
    private int clock;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (gunScript.clock > gunScript.timeBetweenShots)
        {
            
            if (clock > 0)
            {
                clock--;
            }
            else
            {
                clock = 80;
            }
            if (clock == 80)
            {
                spriteRenderer.sprite = reloadSpriteTwo;
            }
            else if (clock == 60)
            {
                spriteRenderer.sprite = reloadSpriteThree;
            }
            else if (clock == 40)
            {
                spriteRenderer.sprite = reloadSpriteFour;
            }
            else if (clock == 20)
            {
                spriteRenderer.sprite = reloadSpriteOne;
            }
            
            else
            {
                clock--;
            }

        }
        else
        {
            spriteRenderer.sprite = magDisplaySprites[gunScript.shotsLeft];
            
        }
    }
}
