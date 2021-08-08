using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerRaycast : MonoBehaviour
{
    public float interactDistance;
    public Text interactText;
    RaycastHit whatIHit;
    public GameObject lookingAt;
    AudioSource audioSource;

    public AudioSource stopAudio;

    public AudioClip[] audioClips;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        stopAudio = transform.GetChild(0).GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (InventoryManager.instance.invBackground.activeInHierarchy == false) // if the player's inventory is not open
        {
            if (BrochureScript.instance.brochureUp == false) //if the brochure is not up
            {
                if (WhatDidISee() != null) // if the player is looking at something
                {
                    lookingAt = WhatDidISee();

                    if (lookingAt.tag == "Rubbish")
                    {
                        interactText.text = "E to pickup";
                        interactText.gameObject.SetActive(true);
                        if (Input.GetKeyDown(KeyCode.E))
                        {
                            if (InventoryManager.instance.CheckForEmptySlot() >= 0)
                            {
                                int rand = Random.Range(0, audioClips.Length);
                                audioSource.clip = audioClips[rand];
                                audioSource.Play();
                                lookingAt.GetComponent<Item>().Destroyed();
                                Destroy(lookingAt);
                            }
                            else
                            {
                                StartCoroutine(InventoryManager.instance.Flash(InventoryManager.instance.invFullText, 3, .4f));
                            }
                        }
                    }
                    else if (lookingAt.tag == "Bin")
                    {
                        interactText.text = "E to use bin";
                        interactText.gameObject.SetActive(true);
                        if (Input.GetKeyDown(KeyCode.E))
                        {
                            InventoryManager.instance.ShowBinUI();
                        }
                    }
                    else if (lookingAt.tag == "Enemy")
                    {
                        interactText.text = "E to shout";
                        interactText.gameObject.SetActive(true);
                        if (Input.GetKeyDown(KeyCode.E))
                        {
                            stopAudio.Play();
                            lookingAt.GetComponent<EnemyMove>().canLitter = false;  
                        }
                    }
                    else
                    {
                        interactText.gameObject.SetActive(false);
                    }
                }
                else
                {
                    interactText.gameObject.SetActive(false);
                }
                //print(lookingAt);
            }
        }
    }

    public GameObject WhatDidISee()
    {
        Debug.DrawRay(transform.position, transform.forward * interactDistance, Color.red);

        if (Physics.Raycast(transform.position, transform.forward, out whatIHit, interactDistance))
        {
            return whatIHit.collider.gameObject;
        }
        else
        {
            return null;
        }
    }
}
