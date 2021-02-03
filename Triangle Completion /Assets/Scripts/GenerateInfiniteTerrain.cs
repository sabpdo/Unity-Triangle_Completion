using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class was created to generate the infinite terrain/floor for the task.

//Class to keep track and create tiles
class Tile
{
    public GameObject theTile;
    public float creationTime;

    public Tile(GameObject t, float ct)
    {
        theTile = t;
        creationTime = ct;
    }
}

//Generate Infinite Terrain
public class GenerateInfiniteTerrain : MonoBehaviour
{
    public GameObject plane;
    public GameObject player;

    int planeSize = 10;
    int halfTilesX = 10;
    int halfTilesZ = 10;

    Vector3 startPos;

    Hashtable tiles = new Hashtable();

    // Start is called before the first frame update
    void Start()
    {
        //Initialize to 0
        this.gameObject.transform.position = Vector3.zero;
        startPos = Vector3.zero;
        //Update and situate time
        float updateTime = Time.realtimeSinceStartup;
        //Create tiles that player is standing on before start
        for (int x = -halfTilesX; x < halfTilesZ; x++)
        {
            for (int z = -halfTilesZ; z < halfTilesZ; z++)
            {
                Vector3 pos = new Vector3((x * planeSize + startPos.x), 0, (z * planeSize + startPos.z));
                GameObject t = (GameObject)Instantiate(plane, pos, Quaternion.identity);

                string tilename = "Tile_" + ((int)(pos.x)).ToString() + "_" + ((int)(pos.z)).ToString();
                t.name = tilename;
                Tile tile = new Tile(t, updateTime);
                tiles.Add(tilename, tile);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //determine how far the player has moved since last terrain update
        int xMove = (int)(player.transform.position.x - startPos.x);
        int zMove = (int)(player.transform.position.z - startPos.z);

        if (Mathf.Abs(xMove) >= planeSize || Mathf.Abs(zMove) >= planeSize) //if player has moved more than one plane size
        {
            float updateTime = Time.realtimeSinceStartup; //time used to destroy old tiles

            //force integer position and round to nearest tilesize (10) - rounded down by Mathf.Floor
            int playerX = (int)(Mathf.Floor(player.transform.position.x / planeSize) * planeSize);
            int playerZ = (int)(Mathf.Floor(player.transform.position.z / planeSize) * planeSize);

            for (int x = -halfTilesX; x < halfTilesX; x++)
            {
                for (int z = -halfTilesZ; z < halfTilesZ; z++)
                {
                    Vector3 pos = new Vector3((x * planeSize + playerX), 0, (z * planeSize + playerZ));

                    string tilename = "Tile_" + ((int)(pos.x)).ToString() + "_" + ((int)(pos.z)).ToString();

                    if (!tiles.ContainsKey(tilename)) //if tiles not are already created
                    {
                        GameObject t = (GameObject)Instantiate(plane, pos, Quaternion.identity); //create Tile
                        t.name = tilename; //gives name
                        Tile tile = new Tile(t, updateTime); //create new Tile object using line 77
                        tiles.Add(tilename, tile);
                    }
                    else //scenario where tile was already created
                    {
                        (tiles[tilename] as Tile).creationTime = updateTime; //update time stamp so not destroyed
                    }
                }
            }
            //destroy all tiles not just created or with time updatetd
            //and put new tiles and tiles to be kepts in a new hashtable
            Hashtable newTerrain = new Hashtable();//create new hashtable to put new tiles
            foreach (Tile tls in tiles.Values) //loop around old hashtable
            {
                if (tls.creationTime != updateTime) //check if it is an old tile, not in range generating for player
                {
                    //delete gameobject
                    Destroy(tls.theTile);
                }
                else
                {
                    newTerrain.Add(tls.theTile.name, tls); //if tile you want to keep, put tile in new hashtable
                }
            }
            //copy new hastable contents to the working hashtable
            tiles = newTerrain;

            startPos = player.transform.position; //set start pos value to current player position
        }
    }
}
