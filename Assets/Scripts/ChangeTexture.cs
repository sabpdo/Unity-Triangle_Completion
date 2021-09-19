using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeTexture : MonoBehaviour
{
    [SerializeField]
    public GameObject cube;

    [SerializeField]
    public Texture[] textures;
    private Renderer cubeRenderer;
    private int textureIndex;


    // Start is called before the first frame update
    void Start()
    {
        cubeRenderer = cube.GetComponent<Renderer>();
        textureIndex = Random.Range(0, textures.Length);
        cubeRenderer.material.mainTexture = textures[textureIndex];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
