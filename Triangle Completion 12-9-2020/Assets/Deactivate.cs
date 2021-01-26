using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deactivate : MonoBehaviour
{
    public GameObject wall;

    // Start is called before the first frame update
    void Start()
    {
        wall.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
