using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class h0k : MonoBehaviour
{
    /* Wave Settings */
    [SerializeField]
    private int N;
    [SerializeField]
    private int L;
    [SerializeField]
    private float amplitude;
    [SerializeField]
    private float windSpeed;
    [SerializeField]
    private float capillarSupressFactor;
    [SerializeField]
    private Vector2 direction;

    // Shader output material and render texture
    [SerializeField]
    private Material h0kOutputMaterial;
    private RenderTexture h0kOutputRenderTexture;
    private RenderTexture h0kminusOutputRenderTexture;

    // Shader input textures
    [SerializeField]
    private Texture2D noiseTexture_1;
    [SerializeField]
    private Texture2D noiseTexture_2;
    [SerializeField]
    private Texture2D noiseTexture_3;
    [SerializeField]
    private Texture2D noiseTexture_4;

    // Compute shader 
    [SerializeField]
    private ComputeShader h0kComputerShader;
    private int kernelProgramId;

    /* Shader const strings */
    // h0k
    private const string shaderMainProgramstr = "main";
    private const string shaderh0kOutputTextureStr = "h0kResult";
    private const string shaderh0kminusOutputTextureStr = "h0kMinusResult";
    private const string shaderInputTextureStr_1 = "noise_1";
    private const string shaderInputTextureStr_2 = "noise_2";
    private const string shaderInputTextureStr_3 = "noise_3";
    private const string shaderInputTextureStr_4 = "noise_4";
    private const string shaderInput_N = "N";
    private const string shaderInput_L = "L";
    private const string shaderInput_amplitude = "amplitude";
    private const string shaderInput_windSpeed = "windSpeed";
    private const string shaderInput_capillarSupressFactor = "capillarSupressFactor";
    private const string shaderInput_direction = "direction";
    // hkt

    void Awake ()
    {
        // Create h0k render texture output
        h0kOutputRenderTexture = new RenderTexture(256, 256, 0);
        // Enable writing to h0k output render texture
        h0kOutputRenderTexture.enableRandomWrite = true;
        // "Actually create render texture"
        h0kOutputRenderTexture.Create();

        // Create minus h0k render texture output
        h0kminusOutputRenderTexture = new RenderTexture(256, 256, 0);
        // Enable writing to h0k output render texture
        h0kminusOutputRenderTexture.enableRandomWrite = true;
        // "Actually create render texture"
        h0kminusOutputRenderTexture.Create();

        // Get shader kernel id of program we want to execute
        kernelProgramId = h0kComputerShader.FindKernel(shaderMainProgramstr);
        // Set shader wave settings
        h0kComputerShader.SetInt(shaderInput_N, N);
        h0kComputerShader.SetInt(shaderInput_L, L);
        h0kComputerShader.SetFloat(shaderInput_amplitude, amplitude);
        h0kComputerShader.SetFloat(shaderInput_windSpeed, windSpeed);
        h0kComputerShader.SetFloat(shaderInput_capillarSupressFactor, capillarSupressFactor);
        h0kComputerShader.SetVector(shaderInput_direction, direction);
        // Set input textures
        h0kComputerShader.SetTexture(kernelProgramId, shaderInputTextureStr_1, noiseTexture_1);
        h0kComputerShader.SetTexture(kernelProgramId, shaderInputTextureStr_2, noiseTexture_2);
        h0kComputerShader.SetTexture(kernelProgramId, shaderInputTextureStr_3, noiseTexture_3);
        h0kComputerShader.SetTexture(kernelProgramId, shaderInputTextureStr_4, noiseTexture_4);
        // Set output textures
        h0kComputerShader.SetTexture(kernelProgramId, shaderh0kOutputTextureStr, h0kOutputRenderTexture);
        h0kComputerShader.SetTexture(kernelProgramId, shaderh0kminusOutputTextureStr, h0kminusOutputRenderTexture);
        // Run h0k compute shader
        h0kComputerShader.Dispatch(kernelProgramId, 16, 16, 1);

        // Set ocean material albedo to h0k output texture
        h0kOutputMaterial.mainTexture = h0kOutputRenderTexture;
    }
    void Update ()
    {
        // Swap displayed h0k texture
        if (Input.GetKeyDown(KeyCode.A))
        {
            h0kOutputMaterial.mainTexture = h0kOutputRenderTexture;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            h0kOutputMaterial.mainTexture = h0kminusOutputRenderTexture;
        }
    }
}
