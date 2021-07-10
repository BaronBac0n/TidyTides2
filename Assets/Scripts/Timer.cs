using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float timeValue = 90;
    public Text timerText;

    #region Singleton
    public static Timer instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Timer found");
            return;
        }
        instance = this;
    }
    #endregion

    // Update is called once per frame
    void Update()
    {
       if (timeValue > 0)
        {
            timeValue -= Time.deltaTime;
        }
       else
        {
            timeValue = 0;
        }

       if(timeValue <= 0)
        {
            SceneSwapper.instance.ChangeScene("Score Scene");
        }

        DisplayTime(timeValue);
    }

    void DisplayTime(float timeToDisplay)
    {
        if (timeToDisplay < 0)
        {
            timeToDisplay = 0;
        }

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
