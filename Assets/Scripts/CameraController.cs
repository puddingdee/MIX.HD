using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{


    private Vector3 v = Vector3.zero;
    

    private float rotationX = 0f;
    private float rotationY = 155f;

    [SerializeField] private Vector3 camOffset = new Vector3(0f, 0f, 0f);

    [SerializeField] private Transform focalPoint;
    [SerializeField] float minVerticalAngle = -65;
    [SerializeField] float maxVerticalAngle = 65;
    [SerializeField] private float shakeIntensity = 0f;
    [SerializeField] float workingIntensity;
    [SerializeField] float intensityMultiplier;
    
    [SerializeField] private MixingBrain mixBr;

    private AudioSource bassSource;
    [SerializeField] private float[] bassSamples = new float[64];
    [SerializeField] private GameObject speakerL;
    private CsoundUnity csound;
    
    private Vector3 staticPos;
    private long csoundTime;
    public bool csoundInit = false;

    // Start is called before the first frame update
    void Start()
    {
        bassSource = GetComponent<AudioSource>();
        csound = speakerL.GetComponent<CsoundUnity>();
        
        staticPos = transform.position;
        
        StartCoroutine(WaitForCSound());
        
        
        
    }




    // Update is called once per frame
    void LateUpdate()
    {
        csoundTime = csound.GetCurrentTimeSamples();

        rotationX += Input.GetAxis("Mouse Y");
            rotationY += Input.GetAxis("Mouse X");
            rotationX = Mathf.Clamp(rotationX, minVerticalAngle, maxVerticalAngle);

            Quaternion targetRotation = Quaternion.Euler(-rotationX, rotationY, 0);

        
        
            

            transform.rotation = targetRotation;




        bassSource.GetOutputData(bassSamples, 0);
        for (int a = 32; a < bassSamples.Length; a++)
        {
            float random = Random.Range(-0.1f, 0.1f);
            float random1 = Random.Range(-0.1f, 0.1f);
            float random2 = Random.Range(-0.1f, 0.1f);

            

            //transform.localPosition = new Vector3(staticPos.x + random * bassSamples[a], staticPos.y + random2 * bassSamples[a], staticPos.z + random1 * bassSamples[a]);
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x + random * bassSamples[a], transform.rotation.eulerAngles.y + random2 * bassSamples[a], transform.rotation.eulerAngles.z + random1 * bassSamples[a]);


        }







    }




    private IEnumerator WaitForCSound()
    {
        
        while (csoundTime <= 144000)
        {
            
            Debug.Log(csoundTime);
            yield return null;
        }

        yield return null;
        bassSource.Play();
        Debug.Log("playing bass");
    }




    //public Quaternion PlanarRotation => Quaternion.Euler(0, rotationY, 0);

}
