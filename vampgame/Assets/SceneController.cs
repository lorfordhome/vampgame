using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public Animator transitionAnim;
    public static SceneController instance;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogWarning("gamemanager singleton error");
        }
    }
    public void ChangeScene(string sceneToLoad)
    {
        StartCoroutine(LoadScene(sceneToLoad));
    }
    IEnumerator LoadScene(string sceneToLoad)
    {
        transitionAnim.SetTrigger("End");
        yield return new WaitForSeconds(0.8f);
        AsyncOperation asyncLoad= SceneManager.LoadSceneAsync(sceneToLoad);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        transitionAnim.SetTrigger("Start");
    }
}
