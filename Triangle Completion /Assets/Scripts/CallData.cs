using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CallData : MonoBehaviour
{
    public float time = 0;
    private bool safe = false;
    private bool timeEnabled = false;
    public GameObject firstins;
    // Start is called before the first frame update
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
        if (firstins == null)
        {
            if (safe == false)
            {
                timeEnabled = true;
            }
        }
        if (timeEnabled == true)
        {
            time += Time.deltaTime;
        }
        if (time > 0.0999)
        {
            Data.MyTools1.DEV_AppendDefaultsToReport();
            time = 0;
        }
    }
}