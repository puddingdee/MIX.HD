using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class HypeMeter : MonoBehaviour

{
    [SerializeField] Grader grader;
    [SerializeField] GameObject display;
    [SerializeField] float scale;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 displayChange = new Vector3(display.transform.localScale.x, grader.epicValue*scale, display.transform.localScale.z);
        display.transform.localScale = displayChange;
    }
}
