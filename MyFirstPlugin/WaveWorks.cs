using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices; // Needed to import DLLs
using UnityEngine;

public class WaveWorks : MonoBehaviour
{
    [DllImport("WaveWorks")]
    private static extern int GetWaveAmplitude();

	void Start ()
    {
        Debug.Log(GetWaveAmplitude());
	}

}
