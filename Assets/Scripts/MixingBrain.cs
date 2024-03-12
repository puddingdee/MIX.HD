using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine;
using csoundcsharp;
using System;
using Unity.VisualScripting;

public class MixingBrain : MonoBehaviour
{
    [SerializeField] GameObject dlm1;
    [SerializeField] GameObject dlm2;
    [SerializeField] GameObject dlm3;
    [SerializeField] GameObject dlm4;
    [SerializeField] int dlmWeight;

    [SerializeField] GameObject dlt1;
    [SerializeField] GameObject dlt2;
    [SerializeField] GameObject dlt3;
    [SerializeField] GameObject dlt4;
    [SerializeField] GameObject dlt1light;
    [SerializeField] GameObject dlt2light;
    [SerializeField] GameObject dlt3light;
    [SerializeField] GameObject dlt4light;
    [SerializeField] int dltWeight;


    [SerializeField] GameObject dist1;
    [SerializeField] GameObject dist2;
    [SerializeField] GameObject dist3;
    [SerializeField] GameObject dist4;
    [SerializeField] int distWeight;


    [SerializeField] GameObject ex1;
    [SerializeField] GameObject ex2;
    [SerializeField] GameObject ex3;
    [SerializeField] GameObject ex4;
    [SerializeField] GameObject exW1;
    [SerializeField] GameObject exW2;
    [SerializeField] GameObject exW3;
    [SerializeField] GameObject exW4;
    [SerializeField] int exWeight;


    [SerializeField] GameObject fold1;
    [SerializeField] GameObject fold2;
    [SerializeField] GameObject fold3;
    [SerializeField] GameObject fold4;
    [SerializeField] int foldWeight;


    [SerializeField] GameObject gain1;
    [SerializeField] GameObject gain2;
    [SerializeField] GameObject gain3;
    [SerializeField] GameObject gain4;
    [SerializeField] int gainWeight;


    [SerializeField] GameObject cut1;
    [SerializeField] GameObject cut2;
    [SerializeField] GameObject cut3;
    [SerializeField] GameObject cut4;
    [SerializeField] int cutWeight;


    [SerializeField] GameObject clip;
    [SerializeField] int clipWeight;
    [SerializeField] GameObject clipLight;
    [SerializeField] GameObject cross;
    [SerializeField] int crossWeight;


    [SerializeField] GameObject selecticle;
    [SerializeField] GameObject adjusticle;

    [SerializeField] GameObject o_shownote;
    [SerializeField] GameObject s_shownote;
    [SerializeField] GameObject g_shownote;
    [SerializeField] GameObject o_inspecticle;
    [SerializeField] GameObject s_inspecticle;
    [SerializeField] GameObject g_inspecticle;
    [SerializeField] GameObject soundCheck;
    [SerializeField] GameObject soundCheckInspecticle;

    [SerializeField] GameObject backToMenu;

    [SerializeField] GameObject lightSwitch;
    [SerializeField] public GameObject darkticle;
    [SerializeField]  GameObject backtoticle;

    [SerializeField] GameManager gameManager;

    private bool isDark = false;

    [SerializeField] float testZ;

    private float DLM_Y1;
    private float DLM_Y2;
    private float DLM_Y3;
    private float DLM_Y4;

    private float FOLD_Y1;
    private float FOLD_Y2;
    private float FOLD_Y3;
    private float FOLD_Y4;

    private float GAIN_Z1;
    private float GAIN_Z2;
    private float GAIN_Z3;
    private float GAIN_Z4;

    private float CUT_Y1;
    private float CUT_Y2;
    private float CUT_Y3;
    private float CUT_Y4;

    private CsoundUnity csound;

    public bool isCross = false;
    public bool isInspecting = false;
    public bool loading = false;
    public int loadingIndex;

    [SerializeField] private CameraController cam;
    

    [SerializeField] Grader grader;
    


