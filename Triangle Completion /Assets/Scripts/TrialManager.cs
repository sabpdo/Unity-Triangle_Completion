using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TrialManager : MonoBehaviour
{

    public static int trialnum = 1;
    public static bool trialStart;
    public static bool practice = false;
    public int trialMax = 48;
    public DataManager DM;

    public int right = 1; //for right triangle
    public int right1 = 105; //2,6,90
    public int right2 = 106; //6,4,90
    public int leftRightNumber1 = 0;
    public int leftRightNumber2 = 0;
    public int rightRightNumber1 = 0;
    public int rightRightNumber2 = 0;

    public int RightNumber = 0;

    public int acute = 2;
    public int acute1 = 202; //4,2,60
    public int acute2 = 203; //4,6,60
    private int leftAcuteNumber1 = 0;
    private int leftAcuteNumber2 = 0;
    private int rightAcuteNumber1 = 0;
    private int rightAcuteNumber2 = 0;

    private int AcuteNumber = 0;

    public int obtuse = 3;
    public int obtuse1 = 305; //2,4,120
    public int obtuse2 = 306; //6,2,120
    private int leftObtuseNumber1 = 0;
    private int leftObtuseNumber2 = 0;
    private int rightObtuseNumber1 = 0;
    private int rightObtuseNumber2 = 0;

    private int ObtuseNumber = 0;

    public bool openField;

    public int current;
    public int type;

    public Vector3 thirdPoleLocation;
    public Vector3 thirdOpenPoleLocation;

    public string typeTriangle = "";

    //Variant values
    public const int maxIndividual = 4; //Max each triangle is shown


    public bool left; //left handed triangle


    // Start is called before the first frame update
    void Start()
    {
        ThirdHallway refScript = GetComponent<ThirdHallway>();
        
        Debug.Log("TRIAL SCENE LOADED: " + trialnum);

        //Determine left or right handed triangle first
        int leftOrRight = Random.Range(0, 2);
        if (leftOrRight == 0)
            left = true;
        else if (leftOrRight == 1)
            left = false;

        /*
        //Determine if initially open field or not
        int openFieldOrNot = Random.Range(0, 2);
        if (openFieldOrNot == 0)
            openField = true;
        else if (openFieldOrNot == 1)
            openField = false;
        */
        

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


        NextTrial();

    }


    // Update is called once per frame
    void Update()
    {
        GameObject player = GameObject.Find("Camera View");
        TrialScript TS = player.GetComponent<TrialScript>();
        trialStart = TS.trialStart;

        if (trialnum <= 6)
            practice = true;
        else
            practice = false;

        //For Data Manager
        if (current == right)
        {
            if (type == right1)
                typeTriangle = "Right Triangle(90) - Length 2, 6";
            else if (type == right2)
                typeTriangle = "Right Triangle(90) - Length 6,2";

        }
        else if (current == acute)
        {
            if (type == acute1)
                typeTriangle = "Acute Triangle (60) - Length 4,2";
            else if (type == acute2)
                typeTriangle = "Acute Triangle(60) - Length 4,6";
        }
        else if (current == obtuse)
        {
            if (type == obtuse1)
                typeTriangle = "Obtuse Triangle (120) - Length 2,4";
            else if (type == obtuse2)
                typeTriangle = "Obtuse Triangle(120) - Length 6,4";
        }
    }


    public void NextTrial()
    {
        GameObject player = GameObject.Find("Camera View");
        ThirdHallway TH = player.GetComponent<ThirdHallway>();

        if (trialnum == trialMax + 1)
        {
            SceneManager.LoadScene("Complete");
        }

        else if (trialnum<trialMax)
        {
            
            Debug.Log("Current Trial loaded: " + trialnum);
            
           
            //Moving Third Pole Marker Around
            //Right Handed Triangle - Code to Situate ThirdPole+SecondOpenFieldPole properly
            if (left == false)
            {
                if (current == right)
                {
                    if (type == right1)
                    {
                        thirdPoleLocation = new Vector3(60f, 0f, 29f);
                        thirdOpenPoleLocation = new Vector3(100f, 3f, 29f);
                    }
                    else if (type == right2)
                    {
                        thirdPoleLocation = new Vector3(40f, 0f, 69f);
                        thirdOpenPoleLocation = new Vector3(100f, 3f, 69f);
                    }
                }
                else if (current == acute)
                {
                    if (type == acute1)
                    {
                        thirdPoleLocation = new Vector3(17.32f, 0f, 39f);
                        thirdOpenPoleLocation = new Vector3(86.6f, 3f, -1f);
                    }
                    else if (type == acute2)
                    {
                        thirdPoleLocation = new Vector3(51.96f, 0f, 19f);
                        thirdOpenPoleLocation = new Vector3(86.6f, 3f, -1f);
                    }
                }
                else if (current == obtuse)
                {
                    if (type == obtuse1)
                    {
                        thirdPoleLocation = new Vector3(20 * Mathf.Sqrt(3), 0f, 49f);
                        thirdOpenPoleLocation = new Vector3(86.6f, 3f, 83f);
                    }
                    else if (type == obtuse2)
                    {
                        thirdPoleLocation = new Vector3(10 * Mathf.Sqrt(3), 0f, 79f);
                        thirdOpenPoleLocation = new Vector3(86.6f, 3f, 123f);
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
                        thirdPoleLocation = new Vector3(-60f, 0f, 29f);
                        thirdOpenPoleLocation = new Vector3(-100f, 3f, 29f);
                    }
                    else if (type == right2)
                    {
                        thirdPoleLocation = new Vector3(-40f, 0f, 69f);
                        thirdOpenPoleLocation = new Vector3(-100f, 3f, 29f);
                    }
                }
                else if (current == acute)
                {
                    if (type == acute1)
                    {
                        thirdPoleLocation = new Vector3(-17.32f, 0f, 39f);
                        thirdOpenPoleLocation = new Vector3(-86.6f, 3f, -1f);
                    }
                    else if (type == acute2)
                    {
                        thirdPoleLocation = new Vector3(-51.96f, 0f, 19f);
                        thirdOpenPoleLocation = new Vector3(-86.6f, 3f, -1f);
                    }
                }
                else if (current == obtuse)
                {
                    if (type == obtuse1)
                    {
                        thirdPoleLocation = new Vector3(-20 * Mathf.Sqrt(3), 0f, 49f);
                        thirdOpenPoleLocation = new Vector3(-86.6f, 3f, 83f);
                    }
                    else if (type == obtuse2)
                    {
                        thirdPoleLocation = new Vector3(-10 * Mathf.Sqrt(3), 0f, 79f);
                        thirdOpenPoleLocation = new Vector3(-86.6f, 3f, 123f);
                    }
                }
            }


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


            //Overall counter for Triangle
            if (current == right)
                RightNumber++;
            else if (current == obtuse)
                ObtuseNumber++;
            else if (current == acute)
                AcuteNumber++;     


            if(TH.done)
            {
                ResetTrial();
                trialnum += 1;
                NextTrial();
            }
        }

    }

    private void ResetTrial()
    {
        //Switch handed of the triaangle
        if (left == false)
            left = true;
        else if (left == true)
            left = false;

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
