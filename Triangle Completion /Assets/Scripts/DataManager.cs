using System.Collections;
using UnityEngine;
using System.Reflection;
using UnityEngine.UI;
using System.IO;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

//Ask how to use FirstPersonController

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
        GameObject player = GameObject.Find("Camera View");
        ThirdHallway TH = player.GetComponent<ThirdHallway>();
        wallSpawned = TH.wallSpawned;
        done = TH.done;

        typeTriangle = TM.typeTriangle;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && wallSpawned)
        {
            recordData();
        }

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


        if (TM.current == TM.acute)
        {
            correctAngle = 60;
        }
        else if (TM.current == TM.right)
        {
            correctAngle = 90;
           
        }
        else if (TM.current == TM.obtuse)
        {
            correctAngle = 120;
        }

    }

    //Get Angle that the player turned
    public void recordData()
    { 
        inputAngle = GetInputAngle();
    }

    public float GetInputAngle()
    {
        float angle = GetComponent<ThirdHallway>().yrotation;

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




