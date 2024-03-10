
using UnityEngine;

public class helloCsound : MonoBehaviour
{
    private CsoundUnity csound;
    private int minimum = 50;
    private int maximum = 10000;
    static float t = 0.0f;

    
    // Start is called before the first frame update
    void Start()
    {
        csound = GetComponent<CsoundUnity>();
        
    }

    // Update is called once per frame
    void Update()
    {
        t += 1f * Time.deltaTime;
        if (!csound.IsInitialized) return;
        csound.SetChannel("freq1", Mathf.Lerp(minimum, maximum, t));
        if (t > 1.0f)
        {
            int temp = maximum;
            maximum = minimum;
            minimum = temp;
            t = 0.0f;
        }
    }
}
