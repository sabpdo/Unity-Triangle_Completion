﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Code to Create Third Hallway Based of User Pressing Space

public class ThirdHallway : MonoBehaviour
{

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


    public GameObject startingPole;
    // Start is called before the first frame update
    void Start()
    {

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
                Vector3 posSecond3 = new Vector3(playerPositionx - (5 / 10), (3 / 10), playerPositionz - (5 / 10));
                GameObject newWallL2 = (GameObject)Instantiate(wall, posSecond3, Quaternion.Euler(0f, yrotation + 90, 0f));
                newWallL2.tag = "wallL";
            }
            else if (yrotation >= 90 || yrotation <= 180)
            {
                Vector3 posSecond3 = new Vector3(playerPositionx + (4 / 10), (3 / 10), playerPositionz + (4 / 10));
                GameObject newWallL2 = (GameObject)Instantiate(wall, posSecond3, Quaternion.Euler(0f, yrotation + 90, 0f));
                newWallL2.tag = "wallL";
            }
            else if (yrotation >= 180 || yrotation <= 270)
            {
                Vector3 posSecond3 = new Vector3(playerPositionx + (1 / 2), (3 / 10), playerPositionz + (5 / 10));
                GameObject newWallL2 = (GameObject)Instantiate(wall, posSecond3, Quaternion.Euler(0f, yrotation + 90, 0f));
                newWallL2.tag = "wallL";
            }
            else
            {
                Vector3 posSecond3 = new Vector3(playerPositionx + 1, (3 / 10), playerPositionz + (5 / 10));
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
        TrialScript TS = player.GetComponent<TrialScript>();

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

                
                startingPole.SetActive(true);
                startingPole.transform.position = TS.FirstCoordinates[TM.current].transform.position;
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

