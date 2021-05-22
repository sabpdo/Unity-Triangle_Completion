using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class TrialInstructions : MonoBehaviour
{
    public Canvas canvasGroup;

    public Text instruction;

    private int numSpace = 0;
    private bool temp = false;

    // Start is called before the first frame update
    public void Start()
    {
        canvasGroup.gameObject.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {
        GameObject player = GameObject.Find("Camera View");
        TrialManager TM = player.GetComponent<TrialManager>();
        //To make slider accord to how much of the scene has loaded
        if (Input.GetKeyDown(KeyCode.Space) && TM.practiceDone)
        {
            TM.practiceDone = false;
            canvasGroup.gameObject.SetActive(true);
            instruction.text = "Now you will do the test. Please try to respond accurately"
            + "\n When you are ready to start, press SPACE BAR.";
            numSpace++;
        }
        if (numSpace>=1)
        {
            canvasGroup.gameObject.SetActive(false);
        }


    }

}