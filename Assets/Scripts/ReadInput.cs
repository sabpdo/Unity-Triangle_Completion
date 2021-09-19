using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ReadInput : MonoBehaviour
{
    private string input;

    private float numSpace;

    public Text dataValue;

    public InputField inputField;


    // Start is called before the first frame update
    void Start()
    {
        ReadStringInput();
    }

    // Update is called once per frame
    void Update()
    {

        if (numSpace == 1)
        {
            //Instructions
            dataValue.text = "Age";
        }
        else if (numSpace == 2)
        {
            dataValue.text = "Sex";
        }
        else if (numSpace == 3)
        {
            dataValue.text = "Handedness";
        }
        else if (numSpace == 4)
        {
            StartInstructions();
        }
    }

    void StartInstructions()
    {
        //Load Instructions
        SceneManager.LoadSceneAsync("Instructions");

        //Unload beginning scene with instructions
        SceneManager.UnloadSceneAsync("CollectData");

    }



    public void ReadStringInput()
    {
        var input = gameObject.GetComponent<InputField>();
        var se = new InputField.SubmitEvent();
        se.AddListener(SubmitName);
        input.onEndEdit = se;


    }

    private void SubmitName(string arg0)
    {
        numSpace++;
        Debug.Log(arg0);
        if (numSpace == 0)
            PlayerPrefs.SetString("PrefParticipant", arg0);
        else if (numSpace == 1)
            PlayerPrefs.SetString("PrefAge", arg0);
        else if (numSpace == 2)
            PlayerPrefs.SetString("PrefSex", arg0);
        else if (numSpace==3)
            PlayerPrefs.SetString("PrefHand", arg0);
    }


    public void ReadStringInput(string s)
    {
        input = s;
        Debug.Log(input);
    }
}



