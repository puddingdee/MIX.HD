using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour
{
    [SerializeField] MixingBrain mixBr;
    [SerializeField] GameManager gameManager;
    private bool isLoading = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (mixBr.loading && !isLoading)
        {
            isLoading = true;
            StartCoroutine(LoadNewScene(mixBr.loadingIndex));

            
        }
    }

    private IEnumerator LoadNewScene(int scene)
    {

        
        gameManager.TurnOffMixBr();
        yield return new WaitForSeconds(3);

        AsyncOperation async = SceneManager.LoadSceneAsync(scene);
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
}
