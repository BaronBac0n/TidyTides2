using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageBag : MonoBehaviour
{

    public GameObject garbageManager;
    public int garbageAmount = 50;

    private void OnCollisionEnter(Collision collision)
    {
        GarbageManager gManager = garbageManager.GetComponent<GarbageManager>();
        for(int i=0; i < garbageAmount; i++)
        {
            int garbageNumber = Random.Range(0, gManager.prefabPool.Length);
            Instantiate(gManager.prefabPool[garbageNumber], transform.position, Quaternion.identity);
        }
        Destroy(this);
    }
}