    // Start is called before the first frame update
    void Start()
    {
        csound = GetComponent<CsoundUnity>();
        StartCoroutine(WaitForMessage());

        DLM_Y1 = dlm1.transform.position.y;
        DLM_Y2 = dlm2.transform.position.y;
        DLM_Y3 = dlm3.transform.position.y;
        DLM_Y4 = dlm4.transform.position.y;

        FOLD_Y1 = fold1.transform.position.y;
        FOLD_Y2 = fold1.transform.position.y;
        FOLD_Y3 = fold1.transform.position.y;
        FOLD_Y4 = fold1.transform.position.y;

        GAIN_Z1 = gain1.transform.localPosition.z;
        GAIN_Z2 = gain2.transform.localPosition.z;
        GAIN_Z3 = gain3.transform.localPosition.z;
        GAIN_Z4 = gain4.transform.localPosition.z;

        CUT_Y1 = cut1.transform.localPosition.y;
        CUT_Y2 = cut2.transform.localPosition.y;
        CUT_Y3 = cut3.transform.localPosition.y;
        CUT_Y4 = cut4.transform.localPosition.y;

        GameObject[] gain = { gain1, gain2, gain3, gain4 };
        GameObject[] cut = { cut1, cut2, cut3, cut4 };

        double gainVal = csound.GetChannel("gain1");
        
        for (int i = 0; i < gain.Length; i++)
        {
            gain[i].transform.localPosition = new Vector3(gain[i].transform.localPosition.x, gain[i].transform.localPosition.y, testZ); 
        }
        /*
        for (int i = 0; i < cut.Length; i++)
        {
            cut[i].transform.localPosition = new Vector3(cut[i].transform.localPosition.x, cut[i].transform.localPosition.y, testZ);
        }
        */

        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (!csound.IsInitialized) return;
        if (csound.IsInitialized)
        {
            cam.csoundInit = true;
        }
        ;
        Mouse mouse = Mouse.current;

        CheckForParam();
        if (mouse.leftButton.wasPressedThisFrame)
        {
            GameObject currentParam = GetClickedParameter();
            
            if (currentParam == null)
            {
                
            }
            else if (currentParam == dlm1 || currentParam == dlm2 || currentParam == dlm3 || currentParam == dlm4)
            {
                
                ToggleParam(currentParam);
                grader.epicValue += dlmWeight;
                
            }
            else if (currentParam == dlt1 || currentParam == dlt2 || currentParam == dlt3 || currentParam == dlt4)
            {

                StartCoroutine(DialDLT(currentParam));
                adjusticle.SetActive(true);
                grader.epicValue += dltWeight;
                
            }
            else if (currentParam == dist1 || currentParam == dist2 || currentParam == dist3 || currentParam == dist4)
            {

                StartCoroutine(DialDist(currentParam));
                adjusticle.SetActive(true);
                grader.epicValue += distWeight;
                
            }
            else if (currentParam == ex1 || currentParam == ex2 || currentParam == ex3 || currentParam == ex4)
            {

                ToggleParam(currentParam);
                grader.epicValue += exWeight;
                
            }
            else if (currentParam == fold1 || currentParam == fold2 || currentParam == fold3 || currentParam == fold4)
            {

                StartCoroutine(DialFold(currentParam));
                adjusticle.SetActive(true);
                grader.epicValue += foldWeight;
            }
            else if (currentParam == gain1 || currentParam == gain2 || currentParam == gain3 || currentParam == gain4)
            {
                
                StartCoroutine(DialGain(currentParam));
                adjusticle.SetActive(true);
            }
            else if (currentParam == cut1 || currentParam == cut2 || currentParam == cut3 || currentParam == cut4)
            {
                StartCoroutine(DialCut(currentParam));
                adjusticle.SetActive(true);
                grader.epicValue -= cutWeight;
            }
            else if (currentParam == clip)
            {

                ToggleParam(currentParam);
                grader.epicValue += clipWeight;
            }
            else if (currentParam == cross)
            {

                ToggleParam(currentParam);
                grader.epicValue += crossWeight;
            }
            else if (currentParam == o_shownote)
            {
                isInspecting = true;
                o_inspecticle.SetActive(true);
            }
            else if (currentParam == s_shownote)
            {
                isInspecting = true;

                s_inspecticle.SetActive(true);
            }
            else if (currentParam == g_shownote)
            {
                isInspecting = true;

                g_inspecticle.SetActive(true);
            }
            else if (currentParam == soundCheck)
            {
                isInspecting = true;

                soundCheckInspecticle.SetActive(true);
            }
            else if (currentParam.name == "s_inspecticle" && !loading)
            {
                loadingIndex = 2;
                loading = true;
                
                
            }
            else if (currentParam.name == "g_inspecticle" && !loading)
            {
                loadingIndex = 3;
                loading = true;
                
                
            }
            else if (currentParam.name == "o_inspecticle" && !loading)
            {
                loadingIndex = 1;
                loading = true;
            }
            else if (currentParam == backtoticle)
            {
                loadingIndex = 0;
                loading = true;
            }
            else if (currentParam == lightSwitch)
            {
                if (isDark)
                {
                    darkticle.SetActive(false);
                    isDark = false;
                }
                else
                {
                    darkticle.SetActive(true);
                    isDark = true;
                }
                
            }

        }
        
        if (mouse.leftButton.wasReleasedThisFrame)
        {
            StopAllCoroutines();
            adjusticle.SetActive(false);
            //CHANGE THIS TO SPECIFIC COROUTINES IF IT BREAKS OTHER THINGS LATER
        }

        if (this.name == "Speaker Left")
        {
            if (mouse.rightButton.wasPressedThisFrame)
            {
                if (isInspecting)
                {
                    o_inspecticle.SetActive(false);
                    s_inspecticle.SetActive(false);
                    g_inspecticle.SetActive(false);
                    soundCheckInspecticle.SetActive(false);
                    backToMenu.SetActive(false);
                    backtoticle.SetActive(false);
                    isInspecting = false;

                }
                else
                {
                    backToMenu.SetActive(true);
                    backtoticle.SetActive(true);
                    isInspecting = true;
                    
                }

            }
        }




        
    }

