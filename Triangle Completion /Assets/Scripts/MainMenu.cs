using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider progressBar;
    AsyncOperation loadingOperation;
    public CanvasGroup canvasGroup;

    public Text instruction;
    public Text welcome;

    private int numSpace = 0;
    private bool temp=false;

    // Start is called before the first frame update
    public void Start()
    {
        //SceneManager.LoadSceneAsync("WelcomeInstructions");
        loadingScreen.SetActive(false);

    }
    

    // Update is called once per frame
    void Update()
    {
        if (temp == true)
        {
            progressBar.value = Mathf.Clamp01(loadingOperation.progress / 0.9f);
        }

        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            numSpace++;
        }

        if (numSpace == 1)
        {
            welcome.text = "";
            instruction.text = "Welcome to Triangle Completion. In this task, you will be traversing " +
                "\n the first two legs of a triangle and will be in charge of finding your way " +
                "\n back to your original position. Follow the instructions for the task" +
                "\n in the upper left hand corner.";
        }
        if (numSpace == 2)
        {
            instruction.text = "To move around in the virtual environment, press" +
                "\n WASD, A and D to turn left and right respectively, and W and S to move forward " +
                "\n and backward. Please also do not stray from the instructions/designated path.";
        }
        if (numSpace >= 3)
        {
            StartGame();

        }

    }

    void StartGame()
    {
        //Load MainScene
        SceneManager.LoadSceneAsync("MainScene");
        loadingOperation = SceneManager.LoadSceneAsync("MainScene");
        welcome.text = "";
        instruction.text = "";
        //Triggers IEnumerator
        StartCoroutine(StartLoad());
        //Unload beginning scene with instructions
        SceneManager.UnloadSceneAsync("WelcomeInstructions");
        temp = true;
    }

    IEnumerator StartLoad()
    {
        //Set the loading Screen to Active
        loadingScreen.SetActive(true);
        yield return StartCoroutine(FadeLoadingScreen(1, 1));

        AsyncOperation operation = SceneManager.LoadSceneAsync("MainScene");
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
