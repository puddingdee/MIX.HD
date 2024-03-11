using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class amplitudeScope : MonoBehaviour
{

    private LineRenderer lineRenderer;
    private float[] samples = new float[512];
    private int lineLength = 512;
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = lineLength;
    }

    // Update is called once per frame
    void Update()
    {


        AudioListener.GetOutputData(samples, 0);


        for (int i = 1; i < samples.Length; i++)
        {

            lineRenderer.SetPosition(i, new Vector3(i*0.01f, Mathf.Lerp(lineRenderer.GetPosition(i).y, samples[i] * 1.6f, 0.6f), 0.0f));
        }
    }
}
