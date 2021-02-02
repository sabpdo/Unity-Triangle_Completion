using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class InstructionsManager : MonoBehaviour
{
    public Text instruction;
    public string sceneToLoad;
    AsyncOperation loadingOperation;
    public MainMenu other;

    // Start is called before the first frame update
    public void Start()
    {
        loadingOperation = SceneManager.LoadSceneAsync(sceneToLoad);
    }


    // Update is called once per frame
    void Update()
    {
        int numSpace = 0;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            numSpace++;
        }

        if (numSpace == 1)
        {
            instruction.text = "Welcome to Triangle Completion. In this task, you will be traversing the first two legs of a triangle, and will be in charge of finding your way back to your original position.";
        }
        if (numSpace ==2)
        {
            other.StartGame();
            
        }
    }
}
