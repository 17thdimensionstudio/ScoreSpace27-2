using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    [SerializeField] int reloadTime;
    [SerializeField] int shotsPerMag;
    [SerializeField] int timeBetweenShots;
    [SerializeField] float range;
    [SerializeField] float knockBack;
    [SerializeField] Rigidbody2D playerRB;
    [SerializeField] Transform gunPivot;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform bulletSpawnLocation;

    private AudioSource reloadSound;
    private int clock;
    private int shotsLeft;
    // Start is called before the first frame update
    void Start()
    {
        reloadSound = gameObject.GetComponent<AudioSource>();
        clock = 0;
        shotsLeft = shotsPerMag;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (clock > 0)
        {
            clock--;
        }
        if (clock <= 0)
        {
            if (Input.GetMouseButton(0))
            {
                if (shotsLeft > 0)
                {
                    Shoot();
                }
                else
                {
                    clock = reloadTime;
                    shotsLeft = shotsPerMag;
                    reloadSound.Play();
                }
            }
        }
    }

    private void Shoot()
    {
        Instantiate(bullet, bulletSpawnLocation.position, gunPivot.rotation);
        playerRB.AddForce(-new Vector2(Mathf.Cos(gunPivot.eulerAngles.z * Mathf.Deg2Rad), Mathf.Sin(gunPivot.eulerAngles.z * Mathf.Deg2Rad)) * knockBack);
        clock = timeBetweenShots;
        shotsLeft--;
    }
}
