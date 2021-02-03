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
    private bool done = false;
    private bool left = true;
    private bool openField;

    private int numSpace = 0;

    private float yrotation;
    private float playerPositionx;
    private float playerPositionz;

    // Start is called before the first frame update
    void Start()
    {
        /*Initializing start position which is the beginning of the Third Hallway
        startPosition = new Vector3(-58, 3, 55);*/

        //Get Components from Main Script
        Main refScript = GetComponent<Main>();
        left = GetComponent<Main>().left;
        openField = GetComponent<Main>().openField;
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

        //Create Walls Based off Rotation and Position
        if (openField == false)
        {
            Vector3 posSecond3 = new Vector3(playerPositionx+10, 3, playerPositionz+10);
            GameObject newWallL2 = (GameObject)Instantiate(wall, posSecond3, Quaternion.Euler(0f, yrotation, 90f));
            newWallL2.tag = "wallL";

            Vector3 posSecond = new Vector3(playerPositionx - 7, 3, playerPositionz- 10);
            GameObject newWallL = (GameObject)Instantiate(wall, posSecond, Quaternion.Euler(0f, yrotation, 90f));
            newWallL.tag = "wallL";
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Get left from Main Script and get info if OpenField or not from Main Script
        left = GetComponent<Main>().left;
        openField = GetComponent<Main>().openField;

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
            }

            if (numSpace >= 3)
            {
                if (left == false)
                    left = true;
                else
                    left = false;
                numSpace = 0;
                done = true;
            }
        }
        

        /* THIS CODE IS FOR INFINITE HALLWAY
         * if (wallDone == true)
        {
            //Determine how far the player has moved since last terrain update
            int xMove = (int)Mathf.Abs(player.transform.position.x - startPosition.x);
            int zMove = (int)Mathf.Abs(player.transform.position.z - startPosition.z);

            if (xMove >= planeSize || zMove >= planeSize) //If player has moved more than one plane size
            {

                int random = (int)(Mathf.Floor(Mathf.Sqrt((xMove * xMove) + (zMove * zMove)))); //distance player has traveled - using distance formula

                if (left == true)
                {
                    //Create walls based off how far the player has traveled
                    for (int x = 1; x < (random + 1); x++)
                    {
                        Vector3 posSecond = new Vector3(87 + (x * 5), 3, -90 + (-5 * x));
                        GameObject newWall3 = (GameObject)Instantiate(wall, posSecond, Quaternion.Euler(0f, 135f, 90f));
                        newWall3.tag = "wallL";

                        if (Input.GetKeyDown(KeyCode.Y))
                        {
                            break;
                        }
                    }

                    for (int z = 1; z < (random + 1); z++)
                    {
                        Vector3 posSecond3 = new Vector3(95 + (z * 5), 3, -90 + (-5 * z));
                        GameObject newWall4 = (GameObject)Instantiate(wall, posSecond3, Quaternion.Euler(0f, 135f, 90f));
                        newWall4.tag = "wallL";

                        if (Input.GetKeyDown(KeyCode.Y))
                        {
                            break;
                        }
                    }
                }
                else if (left == false)
                {
                    //Create walls based off how far the player has traveled
                    for (int x = 1; x < (random + 1); x++)
                    {
                        Vector3 posSecond = new Vector3(87 + (x * -5), 3, -90 + (-5 * x));
                        GameObject newWall3 = (GameObject)Instantiate(wall, posSecond, Quaternion.Euler(0f, 135f, 90f));
                        newWall3.tag = "wallR";
        if (Input.GetKeyDown(KeyCode.Space))
                        {
                            break;
                        }
                    }

                    for (int z = 1; z < (random + 1); z++)
                    {
                        Vector3 posSecond3 = new Vector3(95 + (z * -5), 3, -90 + (-5 * z));
                        GameObject newWall4 = (GameObject)Instantiate(wall, posSecond3, Quaternion.Euler(0f, 135f, 90f));
                        newWall4.tag = "wallR";

                        if (Input.GetKeyDown(KeyCode.Space))
                        {
                            break;
                        }
                    }
                }
            }

            }*/


        //When Done
        if (done == true)
        {
            //Reset Boolean Values
            ThirdPole = false;
            //wallDone = false;
            done = false;

            //Destroy Walls
            GameObject[] walls = GameObject.FindGameObjectsWithTag("wallL");
            foreach (GameObject wall in walls)
            {
                Destroy(wall);
            }
                
        }
    }
        
}

