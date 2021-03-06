using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreTracker : MonoBehaviour
{
    public int score = 0;
    public Text scoreText;
    #region Singleton
    public static ScoreTracker instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of ScoreTracker found");
            return;
        }
        instance = this;
        //scoreText = GameObject.FindGameObjectWithTag("Score Text").GetComponent<Text>();
    }
    #endregion

    private void Start()
    {
        if(SceneManager.GetActiveScene().name == "Main Scene")
        //scoreText = InventoryManager.instance.scoreText.GetComponent<Text>();
        Object.DontDestroyOnLoad(this.gameObject);
        
    }

    private void Update()
    {
        if(scoreText != null)
        scoreText.text = "SCORE: " + score;
    }

    private void OnLevelWasLoaded(int level)
    {
        if(level == 1)
        score = 0;
        if (level == 3)
        scoreText = GameObject.FindGameObjectWithTag("Score Text").GetComponent<Text>();
    }
}
