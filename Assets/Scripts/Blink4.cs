using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blink4 : MonoBehaviour
{
    [SerializeField] private Material mat;
    private float minimum = -10;
    private float maximum = 6;
    private float t = 0.0f;
    private Color startColor = new Color(0.4f,0.1f,0,1);
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(waitabit());

    }

    IEnumerator waitabit()
    {
        yield return new WaitForSeconds(1.5f);
        t += 1f * Time.deltaTime;
        float intensity = Mathf.Lerp(minimum, maximum, t);
        mat.SetColor("_EmissionColor", startColor * Mathf.Pow(2, intensity));
        if (t > 1.0f)
        {
            float temp = maximum;
            maximum = minimum;
            minimum = temp;
            t = 0.0f;
        }
    }
}
