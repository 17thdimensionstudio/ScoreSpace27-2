using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlatformScript : MonoBehaviour
{
    private Vector3Int platformStart;
    private int despawnDistance;
    private Transform playerTransform;
    private Tilemap tilemap;
    private RuleTile CheeseTile;
    private DatastoreScript datastoreScript;
    [SerializeField] int enemySpawnrate;
    private int platformsUntilSpawn;
    [SerializeField] GameObject enemy;
    [SerializeField] GameObject fly;


    // Start is called before the first frame update
    void Start()
    {
        platformsUntilSpawn = enemySpawnrate;
        datastoreScript = FindObjectOfType<DatastoreScript>();
        despawnDistance = datastoreScript.despawnDistance;
        playerTransform = datastoreScript.playerTransform;
        tilemap = datastoreScript.tilemap;
        CheeseTile = datastoreScript.CheeseTile;
    }

    // Update is called once per frame
    public void move(Vector3 position)
    {
        transform.position = position;
        platformStart = tilemap.WorldToCell(transform.position);
        tilemap.SetTile(platformStart, CheeseTile);
        tilemap.SetTile(new Vector3Int(platformStart.x - 1, platformStart.y), CheeseTile);
        tilemap.SetTile(new Vector3Int(platformStart.x + 1, platformStart.y), CheeseTile);
        if (platformsUntilSpawn > 0)
        {
            platformsUntilSpawn--;
        }
        else
        {
            if (Random.Range(1, 3) == 2)
            {
                Instantiate(enemy, new Vector3(tilemap.CellToWorld(platformStart).x, tilemap.CellToWorld(platformStart).y + 1.1f), Quaternion.Euler(0, 0, 0));
            }
            else
            {
                Instantiate(fly, new Vector3(tilemap.CellToWorld(platformStart).x, tilemap.CellToWorld(platformStart).y + 3f), Quaternion.Euler(0, 0, 0));
            }
            platformsUntilSpawn = enemySpawnrate;
        }
    }
}
