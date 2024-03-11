using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slowosc : MonoBehaviour
{


    private LineRenderer lineRenderer;
    private float[] spectrum = new float[256];
    private int lineLength = 256;
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = lineLength;
    }

    // Update is called once per frame
    void Update()
    {


        AudioListener.GetSpectrumData(spectrum, 0, FFTWindow.Blackman);


        for (int i = 1; i < spectrum.Length; i++)
        {
            
            lineRenderer.SetPosition(i, new Vector3(Mathf.Log(i), Mathf.Lerp(lineRenderer.GetPosition(i).y, spectrum[i] * 7f, 0.005f), 0.0f));
        }
    }
}