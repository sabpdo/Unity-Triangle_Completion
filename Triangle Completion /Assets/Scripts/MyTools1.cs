using UnityEngine;
using UnityEditor;
namespace Data
{
    public static class MyTools1
    {
        //Variables That Will Be Recorded
        private static string trial;
        private static string Participant;
        private static string Age;
        private static string Sex;
        private static string Handedness;
        private static string typeTriangle;
        private static string left;
        private static string startingPoleLocation;
        private static string secondPoleLocation;
        private static string thirdPoleLocation;
        private static string correctDistance;
        private static string inputDistance;
        private static float correctAngle;
        private static float inputAngle;
        private static float angularError;
        private static string pctAngular180Error;
        private static string totalTime;
        private static string totalTime1;
        private static string firstLegTime;
        private static string secondLegTime;
        private static string thirdLegTime;
        private static string firstTurnTime;
        private static string secondTurnTime;

        private static bool timeEnabled = false;
        private static bool safe = false;
        private static float time = 0;

        //Other variables to faciliate the obtainment of values of the variables above
        public static Vector3 playerPos;
        public static Vector3 playerRot;
        public static TrialManager TM;

        public static void DEV_AppendDefaultsToReport()
        {
            playerPos = GameObject.Find("Player").transform.position;
            playerRot = GameObject.Find("Player").transform.eulerAngles;

            trial = TrialManager.trialnum.ToString();
            Participant = PlayerPrefs.GetString("PrefParticipant");
            Age = PlayerPrefs.GetString("PrefAge");
            Sex = PlayerPrefs.GetString("PrefSex");
            Handedness = PlayerPrefs.GetString("PrefHand");
            typeTriangle = TM.typeTriangle;
            left = getLeft();

            GameObject player = GameObject.Find("Player");
            TrialScript TS = player.GetComponent<TrialScript>();
            startingPoleLocation = TS.startingPole.transform.position.ToString();
            secondPoleLocation = TS.SecondPole.transform.position.ToString();
            thirdPoleLocation = TS.ThirdPole.transform.position.ToString();

            GameObject dataMan = GameObject.Find("Data Manager");
            DataManager DM = dataMan.GetComponent<DataManager>();
            correctDistance = DM.getCorrectDistance();
            inputDistance = DM.getInputDistance();
            correctAngle = DM.getCorrectAngle();
            inputAngle = DM.GetInputAngle();
            angularError = Mathf.DeltaAngle((inputAngle), (correctAngle));
            pctAngular180Error = Mathf.Abs(angularError / 180).ToString();
            
            CSVManager.AppendToReport(
                new string[29] {
                trial,
                Participant,
                Age,
                Sex,
                Handedness,
                typeTriangle,
                left,
                startingPoleLocation,
                secondPoleLocation,
                thirdPoleLocation,
                correctDistance,
                inputDistance,
                correctAngle.ToString(),
                inputAngle.ToString(),
                angularError.ToString(),
                pctAngular180Error,
                totalTime,
                totalTime1,
                firstLegTime,
                firstTurnTime,
                secondLegTime,
                secondTurnTime,
                thirdLegTime,
                playerPos.x.ToString(),
                playerPos.y.ToString(),
                playerPos.z.ToString(),
                playerRot.x.ToString(),
                playerRot.y.ToString(),
                playerRot.z.ToString(),
                }

            );
            
          
        }
        [MenuItem("My Tools/2. Reset Report %F12")]
        static void DEV_ResetReport()
        {
            CSVManager.CreateReport();
            EditorApplication.Beep();
            Debug.Log("<color=orange>The report has been reset...</color>");
        }



        //Methods to Obtain Data
        public static string getLeft()
        {
            if (TM.left == true)
                return "left";
            else
            {
                return "right";
            }
        }

        
    }
}