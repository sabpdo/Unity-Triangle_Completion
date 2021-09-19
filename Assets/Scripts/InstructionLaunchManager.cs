using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class InstructionLaunchManager : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider progressBar;
    AsyncOperation loadingOperation;
    public CanvasGroup canvasGroup;

    public Text instruction;
    public Text welcome;

    private int numSpace = 0;
    private bool temp = false;

    // Start is called before the first frame update
    public void Start()
    {
        loadingScreen.SetActive(false);
    }
    

    // Update is called once per frame
    void Update()
    {
        //To make slider accord to how much of the scene has loaded
        if (temp == true)
        {
            progressBar.value = Mathf.Clamp01(loadingOperation.progress / 0.9f);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            numSpace++;
        }

        if (numSpace==1)
        {
            //Instructions
            welcome.text = "";
            instruction.text = "Welcome to Triangle Completion. In this task, you will be traversing " +
                "\n the first two legs of a triangle and will be in charge of finding your way " +
                "\n back to your original position. Follow the instructions for the task" +
                "\n in the upper left hand corner. Press the SPACE BAR to continue.";
        }
        else if (numSpace==2)
        {
            instruction.text = "To move around in the virtual environment, press" +
                    "\n WASD, A and D to turn left and right respectively, and W and S to move forward " +
                    "\n and backward. Please also do not stray from the instructions/designated path." +
                    "\n Press the SPACE BAR to continue.";
        }
        else if (numSpace==3)
        {
            instruction.text = "Now you will do six practice trials. When each trial appears, follow the instructions"
                + "\n in the top left corner. Press SPACE BAR to see the first practice trial.";
        }
        else if (numSpace==4)
        {
            Destroy(instruction);
            StartGame();
        }
        
        /*
        else (section == "Test")
        {
            instruction.text = "Now you will do the test. When each trial appears, follow the instructions"
                + "\n in the top left corner. Please try to respond accurately"
                + "\n When you are ready to startt, press SPACE BAR.";

            testViewed = true;
        }
        */
    }

    void StartGame()
    {
        //Load MainScene
        SceneManager.LoadSceneAsync("Trial");
        loadingOperation = SceneManager.LoadSceneAsync("Trial");

        //Reset Text
        welcome.text = "";

        //Triggers IEnumerator
        StartCoroutine(StartLoad());

        //Unload beginning scene with instructions
        SceneManager.UnloadSceneAsync("Instructions");

        //Sets off boolean values in Update()
        temp = true;
    }

    IEnumerator StartLoad()
    {
        //Set the loading Screen to Active
        loadingScreen.SetActive(true);
        yield return StartCoroutine(FadeLoadingScreen(1, 1));

        AsyncOperation operation = SceneManager.LoadSceneAsync("Trial");
        while (!operation.isDone)
        {
            yield return null;
        }

        yield return StartCoroutine(FadeLoadingScreen(0, 1));
        loadingScreen.SetActive(false);
    }

    //To fade loading Screen
    IEnumerator FadeLoadingScreen(float targetValue, float duration)
    {
        float startValue = canvasGroup.alpha;
        float time = 0;

        while (time < duration)
        {
            canvasGroup.alpha = Mathf.Lerp(startValue, targetValue, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = targetValue;
    }

  
   
}
