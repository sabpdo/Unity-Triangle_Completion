using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Main : MonoBehaviour
{
    //instructions
    public Text instructions;

    //To set off values in update
    private bool firstCylinder = false;
    private bool secondCylinder = false;
    private bool thirdLegOpen = false;

    //Variant values
    public const int max = 4; //Max each triangle is shown

    public bool left; //left handed triangle

    private const int right = 1; //for right triangle
    private const int right1 = 105; //2,6,90
    private const int right2 = 106; //6,2,90
    private int leftRightNumber1 = 0;
    private int leftRightNumber2 = 0;
    private int rightRightNumber1 = 0;
    private int rightRightNumber2 = 0;

    private int RightNumber = 0;

    private const int acute = 2;
    private const int acute1 = 202; //For variants of the different acute triangles //4,2,60
    private const int acute2 = 203; //4,6,60
    private int leftAcuteNumber1 = 0;
    private int leftAcuteNumber2 = 0;
    private int rightAcuteNumber1 = 0;
    private int rightAcuteNumber2 = 0;

    private int AcuteNumber = 0;

    private const int obtuse = 3;
    private const int obtuse1 = 305; //2,4,120
    private const int obtuse2 = 306; //6,4,120
    private int leftObtuseNumber1 = 0;
    private int leftObtuseNumber2 = 0;
    private int rightObtuseNumber1 = 0;
    private int rightObtuseNumber2 = 0;

    private int ObtuseNumber = 0;

    //For Open Field - if open Field or not
    public bool openField; 

    //Keep track of current type of triangle
    public int current;
    public int type;

    //Track number of User Pressed Spaces
    private int numSpace = 0;

    //All Main GameObjects
    public GameObject wall;
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
    public AudioClip sound;
    
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
        wall.SetActive(true);
        openFieldPole.SetActive(false);
        secondOpenFieldPole.SetActive(false);

        //Determine left or right handed triangle first
        int leftOrRight = Random.Range(0,2);
        if (leftOrRight == 0)
            left = true;
        else if (leftOrRight ==1)
            left = false;

        /*Determine if initially open field or not
        int openFieldOrNot = Random.Range(0, 2);
        if (openFieldOrNot == 0)
            openField = true;
        else if (openFieldOrNot == 1)
            openField = false;*/

        //Determine starting type triangle
        int typeTriangleStart = Random.Range(1, 4);
        int lengthTriangleStart = Random.Range(0, 2);
        if (typeTriangleStart == right)
        {
            current = right;
            if (lengthTriangleStart == 0)
                type = right1;
            else
                type = right2;
        }
        else if (typeTriangleStart == acute)
        {
            current = acute;
            if (lengthTriangleStart == 0)
                type = acute1;
            else
                type = acute2;
        }
        else if (typeTriangleStart == obtuse)
        {
            current = obtuse;
            if (lengthTriangleStart == 0)
                type = obtuse1;
            else
                type = obtuse2;
        }
        //Instructions
        instructions.text = "Please walk forward.";
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Upon the collision of the First Pole
        if (collision.gameObject.CompareTag("StartPole"))
        {
            //Moving player to original position
            transform.position = new Vector3(0f, 0f, 9f);

            //Deactivate the First Starting Pole and SetActive the Second Pole
            startingPole.SetActive(false); 
            SecondPole.SetActive(true);

            //Move SecondPole to appropriate place
            if (current == right)
            {
                if (type == right1)
                {
                    SecondPole.transform.position = new Vector3(0f, -0.5f, 29f);
                    SecondPole.SetActive(true);
                }
                else if (type == right2)
                {
                    SecondPole.transform.position = new Vector3(0f, -0.5f, 69f);
                    SecondPole.SetActive(true);
                }
            }
            else if (current == acute)
            {
                if (type == acute1)
                {
                    SecondPole.transform.position = new Vector3(0f, -0.5f, 49f);
                    SecondPole.SetActive(true);
                }
                else if (type == acute2)
                {
                    SecondPole.transform.position = new Vector3(0f, -0.5f, 49f);
                    SecondPole.SetActive(true);
                }
            }
            else if (current == obtuse)
            {
                if (type == obtuse1)
                {
                    SecondPole.transform.position = new Vector3(0f, -0.5f, 29f);
                    SecondPole.SetActive(true);
                }
                else if (type == obtuse2)
                {
                    SecondPole.transform.position = new Vector3(0f, -0.5f, 69f);
                    SecondPole.SetActive(true);
                }
            }

            //If Open Field
            if (openField == false)
            {
                //Instructions
                instructions.text = "Walk forward in the corridor until you hear a chime " +
                    "\n and are located in a cylinder.";

                //Left handed triangle
                if (left == true) 
                {
                    //First Hallway Right Side
                    for (int x = 1; x <= 15; x++)
                    {
                        Vector3 pos = new Vector3(3, 3, (10 * x));
                        GameObject newWall = (GameObject)Instantiate(wall, pos, Quaternion.Euler(90.0f, 0.0f, 90.0f));
                        newWall.tag = "wall1";
                    }

                    //First Hallway Left Side
                    for (int x = 1; x <= 15; x++)
                    {
                        Vector3 pos = new Vector3(-3, 3, (10 * x));
                        GameObject newWall2 = (GameObject)Instantiate(wall, pos, Quaternion.Euler(90.0f, 180.0f, 90.0f));
                        newWall2.tag = "wall1";
                    }

                    //First Hallway Back Side
                    Vector3 pos3 = new Vector3(0, 3, 5);
                    GameObject newBackWall = (GameObject)Instantiate(wall, pos3, Quaternion.Euler(0.0f, 90.0f, 90.0f));
                    newBackWall.tag = "wall1";
                }
                //Right handed Triangle
                else if (left == false)
                {
                    //First Hallway Right Side
                    for (int x = 1; x <= 15; x++)
                    {
                        Vector3 pos = new Vector3(3, 3, (10 * x));
                        GameObject newWallR = (GameObject)Instantiate(wall, pos, Quaternion.Euler(90.0f, 0.0f, 90.0f));
                        newWallR.tag = "wall1R";
                    }

                    //First Hallway Left Side
                    for (int x = 1; x <= 15; x++)
                    {
                        Vector3 pos = new Vector3(-3, 3, (10 * x));
                        GameObject newWall2R = (GameObject)Instantiate(wall, pos, Quaternion.Euler(90.0f, 180.0f, 90.0f));
                        newWall2R.tag = "wall1R";
                    }

                    //First Hallway Back Side
                    Vector3 pos3 = new Vector3(0, 3, 5);
                    GameObject newBackWallR = (GameObject)Instantiate(wall, pos3, Quaternion.Euler(0.0f, 90.0f, 90.0f));
                    newBackWallR.tag = "wall1R";
                }
            }
            //If Open Field
            else if (openField == true)
            {
                openFieldPole.SetActive(true);
                instructions.text = "Walk forward until you hear a chime.";
            }
        }

        //Upon Collision of Second Pole
        if (collision.gameObject.CompareTag("SecondPole"))
        {
            //Set Off Boolean Value in Update
            firstCylinder = true;

            //Instructions
            instructions.text = "First Leg Completed. Please turn until you hear a second chime.";

            //Sound
            source.PlayOneShot(sound, 1.0f);

            //Deactivate SecondPole
            SecondPole.SetActive(false);

            //Right Handed Triangle - Code to Situate ThirdPole+SecondOpenFieldPole properly
            if (left == false)
            {
                if (current == right)
                {
                    if (type == right1)
                    {
                        ThirdPole.transform.position = new Vector3(60f, 0f, 29f);
                        ThirdPole.SetActive(true);
                        secondOpenFieldPole.transform.position = new Vector3(100f, 3f, 29f);
                    }
                    else if (type == right2)
                    {
                        ThirdPole.transform.position = new Vector3(40f, 0f, 69f);
                        ThirdPole.SetActive(true);
                        secondOpenFieldPole.transform.position = new Vector3(100f, 3f, 69f);
                    }
                }
                else if (current == acute)
                {
                    if (type == acute1)
                    {
                        ThirdPole.transform.position = new Vector3(17.32f, 0f, 39f);
                        ThirdPole.SetActive(true);
                        secondOpenFieldPole.transform.position = new Vector3(86.6f, 3f, -1f);
                    }
                    else if (type == acute2)
                    {
                        ThirdPole.transform.position = new Vector3(51.96f, 0f, 19f);
                        ThirdPole.SetActive(true);
                        secondOpenFieldPole.transform.position = new Vector3(86.6f, 3f, -1f);
                    }
                }
                else if (current == obtuse)
                {
                    if (type == obtuse1)
                    {
                        ThirdPole.transform.position = new Vector3(20 * Mathf.Sqrt(3), 0f, 49f);
                        ThirdPole.SetActive(true);
                        secondOpenFieldPole.transform.position = new Vector3(86.6f, 3f, 83f);
                    }
                    else if (type == obtuse2)
                    {
                        ThirdPole.transform.position = new Vector3(10 * Mathf.Sqrt(3), 0f, 79f);
                        ThirdPole.SetActive(true);
                        secondOpenFieldPole.transform.position = new Vector3(86.6f, 3f, 123f);
                    }
                }
            }
            //Left Handed Triangle
            else if (left == true)
            {
                if (current == right)
                {
                    if (type == right1)
                    {
                        ThirdPole.transform.position = new Vector3(-60f, 0f, 29f);
                        ThirdPole.SetActive(true);
                        secondOpenFieldPole.transform.position = new Vector3(-100f, 3f, 29f);
                    }
                    else if (type == right2)
                    {
                        ThirdPole.transform.position = new Vector3(-40f, 0f, 69f);
                        ThirdPole.SetActive(true);
                        secondOpenFieldPole.transform.position = new Vector3(-100f, 3f, 29f);
                    }
                }
                else if (current == acute)
                {
                    if (type == acute1)
                    {
                        ThirdPole.transform.position = new Vector3(-17.32f, 0f, 39f);
                        ThirdPole.SetActive(true);
                        secondOpenFieldPole.transform.position = new Vector3(-86.6f, 3f, -1f);
                    }
                    else if (type == acute2)
                    {
                        ThirdPole.transform.position = new Vector3(-51.96f, 0f, 19f);
                        ThirdPole.SetActive(true);
                        secondOpenFieldPole.transform.position = new Vector3(-86.6f, 3f, -1f);
                    }
                }
                else if (current == obtuse)
                {
                    if (type == obtuse1)
                    {
                        ThirdPole.transform.position = new Vector3(-20*Mathf.Sqrt(3), 0f, 49f);
                        ThirdPole.SetActive(true);
                        secondOpenFieldPole.transform.position = new Vector3(-86.6f, 3f, 83f);
                    }
                    else if (type == obtuse2)
                    {
                        ThirdPole.transform.position = new Vector3(-10*Mathf.Sqrt(3), 0f, 79f);
                        ThirdPole.SetActive(true);
                        secondOpenFieldPole.transform.position = new Vector3(-86.6f, 3f, 123f);
                    }
                } 
            }

            //If non-Open Field
            if (openField == false)
            {
                //Create the cylinder container
                Instantiate(cylinder);
                cylinder.tag = "cylinder";

                //Move cylinder to proper place depending on triangle
                if (current == right)
                {
                    if (type == right1)
                    {
                        cylinder.transform.position = new Vector3(0f, 0f, 29f);

                    }
                    else if (type == right2)
                    {
                        cylinder.transform.position = new Vector3(0f, 0f, 69f);
                    }
                }
                else if (current == acute)
                {
                    if (type == acute1)
                    {
                        cylinder.transform.position = new Vector3(0f, 0f, 49f);
                    }
                    else if (type == acute2)
                    {
                        cylinder.transform.position = new Vector3(0f, 0f, 49f);
                    }
                }
                else if (current == obtuse)
                {
                    if (type == obtuse1)
                    {
                        cylinder.transform.position = new Vector3(0f, -0.5f, 29f);
                    }
                    else if (type == obtuse2)
                    {
                        cylinder.transform.position = new Vector3(0f, -0.5f, 69f);
                    }
                }

                //Destroy walls if non-Open Field
                if (left == true)
                {
                    //Destroy previous hallway, second pole indicator
                    GameObject[] argo = GameObject.FindGameObjectsWithTag("wall1");
                    foreach (GameObject go in argo)
                    {
                        go.SetActive(false);
                    }
                }
                else if (left == false)
                {
                    //Destroy previous hallway, deactivate second pole indicator
                    GameObject[] argo = GameObject.FindGameObjectsWithTag("wall1R");
                    foreach (GameObject go in argo)
                    {
                        Destroy(go);
                    }
                }
            }
            else if (openField == true)
            {
                //Deactivate first OpenFieldPole
                openFieldPole.SetActive(false);
            }
        }

        //Upon the collision of the Third Pole
        if (collision.gameObject.CompareTag("ThirdPole"))
        {
            //Deactivate Third Pole indicator
            ThirdPole.SetActive(false);

            //Sound
            source.PlayOneShot(sound, 1.0f);
            
            if (openField == false)
            {
                //instructions
                instructions.text = "Second Leg Completed. Please turn until you believe you " +
                    "\n are facing the correct direction of where you started. When you are at the " +
                    "\n desired turn angle, press space once and another hallway will appear. " +
                    "\n Travel down the hallway and press space once when you believe you are " +
                    "\n at the starting location.";

                //Second Cylinder boolean value for update
                secondCylinder = true;

                //Code to destroy Previous Walls
                if (left == true)
                {
                    GameObject[] args = GameObject.FindGameObjectsWithTag("wall2");
                    foreach (GameObject go in args)
                    {
                        Destroy(go);
                    }
                }
                else if (left == false)
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
                instructions.text = "Second Leg Completed. Please return to where you believe you started. " +
                    "\nWhen you think you have reached your initial position, please press space once.";
                //Deactivate SecondOpenFieldPole
                secondOpenFieldPole.SetActive(false);
            }
        }
    }

    private void Update()
    {
        if (left == true)
        {
            if (firstCylinder == true) //After colliding with Second Pole
            {
                if (current == right)
                {
                    if (transform.localEulerAngles.y < 273 && transform.localEulerAngles.y > 267)
                    {
                        //Instructions
                        instructions.text = "Walk forward until you hear a chime. ";

                        //Sound
                        source.PlayOneShot(sound, 1.0f);

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
                            if (type == right1)
                            {
                                //Create hallway
                                for (int x = 1; x < 20; x++)
                                {
                                    Vector3 posSecond = new Vector3(7 + (-8 * x), 3, 33);
                                    GameObject newWall2 = (GameObject)Instantiate(wall, posSecond, Quaternion.Euler(0f, 270f, 90f));
                                    newWall2.tag = "wall2";
                                }

                                for (int x = 1; x < 20; x++)
                                {
                                    Vector3 posSecond2 = new Vector3(7 + (-8 * x), 3, 25);
                                    GameObject newWall2R = (GameObject)Instantiate(wall, posSecond2, Quaternion.Euler(0f, 270f, 90f));
                                    newWall2R.tag = "wall2";
                                }

                                //Create back wall
                                Vector3 pos3 = new Vector3(3, 3, 29);
                                GameObject newBackWall2 = (GameObject)Instantiate(wall, pos3, Quaternion.Euler(90f, 0f, 90f));
                                newBackWall2.tag = "wall2";
                            }
                            else if (type == right2)
                            {
                                //Create hallway
                                for (int x = 1; x < 20; x++)
                                {
                                    Vector3 posSecond = new Vector3(7 + (-8 * x), 3, 65);
                                    GameObject newWall2 = (GameObject)Instantiate(wall, posSecond, Quaternion.Euler(0f, 270f, 90f));
                                    newWall2.tag = "wall2";
                                }

                                for (int x = 1; x < 20; x++)
                                {
                                    Vector3 posSecond2 = new Vector3(7 + (-8 * x), 3, 73);
                                    GameObject newWall2R = (GameObject)Instantiate(wall, posSecond2, Quaternion.Euler(0f, 270f, 90f));
                                    newWall2R.tag = "wall2";
                                }

                                //Create back wall
                                Vector3 pos3 = new Vector3(3, 3, 69);
                                GameObject newBackWall2 = (GameObject)Instantiate(wall, pos3, Quaternion.Euler(90f, 180f, 90f));
                                newBackWall2.tag = "wall2";
            
                            }
                        }
                        else if (openField == true)
                        {
                            //Deactivate SecondOpenFieldPole (again)
                            secondOpenFieldPole.SetActive(true);
                            //Set Third Pole to Active
                            ThirdPole.SetActive(true);
                        }
                    }
                }
                else if (current == acute)
                {
                    if (transform.localEulerAngles.y < 243 && transform.localEulerAngles.y > 237)
                    {
                        instructions.text = "Walk forward until you hear a chime. ";
                        source.PlayOneShot(sound, 1.0f);
                        firstCylinder = false;

                        if (openField == false)
                        {
                            //Destroy Cylinder
                            GameObject[] arg = GameObject.FindGameObjectsWithTag("cylinder");
                            foreach (GameObject go in arg)
                            {
                                Destroy(go);
                            }   
                            if (type == acute1)
                            {
                                //Create hallway
                                for (int x = 0; x < 20; x++)
                                {
                                    Vector3 posSecond = new Vector3((7 + (x * -5 * Mathf.Sqrt(3))), 3, 55 + (x*-5));
                                    GameObject newWall2 = (GameObject)Instantiate(wall, posSecond, Quaternion.Euler(0f, 240f, 90f));
                                    newWall2.tag = "wall2";
                                }
                                for (int x = 0; x < 20; x++)
                                {
                                    Vector3 posSecond2 = new Vector3((7 + (x * -5 * Mathf.Sqrt(3))), 3, 46 + (x * -5));
                                    GameObject newWall2R = (GameObject)Instantiate(wall, posSecond2, Quaternion.Euler(0f, 240f, 90f));
                                    newWall2R.tag = "wall2";
                                }
                                //Create back wall
                                Vector3 pos3 = new Vector3(3, 3, 49);
                                GameObject newBackWall2 = (GameObject)Instantiate(wall, pos3, Quaternion.Euler(90f, 150f, 90f));
                                newBackWall2.tag = "wall2";
                            }
                            else if (type == acute2)
                            {
                                //Create hallway
                                for (int x = 0; x < 20; x++)
                                {
                                    Vector3 posSecond = new Vector3((x * -5 * Mathf.Sqrt(3)), 3, 52 + (x * -5));
                                    GameObject newWall2 = (GameObject)Instantiate(wall, posSecond, Quaternion.Euler(0f, 240f, 90f));
                                    newWall2.tag = "wall2";
                                }
                                for (int x = 0; x < 20; x++)
                                {
                                    Vector3 posSecond2 = new Vector3((x * -5 * Mathf.Sqrt(3)), 3, 46 + (x * -5));
                                    GameObject newWall2R = (GameObject)Instantiate(wall, posSecond2, Quaternion.Euler(0f, 240f, 90f));
                                    newWall2R.tag = "wall2";
                                }
                                //Create back wall
                                Vector3 pos3 = new Vector3(3, 3, 49);
                                GameObject newBackWall2 = (GameObject)Instantiate(wall, pos3, Quaternion.Euler(90f, 150f, 90f));
                                newBackWall2.tag = "wall2";
                            }
                        }
                        else if (openField == true)
                        {
                            secondOpenFieldPole.SetActive(true);
                            ThirdPole.SetActive(true);
                        }
                    }
                }

                else if (current == obtuse) 
                {
                    if (transform.localEulerAngles.y < 303 && transform.localEulerAngles.y > 297)
                    {
                        instructions.text = "Walk forward until you hear a chime. ";
                        source.PlayOneShot(sound, 1.0f);
                        firstCylinder = false;

                        if (openField == false)
                        {
                            //Destroy Cylinder
                            GameObject[] arg = GameObject.FindGameObjectsWithTag("cylinder");
                            foreach (GameObject go in arg)
                            {
                                Destroy(go);
                            }

                            if (type == obtuse1)
                            {
                                //Create hallway
                                for (int x = 0; x < 20; x++)
                                {
                                    Vector3 posSecond = new Vector3(((x * -5 * Mathf.Sqrt(3))), 3, 33 + (x*5));
                                    GameObject newWall2 = (GameObject)Instantiate(wall, posSecond, Quaternion.Euler(0f, 300f, 90f));
                                    newWall2.tag = "wall2";
                                }

                                for (int x = 0; x < 20; x++)
                                {
                                    Vector3 posSecond2 = new Vector3(((x * -5 * Mathf.Sqrt(3))), 3, 25 + (x * 5));
                                    GameObject newWall2R = (GameObject)Instantiate(wall, posSecond2, Quaternion.Euler(0f, 300f, 90f));
                                    newWall2R.tag = "wall2";
                                }


                                //Create back wall
                                Vector3 pos3 = new Vector3(3, 3, 29);
                                GameObject newBackWall2 = (GameObject)Instantiate(wall, pos3, Quaternion.Euler(90f, 210f, 90f));
                                newBackWall2.tag = "wall2";
                                source.Stop();
                            }
                            else if (type == obtuse2)
                            {
                                //Create hallway
                                for (int x = 0; x < 20; x++)
                                {
                                    Vector3 posSecond = new Vector3(((x * -5 * Mathf.Sqrt(3))), 3, 73 + (x * 5));
                                    GameObject newWall2 = (GameObject)Instantiate(wall, posSecond, Quaternion.Euler(0f, 300f, 90f));
                                    newWall2.tag = "wall2";
                                }

                                for (int x = 0; x < 20; x++)
                                {
                                    Vector3 posSecond2 = new Vector3(((x * -5 * Mathf.Sqrt(3))), 3, 65 + (x * 5));
                                    GameObject newWall2R = (GameObject)Instantiate(wall, posSecond2, Quaternion.Euler(0f, 300f, 90f));
                                    newWall2R.tag = "wall2";
                                }

                                //Create back wall
                                Vector3 pos3 = new Vector3(3, 3, 69);
                                GameObject newBackWall2 = (GameObject)Instantiate(wall, pos3, Quaternion.Euler(90f, 210f, 90f));
                                newBackWall2.tag = "wall2";
                                source.Stop();
                            }
                        }
                        else if (openField == true)
                        {
                            secondOpenFieldPole.SetActive(true);
                            ThirdPole.SetActive(true);
                        }
                    }
                }       
            }
        }
        else if (left == false)
        {
            if (firstCylinder == true)
            {
                if (current == right)
                {
                    if (transform.localEulerAngles.y > 87 && transform.localEulerAngles.y < 93)
                    {
                        instructions.text = "Walk forward until you hear a chime. ";
                        source.PlayOneShot(sound, 1.0f);
                        firstCylinder = false;

                        if (openField == false)
                        {
                            //Destroy Cylinder
                            GameObject[] arg = GameObject.FindGameObjectsWithTag("cylinder");
                            foreach (GameObject go in arg)
                            {
                                Destroy(go);
                            }

                            if (type == right1)
                            {
                                //Create hallway
                                for (int x = 1; x < 20; x++)
                                {
                                    Vector3 posSecond = new Vector3(-8 + (8 * x), 3, 33);
                                    GameObject newWall2R = (GameObject)Instantiate(wall, posSecond, Quaternion.Euler(0f, 90f, 90f));
                                    newWall2R.tag = "wall2R";
                                }

                                for (int x = 1; x < 20; x++)
                                {
                                    Vector3 posSecond2 = new Vector3(-8 + (8 * x), 3, 25);
                                    GameObject newWall2RR = (GameObject)Instantiate(wall, posSecond2, Quaternion.Euler(0f, 90f, 90f));
                                    newWall2RR.tag = "wall2R";
                                }

                                //Create back wall
                                Vector3 pos3 = new Vector3(-3, 3, 29);
                                GameObject newBackWall2R = (GameObject)Instantiate(wall, pos3, Quaternion.Euler(90f, 0f, 90f));
                                newBackWall2R.tag = "wall2R";
                                source.Stop();
                            }
                            else if (type == right2)
                            {
                                //Create hallway
                                for (int x = 1; x < 20; x++)
                                {
                                    Vector3 posSecond = new Vector3(-8 + (8 * x), 3, 73);
                                    GameObject newWall2R = (GameObject)Instantiate(wall, posSecond, Quaternion.Euler(0f, 90f, 90f));
                                    newWall2R.tag = "wall2R";
                                }

                                for (int x = 1; x < 20; x++)
                                {
                                    Vector3 posSecond2 = new Vector3(-8 + (8 * x), 3, 65);
                                    GameObject newWall2RR = (GameObject)Instantiate(wall, posSecond2, Quaternion.Euler(0f, 90f, 90f));
                                    newWall2RR.tag = "wall2R";
                                }

                                //Create back wall
                                Vector3 pos3 = new Vector3(-3, 3, 69);
                                GameObject newBackWall2R = (GameObject)Instantiate(wall, pos3, Quaternion.Euler(90f, 0f, 90f));
                                newBackWall2R.tag = "wall2R";
                                source.Stop();
                            }
                        }
                        else if (openField == true)
                        {
                            secondOpenFieldPole.SetActive(true);
                            ThirdPole.SetActive(true);
                        }
                    }
                }
                else if (current == acute)
                {
                    if (transform.localEulerAngles.y > 117 && transform.localEulerAngles.y < 123)
                    {
                        instructions.text = "Walk forward until you hear a chime. ";
                        source.PlayOneShot(sound, 1.0f);
                        firstCylinder = false;

                        if (openField == false)
                        {
                            //Destroy Cylinder
                            GameObject[] arg = GameObject.FindGameObjectsWithTag("cylinder");
                            foreach (GameObject go in arg)
                            {
                                Destroy(go);
                            }

                            if (type == acute1)
                            {
                                //Create hallway
                                for (int x = 0; x < 20; x++)
                                {
                                    Vector3 posSecond = new Vector3((x * 5 * Mathf.Sqrt(3)), 3, 46 + (x * -5));
                                    GameObject newWall2R = (GameObject)Instantiate(wall, posSecond, Quaternion.Euler(0f, 120f, 90f));
                                    newWall2R.tag = "wall2R";
                                }

                                for (int x = 0; x < 20; x++)
                                {
                                    Vector3 posSecond2 = new Vector3((x * 5 * Mathf.Sqrt(3)), 3, 52+ (x * -5));
                                    GameObject newWall2RR = (GameObject)Instantiate(wall, posSecond2, Quaternion.Euler(0f, 120f, 90f));
                                    newWall2RR.tag = "wall2R";
                                }

                                //Create back wall
                                Vector3 pos3 = new Vector3(-3, 3, 49);
                                GameObject newBackWall2R = (GameObject)Instantiate(wall, pos3, Quaternion.Euler(90f, 30f, 90f));
                                newBackWall2R.tag = "wall2R";
                                source.Stop();
                            }
                            else if (type == acute2)
                            {
                                //Create hallway 
                                for (int x = 0; x < 20; x++)
                                {
                                    Vector3 posSecond = new Vector3((x * 5 * Mathf.Sqrt(3)), 3, 46 + (x * -5));
                                    GameObject newWall2R = (GameObject)Instantiate(wall, posSecond, Quaternion.Euler(0f, 120f, 90f));
                                    newWall2R.tag = "wall2R";
                                }

                                for (int x = 0; x < 20; x++)
                                {
                                    Vector3 posSecond2 = new Vector3(x * 5 * Mathf.Sqrt(3), 3, 52 + (x * -5));
                                    GameObject newWall2RR = (GameObject)Instantiate(wall, posSecond2, Quaternion.Euler(0f, 120f, 90f));
                                    newWall2RR.tag = "wall2R";
                                }

                                //Create back wall
                                Vector3 pos3 = new Vector3(-3, 3, 49);
                                GameObject newBackWall2R = (GameObject)Instantiate(wall, pos3, Quaternion.Euler(90f, 30f, 90f));
                                newBackWall2R.tag = "wall2R";
                                source.Stop();
                            }
                        }
                        else if (openField == true)
                        {
                            secondOpenFieldPole.SetActive(true);
                            ThirdPole.SetActive(true);
                        }
                    }
                }
                else if (current == obtuse)
                {
                    if (transform.localEulerAngles.y > 57 && transform.localEulerAngles.y < 63) 
                    {
                        instructions.text = "Walk forward until you hear a chime. ";
                        source.PlayOneShot(sound, 1.0f);
                        firstCylinder = false;

                        if (openField == false)
                        {
                            //Destroy Cylinder
                            GameObject[] arg = GameObject.FindGameObjectsWithTag("cylinder");
                            foreach (GameObject go in arg)
                            {
                                Destroy(go);
                            }

                            if (type == obtuse1)
                            {
                                //Create hallway
                                for (int x = 0; x < 20; x++)
                                {
                                    Vector3 posSecond = new Vector3(((x * 5 * Mathf.Sqrt(3))), 3, 33 + (x * 5));
                                    GameObject newWall2R = (GameObject)Instantiate(wall, posSecond, Quaternion.Euler(0f, 60f, 90f));
                                    newWall2R.tag = "wall2R";
                                }

                                for (int x = 0; x < 20; x++)
                                {
                                    Vector3 posSecond2 = new Vector3(((x * 5 * Mathf.Sqrt(3))), 3, 25 + (x * 5));
                                    GameObject newWall2RR = (GameObject)Instantiate(wall, posSecond2, Quaternion.Euler(0f, 60f, 90f));
                                    newWall2RR.tag = "wall2R";
                                }

                                //Create back wall
                                Vector3 pos3 = new Vector3(-3, 3, 29);
                                GameObject newBackWall2R = (GameObject)Instantiate(wall, pos3, Quaternion.Euler(90f, -30f, 90f));
                                newBackWall2R.tag = "wall2R";
                                source.Stop();
                            }
                            else if (type == obtuse2)
                            {
                                //Create hallway
                                for (int x = 0; x < 20; x++)
                                {
                                    Vector3 posSecond = new Vector3(((x * 5 * Mathf.Sqrt(3))), 3, 72 + (x * 5));
                                    GameObject newWall2R = (GameObject)Instantiate(wall, posSecond, Quaternion.Euler(0f, 60f, 90f));
                                    newWall2R.tag = "wall2R";
                                }

                                for (int x = 0; x < 20; x++)
                                {
                                    Vector3 posSecond2 = new Vector3(((x * 5 * Mathf.Sqrt(3))), 3, 66 + (x * 5));
                                    GameObject newWall2RR = (GameObject)Instantiate(wall, posSecond2, Quaternion.Euler(0f, 60f, 90f));
                                    newWall2RR.tag = "wall2R";
                                }

                                //Create back wall
                                Vector3 pos3 = new Vector3(-4, 3, 69);
                                GameObject newBackWall2R = (GameObject)Instantiate(wall, pos3, Quaternion.Euler(90f, -30f, 90f));
                                newBackWall2R.tag = "wall2R";
                                source.Stop();
                            }
                        }
                        else if (openField == true)
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

                //To Keep Track of How Many of Each Trial Completed, Taking Account of Hand, Triangle, and Type
                if (current == right && left == false && type == right1)
                    rightRightNumber1++;
                else if (current == right && left == true && type == right1)
                    leftRightNumber1++;
                else if (current == right && left == false && type == right2)
                    rightRightNumber2++;
                else if (current == right && left == true && type == right2)
                    leftRightNumber2++;
                else if (current == acute && left == false && type == acute1)
                    rightAcuteNumber1++;
                else if (current == acute && left == true && type == acute1)
                    leftAcuteNumber1++;
                else if (current == acute && left == false && type == acute2)
                    rightAcuteNumber2++;
                else if (current == acute && left == true && type == acute2)
                    leftAcuteNumber2++;
                else if (current == obtuse && left == false && type == obtuse1)
                    rightObtuseNumber1++;
                else if (current == obtuse && left == true && type == obtuse1)
                    leftObtuseNumber1++;
                else if (current == obtuse && left == false && type == obtuse2)
                    rightObtuseNumber2++;
                else if (current == obtuse && left == true && type == obtuse2)
                    leftObtuseNumber2++;

                //To alternate left and right handed triangles
                if (left == false)
                    left = true;
                else
                    left = false;

                //Overall counter for Triangle
                if (current == right)
                    RightNumber++;
                else if (current == obtuse)
                    ObtuseNumber++;
                else if (current == acute)
                    AcuteNumber++;

                //Instructions
                instructions.text = "Please walk back to the starting cone.";

                //Determine starting type triangle
                int previous = type;
                while (previous==type)
                {
                    int typeTriangle = Random.Range(1, 4);

                    if (typeTriangle == right && RightNumber < 16)
                    {
                        int variant = Random.Range(105, 107);
                        current = right;
                        if (variant == right1 && left == true && leftRightNumber1 < 4)
                            type = right1;
                        else if (variant == right2 && left == true && leftRightNumber2 < 4)
                            type = right2;
                        else if (variant == right1 && left == false && rightRightNumber1 < 4)
                            type = right1;
                        else if (variant == right2 && left == false && rightRightNumber2 < 4)
                            type = right2;
                        
                    }
                    else if (typeTriangle == acute && AcuteNumber<16)
                    {
                        int variant = Random.Range(202,204);
                        current = acute;
                        if (variant == acute1 && left == true && leftAcuteNumber1 < 4)
                            type = acute1;
                        else if (variant == acute2 && left == true && leftAcuteNumber2 < 4)
                            type = acute2;
                        else if (variant == acute1 && left == false && rightAcuteNumber1 < 4)
                            type = acute1;
                        else if (variant == acute2 && left == false && rightAcuteNumber2 < 4)
                            type = acute2;
                    }
                    else if (typeTriangle == obtuse && ObtuseNumber<16)
                    {
                        int variant = Random.Range(305, 307);
                        current = obtuse;
                        if (variant == obtuse1 && left == true && leftObtuseNumber1 < 4)
                            type = obtuse1;
                        else if (variant == obtuse2 && left == true && leftObtuseNumber2 < 4)
                            type = obtuse2;
                        else if (variant == obtuse1 && left == false && rightObtuseNumber1 < 4)
                            type = obtuse1;
                        else if (variant == obtuse2 && left == false && rightObtuseNumber2 < 4)
                            type = obtuse2;
                    }
                    else
                    {
                        typeTriangle = Random.Range(1, 4);
                    }
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

                //Instructions
                instructions.text = "Please walk back to the starting cone.";

                //To Keep Track of How Many of Each Trial Completed, Taking Account of Hand, Triangle, and Type
                if (current == right && left == false && type == right1)
                    rightRightNumber1++;
                else if (current == right && left == true && type == right1)
                    leftRightNumber1++;
                else if (current == right && left == false && type == right2)
                    rightRightNumber2++;
                else if (current == right && left == true && type == right2)
                    leftRightNumber2++;
                else if (current == acute && left == false && type == acute1)
                    rightAcuteNumber1++;
                else if (current == acute && left == true && type == acute1)
                    leftAcuteNumber1++;
                else if (current == acute && left == false && type == acute2)
                    rightAcuteNumber2++;
                else if (current == acute && left == true && type == acute2)
                    leftAcuteNumber2++;
                else if (current == obtuse && left == false && type == obtuse1)
                    rightObtuseNumber1++;
                else if (current == obtuse && left == true && type == obtuse1)
                    leftObtuseNumber1++;
                else if (current == obtuse && left == false && type == obtuse2)
                    rightObtuseNumber2++;
                else if (current == obtuse && left == true && type == obtuse2)
                    leftObtuseNumber2++;

                //Switch handed of the triaangle
                if (left == false)
                    left = true;
                else if (left == true)
                    left = false;

                //Overall counter for Triangle
                if (current == right)
                    RightNumber++;
                else if (current == obtuse)
                    ObtuseNumber++;
                else if (current == acute)
                    AcuteNumber++;

                //Determine starting type triangle
                int previous = type;
                while (previous == type)
                {
                    int typeTriangle = Random.Range(1, 4);

                    if (typeTriangle == right && RightNumber < 16)
                    {
                        int variant = Random.Range(105, 107);
                        current = right;
                        if (variant == right1 && left == true && leftRightNumber1 < 4)
                            type = right1;
                        else if (variant == right2 && left == true && leftRightNumber2 < 4)
                            type = right2;
                        else if (variant == right1 && left == false && rightRightNumber1 < 4)
                            type = right1;
                        else if (variant == right2 && left == false && rightRightNumber2 < 4)
                            type = right2;
                    }
                    else if (typeTriangle == acute && AcuteNumber < 16)
                    {
                        int variant = Random.Range(202, 204);
                        current = acute;
                        if (variant == acute1 && left == true && leftAcuteNumber1 < 4)
                            type = acute1;
                        else if (variant == acute2 && left == true && leftAcuteNumber2 < 4)
                            type = acute2;
                        else if (variant == acute1 && left == false && rightAcuteNumber1 < 4)
                            type = acute1;
                        else if (variant == acute2 && left == false && rightAcuteNumber2 < 4)
                            type = acute2;
                    }
                    else if (typeTriangle == obtuse && ObtuseNumber < 16)
                    {
                        int variant = Random.Range(305, 307);
                        current = obtuse;
                        if (variant == obtuse1 && left == true && leftObtuseNumber1 < 4)
                            type = obtuse1;
                        else if (variant == obtuse2 && left == true && leftObtuseNumber2 < 4)
                            type = obtuse2;
                        else if (variant == obtuse1 && left == false && rightObtuseNumber1 < 4)
                            type = obtuse1;
                        else if (variant == obtuse2 && left == false && rightObtuseNumber2 < 4)
                            type = obtuse2; 
                    }
                    else
                    {
                        typeTriangle = Random.Range(1, 4);
                    }
                }
            }
        }   
    }
}