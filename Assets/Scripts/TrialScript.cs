using System.Collections;
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
    public bool openField = false;

    //Track number of User Pressed Spaces
    private int numSpace = 0;

    //All Main GameObjects
    public GameObject longWall;
    public GameObject singleWall;
    public GameObject singleWallLarge;
    public GameObject startWall;
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

    public List<GameObject> FirstCoordinates;
    public List<GameObject> SecondCoordinates;
    public List<GameObject> ThirdCoordinates;

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
        startingPole.transform.position = FirstCoordinates[TM.current].transform.position;
        singleWall.SetActive(true);
        openFieldPole.SetActive(false);
        secondOpenFieldPole.SetActive(false);

        transform.position = new Vector3(FirstCoordinates[TM.current].transform.position.x, 1.8f, FirstCoordinates[TM.current].transform.position.z - 1);

        if (practice)
            //Instructions
            instructions.text = "Please walk forward.";

        practice = TrialManager.practice;

    }

    void OnCollisionEnter(Collision collision)
    {
        GameObject player = GameObject.Find("Player");
        TrialManager trialManager = player.GetComponent<TrialManager>();
        TrialManager TM = trialManager;
        source.Stop();
        //Upon the collision of the First Pole
        if (collision.gameObject.CompareTag("StartPole"))
        {
            trialStart = true;
            collisionFirst = true;

            //Deactivate the First Starting Pole and SetActive the Second Pole
            startingPole.SetActive(false);
            SecondPole.SetActive(true);

            //Moving Second Pole Marker Around
            SecondPole.transform.position = SecondCoordinates[TM.current].transform.position;


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
                Vector3 pos = new Vector3(FirstCoordinates[TM.current].transform.position.x - 1, 0, FirstCoordinates[TM.current].transform.position.z - 1f);
                GameObject newWall = (GameObject)Instantiate(startWall, pos, Quaternion.Euler(0f, 90.0f, 0.0f));
                newWall.tag = "wall";


                //First Hallway Back Side
                Vector3 pos3 = new Vector3(FirstCoordinates[TM.current].transform.position.x-1.5f, 0, FirstCoordinates[TM.current].transform.position.z-1.5f);
                GameObject newBackWall = (GameObject)Instantiate(singleWall, pos3, Quaternion.Euler(0.0f, 0.0f, 0.0f));
                newBackWall.tag = "wall";

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
            ThirdPole.transform.position = ThirdCoordinates[TM.current].transform.position;


            //If non-Open Field
            if (openField == false)
            {
                //Create the cylinder container
                Instantiate(cylinder);
                cylinder.tag = "cylinder";
                cylinder.transform.position = SecondCoordinates[TM.current].transform.position;

                player.transform.position = SecondCoordinates[TM.current].transform.position;

                //Destroy previous hallway, second pole indicator
                GameObject[] argo = GameObject.FindGameObjectsWithTag("wall");
                foreach (GameObject go in argo)
                {
                    go.SetActive(false);
                }
            }
            else if (openField == true)
            {
                //Deactivate first OpenFieldPole
                openFieldPole.SetActive(false);
                secondOpenFieldPole.transform.position = ThirdCoordinates[TM.current].transform.position;
            }
        }

        //Upon the collision of the Third Pole
        if (collision.gameObject.CompareTag("ThirdPole"))
        {
            //Deactivate Third Pole indicator
            ThirdPole.SetActive(false);

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
                    GameObject[] args = GameObject.FindGameObjectsWithTag("wall");
                    foreach (GameObject go in args)
                    {
                        Destroy(go);
                    }
                }
                else if (TM.left == false)
                {
                    GameObject[] args = GameObject.FindGameObjectsWithTag("wall");
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


        if (firstCylinder == true) //After colliding with Second Pole
        {
            if (practice)
            {
                //Instructions
                instructions.text = "Walk forward until you hear a chime. ";
            }
            if (TM.current == 0)
            {
                if (transform.localEulerAngles.y < 123 && transform.localEulerAngles.y > 117)
                {
                    //Sound
                    source.PlayOneShot(rightTurn, 1.0f);

                    if (openField == false)
                    {
                        //Destroy Cylinder
                        GameObject[] arg = GameObject.FindGameObjectsWithTag("cylinder");
                        foreach (GameObject go in arg)
                        {
                            Destroy(go);
                        }
                        Vector3 posSecond = new Vector3(-9.6f, 0, 4.3f);
                        GameObject newWall2 = (GameObject)Instantiate(startWall, posSecond, Quaternion.Euler(0f, 30f, 180f));
                        newWall2.tag = "wall";


                        //Create back wall
                        Vector3 pos3 = new Vector3(-3.74f, 3, 5.05f);
                        GameObject newBackWall2 = (GameObject)Instantiate(singleWallLarge, pos3, Quaternion.Euler(0f, 120f, 0f));
                        newBackWall2.tag = "wall";
                    }
                    else if (TM.openField == true)
                    {
                        //Deactivate SecondOpenFieldPole (again)
                        secondOpenFieldPole.SetActive(true);
                        //Set Third Pole to Active
                        ThirdPole.SetActive(true);
                        ThirdPole.transform.position = ThirdCoordinates[TM.current].transform.position;
                    }

                    //Reset boolean value
                    firstCylinder = false;
                }
            }
            else if (TM.current == 1)
            {
                if (transform.localEulerAngles.y < 93 && transform.localEulerAngles.y > 87)
                {
                    if (openField == false)
                    {
                        //Destroy Cylinder
                        GameObject[] arg = GameObject.FindGameObjectsWithTag("cylinder");
                        foreach (GameObject go in arg)
                        {
                            Destroy(go);
                        }
                        Vector3 posSecond = new Vector3(-4.9f, 0, 4.7f);
                        GameObject newWall2 = (GameObject)Instantiate(startWall, posSecond, Quaternion.Euler(0f, 180f, 0f));
                        newWall2.tag = "wall";


                        //Create back wall
                        Vector3 pos3 = new Vector3(-6.2f, 3, 3f);
                        GameObject newBackWall2 = (GameObject)Instantiate(singleWallLarge, pos3, Quaternion.Euler(0f, 90f, 0f));
                        newBackWall2.tag = "wall";
                    }
                    else if (TM.openField == true)
                    {
                        //Deactivate SecondOpenFieldPole (again)
                        secondOpenFieldPole.SetActive(true);
                        //Set Third Pole to Active
                        ThirdPole.SetActive(true);
                        ThirdPole.transform.position = ThirdCoordinates[TM.current].transform.position;
                    }

                    //Reset boolean value
                    firstCylinder = false;
                }
            }
            else if (TM.current == 2)
            {
                if (transform.localEulerAngles.y < 123 && transform.localEulerAngles.y > 120)
                {
                    if (openField == false)
                    {
                        //Destroy Cylinder
                        GameObject[] arg = GameObject.FindGameObjectsWithTag("cylinder");
                        foreach (GameObject go in arg)
                        {
                            Destroy(go);
                        }
                        Vector3 posSecond = new Vector3(-9.6f, 0, 4.6f);
                        GameObject newWall2 = (GameObject)Instantiate(startWall, posSecond, Quaternion.Euler(0f, 30f, 180f));
                        newWall2.tag = "wall";


                        //Create back wall
                        Vector3 pos3 = new Vector3(-3.74f, 3, 5.05f);
                        GameObject newBackWall2 = (GameObject)Instantiate(singleWallLarge, pos3, Quaternion.Euler(0f, 120f, 0f));
                        newBackWall2.tag = "wall";
                    }
                    else if (TM.openField == true)
                    {
                        //Deactivate SecondOpenFieldPole (again)
                        secondOpenFieldPole.SetActive(true);
                        //Set Third Pole to Active
                        ThirdPole.SetActive(true);
                        ThirdPole.transform.position = ThirdCoordinates[TM.current].transform.position;
                    }

                    //Reset boolean value
                    firstCylinder = false;
                }
            }
            else if (TM.current == 3)
            {
                if (transform.localEulerAngles.y < 63 && transform.localEulerAngles.y > 57)
                {
                    if (openField == false)
                    {
                        //Destroy Cylinder
                        GameObject[] arg = GameObject.FindGameObjectsWithTag("cylinder");
                        foreach (GameObject go in arg)
                        {
                            Destroy(go);
                        }
                        Vector3 posSecond = new Vector3(-9.6f, 0, 2.2f);
                        GameObject newWall2 = (GameObject)Instantiate(startWall, posSecond, Quaternion.Euler(0f, 30f, 180f));
                        newWall2.tag = "wall";


                        //Create back wall
                        Vector3 pos3 = new Vector3(-6.26f, 3, 3.71f);
                        GameObject newBackWall2 = (GameObject)Instantiate(singleWallLarge, pos3, Quaternion.Euler(0f, 120f, 0f));
                        newBackWall2.tag = "wall";
                    }
                    else if (TM.openField == true)
                    {
                        //Deactivate SecondOpenFieldPole (again)
                        secondOpenFieldPole.SetActive(true);
                        //Set Third Pole to Active
                        ThirdPole.SetActive(true);
                        ThirdPole.transform.position = ThirdCoordinates[TM.current].transform.position;
                    }

                    //Reset boolean value
                    firstCylinder = false;

                }
            }
            else if (TM.current == 4)
            {
                if (transform.localEulerAngles.y < 163 && transform.localEulerAngles.y > 157)
                {
                    if (openField == false)
                    {
                        //Destroy Cylinder
                        GameObject[] arg = GameObject.FindGameObjectsWithTag("cylinder");
                        foreach (GameObject go in arg)
                        {
                            Destroy(go);
                        }
                        Vector3 posSecond = new Vector3(-6.2f, 0, 8.9f);
                        GameObject newWall2 = (GameObject)Instantiate(startWall, posSecond, Quaternion.Euler(0f, 70f, 180f));
                        newWall2.tag = "wall";


                        //Create back wall
                        Vector3 pos3 = new Vector3(-3.74f, 3, 5.05f);
                        GameObject newBackWall2 = (GameObject)Instantiate(singleWallLarge, pos3, Quaternion.Euler(0f, 160f, 0f));
                        newBackWall2.tag = "wall";
                    }
                    else if (TM.openField == true)
                    {
                        //Deactivate SecondOpenFieldPole (again)
                        secondOpenFieldPole.SetActive(true);
                        //Set Third Pole to Active
                        ThirdPole.SetActive(true);
                        ThirdPole.transform.position = ThirdCoordinates[TM.current].transform.position;
                    }

                    //Reset boolean value
                    firstCylinder = false;
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
                    startingPole.transform.position = FirstCoordinates[TM.current].transform.position;
                    firstCylinder = false;
                    source.Stop();
                    numSpace = 0;
                    trialStart = false;
                    if (practice)
                    {
                        instructions.text = "Please walk back to the starting pole. ";
                    }
                    secondCylinder = false;

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
                    startingPole.transform.position = FirstCoordinates[TM.current].transform.position;

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
}