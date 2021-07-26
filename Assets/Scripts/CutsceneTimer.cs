using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneTimer : MonoBehaviour
{

    [Tooltip("put player gameobject here")]
    public GameObject player;
    [Tooltip("put cutscene camera here")]
    public GameObject cutsceneCamera;
    bool brochureOpened = false;
    bool timerStarted = false;
    [Tooltip("how long is the cutscene")]
    public float timer;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B) && brochureOpened == false)
        {
            timerStarted = true;
            player.SetActive(false);
            cutsceneCamera.SetActive(true);
            brochureOpened = true;
        }

        if(timerStarted == true)
        {
            timer -= Time.deltaTime;
        }

        if(timer <= 0)
        {
            player.SetActive(true);
            cutsceneCamera.SetActive(false);
        }
    }


}
