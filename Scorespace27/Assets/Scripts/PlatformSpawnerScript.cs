using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawnerScript : MonoBehaviour
{
    [SerializeField] Vector3 previousPlatform;
    [SerializeField] float maxDistanceFromLastPlatform;
    [SerializeField] float minDistanceFromLastPlatform;
    [SerializeField] GameObject platform;
    [SerializeField] Transform player;
    [SerializeField] PlatformScript platformScript;
    private float ScreenBoundsLeft;
    private float ScreenBoundsRight;
    private Camera mainCamera;
    private float xLocation;
    private float yLocation;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        ScreenBoundsLeft = mainCamera.ViewportToWorldPoint(Vector3.zero).x;
        ScreenBoundsRight = mainCamera.ViewportToWorldPoint(Vector2.one).x;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y - previousPlatform.y > minDistanceFromLastPlatform)
        {
            spawnPlatform();
        }
        if (player.position.x < ScreenBoundsLeft)
        {
            player.position = new Vector3(ScreenBoundsRight, player.position.y, 0);
        }
        if (player.position.x > ScreenBoundsRight)
        {
            player.position = new Vector3(ScreenBoundsLeft, player.position.y, 0);
        }
    }
    public void spawnPlatform()
    {
        xLocation = Random.Range(ScreenBoundsLeft, ScreenBoundsRight);
        yLocation = Random.Range(previousPlatform.y + minDistanceFromLastPlatform, previousPlatform.y + maxDistanceFromLastPlatform);
        platformScript.move(new Vector3(xLocation, yLocation, 0));

        xLocation = Random.Range(ScreenBoundsLeft, ScreenBoundsRight);
        yLocation = Random.Range(previousPlatform.y + minDistanceFromLastPlatform, previousPlatform.y + maxDistanceFromLastPlatform);
        platformScript.move(new Vector3(xLocation, yLocation, 0));
        previousPlatform = new Vector3(xLocation, yLocation, 0);
    }
}
