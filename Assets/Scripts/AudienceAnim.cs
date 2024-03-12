using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudienceAnim : MonoBehaviour
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
        upperRange =grader.epicValue;

    }

    private IEnumerator RotateToBeat()
    {
        while (true)
        {
            while (!mixBr.isCross)
            {
                transform.rotation = Quaternion.Euler(transform.rotation.x-90 + Random.Range(0, upperRange), transform.rotation.y + Random.Range(0, upperRange), transform.rotation.z + Random.Range(0, upperRange));
                yield return new WaitForSeconds(30f / gameManager.BPM);
            }
            while (mixBr.isCross)
            {
                transform.rotation = Quaternion.Euler(transform.rotation.x-90 + Random.Range(0, 300), transform.rotation.y + Random.Range(0, 300), transform.rotation.z + Random.Range(0, 300));
                yield return null;
            }
        }

    }

}
