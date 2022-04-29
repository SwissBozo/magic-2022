using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;
public class GrowAnim : MonoBehaviour


{
    public InputAction encoder1;
    public InputAction encoder2;
    public InputAction button;
    Renderer rend;

    [Range(0f, 1f)]
    public float scaleFact;


    public float grow;
    float lastPos;

    float baseScale;
    void Awake()
    {
        scaleFact = 0f;
        encoder1.Enable();
        encoder2.Enable();
        button.Enable();
    }
    // Start is called before the first frame update
    void Start()
    {
        float grow = 0.0f;
        baseScale = 42f;
        rend = GetComponent<Renderer>();
        lastPos = 0.0f;
        rend.material.shader = Shader.Find("Shader Graphs/Growing");
    }

    // Update is called once per frame
    void Update()
    {
        // float grow = Mathf.PingPong(Time.time, 1.0f);
        float inPut = encoder1.ReadValue<float>() * 100f;
        float growRaw = grow + (inPut - lastPos);
        grow = Mathf.Clamp(growRaw, 0.0f, 1.0f);
        lastPos = inPut;
        rend.material.SetFloat("Grow_", grow);
        transform.localScale = new Vector3(grow * baseScale, grow * baseScale, grow * baseScale);
    }
}
