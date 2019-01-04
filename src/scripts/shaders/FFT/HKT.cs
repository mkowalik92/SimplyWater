using UnityEngine;

public class HKT
{
    private int N; // Resolution
    private int L;
    private float T; // Frame time

    // H0K shader output texture/HKT shader input textures
    public Texture2D h0kTexture;
    public Texture2D h0minuskTexture;

    private ComputeShader hktComputeShader;
    private int kernelProgramId; // Shader kernel program ID

    public RenderTexture dxDisplacementOutputRenderTexture;
    public RenderTexture dyDisplacementOutputRenderTexture;
    public RenderTexture dzDisplacementOutputRenderTexture;

    public HKT (int N, int L, float T, Texture2D h0kTexture, Texture2D h0minuskTexture, ComputeShader hktComputeShader)
    {
        this.N = N;
        this.L = L;
        this.T = T;

        this.h0kTexture = h0kTexture;
        this.h0minuskTexture = h0minuskTexture;

        this.hktComputeShader = hktComputeShader;
    }

    public void Run ()
    {
        // Create dx displacement render texture output
        dxDisplacementOutputRenderTexture = new RenderTexture(N, N, 0);
        // Enable writing to dx displacement output render texture
        dxDisplacementOutputRenderTexture.enableRandomWrite = true;
        // "Actually create render texture"
        dxDisplacementOutputRenderTexture.Create();

        // Create dy displacement render texture output
        dyDisplacementOutputRenderTexture = new RenderTexture(N, N, 0);
        // Enable writing to dy displacement output render texture
        dyDisplacementOutputRenderTexture.enableRandomWrite = true;
        // "Actually create render texture"
        dyDisplacementOutputRenderTexture.Create();

        // Create dz displacement render texture output
        dzDisplacementOutputRenderTexture = new RenderTexture(N, N, 0);
        // Enable writing to dz displacement output render texture
        dzDisplacementOutputRenderTexture.enableRandomWrite = true;
        // "Actually create render texture"
        dzDisplacementOutputRenderTexture.Create();

        // Get shader kernel id of program we want to execute
        kernelProgramId = hktComputeShader.FindKernel(ConstStrings.shaderMainProgramstr);
        // Set shader input textures (H0K output)
        hktComputeShader.SetTexture(kernelProgramId, ConstStrings.shaderh0kOutputTextureStr, h0kTexture);
        hktComputeShader.SetTexture(kernelProgramId, ConstStrings.shaderh0minuskOutputTextureStr, h0minuskTexture);
        // Set shader output textures (dx/dy/dz displacement maps(hkt))
        hktComputeShader.SetTexture(kernelProgramId, ConstStrings.dxDisplacementTextureStr, dxDisplacementOutputRenderTexture);
        hktComputeShader.SetTexture(kernelProgramId, ConstStrings.dyDisplacementTextureStr, dyDisplacementOutputRenderTexture);
        hktComputeShader.SetTexture(kernelProgramId, ConstStrings.dzDisplacementTextureStr, dzDisplacementOutputRenderTexture);
        // Set shader uniform values
        hktComputeShader.SetInt(ConstStrings.shaderInput_N, N);
        hktComputeShader.SetInt(ConstStrings.shaderInput_L, L);
        hktComputeShader.SetFloat(ConstStrings.shaderInput_T, T);
        // Run hkt compute shader
        hktComputeShader.Dispatch(kernelProgramId, N / 16, N / 16, 1);
    }

    public void RunUpdate (float T)
    {
        // Update time and recompute hkt for new time
        hktComputeShader.SetFloat(ConstStrings.shaderInput_T, T);
        // Run hkt compute shader
        hktComputeShader.Dispatch(kernelProgramId, N / 16, N / 16, 1);
    }
}
