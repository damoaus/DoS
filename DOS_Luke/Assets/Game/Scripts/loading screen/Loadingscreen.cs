using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Loadingscreen : MonoBehaviour
{

    public GameObject LoadingScreen;
    GameObject loadtext;
    GameObject progressbar;
    private float loadingProgress = 0;
    public string loadlevel;
    public bool Nextscene = false;
    AsyncOperation Async;


    void Start()
    {
        LoadingScreen.SetActive(false);

        loadtext = LoadingScreen.transform.FindChild("Text").gameObject;
        progressbar = LoadingScreen.transform.FindChild("LoadingBar").gameObject;
        

    }


    void Update()
    {
        //Load next scene when Nextscene true Nextscene is changed in Go_to_tutorial and Gobal scripts
        if (Nextscene)
        {
            gameObject.GetComponent<SoundManger>().FadeSound();
            StartCoroutine(Wait());
            //StartCoroutine(displayLoadingScreen(loadlevel)); without waiting

        }
        if (Input.GetButtonDown("Fire1"))
        {
            ActivateScene();
            
        }
       
    }

    IEnumerator Wait()
    {
        progressbar.transform.localScale = new Vector3(loadingProgress, progressbar.transform.localScale.y, progressbar.transform.localScale.z);
        
        LoadingScreen.SetActive(true);
        loadtext.GetComponent<Text>().text = " Loading Progress " + loadingProgress + " % ";
        Nextscene = false;
        Time.timeScale = 1;
        yield return new WaitForSeconds(1f);
        StartCoroutine(displayLoadingScreen(loadlevel));

    }



    /// <summary>
    ///  Sets LoadingScreen to Active while the next scene is loading and shows progress of the load
    /// </summary>
    /// <param name="level"></param>
    /// <returns></returns>
    /// 
    IEnumerator displayLoadingScreen(string level)
    {
        
        Async = SceneManager.LoadSceneAsync(level);
        Async.allowSceneActivation = false;
        while (!Async.isDone)
        {
           
            loadingProgress =(Async.progress * 100);
            loadtext.GetComponent<Text>().text = " Loading Progress " + (int)(loadingProgress/.9f) + " % ";
            progressbar.transform.localScale = new Vector3((int)(Async.progress/.9f), progressbar.transform.localScale.y, progressbar.transform.localScale.z);

            yield return null;
            Time.timeScale = 0;
            
        }



    }
    public void ActivateScene()
    {
        if (Async != null)
        {
            Async.allowSceneActivation = true;

        }
    }
}
