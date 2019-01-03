using UnityEngine;

public class H0K
{
    private int N; // Ocean mesh plane vertex resolution
    private int L;
    private float amplitude;
    private float windSpeed; // Wind speed
    private float capillarSupressFactor;
    private Vector2 direction; // Wind direction

    // Noise textures for h0k shader
    private Texture2D noiseTexture1;
    private Texture2D noiseTexture2;
    private Texture2D noiseTexture3;
    private Texture2D noiseTexture4;

    private ComputeShader h0kComputeShader;
    private int kernelProgramId; // Shader kernel program ID

    public RenderTexture h0kOutputRenderTexture;
    public RenderTexture h0minuskOutputRenderTexture;

    public H0K (int N, int L, float amplitude, float windSpeed, float capillarSupressFactor, Vector2 direction, Texture2D noiseTexture1, Texture2D noiseTexture2, Texture2D noiseTexture3, Texture2D noiseTexture4, ComputeShader h0kComputeShader)
    {
        this.N = N;
        this.L = L;
        this.amplitude = amplitude;
        this.windSpeed = windSpeed;
        this.capillarSupressFactor = capillarSupressFactor;
        this.direction = direction;
        this.noiseTexture1 = noiseTexture1;
        this.noiseTexture2 = noiseTexture2;
        this.noiseTexture3 = noiseTexture3;
        this.noiseTexture4 = noiseTexture4;
        this.h0kComputeShader = h0kComputeShader;
    }

    public void Run ()
    {
        // Create h0k render texture output
        h0kOutputRenderTexture = new RenderTexture(N, N, 0);
        // Enable writing to h0k output render texture
        h0kOutputRenderTexture.enableRandomWrite = true;
        // "Actually create render texture"
        h0kOutputRenderTexture.Create();

        // Create h0minusk render texture output
        h0minuskOutputRenderTexture = new RenderTexture(N, N, 0);
        // Enable writing to h0k output render texture
        h0minuskOutputRenderTexture.enableRandomWrite = true;
        // "Actually create render texture"
        h0minuskOutputRenderTexture.Create();

        // Get shader kernel id of program we want to execute
        kernelProgramId = h0kComputeShader.FindKernel(ConstStrings.shaderMainProgramstr);
        // Set shader wave settings
        h0kComputeShader.SetInt(ConstStrings.shaderInput_N, N);
        h0kComputeShader.SetInt(ConstStrings.shaderInput_L, L);
        h0kComputeShader.SetFloat(ConstStrings.shaderInput_amplitude, amplitude);
        h0kComputeShader.SetFloat(ConstStrings.shaderInput_windSpeed, windSpeed);
        h0kComputeShader.SetFloat(ConstStrings.shaderInput_capillarSupressFactor, capillarSupressFactor);
        h0kComputeShader.SetVector(ConstStrings.shaderInput_direction, direction);
        // Set input textures
        h0kComputeShader.SetTexture(kernelProgramId, ConstStrings.noiseTextureStr1, noiseTexture1);
        h0kComputeShader.SetTexture(kernelProgramId, ConstStrings.noiseTextureStr2, noiseTexture2);
        h0kComputeShader.SetTexture(kernelProgramId, ConstStrings.noiseTextureStr3, noiseTexture3);
        h0kComputeShader.SetTexture(kernelProgramId, ConstStrings.noiseTextureStr4, noiseTexture4);
        // Set output textures
        h0kComputeShader.SetTexture(kernelProgramId, ConstStrings.shaderh0kOutputTextureStr, h0kOutputRenderTexture);
        h0kComputeShader.SetTexture(kernelProgramId, ConstStrings.shaderh0minuskOutputTextureStr, h0minuskOutputRenderTexture);
        // Run h0k compute shader
        h0kComputeShader.Dispatch(kernelProgramId, N / 16, N / 16, 1);
    }
}
