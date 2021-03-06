using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{
    public bool canLitter = true;
    public float shoutCooldown;
    float counter = 0;
    [Tooltip("Put an empty gameobject here for the ai to chase")]
    public Transform destination;
    [Tooltip("You can leave this, it is handled by the script it is only public for testing purposes")]
    public NavMeshAgent navMeshAgent;

    [Tooltip("Put the player gameobject in here")]
    public GameObject Player;
    [Tooltip("How far will the ai try to stay away from the player")]
    public float EnemyDistanceRun = 4f;

    [Tooltip("Place the garbageManager empty game object here or it won't work")]
    public GameObject garbageManager;

    [Tooltip("These vectors control the size of the space where the AI can walk, a green box is shown in the editor to represent this space")]
    public Vector3 size;
    [Tooltip("These vectors control the size of the space where the AI can walk, a green box is shown in the editor to represent this space")]
    public Vector3 centre;

    [Tooltip("This is the largest amount of time the randomised timer can be set to, larger number here means longer delay between movement and litter actions")]
    public float maxTimer;
    private float timer;

    private int state;

    private Animator anim;

    enum EnemyState { Litter = 1, Flee}

    private void Start()
    {
        anim = this.GetComponent<Animator>();
        navMeshAgent = this.GetComponent<NavMeshAgent>();
        if(navMeshAgent == null)
        {
            Debug.LogError("No NavMeshAgent found!");
        }
        else
        {
            SetDestination();
        }
    }


    private void SetDestination() //Move our agent to the destination
    {
        if (destination != null)
        {
            Vector3 targetVector = destination.transform.position;
            navMeshAgent.SetDestination(targetVector);
        }
    }

    private void MoveDestination() //Randomly move our destination within the constrains of the box
    {
        Vector3 pos = centre + new Vector3(Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.y / 2, size.y / 2), Random.Range(-size.z / 2, size.z / 2));
        destination.transform.position = pos;
        SetDestination();
    }

    void RandomiseTimer() //Randomly set our move and litter delay timer
    {
        timer = Random.Range(1f, maxTimer);
    }

    void Litter() //Spawn the prefabs that are in the garbageManager game object's array
    {
        GarbageManager gManager = garbageManager.GetComponent<GarbageManager>();
        int garbageNumber = Random.Range(0, gManager.prefabPool.Length);
        Instantiate(gManager.prefabPool[garbageNumber], transform.position, Quaternion.identity);
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        float distance = Vector3.Distance(transform.position, Player.transform.position);
        //Debug.Log("Distance: " + distance);

        if (distance < EnemyDistanceRun)
        {
            state = 2;
        }
        else
        {
            state = 1;
        }

        if (navMeshAgent.velocity != Vector3.zero)
        {
            anim.SetBool("isMoving", true);
        }
        else
        {
            anim.SetBool("isMoving", false);
        }

        if (canLitter)
        {
            this.tag = "Enemy";
        }
        else
        {
            this.tag = "Passive";
            counter += Time.deltaTime;
            if(counter >= shoutCooldown)
            {
                canLitter = true;
                counter = 0;
            }
        }
        
        switch (state)
        {
            case (int)EnemyState.Litter:
                if (timer <= 0)
                {
                    MoveDestination();
                    RandomiseTimer();
                    if(canLitter)
                    Litter();
                }
                break;
            case (int)EnemyState.Flee:
                Debug.Log("Running!");
                Vector3 dirToPlayer = transform.position - Player.transform.position;
                Vector3 newPos = transform.position + dirToPlayer;
                navMeshAgent.SetDestination(newPos);
                break;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0, 1, 0, 0.5f); // Fancy visual indicator go brr
        Gizmos.DrawCube(centre, size);
    }

}
