using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwapper : MonoBehaviour
{
    #region Singleton
    public static SceneSwapper instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of SceneSwapper found");
            return;
        }
        instance = this;
    }
    #endregion

    private void Update()
    {
        //if(Input.GetKeyDown(KeyCode.Y))
        //{
        //    ChangeScene("Score Scene");
        //}
    }

    public void ChangeScene(string name)
    {
        SceneManager.LoadScene(name, LoadSceneMode.Single);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