    private GameObject GetClickedParameter()
    {
            



        RaycastHit hit;
        var mainCamera = Camera.main.transform;
        if (Physics.Raycast(mainCamera.position, mainCamera.forward, out hit, 100f))
        {

            
            return hit.transform.gameObject;
        }
        else
        {
            return null;
        }



    }

    private void CheckForParam()
    {
        RaycastHit hit;
        var mainCamera = Camera.main.transform;
        if (Physics.Raycast(mainCamera.position, mainCamera.forward, out hit, 100f))
        {

            selecticle.SetActive(true);
            if (hit.transform.gameObject == GameObject.Find("surface"))
            {
                selecticle.SetActive(false);
            }
            
            
        }
        else
        {
            selecticle.SetActive(false);
        }

    }

    private void ToggleParam(GameObject param)
    {
        if (param == cross)
        {
            CrossThing(param);
        }
        else if (param == ex1 || param == ex2 || param == ex3 || param == ex4)
        {
            ExThing(param);
        }
        if (csound.GetChannel(param.name) == 0)
        {
            csound.SetChannel(param.name, 1);
            lightUpDLM(param);
            if (param == clip)
            {
                clipLight.SetActive(true);
            }
        }
        else
        {
            csound.SetChannel(param.name, 0);
            lightUpDLM(param);
            if (param == clip)
            {
                clipLight.SetActive(false);
            }
        }
        
    }

    private void CrossThing(GameObject param)
    {
        if (csound.GetChannel(param.name) == 1)
        {
            cross.transform.localRotation = Quaternion.Euler(transform.localRotation.x, transform.localRotation.y + 45, transform.localRotation.z);
            isCross = false;
        }
        else if (csound.GetChannel(param.name) == 0)
        {
            cross.transform.localRotation = Quaternion.Euler(transform.localRotation.x, transform.localRotation.y - 45, transform.localRotation.z);
            isCross = true;
        }

    }

