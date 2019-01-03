//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class TwiddleFactors
{
    private int N; // Ocean mesh plane vertex resolution

    private ComputeShader twiddleFactorsComputeShader;
    private int kernelProgramId; // Shader kernel program ID

    public RenderTexture twiddleFactorsOutputRenderTexture;

    int log_2_N;

    public TwiddleFactors (int N, ComputeShader twiddleFactorsComputeShader)
    {
        this.N = N;
        this.twiddleFactorsComputeShader = twiddleFactorsComputeShader;

        log_2_N = (int)(Mathf.Log(N) / Mathf.Log(2));
    }

    public void Run ()
    {
        // Create twiddle factors render texture output
        twiddleFactorsOutputRenderTexture = new RenderTexture(log_2_N, N, 0);
        // Enable writing to twiddle factors output render texture
        twiddleFactorsOutputRenderTexture.enableRandomWrite = true;
        // "Actually create render texture"
        twiddleFactorsOutputRenderTexture.Create();

        // Get shader kernel id of program we want to execute
        kernelProgramId = twiddleFactorsComputeShader.FindKernel(ConstStrings.shaderMainProgramstr);
        // Set output textures
        twiddleFactorsComputeShader.SetTexture(kernelProgramId, ConstStrings.shaderTwiddleFactorsOutputTextureStr, twiddleFactorsOutputRenderTexture);

        // Initialize bit reversed indices
        // Declare int array of size N (mesh vertex resolution n x n = N ) | Currently supports 256x256
        int[] bitReversedIndices = new int[N];
        for (int i = 0; i < N; i++)
        {
            string indexBinaryString = System.Convert.ToString(i, 2).PadLeft(log_2_N, '0');
            bitReversedIndices[i] = System.Convert.ToInt32(Reverse(indexBinaryString), 2);
        }
        // Set bit reversed indices(indices int array bitReversedIndices)
        twiddleFactorsComputeShader.SetInts(ConstStrings.shaderTwiddleFactorsInput_bitReverseIndices, bitReversedIndices);
        // Set input N uniform value
        twiddleFactorsComputeShader.SetInt(ConstStrings.shaderInput_N, N);

        //// Run twiddle factors compute shader
        twiddleFactorsComputeShader.Dispatch(kernelProgramId, log_2_N, N / 16, 1);
    }
    private static string Reverse(string indexString)
    {
        char[] indexCharArray = indexString.ToCharArray();
        System.Array.Reverse(indexCharArray);
        return new string(indexCharArray);
    }
}
