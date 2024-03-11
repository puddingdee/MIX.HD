using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AdjusticleBounce : MonoBehaviour
{
    private float t = 0.0f;
    [SerializeField] private float loopTime = 1f;
    [SerializeField] float maximum = 0.01f;
    [SerializeField] float minimum = -0.01f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Bounce())
;    }

    // Update is called once per frame
    void Update()
    {
        t += 1f * Time.deltaTime;
        float change = Mathf.Lerp(minimum, maximum, t);

        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + change, transform.localPosition.z);

        

        if (t > loopTime)
        {
            float temp = maximum;
            maximum = minimum;
            minimum = temp;
            t = 0.0f;
        }
    }

    IEnumerator Bounce()
    {
        yield return null;

    }
}
