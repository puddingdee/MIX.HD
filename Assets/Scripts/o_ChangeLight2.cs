using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class o_ChangeLight : MonoBehaviour
{
    [SerializeField] Color Color1;
    [SerializeField] Color Color2;
    private Light light;


    // Start is called before the first frame update
    void Start()
    {
        light = GetComponent<Light>();
        StartCoroutine(LightColorToBeat());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator LightColorToBeat()
    {
        while (true)
        {
            light.color = Color1;
            yield return new WaitForSeconds(60f / 186f);
            light.color = Color2;
            yield return new WaitForSeconds(60f / 186f);
        }

    }

}