    private void ExThing(GameObject param)
    {
        if (csound.GetChannel(param.name) == 0)
        {


            //light up
            if (param == ex1)
            {
                
                exW1.SetActive(true);
            }
            else if (param == ex2)
            {
                exW2.SetActive(true);
            }
            else if (param == ex3)
            {
                exW3.SetActive(true);
            }
            else if (param == ex4)
            {
                exW4.SetActive(true);
            }
        }
        else
        {

            if (param == ex1)
            {

                exW1.SetActive(false);
            }
            else if (param == ex2)
            {
                exW2.SetActive(false);
            }
            else if (param == ex3)
            {
                exW3.SetActive(false);
            }
            else if (param == ex4)
            {
                exW4.SetActive(false);
            }
        }

    }

    private void lightUpDLM(GameObject param)
    {
        if (csound.GetChannel(param.name) == 1)
        {
            

            //light up
            if (param == dlm1)
            {
                
                dlt1light.SetActive(true);
            }
            else if (param == dlm2)
            {
                dlt2light.SetActive(true);
            }
            else if (param == dlm3)
            {
                dlt3light.SetActive(true);
            }
            else if (param == dlm4)
            {
                dlt4light.SetActive(true);
            }
        }
        else
        {
            
            if (param == dlm1)
            {
               
                dlt1light.SetActive(false);
            }
            else if (param == dlm2)
            {
                dlt2light.SetActive(false);
            }
            else if (param == dlm3)
            {
                dlt3light.SetActive(false);
            }
            else if (param == dlm4)
            {
                dlt4light.SetActive(false);
            }
        }
    }

    private IEnumerator DialDLT(GameObject param)
    {
        Mouse mouse = Mouse.current;
        while (!mouse.leftButton.wasReleasedThisFrame)
        {
            float mouseY = Input.GetAxis("Mouse Y");
            double change = csound.GetChannel(param.name);
            change += mouseY*50f;
            change = Mathf.Clamp((float)change, 1f, 500f);
            
            csound.SetChannel(param.name, change);
            yield return null;

            //calculate percent of param
            float percentParam = (float)change / 500f;
            


            //move it
            GameObject GOparam = GameObject.Find(param.name);
            GOparam.transform.rotation = Quaternion.Euler(GOparam.transform.eulerAngles.x, GOparam.transform.eulerAngles.y, percentParam * 1000);

            
            if (param == dlt1)
            {
                dlm1.transform.position = new Vector3(dlm1.transform.position.x, DLM_Y1+(0.2f * percentParam), dlm1.transform.position.z);
            }
            else if (param == dlt2)
            {
                dlm2.transform.position = new Vector3(dlm2.transform.position.x, DLM_Y2+(0.2f * percentParam), dlm2.transform.position.z);

            }
            else if (param == dlt3)
            {
                dlm3.transform.position = new Vector3(dlm3.transform.position.x, DLM_Y3+(0.2f * percentParam), dlm3.transform.position.z);

            }
            else if (param == dlt4)
            {
                dlm4.transform.position = new Vector3(dlm4.transform.position.x, DLM_Y4+(0.2f * percentParam), dlm4.transform.position.z);

            }
            yield return null;
        }

        yield return null;
        
    }
    private IEnumerator DialDist(GameObject param)
    {
        Mouse mouse = Mouse.current;
        while (!mouse.leftButton.wasReleasedThisFrame)
        {

            //csound
            float mouseY = Input.GetAxis("Mouse Y");
            double change = csound.GetChannel(param.name);
            change += mouseY/2f;
            change = Mathf.Clamp((float)change, 0f, 2f);
            
            csound.SetChannel(param.name, change);

            //calculate percent of param
            float percentParam = (float)change / 2f;
            


            //move it
            GameObject GOparam = GameObject.Find(param.name);
            GOparam.transform.rotation = Quaternion.Euler(GOparam.transform.eulerAngles.x, GOparam.transform.eulerAngles.y, percentParam * 100);
            
            yield return null;
        }

        yield return null;

    }

