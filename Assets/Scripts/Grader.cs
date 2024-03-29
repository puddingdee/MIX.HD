using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class Grader : MonoBehaviour
{
    public int epicValue = 0;
    public readonly int targetEpicness = 300;
    public string finalGrade = "";
    public int epicDisparity;
    [SerializeField] GameManager gameManager;
    [SerializeField] GameObject gradeText;
    [SerializeField] MixingBrain mixBr;
    [SerializeField] float drainSpeed;
    TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {

        StartCoroutine(TimeTilEnd());
        StartCoroutine(EpicDrain());

        text = gradeText.GetComponent<TextMeshProUGUI>();


    }

    // Update is called once per frame
    void Update()
    {
        if (epicValue < 0)
        {
            epicValue = 0;
        }
        else if (epicValue > 300)
        {
            epicValue = 300;
        }
        epicDisparity = targetEpicness - epicValue;
        
    }

    public void MakeAGrade()
    {
        Debug.Log(epicValue + " " + targetEpicness);
        

        if (epicDisparity < 10)
        {
            finalGrade = "MONUMENTAL.";
        }
        else if (epicDisparity < 50)
        {
            finalGrade = "SATISFACTORY.";
        }
        else if (epicDisparity < 100)
        {
            finalGrade = "...";
        }
        else if (epicDisparity < 150)
        {
            finalGrade = "FORGETTABLE.";
        }
        else if (epicDisparity < 200)
        {
            finalGrade = "INEXCUSABLE.";
        }
        else if (epicDisparity >= 200)
        {
            finalGrade = "WORSE THAN DEATH ITSELF.";
        }
        if (SceneManager.GetActiveScene().name == "g_basement")
        {
            epicDisparity = Random.Range(0, 250);
            if (epicDisparity < 10)
            {
                finalGrade = "MONUMENTAL.";
            }
            else if (epicDisparity < 50)
            {
                finalGrade = "SATISFACTORY.";
            }
            else if (epicDisparity < 100)
            {
                finalGrade = "...";
            }
            else if (epicDisparity < 150)
            {
                finalGrade = "FORGETTABLE.";
            }
            else if (epicDisparity < 200)
            {
                finalGrade = "INEXCUSABLE.";
            }
            else if (epicDisparity >= 200)
            {
                finalGrade = "WORSE THAN DEATH ITSELF.";
            }
        }
        StartCoroutine(DisplayGrade());
    }

    private IEnumerator TimeTilEnd()
    {
        yield return new WaitForSeconds(gameManager.totalSongTime);
        MakeAGrade();
    }

    private IEnumerator DisplayGrade()
    {
        //TODO play grade sfx
        text.SetText(finalGrade);
        mixBr.darkticle.SetActive(true);
        gradeText.SetActive(true);
        yield return new WaitForSeconds(5);
        mixBr.darkticle.SetActive(false);

        text.SetText("CHOOSE ANOTHER BAND. SHOW NOTES ARE ON THE BACK WALL.");
        //TODO go back to sound check

    }
    private IEnumerator EpicDrain()
    {
        while (true)
        {
            epicValue -= 1;
            yield return new WaitForSeconds(drainSpeed);
        }
        
    }
}
