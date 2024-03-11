using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Grader : MonoBehaviour
{
    public int epicValue = 0;
    public readonly int targetEpicness = 300;
    public string finalGrade = "";
    public int epicDisparity;
    [SerializeField] GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {

        StartCoroutine(TimeTilEnd());
        
    }

    // Update is called once per frame
    void Update()
    {
        if (epicValue < 0)
        {
            epicValue = 0;
        }
        epicDisparity = targetEpicness - epicValue;

    }

    public void MakeAGrade()
    {
        Debug.Log(epicValue + " " + targetEpicness);
        

        if (epicDisparity < 10)
        {
            finalGrade = "MONUMENTAL";
        }
        else if (epicDisparity < 50)
        {
            finalGrade = "SATISFACTORY";
        }
        else if (epicDisparity < 100)
        {
            finalGrade = "...";
        }
        else if (epicDisparity < 150)
        {
            finalGrade = "FORGETTABLE";
        }
        else if (epicDisparity < 200)
        {
            finalGrade = "INEXCUSABLE";
        }
        else if (epicDisparity >= 200)
        {
            finalGrade = "WORSE THAN DEATH ITSELF";
        }
        Debug.Log(finalGrade);
    }

    private IEnumerator TimeTilEnd()
    {
        yield return new WaitForSeconds(gameManager.o_TotalSongTime);
        MakeAGrade();
    }
}
