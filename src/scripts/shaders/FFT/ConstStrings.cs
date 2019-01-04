using UnityEngine;

public static class ConstStrings
{
    /* Shader const strings */
    public const string shaderMainProgramstr = "main";
    // h0k
    public const string noiseTextureStr1 = "noiseTexture1";
    public const string noiseTextureStr2 = "noiseTexture2";
    public const string noiseTextureStr3 = "noiseTexture3";
    public const string noiseTextureStr4 = "noiseTexture4";

    public const string shaderh0kOutputTextureStr = "h0kTexture";
    public const string shaderh0minuskOutputTextureStr = "h0minuskTexture";
    // twiddle factors
    public const string shaderTwiddleFactorsOutputTextureStr = "twiddleFactorsTexture";

    public const string shaderTwiddleFactorsInput_bitReverseIndices = "bitReversedIndices";
    // hkt
    public const string shaderInput_T = "T"; // Frame time

    public const string dxDisplacementTextureStr = "dxDisplacement";
    public const string dyDisplacementTextureStr = "dyDisplacement";
    public const string dzDisplacementTextureStr = "dzDisplacement";

    // ocean settings
    public const string shaderInput_N = "N";
    public const string shaderInput_L = "L";
    public const string shaderInput_amplitude = "amplitude";
    public const string shaderInput_windSpeed = "windSpeed";
    public const string shaderInput_capillarSupressFactor = "capillarSupressFactor";
    public const string shaderInput_direction = "direction";
}
