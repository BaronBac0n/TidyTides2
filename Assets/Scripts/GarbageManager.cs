using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageManager : MonoBehaviour
{
    [Tooltip("A hardcoded amount of garbage in the scene")]
    public int garbageAmount = 25; //How much garbage do we spawn?
    [Tooltip("If selected, set a minimum and maximum amount of garbage and it will choose a random amount each time")]
    public bool randomAmount = false;
    public int minAmount;
    public int maxAmount;
    [Tooltip("Add more elements and drop garbage prefabs into this array")]
    public GameObject[] prefabPool; //Garbage Prefabs go here

    public Vector3 centre; // These control where the garbage can spawn
    public Vector3 size; // Big Cube, can have multiple cubes for odd shaped terrain

    private void Start()
    {
        if (randomAmount)
        {
            garbageAmount = Random.Range(minAmount, maxAmount);
        }
        Spawn();
    }

    public void RandomPosition()
    {
        Vector3 pos = centre + new Vector3(Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.y / 2, size.y / 2), Random.Range(-size.z / 2, size.z / 2)); // Randomize position within the cube
    }

    public void Spawn()
    {
        for (int i = 0; i <= garbageAmount; i++) // Keeps spawning until the specified amount of garbage is reached
        {
            Vector3 pos = centre + new Vector3(Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.y / 2, size.y / 2), Random.Range(-size.z / 2, size.z / 2)); // Randomize position within the cube
            int garbageNumber = Random.Range(0, (prefabPool.Length)); // Random piece of garbage from the array
            Instantiate(prefabPool[garbageNumber], pos, Quaternion.Euler(new Vector3(Random.Range(0,360), Random.Range(0, 360), Random.Range(0, 360))));
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f); // Fancy visual indicator go brr
        Gizmos.DrawCube(centre, size);
    }
}
