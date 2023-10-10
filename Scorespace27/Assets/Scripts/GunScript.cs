using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunScript : MonoBehaviour
{
    [SerializeField] int reloadTime;
    [SerializeField] int shotsPerMag;
    public int timeBetweenShots;
    [SerializeField] float range;
    [SerializeField] float knockBack;
    [SerializeField] Rigidbody2D playerRB;
    [SerializeField] Transform gunPivot;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform bulletSpawnLocation;
    [SerializeField] AutoScrollScript autoScrollScript;
    [SerializeField] Image ShotgunReloadsprite;
    [SerializeField] SpriteRenderer gunSpriteRenderer;
    [SerializeField] Sprite shotgunLoaded;
    [SerializeField] List<Sprite> shotgunUnloaded;
    [SerializeField] Sprite rifleSprite;
    [SerializeField] Sprite shotGunSprite;
    [SerializeField] GameObject rocket;
    [SerializeField] Sprite rocketLauncherSprite;

    private AudioSource reloadSound;
    public int clock;
    private int shotgunClock;
    public int shotsLeft;
    private Transform cameraTransform;
    public int gunID = 0;
    public int rocketClock;

    // Start is called before the first frame update
    void Start()
    {
        reloadSound = gameObject.GetComponent<AudioSource>();
        clock = 0;
        shotsLeft = shotsPerMag;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            clock = reloadTime;
            shotsLeft = shotsPerMag;
            reloadSound.Play();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (gunID < 2)
            {
                gunID++;
            }
            else
            {
                gunID = 0;
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (clock > 0)
        {
            clock--;
        }
        if (shotgunClock > 0)
        {
            shotgunClock--;
        }
        if (rocketClock > 0)
        {
            rocketClock--;
        }
        if (gunID == 0)
        {
            gunSpriteRenderer.sprite = rifleSprite;
        }
        else if (gunID == 1)
        {
            gunSpriteRenderer.sprite = shotGunSprite;
        }
        else if (gunID == 2)
        {
            gunSpriteRenderer.sprite = rocketLauncherSprite;
        }
        if (clock <= 0)
        {
            if (gunID == 0)
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
        if (gunID == 1)
        {
            if (Input.GetMouseButton(0))
            {
                if (shotgunClock <= 0)
                {
                    ShootShotgun();
                    shotgunClock = reloadTime;
                    reloadSound.Play();
                }
            }
        }
        if (gunID == 2)
        {
            if (Input.GetMouseButton(0))
            {
                if (rocketClock <= 0)
                {
                    ShootRocket();
                    rocketClock = reloadTime;
                    reloadSound.Play();
                }
            }
        }
        if (shotgunClock > 0)
        {
            ShotgunReloadsprite.sprite = shotgunUnloaded[shotgunClock % 4];
        }
        else
        {
            ShotgunReloadsprite.sprite = shotgunLoaded;
        }
    }

    private void Shoot()
    {
        Instantiate(bullet, bulletSpawnLocation.position, gunPivot.rotation);
        playerRB.AddForce(-new Vector2(Mathf.Cos(gunPivot.eulerAngles.z * Mathf.Deg2Rad), Mathf.Sin(gunPivot.eulerAngles.z * Mathf.Deg2Rad)) * knockBack);
        clock = timeBetweenShots;
        shotsLeft--;
        StartCoroutine(autoScrollScript.ScreenShake(.3f, .3f, .05f, 5));
    }
    private void ShootShotgun()
    {
        Instantiate(bullet, bulletSpawnLocation.position, Quaternion.Euler(0, 0, gunPivot.rotation.eulerAngles.z - 20));
        Instantiate(bullet, bulletSpawnLocation.position, Quaternion.Euler(0, 0, gunPivot.rotation.eulerAngles.z - 10));
        Instantiate(bullet, bulletSpawnLocation.position, Quaternion.Euler(0, 0, gunPivot.rotation.eulerAngles.z));
        Instantiate(bullet, bulletSpawnLocation.position, Quaternion.Euler(0, 0, gunPivot.rotation.eulerAngles.z + 10));
        Instantiate(bullet, bulletSpawnLocation.position, Quaternion.Euler(0, 0, gunPivot.rotation.eulerAngles.z + 20));
        playerRB.AddForce(-new Vector2(Mathf.Cos(gunPivot.eulerAngles.z * Mathf.Deg2Rad), Mathf.Sin(gunPivot.eulerAngles.z * Mathf.Deg2Rad)) * knockBack * 5);
        StartCoroutine(autoScrollScript.ScreenShake(.45f, .3f, .04f, 7));
    }
    private void ShootRocket()
    {
        BulletScript rocketThing = Instantiate(bullet, bulletSpawnLocation.position, gunPivot.rotation).GetComponent<BulletScript>();
        rocketThing.isExplosive = true;
        rocketThing.playerRB = playerRB;
        StartCoroutine(autoScrollScript.ScreenShake(.15f, .15f, .1f, 20));
    }
}
