using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Code to Create Third Hallway Based of User Pressing Space

public class ThirdHallway : MonoBehaviour
{
    //Variables For Infinite Hallway
    /*int planeSize = 10;
     * Vector3 startPosition;
     * private bool wallDone = false;*/

    public GameObject wall;
    public GameObject player;

    private bool ThirdPole = false;
    public bool done = false;
    public bool done2 = false;
    private bool left = true;
    private bool openField;
    public bool wallSpawned = false;

    private int numSpace = 0;

    public float yrotation = 0;
    private float playerPositionx;
    private float playerPositionz;

    public TrialManager TM;

    // Start is called before the first frame update
    void Start()
    {
        /*Initializing start position which is the beginning of the Third Hallway
        startPosition = new Vector3(-58, 3, 55);*/

        //Get Components from Main Script
        GameObject player = GameObject.Find("Player");
        TrialManager TM = player.GetComponent<TrialManager>();
        left = TM.left;
        openField = TM.openField;
    }


    void OnCollisionEnter(Collision collision)
    {
        //Upon the collision of the ThirdPole
        if (collision.gameObject.CompareTag("ThirdPole"))
        {
            if (openField == false)
            {
                ThirdPole = true;
                done = false;
                done2 = false;
                //wallDone = true;
            }
        }
    }

    //Upon the First Space
    public void firstSpace()
    {
        //Obtain player's rotation and position
        yrotation = transform.localEulerAngles.y;
        playerPositionx = transform.position.x;
        playerPositionz = transform.position.z;

        float wallrotation = yrotation + 90;
        //Create Walls Based off Rotation and Position
        if (openField == false)
        {
            
            if (yrotation >= 0 || yrotation <= 90)
            {
                Vector3 posSecond3 = new Vector3(playerPositionx - 5, 3, playerPositionz - 5);
                GameObject newWallL2 = (GameObject)Instantiate(wall, posSecond3, Quaternion.Euler(0f, yrotation + 90, 0f));
                newWallL2.tag = "wallL";
            }
            else if (yrotation >=90 || yrotation<=180)
            {
                Vector3 posSecond3 = new Vector3(playerPositionx + 40, 3, playerPositionz + 40);
                GameObject newWallL2 = (GameObject)Instantiate(wall, posSecond3, Quaternion.Euler(0f, yrotation + 90, 0f));
                newWallL2.tag = "wallL";
            }
            else if (yrotation >= 180 || yrotation <= 270)
            {
                Vector3 posSecond3 = new Vector3(playerPositionx + 5, 3, playerPositionz + 5);
                GameObject newWallL2 = (GameObject)Instantiate(wall, posSecond3, Quaternion.Euler(0f, yrotation + 90, 0f));
                newWallL2.tag = "wallL";
            }
            else
            {
                Vector3 posSecond3 = new Vector3(playerPositionx + 10, 3, playerPositionz + 5);
                GameObject newWallL2 = (GameObject)Instantiate(wall, posSecond3, Quaternion.Euler(0f, yrotation + 90, 0f));
                newWallL2.tag = "wallL";
            }
            
        }
        wallSpawned = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Get left from Main Script and get info if OpenField or not from Main Script
        GameObject player = GameObject.Find("Player");
        TrialManager trialManager = player.GetComponent<TrialManager>();
        TrialManager TM = trialManager;
        left = TM.left;
        openField = TM.openField;

        //Once Collided with ThirdPole, keep track of spaces
        if (ThirdPole == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                numSpace++;
            }

            if (numSpace == 1)
            {
                firstSpace();
                numSpace = 2;
                wallSpawned = true;
            }

            if (numSpace >= 3)
            {
                if (left == false)
                    left = true;
                else
                    left = false;
                numSpace = 0;
                done = true;
                done2 = true;
            }
        }
        

        //When Done
        if (done == true)
        {
            //Reset Boolean Values
            ThirdPole = false;
            //wallDone = false;
            

            //Destroy Walls
            GameObject[] walls = GameObject.FindGameObjectsWithTag("wallL");
            foreach (GameObject wall in walls)
            {
                Destroy(wall);
            }
            done = false;
        }
    }
        
}

