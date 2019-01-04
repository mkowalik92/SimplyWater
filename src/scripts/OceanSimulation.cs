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
    [SerializeField]
    private float frameTimeMultiplier = 0.005f;

    // Shader classes
    private H0K h0k;
    private TwiddleFactors twiddleFactors;
    private HKT hkt;

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
    [SerializeField]
    private ComputeShader hktComputeShader;

    // Simulation globals
    private float T = 0;

    void Awake ()
    {
        /*
                Generate initial wave height field (h0k h0minusk) textures
        */
        h0k = new H0K(N, L, amplitude, windSpeed, capillarSupressFactor, direction, noiseTexture1, noiseTexture2, noiseTexture3, noiseTexture4, h0kComputeShader);
        h0k.Run();

        /*
                Generate twiddle factors texture
        */
        twiddleFactors = new TwiddleFactors(N, twiddleFactorsComputeShader);
        twiddleFactors.Run();

        /*
                Generate wave height field textures at time t (dx/dy/dz displacement)
        */
        hkt = new HKT(N, L, T, ConvertToTexture2D(h0k.h0kOutputRenderTexture, N), ConvertToTexture2D(h0k.h0minuskOutputRenderTexture, N), hktComputeShader);
        hkt.Run();
        outputMaterial.mainTexture = hkt.dyDisplacementOutputRenderTexture;
    }

    void Start ()
    {
        

        

        

        // Set ocean surface material albedo to an output render texture
        //outputMaterial.mainTexture = hkt.dyDisplacementOutputRenderTexture;
        //outputMaterial.mainTexture = h0k.h0kOutputRenderTexture;
        //outputMaterial.mainTexture = twiddleFactors.twiddleFactorsOutputRenderTexture;
    }
    void Update ()
    {
        
        T += (Time.deltaTime * 1000.0f) * frameTimeMultiplier;
        hkt.RunUpdate(T);
        outputMaterial.mainTexture = hkt.dyDisplacementOutputRenderTexture;
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
    private Texture2D ConvertToTexture2D(RenderTexture inputRenderTexture, int resolution)
    {
        Texture2D outputTexture = new Texture2D(resolution, resolution, TextureFormat.RGBA32, false);
        RenderTexture.active = inputRenderTexture;
        outputTexture.ReadPixels(new Rect(0, 0, resolution, resolution), 0, 0);
        outputTexture.Apply();
        return outputTexture;
    }

}
