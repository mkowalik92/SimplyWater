using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OceanSimulation : MonoBehaviour
{
    [SerializeField]
    private Material outputMaterial;

    /* Ocean Settings */
    [Header("Ocean Settings")]
    [SerializeField]
    private int N; // Ocean mesh plane vertex resolution
    [SerializeField]
    private int L;
    [SerializeField]
    private float amplitude;
    [SerializeField]
    private float windSpeed; // Wind speed
    [SerializeField]
    private float capillarSupressFactor;
    [SerializeField]
    private Vector2 direction; // Wind direction

    // Shader classes
    private H0K h0k;
    private TwiddleFactors twiddleFactors;

    // Shader h0k noise input textures
    [Header("H0K Noise Textures")]
    [SerializeField]
    private Texture2D noiseTexture1;
    [SerializeField]
    private Texture2D noiseTexture2;
    [SerializeField]
    private Texture2D noiseTexture3;
    [SerializeField]
    private Texture2D noiseTexture4;

    // Compute shader sources
    [Header("Compute Shaders")]
    [SerializeField]
    private ComputeShader h0kComputeShader;
    [SerializeField]
    private ComputeShader twiddleFactorsComputeShader;
    
    void Awake ()
    {
        h0k = new H0K(N, L, amplitude, windSpeed, capillarSupressFactor, direction, noiseTexture1, noiseTexture2, noiseTexture3, noiseTexture4, h0kComputeShader);
        twiddleFactors = new TwiddleFactors(N, twiddleFactorsComputeShader);
    }

    void Start ()
    {
        /*
                Generate initial wave height field (h0k h0minusk) textures
        */
        h0k.Run();


        /*
                Generate twiddle factors texture
        */
        twiddleFactors.Run();

        // Set ocean surface material albedo to an output render texture
        outputMaterial.mainTexture = twiddleFactors.twiddleFactorsOutputRenderTexture;
    }
    void Update ()
    {
        // Swap displayed h0k texture
        //if (Input.GetKeyDown(KeyCode.A))
        //{
        //    outputMaterial.mainTexture = h0k.h0kOutputRenderTexture;
        //}
        //if (Input.GetKeyDown(KeyCode.D))
        //{
        //    outputMaterial.mainTexture = h0k.h0minuskOutputRenderTexture;
        //}
    }
}
