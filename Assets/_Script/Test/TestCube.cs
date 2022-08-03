using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class TestCube : MonoBehaviour
{
    public Renderer render;
    // Start is called before the first frame update
    void Awake()
    {
        render.material.renderQueue = (int)RenderQueue.Overlay;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
