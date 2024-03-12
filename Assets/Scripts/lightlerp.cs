using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class lightlerp : MonoBehaviour
{
    [SerializeField] Color Color1;
    [SerializeField] Color Color2;
    [SerializeField] GameManager gameManager;
    private Light light;
    private float t = 0.0f;


    // Start is called before the first frame update
    void Start()
    {
        light = GetComponent<Light>();
       
    }

    // Update is called once per frame
    void Update()
    {
        
        t += 1f * Time.deltaTime;
        light.color = Vector4.Lerp(Color2, Color1, t);
        if (t > 5.0f)
        {
            Color temp = Color2;
            Color2 = Color1;
            Color1 = temp;
            t = 0.0f;
        }
        
        
    }



}
