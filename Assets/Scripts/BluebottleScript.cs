using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BluebottleScript : MonoBehaviour
{
    public float stunTime;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            FirstPersonAIO player = other.GetComponent<FirstPersonAIO>();

            player.stunned = true;
            player.stunTime = stunTime;
            Destroy(this.gameObject);
        }
    }
}
