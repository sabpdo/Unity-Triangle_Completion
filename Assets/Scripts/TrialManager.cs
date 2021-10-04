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


    private int rightOneNumber = 0;
    private int rightTwoNumber = 0;
    private int rightThreeNumber = 0;
    private int rightFourNumber = 0;
    private int rightFiveNumber = 0;

    public bool openField;
    public bool practiceViewed = false;
    public bool practiceDone = false;

    public int current;

    public string typeTriangle = "";

    //Variant values
    public const int maxIndividual = 4; //Max each triangle is shown


    public bool left; //left handed triangle


    // Start is called before the first frame update
    void Start()
    {
        practice = true;
        ThirdHallway refScript = GetComponent<ThirdHallway>();

        Debug.Log("TRIAL SCENE LOADED: " + trialnum);

        left = false;

        //Determine left or right handed triangle first
        /*
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
        current = Random.Range(0, 4);

        NextTrial();

    }


    // Update is called once per frame
    void Update()
    {
        GameObject player = GameObject.Find("Player");
        TrialScript TS = player.GetComponent<TrialScript>();
        ThirdHallway TH = player.GetComponent<ThirdHallway>();
        trialStart = TS.trialStart;



        if (trialnum <= 6)
        {
            practice = true;
            Debug.Log("Current Trial loaded: Practice " + trialnum);
        }
        else
        {
            practice = false;
            Debug.Log("Current Trial loaded: " + trialnum);
        }

        //For Data Manager
        if (current == 0)
        {
            typeTriangle = "Acute Triangle (60) - Length 4,2";
        }
        else if (current == 1)
        {
            typeTriangle = "Right Triangle (90) - Length 5,3";
        }
        else if (current == 2)
        {
            typeTriangle = "Acute Triangle (60) - Length 3,3";
        }
        else if (current == 3)
        {
            typeTriangle = "Obtuse Triangle (120) - Length 2,5";
        }
        else if (current == 4)
        {
            typeTriangle = "Acute Triangle (30) - Length 4.5,2";
        }

        NextTrial();

        if (TH.done2 == true)
        {
            trialnum++;
            TH.done2 = false;
            ResetTrial();

        }

        if (trialnum == 7 && TS.collisionFirst == true)
        {
            practiceDone = true;
        }
    }


    public void NextTrial()
    {
        GameObject player = GameObject.Find("Player");
        ThirdHallway TH = player.GetComponent<ThirdHallway>();
        TrialScript TS = player.GetComponent<TrialScript>();

        if (trialnum == trialMax + 1)
        {
            SceneManager.LoadScene("Complete");
        }
        else if (trialnum <= 6)
        {
            Debug.Log("Current Trial loaded: Practice " + trialnum);



            //Do thirdOpen Pole Location too


            if (current == 0)
            {
                rightOneNumber++;
            }
            else if (current == 1)
            {
                rightTwoNumber++;
            }
            else if (current == 2)
            {
                rightThreeNumber++;
            }
            else if (current == 3)
            {
                rightFourNumber++;
            }
            else if (current == 4)
            {
                rightFiveNumber++;
            }
        }
        else if (trialnum <= trialMax)
        {

            Debug.Log("Current Trial loaded: " + (trialnum - 6));


            if (current == 0)
            {
                rightOneNumber++;
            }
            else if (current == 1)
            {
                rightTwoNumber++;
            }
            else if (current == 2)
            {
                rightThreeNumber++;
            }
            else if (current == 3)
            {
                rightFourNumber++;
            }
            else if (current == 4)
            {
                rightFiveNumber++;

            }


        }

    }

    private void ResetTrial()
    {
        //Switch handed of the triangle
        /*
        if (left == false)
            left = true;
        else if (left == true)
            left = false;
        */

        //Determine starting type triangle
        int previous = current;
        while (previous == current)
        {
            current = Random.Range(0, 4);
        }
    }
}
