using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarManager : MonoBehaviour
{
    public int score;
    public int toGet1, toGet2, toGet3;
    public GameObject[] stars;

    private void Start()
    {
        score = ScoreTracker.instance.score;

       if(score <= toGet1)
        {
            print("No score, try harder!");
        }

        if (score >= toGet1)
        {
            stars[0].SetActive(true);
        }

        if (score >= toGet2)
        {
            stars[1].SetActive(true);
        }

        if (score >= toGet3)
        {
            stars[2].SetActive(true);
        }
    }
}
