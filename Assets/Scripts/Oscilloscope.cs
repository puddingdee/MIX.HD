using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscilloscope : MonoBehaviour
{
    [SerializeField] GameObject speakerL;

    [SerializeField] Vector3 startPos;
    [SerializeField] GameObject oscPoint;
    private GameObject[] oscPoints = new GameObject[64];
    private Vector3[] cubes_position = new Vector3[64];
    

    
    AudioSource source;
    // Start is called before the first frame update
    void Start()
    {

        source = speakerL.GetComponent<AudioSource>();

        Vector3 temp = startPos;
        for (int i = 0; i < 64; i++)
        {
            temp = new Vector3(temp.x + 0.05f, temp.y, temp.z);
            GameObject cube = Instantiate(oscPoint, temp, oscPoint.transform.rotation);
            cubes_position[i] = cube.transform.position;
            oscPoints[i] = cube;
            
        }

    }

    // Update is called once per frame
    void Update()
    {
        
        float[] samples = new float[64];
        source.GetSpectrumData(samples, 0, FFTWindow.BlackmanHarris);
        for (int i = 0; i < samples.Length; i++)
        {
            cubes_position[i].Set(cubes_position[i].x, Mathf.Lerp(cubes_position[i].y, (samples[i]+2), 0.1f), cubes_position[i].z);
        }
        for (int i = 0;i < cubes_position.Length; i++)
        {
            oscPoints[i].transform.position = cubes_position[i];
        }
    }
}
