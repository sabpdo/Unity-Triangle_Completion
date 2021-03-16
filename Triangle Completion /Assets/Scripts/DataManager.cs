using System.Collections;
using UnityEngine;
using System.Reflection;
using UnityEngine.UI;
using System.IO;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

//Script to Manage+Export Data

public class DataManager : MonoBehaviour
{
    public float correctAngle;
    private float inputAngle;
    public float correctDistance;
    private float inputDistance;
    private float angularError, pctAngular180Error, distanceError;
    
    public static string token;

    private static bool wallSpawned;
    public float angle;

    public GameObject player;

    public Vector3 playerPos;
    public Vector3 playerRot;
    public Vector3 lastPosition = Vector3.zero;
    public Vector3 lastRotation, lastAngle, playerRotAngle;


    public GameObject ThirdPole;
    public GameObject startingPole;

    public string responseData = "";
    public string trialData = "";
    public static string positionData = "";
    public string FILE_NAME;

    public static bool GameStart = false;
    public bool responseAcquired = false;
    public static bool newTrial = false;

    public bool done;

    public string typeTriangle;

    public TrialManager TM;

    // Start is called before the first frame update
    void Start()
    {
        FILE_NAME =  "trial.csv";

        //Get Component WallSpawned + Done from Other Script
        GameObject player = GameObject.Find("Camera View");
        ThirdHallway TH = player.GetComponent<ThirdHallway>();
        wallSpawned = TH.wallSpawned;
        done = TH.done;

        //Make sure the triangle type matches the other script
        typeTriangle = TM.typeTriangle;
    }

    // Update is called once per frame
    void Update()
    {
        //Record Data/Get the Angle the person turned when they first press the space bar
        if (Input.GetKeyDown(KeyCode.Space) && wallSpawned)
        {
            recordData();
        }

        //Write to file the data when the trial is done
        if (Input.GetKeyDown(KeyCode.Space) && done)
        {
            trialData = GetDataString();
            WriteToFile();
        }

        typeTriangle = TM.typeTriangle;

        // With each update, obtain necessary data
        // Player position and player rotation 
        playerPos = player.transform.position;
        playerRot = player.transform.eulerAngles;
        playerRotAngle = player.transform.forward;


        //Setting the respective correct angle 
        if (TM.current == TM.acute)
        {
            if (TM.type== 202) //acute1 4,2,60
            {
                correctAngle = 90f;
            }
            else //4,6,60
            {
                correctAngle = 40.6546526707f;
            }
        }
        else if (TM.current == TM.right)
        {
            //Using ArcTangent to find the correct turn angle
            if (TM.type == 105) //right 1 2,6,90
            {
                correctAngle = Mathf.Atan2(2, 6) * (Mathf.PI / 180);
            }
            else //6,4,90
            {
                correctAngle = Mathf.Atan2(6, 4) * (Mathf.PI / 180);
            }

        }
        else if (TM.current == TM.obtuse)
        {
            if (TM.type == 305) //obtuse1 2,4,120
            {
                correctAngle = 15.89416531f;
            }
            else //6,2,120
            {
                correctAngle = 46.10211375f;
            }
        }

    }

    //Get Angle that the player turned
    public void recordData()
    { 
        inputAngle = GetInputAngle();
    }

    public float GetInputAngle()
    {
        float wallAngle = GetComponent<ThirdHallway>().yrotation;
        float angle=0;

        //The angle the person turns is essentially the angle they were in the second hallway minus how much they turned (the absolutet
        //value/magnitude)
        if (TM.left)
        {
            if (TM.current == TM.acute)
            {
                angle = Mathf.Abs(270 - wallAngle);
                
            }
            else if (TM.current == TM.right)
            {
                angle = Mathf.Abs(240 - wallAngle);

            }
            else if (TM.current == TM.obtuse)
            {
                angle = Mathf.Abs(300 - wallAngle);
            }
        }
        else
        {
            if (TM.current == TM.acute)
            {
                angle = Mathf.Abs(90 - wallAngle);
            }
            else if (TM.current == TM.right)
            {
                angle = Mathf.Abs(120 - wallAngle);

            }
            else if (TM.current == TM.obtuse)
            {
                angle = Mathf.Abs(60 - wallAngle);
            }
        }

        return angle; 
    }

    public string GetDataString()
    {
        inputDistance = Vector3.Distance(ThirdPole.transform.position, player.transform.position);
        Vector3 initialPosition = new Vector3(0, 0, 9);
        float correctDistance = Vector3.Distance(ThirdPole.transform.position, initialPosition);
        angularError = Mathf.DeltaAngle((inputAngle), (correctAngle));
        pctAngular180Error = Mathf.Abs(angularError / 180);
        float inputAngleEulerClockwise = (360 - inputAngle);
        
        
        string tdata = TrialManager.trialnum.ToString() + ", "
            + typeTriangle + ","
            + correctDistance.ToString() + ","
            + inputDistance.ToString() + ","
            + correctAngle.ToString() + ", "
            + inputAngle.ToString() + ", "
            + angularError.ToString() + ", "
            + pctAngular180Error.ToString() + ", "
            ;

        return tdata;
    }

    void WriteToFile()
    {
        string filePath = getPath();

        // Hard write to folder
        StreamWriter sw = File.AppendText(filePath);
        if (new FileInfo(FILE_NAME).Length == 0)
        {
            sw.WriteLine("trialNum, triangleType, correctDistance, inputDistance, correctAngle, inputAngle, angularError, pctAngular180Error");
        }

        sw.WriteLine(trialData);
        sw.Close();
        
    }

    private string getPath()
    {
        return Application.dataPath + "CSV/" + "Saved_data.csv";
    }

}




