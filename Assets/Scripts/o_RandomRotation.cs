using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class o_RandomRotation : MonoBehaviour
{
    [SerializeField] o_MixingBrain mixBr;
    [SerializeField] Grader grader;
    [SerializeField] GameManager gameManager;
    private float upperRange;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(RotateToBeat());
        
    }

    // Update is called once per frame
    void Update()
    {
        upperRange = grader.epicValue;
    }

    private IEnumerator RotateToBeat()
    {
        while (true)
        {
            while (!mixBr.isCross)
            {
                transform.localRotation = Quaternion.Euler(transform.localRotation.x-90 + Random.Range(0, upperRange), transform.localRotation.y + Random.Range(0, upperRange), transform.localRotation.z + Random.Range(0, upperRange));
                yield return new WaitForSeconds(60f / gameManager.BPM);
            }
            while (mixBr.isCross)
            {
                transform.localRotation = Quaternion.Euler(transform.localRotation.x-90 + Random.Range(0, 300), transform.localRotation.y + Random.Range(0, 300), transform.localRotation.z + Random.Range(0, 300));
                yield return null;
            }
        }
        
    }

}
