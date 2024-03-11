using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public float o_BPM = 186;
    public float o_TotalSongTime = 139;

    // Start is called before the first frame update
    void Start()    
    {

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
