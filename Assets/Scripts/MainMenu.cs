using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEditor;

public class MainMenu : MonoBehaviour
{
    private TextMeshProUGUI instructions;
    [SerializeField] GameObject loadText;
    private bool loading = false;
    // Start is called before the first frame update
    void Start()
    {
        instructions = loadText.GetComponent<TextMeshProUGUI>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        Mouse mouse = Mouse.current;
        if (mouse.leftButton.wasPressedThisFrame && !loading)
        {
            loading = true;
            instructions.SetText("LOADING");
            StartCoroutine(LoadGame());
        }
        else if(mouse.rightButton.wasPressedThisFrame)
        {
            ExitGame();
        }

        
    }

    private IEnumerator LoadGame()
    {
        
        yield return new WaitForSeconds(3);

        AsyncOperation async = SceneManager.LoadSceneAsync(1);
        async.allowSceneActivation = false;
        while (!async.isDone)
        {

            if (async.progress >= 0.9f)
            {

                async.allowSceneActivation = true;
            }

            yield return new WaitForEndOfFrame();
        }
    }
    public void ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
