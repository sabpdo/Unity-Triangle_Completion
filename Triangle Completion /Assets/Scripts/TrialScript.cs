﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TrialScript : MonoBehaviour
{
    //instructions
    public Text instructions;

    public TrialManager TM;

    //To set off values in update
    private bool firstCylinder = false;
    private bool secondCylinder = false;
    private bool thirdLegOpen = false;

    //For Open Field - if open Field or not
    public bool openField=false; 

    //Track number of User Pressed Spaces
    private int numSpace = 0;

    //All Main GameObjects
    public GameObject longWall;
    public GameObject singleWall;
    public GameObject doubleHallway;
    public GameObject startingPole;
    public GameObject SecondPole;
    public GameObject ThirdPole;
    public GameObject cylinder;

    //For open field
    public GameObject openFieldPole;
    public GameObject secondOpenFieldPole;

    //To Destroy Walls
    Hashtable walls = new Hashtable();

    //Audio import
    private AudioSource source;
    public AudioClip rightTurn;
    public AudioClip leftTurn;


    public bool trialStart = false;

    private bool left;

    private bool practice = TrialManager.practice;

    public bool collisionFirst = false;

    // Start is called before the first frame update
    void Start()
    {
        
        //Initialize Audio
        source = GetComponent<AudioSource>();
        source.Stop();

        //Ensure boolean values are false
        firstCylinder = false;
        secondCylinder = false;

        //Set Initial GameObjects Active or not Active
        startingPole.SetActive(true);
        singleWall.SetActive(true);
        openFieldPole.SetActive(false);
        secondOpenFieldPole.SetActive(false);

        if (practice)
        //Instructions
            instructions.text = "Please walk forward.";

        practice = TrialManager.practice;
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject player = GameObject.Find("Player");
        TrialManager trialManager = player.GetComponent<TrialManager>();
        TrialManager TM = trialManager;
        //Upon the collision of the First Pole
        if (collision.gameObject.CompareTag("StartPole"))
        {
            trialStart = true;
            collisionFirst = true;

            //Moving player to original position
            transform.position = new Vector3(0f, 0f, 9f);

            //Deactivate the First Starting Pole and SetActive the Second Pole
            startingPole.SetActive(false); 
            SecondPole.SetActive(true);

            //Moving Second Pole Marker Around
            if (TM.type == TM.acute2 || TM.type == TM.acute1)
                SecondPole.transform.position = new Vector3(0f, -0.5f, 49f);
            else if (TM.type == TM.obtuse2 || TM.type == TM.right2)
                SecondPole.transform.position = new Vector3(0f, -0.5f, 69f);
            else if (TM.type == TM.right1 || TM.type == TM.obtuse1)
                SecondPole.transform.position = new Vector3(0f, -0.5f, 29f);


            //If Not Open Field
            if (openField == false)
            {
                if (practice)
                {
                    //Instructions
                    instructions.text = "Walk forward in the corridor until you hear a chime " +
                       "\n and are located in a cylinder.";
                }

                //First Hallway Right Side
                Vector3 pos = new Vector3(3, 3, 10);
                GameObject newWall = (GameObject)Instantiate(longWall, pos, Quaternion.Euler(0f, 90.0f, 0.0f));
                newWall.tag = "wall1";
                    

                Vector3 pos2 = new Vector3(-3, 3, 10);
                GameObject newWall2 = (GameObject)Instantiate(longWall, pos2, Quaternion.Euler(0f, 90.0f, 00.0f));
                newWall2.tag = "wall1";
                    

                //First Hallway Back Side
                Vector3 pos3 = new Vector3(0, 3, 5);
                GameObject newBackWall = (GameObject)Instantiate(singleWall, pos3, Quaternion.Euler(0.0f, 0.0f, 0.0f));
                newBackWall.tag = "wall1";
                
            }
            //If Open Field
            else if (openField == true)
            {
                openFieldPole.SetActive(true);
                if (practice)
                    instructions.text = "Walk forward until you hear a chime.";
            }
        }

        //Upon Collision of Second Pole
        if (collision.gameObject.CompareTag("SecondPole"))
        {
            collisionFirst = false;
            //Set Off Boolean Value in Update
            firstCylinder = true;

            if (practice)
            {
                //Instructions
                instructions.text = "First Leg Completed. Please turn until you hear a second chime.";
            }

            //Sound
            if (TM.left == true)
                source.PlayOneShot(leftTurn, 1.0f);
            else
                source.PlayOneShot(rightTurn, 1.0f);


            //Deactivate SecondPole
            SecondPole.SetActive(false);

            
            ThirdPole.SetActive(true);
            ThirdPole.transform.position = TM.thirdPoleLocation;
            

            //If non-Open Field
            if (openField == false)
            {
                //Create the cylinder container
                Instantiate(cylinder);
                cylinder.tag = "cylinder";


                //Moving Second Pole Marker Around
                if (TM.type == TM.acute2 || TM.type == TM.acute1)
                    cylinder.transform.position = new Vector3(0f, -0.5f, 49f);
                else if (TM.type == TM.obtuse2 || TM.type == TM.right2)
                    cylinder.transform.position = new Vector3(0f, -0.5f, 69f);
                else if (TM.type == TM.right1 || TM.type == TM.obtuse1)
                    cylinder.transform.position = new Vector3(0f, -0.5f, 29f);


                //Destroy walls if non-Open Field

                //Destroy previous hallway, second pole indicator
                GameObject[] argo = GameObject.FindGameObjectsWithTag("wall1");
                foreach (GameObject go in argo)
                {
                        go.SetActive(false);
                }

            }
            else if (openField == true)
            {
                //Deactivate first OpenFieldPole
                openFieldPole.SetActive(false);
                secondOpenFieldPole.transform.position = TM.thirdOpenPoleLocation;
            }
        }

        //Upon the collision of the Third Pole
        if (collision.gameObject.CompareTag("ThirdPole"))
        {
            //Deactivate Third Pole indicator
            ThirdPole.SetActive(false);

            //Sound
            if (TM.left == true)
                source.PlayOneShot(leftTurn, 1.0f);
            else
                source.PlayOneShot(rightTurn, 1.0f);

            if (openField == false)
            {
                if (practice)
                {
                    //instructions
                    instructions.text = "Second Leg Completed. Please turn until you believe you " +
                        "\n are facing the correct direction of where you started. When you are at the " +
                        "\n desired turn angle, press space once and another hallway will appear. " +
                        "\n Travel down the hallway and press space once when you believe you are " +
                        "\n at the starting location.";
                }

                //Second Cylinder boolean value for update
                secondCylinder = true;

                //Code to destroy Previous Walls
                if (TM.left == true)
                {
                    GameObject[] args = GameObject.FindGameObjectsWithTag("wall2");
                    foreach (GameObject go in args)
                    {
                        Destroy(go);
                    }
                }
                else if (TM.left == false)
                {
                    GameObject[] args = GameObject.FindGameObjectsWithTag("wall2R");
                    foreach (GameObject go in args)
                    {
                        Destroy(go);
                    }
                }
            }
            else if (openField == true)
            {
                //Boolean Value to Set Off Code in Update
                thirdLegOpen = true;
                //Instructions
                if (practice)
                {
                    instructions.text = "Second Leg Completed. Please return to where you believe you started. " +
                        "\nWhen you think you have reached your initial position, please press space once.";
                }
                //Deactivate SecondOpenFieldPole
                secondOpenFieldPole.SetActive(false);
            }
        }
    }

    private void Update()
    {
        GameObject player = GameObject.Find("Player");
        TrialManager TM = player.GetComponent<TrialManager>();

        if (TM.left == true)
        {
            if (firstCylinder == true) //After colliding with Second Pole
            {
                if (TM.current == TM.right)
                {
                    if (transform.localEulerAngles.y < 273 && transform.localEulerAngles.y > 267)
                    {
                        if (practice)
                        {
                            //Instructions
                            instructions.text = "Walk forward until you hear a chime. ";
                        }

                        //Sound
                        source.PlayOneShot(leftTurn, 1.0f);

                        //Reset boolean value
                        firstCylinder = false;

                        if (openField == false)
                        {
                            //Destroy Cylinder
                            GameObject[] arg = GameObject.FindGameObjectsWithTag("cylinder");
                            foreach (GameObject go in arg)
                            {
                                Destroy(go);
                            }
                            if (TM.type == TM.right1)
                            {
                                Vector3 posSecond = new Vector3(-1, 3, 33);
                                GameObject newWall2 = (GameObject)Instantiate(longWall, posSecond, Quaternion.Euler(0f, 0f, 0f));
                                newWall2.tag = "wall2";

                                Vector3 posSecond2 = new Vector3(-1, 3, 25);
                                GameObject newWall2R = (GameObject)Instantiate(longWall, posSecond2, Quaternion.Euler(0f, 0f, 0f));
                                newWall2R.tag = "wall2";
                                
                                //Create back wall
                                Vector3 pos3 = new Vector3(3, 3, 29);
                                GameObject newBackWall2 = (GameObject)Instantiate(singleWall, pos3, Quaternion.Euler(0f, 90f, 00f));
                                newBackWall2.tag = "wall2";
                            }
                            else if (TM.type == TM.right2)
                            {

                                Vector3 posSecond = new Vector3(-1, 3, 65);
                                GameObject newWall2 = (GameObject)Instantiate(longWall, posSecond, Quaternion.Euler(0f, 0f, 0f));
                                newWall2.tag = "wall2";

                                Vector3 posSecond2 = new Vector3(-1, 3, 73);
                                GameObject newWall2R = (GameObject)Instantiate(longWall, posSecond2, Quaternion.Euler(0f, 0f, 0f));
                                newWall2R.tag = "wall2";

                                //Create back wall
                                Vector3 pos3 = new Vector3(3, 3, 69);
                                GameObject newBackWall2 = (GameObject)Instantiate(singleWall, pos3, Quaternion.Euler(0f, 90f, 0f));
                                newBackWall2.tag = "wall2";
                                
                            }
                        }
                        else if (TM.openField == true)
                        {
                            //Deactivate SecondOpenFieldPole (again)
                            secondOpenFieldPole.SetActive(true);
                            //Set Third Pole to Active
                            ThirdPole.SetActive(true);
                        }
                    }
                }
                else if (TM.current == TM.acute)
                {
                    if (transform.localEulerAngles.y < 243 && transform.localEulerAngles.y > 237)
                    {
                        source.PlayOneShot(leftTurn, 1.0f);
                        if (practice)
                        {
                            instructions.text = "Walk forward until you hear a chime. ";
                        }
                       
                        firstCylinder = false;

                        if (openField == false)
                        {
                            //Destroy Cylinder
                            GameObject[] arg = GameObject.FindGameObjectsWithTag("cylinder");
                            foreach (GameObject go in arg)
                            {
                                Destroy(go);
                            }   
                            if (TM.type == TM.acute1)
                            {
                                //Create hallway
                                
                                    Vector3 posSecond = new Vector3(-2.53f, 3, 51.6f);
                                    GameObject newWall2 = (GameObject)Instantiate(doubleHallway, posSecond, Quaternion.Euler(0f, 150f, 180f));
                                    newWall2.tag = "wall2";
                                
                                //Create back wall
                                Vector3 pos3 = new Vector3(3, 3, 49);
                                GameObject newBackWall2 = (GameObject)Instantiate(singleWall, pos3, Quaternion.Euler(0f, 240f, 90f));
                                newBackWall2.tag = "wall2";
                            }
                            else if (TM.type == TM.acute2)
                            {
                                Vector3 posSecond = new Vector3(0, 3, 54.5f);
                                GameObject newWall2 = (GameObject)Instantiate(doubleHallway, posSecond, Quaternion.Euler(0f, 150f, 180f));
                                newWall2.tag = "wall2";

                                //Create back wall
                                Vector3 pos3 = new Vector3(3, 3, 49);
                                GameObject newBackWall2 = (GameObject)Instantiate(singleWall, pos3, Quaternion.Euler(0f, 240f, 0f));
                                newBackWall2.tag = "wall2";
                            }
                        }
                        else if (TM.openField == true)
                        {
                            secondOpenFieldPole.SetActive(true);
                            ThirdPole.SetActive(true);
                        }
                    }
                }

                else if (TM.current == TM.obtuse) 
                {
                    if (transform.localEulerAngles.y < 303 && transform.localEulerAngles.y > 297)
                    {
                        source.PlayOneShot(leftTurn, 1.0f);
                        if (practice)
                        {
                            instructions.text = "Walk forward until you hear a chime. ";
                        }
                        
                        firstCylinder = false;

                        if (TM.openField == false)
                        {
                            //Destroy Cylinder
                            GameObject[] arg = GameObject.FindGameObjectsWithTag("cylinder");
                            foreach (GameObject go in arg)
                            {
                                Destroy(go);
                            }

                            if (TM.type == TM.obtuse1)
                            {
                                //Create hallway
                                
                                    Vector3 posSecond = new Vector3(-2.2f, 3,  26.5f);
                                    GameObject newWall2 = (GameObject)Instantiate(doubleHallway, posSecond, Quaternion.Euler(0f, 30f, 0f));
                                    newWall2.tag = "wall2";
                                


                                //Create back wall
                                Vector3 pos3 = new Vector3(3, 3, 29);
                                GameObject newBackWall2 = (GameObject)Instantiate(singleWall, pos3, Quaternion.Euler(0f, -60f, 0f));
                                newBackWall2.tag = "wall2";
                                
                            }
                            else if (TM.type == TM.obtuse2)
                            {
                                //Create hallway
                                
                                    Vector3 posSecond = new Vector3(3.3f, 3, 63.4f);
                                    GameObject newWall2 = (GameObject)Instantiate(doubleHallway, posSecond, Quaternion.Euler(0f, 30f, 0f));
                                    newWall2.tag = "wall2";
                                

                                //Create back wall
                                Vector3 pos3 = new Vector3(3, 3, 69);
                                GameObject newBackWall2 = (GameObject)Instantiate(singleWall, pos3, Quaternion.Euler(0f, -60f, 90f));
                                newBackWall2.tag = "wall2";

                            }
                        }
                        else if (TM.openField == true)
                        {
                            secondOpenFieldPole.SetActive(true);
                            ThirdPole.SetActive(true);
                        }
                    }
                }       
            }
        }
        else if (TM.left == false)
        {
            if (firstCylinder == true)
            {
                if (TM.current == TM.right)
                {
                    if (transform.localEulerAngles.y > 87 && transform.localEulerAngles.y < 93)
                    {
                        if (practice)
                        {
                            instructions.text = "Walk forward until you hear a chime. ";
                        }
                        source.PlayOneShot(rightTurn, 1.0f);
                        firstCylinder = false;

                        if (openField == false)
                        {
                            //Destroy Cylinder
                            GameObject[] arg = GameObject.FindGameObjectsWithTag("cylinder");
                            foreach (GameObject go in arg)
                            {
                                Destroy(go);
                            }

                            if (TM.type == TM.right1)
                            {
                                Vector3 posSecond = new Vector3(-1, 3, 33);
                                GameObject newWall2 = (GameObject)Instantiate(longWall, posSecond, Quaternion.Euler(0f, 180f, 0f));
                                newWall2.tag = "wall2R";

                                Vector3 posSecond2 = new Vector3(-1, 3, 25);
                                GameObject newWall2R = (GameObject)Instantiate(longWall, posSecond2, Quaternion.Euler(0f, 180f, 0f));
                                newWall2R.tag = "wall2R";

                                //Create back wall
                                Vector3 pos3 = new Vector3(-6, 3, 29);
                                GameObject newBackWall2 = (GameObject)Instantiate(singleWall, pos3, Quaternion.Euler(0f, 90f, 0f));
                                newBackWall2.tag = "wall2R";

                                
                            }
                            else if (TM.type == TM.right2)
                            {
                                //Create hallway
                                
                                    Vector3 posSecond = new Vector3(150, 3, 65);
                                    GameObject newWall2R = (GameObject)Instantiate(doubleHallway, posSecond, Quaternion.Euler(0f, 0f, 0f));
                                    newWall2R.tag = "wall2R";
                                

                                //Create back wall
                                Vector3 pos3 = new Vector3(-3, 3, 69);
                                GameObject newBackWall2R = (GameObject)Instantiate(singleWall, pos3, Quaternion.Euler(0f, 90f, 0f));
                                newBackWall2R.tag = "wall2R";
                                
                            }
                        }
                        else if (TM.openField == true)
                        {
                            secondOpenFieldPole.SetActive(true);
                            ThirdPole.SetActive(true);
                        }
                    }
                }
                else if (TM.current == TM.acute)
                {
                    if (transform.localEulerAngles.y > 117 && transform.localEulerAngles.y < 123)
                    {
                        if (practice)
                        {
                            instructions.text = "Walk forward until you hear a chime. ";
                        }
                        source.PlayOneShot(rightTurn, 1.0f);
                        firstCylinder = false;

                        if (openField == false)
                        {
                            //Destroy Cylinder
                            GameObject[] arg = GameObject.FindGameObjectsWithTag("cylinder");
                            foreach (GameObject go in arg)
                            {
                                Destroy(go);
                            }

                            if (TM.type == TM.acute1)
                            {
                                //Create hallway
                                
                                    Vector3 posSecond = new Vector3(-3,  3, 42.7f);
                                    GameObject newWall2R = (GameObject)Instantiate(doubleHallway, posSecond, Quaternion.Euler(0f, 30f, 180f));
                                    newWall2R.tag = "wall2R";
                                

                                //Create back wall
                                Vector3 pos3 = new Vector3(-3, 3, 49);
                                GameObject newBackWall2R = (GameObject)Instantiate(singleWall, pos3, Quaternion.Euler(0f, 120f, 0f));
                                newBackWall2R.tag = "wall2R";
                                
                            }
                            else if (TM.type == TM.acute2)
                            {
                                //Create hallway 
                                
                                    Vector3 posSecond = new Vector3(-3, 3, 43.9f);
                                    GameObject newWall2R = (GameObject)Instantiate(doubleHallway, posSecond, Quaternion.Euler(0f, 30f, 180f));
                                    newWall2R.tag = "wall2R";
                                

                                

                                //Create back wall
                                Vector3 pos3 = new Vector3(-3, 3, 49);
                                GameObject newBackWall2R = (GameObject)Instantiate(singleWall, pos3, Quaternion.Euler(0f, 120f, 0f));
                                newBackWall2R.tag = "wall2R";
                                
                            }
                        }
                        else if (openField == true)
                        {
                            secondOpenFieldPole.SetActive(true);
                            ThirdPole.SetActive(true);
                        }
                    }
                }
                else if (TM.current == TM.obtuse)
                {
                    if (transform.localEulerAngles.y > 57 && transform.localEulerAngles.y < 63) 
                    {
                        if (practice)
                        {
                            instructions.text = "Walk forward until you hear a chime. ";
                        }
                        source.PlayOneShot(rightTurn, 1.0f);
                        firstCylinder = false;

                        if (openField == false)
                        {
                            //Destroy Cylinder
                            GameObject[] arg = GameObject.FindGameObjectsWithTag("cylinder");
                            foreach (GameObject go in arg)
                            {
                                Destroy(go);
                            }

                            if (TM.type == TM.obtuse1)
                            {
                                
                                //Create hallway
                                Vector3 posSecond2 = new Vector3(-3.5f, 3, 33.8f);
                                GameObject newWall2R = (GameObject)Instantiate(doubleHallway, posSecond2, Quaternion.Euler(0f, 150f, 0f));
                                newWall2R.tag = "wall2R";


                                //Create back wall
                                Vector3 pos3 = new Vector3(-4.18f, 3, 27.88f);
                                GameObject newBackWall2 = (GameObject)Instantiate(singleWall, pos3, Quaternion.Euler(0f, 60f, 0f));
                                newBackWall2.tag = "wall2R";
                            }
                            else if (TM.type == TM.obtuse2)
                            {
                                //Create hallway
                                
                                    Vector3 posSecond = new Vector3(-2.9f, 3, 74.1f);
                                    GameObject newWall2R = (GameObject)Instantiate(doubleHallway, posSecond, Quaternion.Euler(0f, 160f, 0f));
                                    newWall2R.tag = "wall2R";
                                

                                //Create back wall
                                Vector3 pos3 = new Vector3(-4, 3, 69);
                                GameObject newBackWall2R = (GameObject)Instantiate(singleWall, pos3, Quaternion.Euler(0f, 60f, 0f));
                                newBackWall2R.tag = "wall2R";
                                
                            }
                        }
                        else if (TM.openField == true)
                        {
                            secondOpenFieldPole.SetActive(true);
                            ThirdPole.SetActive(true);
                        }
                    }
                }
            }  
        }

        if (secondCylinder == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                numSpace++;
            }

            if (numSpace >= 2)
            {
                //Restart another trial
                startingPole.SetActive(true);
                firstCylinder = false;
                secondCylinder = false;
                source.Stop();
                numSpace = 0;
                trialStart = false;
                if (practice)
                {
                    instructions.text = "Please walk back to the starting pole. ";
                }
            }
        }

        //FOR OPEN FIELD VARIANT
        if (thirdLegOpen == true) 
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //Resetting boolean variables
                thirdLegOpen = false;
                openField = false;
                firstCylinder = false;
                secondCylinder = false;
                source.Stop();
                numSpace = 0;

                startingPole.SetActive(true);

                if (practice)
                {
                    //Instructions
                    instructions.text = "Please walk back to the starting cone.";
                }


                trialStart = false;
            }
        }   
    }
}