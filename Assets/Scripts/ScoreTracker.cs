using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
        scoreText = InventoryManager.instance.scoreText.GetComponent<Text>();
        Object.DontDestroyOnLoad(this.gameObject);
    }

    private void Update()
    {
        scoreText.text = "SCORE: " + score;
    }

    private void OnLevelWasLoaded(int level)
    {
        scoreText = GameObject.FindGameObjectWithTag("Score Text").GetComponent<Text>();
    }
}