    private IEnumerator DialFold(GameObject param)
    {
        Mouse mouse = Mouse.current;
        while (!mouse.leftButton.wasReleasedThisFrame)
        {
            float mouseY = -Input.GetAxis("Mouse Y");
            double change = csound.GetChannel(param.name);
            change += mouseY * 10f;
            change = Mathf.Clamp((float)change, 0f, 100f);
            
            csound.SetChannel(param.name, change);

            //calculate percent of param
            float percentParam = (float)change / 100f;



            //move it
            GameObject GOparam = GameObject.Find(param.name);
            GOparam.transform.rotation = Quaternion.Euler(GOparam.transform.eulerAngles.x, GOparam.transform.eulerAngles.y, percentParam * 200);


            if (param == fold1)
            {
                param.transform.position = new Vector3(param.transform.position.x, FOLD_Y1 - (0.1f * percentParam), param.transform.position.z);
            }
            else if (param == fold2)
            {
                param.transform.position = new Vector3(param.transform.position.x, FOLD_Y2 - (0.1f * percentParam), param.transform.position.z);

            }
            else if (param == fold3)
            {
                param.transform.position = new Vector3(param.transform.position.x, FOLD_Y3 - (0.1f * percentParam), param.transform.position.z);

            }
            else if (param == fold4)
            {
                param.transform.position = new Vector3(param.transform.position.x, FOLD_Y4 - (0.1f * percentParam), param.transform.position.z);

            }
            yield return null;
        }

        yield return null;

    }
    private IEnumerator DialGain(GameObject param)
    {
        Mouse mouse = Mouse.current;
        while (!mouse.leftButton.wasReleasedThisFrame)
        {
            float mouseY = Input.GetAxis("Mouse Y");
            double change = csound.GetChannel(param.name);
            change += mouseY/4f;
            change = Mathf.Clamp((float)change, 0f, 1f);
            
            csound.SetChannel(param.name, change);
            //calculate percent of param
            float percentParam = (float)change / 1f;
            //move it


            if (param == gain1)
            {
                param.transform.localPosition = new Vector3(param.transform.localPosition.x, param.transform.localPosition.y, GAIN_Z1 - (0.3f * percentParam) + 0.2f);
            }
            else if (param == gain2)
            {
                param.transform.localPosition = new Vector3(param.transform.localPosition.x, param.transform.localPosition.y, GAIN_Z2 - (0.3f * percentParam) + 0.2f);

            }
            else if (param == gain3)
            {
                param.transform.localPosition = new Vector3(param.transform.localPosition.x, param.transform.localPosition.y, GAIN_Z3 - (0.3f * percentParam) + 0.2f);

            }
            else if (param == gain4)
            {
                param.transform.localPosition = new Vector3(param.transform.localPosition.x, param.transform.localPosition.y, GAIN_Z4 - (0.3f * percentParam) + 0.2f);

            }
            yield return null;
        }

        yield return null;

    }
    private IEnumerator DialCut(GameObject param)
    {
        Mouse mouse = Mouse.current;
        while (!mouse.leftButton.wasReleasedThisFrame)
        {
            float mouseY = Input.GetAxis("Mouse Y");
            double change = csound.GetChannel(param.name);
            change += mouseY * 1000;
            change = Mathf.Clamp((float)change, 10000f, 20000f);
            
            csound.SetChannel(param.name, change);

            //calculate percent of param
            float percentParam = (float)change / 20000f;



            //move it



            if (param == cut1)
            {
                param.transform.position = new Vector3(param.transform.position.x, CUT_Y1 + (0.4f * percentParam) - 1.9f, param.transform.position.z);
            }
            else if (param == cut2)
            {
                param.transform.position = new Vector3(param.transform.position.x, CUT_Y2 + (0.4f * percentParam) - 1.9f, param.transform.position.z);

            }
            else if (param == cut3)
            {
                param.transform.position = new Vector3(param.transform.position.x, CUT_Y3 + (0.4f * percentParam) - 1.9f, param.transform.position.z);

            }
            else if (param == cut4)
            {
                param.transform.position = new Vector3(param.transform.position.x, CUT_Y4 + (0.4f * percentParam) - 1.9f, param.transform.position.z);

            }
            yield return null;
        }
        
        yield return null;

    }



}
