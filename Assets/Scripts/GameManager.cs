using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public float BPM = 186;
    public float totalSongTime = 139;
    [SerializeField] private GameObject MixBrR;
    [SerializeField] private GameObject MixBrL;

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

    public void TurnOffMixBr()
    {
        MixBrR.SetActive(false);
        MixBrL.SetActive(false);
    }
}
