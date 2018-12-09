using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RNGCompute : MonoBehaviour
{
    public Material computeMaterial;
    public int textureSize = 256;
    public float timer = 0.0f;
    public float timeToWait = 1.0f;

    public int seed; //temporarily public
    private ComputeShader fillWithRedShader;
    private int kernelIndex;
    private RenderTexture tempTex;

    void Start()
    {
        fillWithRedShader = (ComputeShader)Resources.Load(MyStrings.WavePractice);
        kernelIndex = fillWithRedShader.FindKernel(MyStrings.FillWithRandom);

        tempTex = new RenderTexture(textureSize, textureSize, 0);
        tempTex.enableRandomWrite = true;
        tempTex.Create();

        fillWithRedShader.SetInt("seed", seed);
        fillWithRedShader.SetTexture(kernelIndex, MyStrings.outputTexture, tempTex);
        fillWithRedShader.Dispatch(kernelIndex, textureSize, textureSize, 1);

        computeMaterial.mainTexture = tempTex;
    }

    void Update ()
    {
        timer += Time.deltaTime;
        if (timer > timeToWait)
        {
            timer = 0.0f;
            UpdateComputeShader();
        }
    }

    void UpdateComputeShader ()
    {
        fillWithRedShader.SetInt("seed", seed++);
        fillWithRedShader.Dispatch(kernelIndex, textureSize, textureSize, 1);
    }
}
