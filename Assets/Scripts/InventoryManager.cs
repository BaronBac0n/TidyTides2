using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//TODO no two items in the same slot

public class InventoryManager : MonoBehaviour
{
    public InventorySlot[] slots;

    [HideInInspector]
    public GameObject dragging;

    public GameObject itemToAdd;

    public Image tickImage;
    public Image crossImage;

    public GameObject inventoryParent;
    public GameObject playetInv;
    public GameObject binUI;
    public GameObject invBackground;
    public GameObject interactText;
    public GameObject scoreText;

    public GameObject invFullText;

    FirstPersonAIO player;

    #region Singleton
    public static InventoryManager instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of InventoryManager found");
            return;
        }
        instance = this;
        ScoreTracker.instance.scoreText = scoreText.GetComponent<Text>();
        
    }
    #endregion

    void Start()
    {
        ScoreTracker.instance.scoreText = scoreText.GetComponent<Text>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonAIO>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && BrochureScript.instance.brochureUp == false)
        {
            ShowPlayerInventory();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            //ShowBinUI();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            HideUIs();
        }
    }

    public void ShowPlayerInventory()
    {
        if (inventoryParent.activeInHierarchy == false)
        {
            invBackground.SetActive(true);
            inventoryParent.SetActive(true);
            binUI.SetActive(false);
            interactText.SetActive(false);
            player.ControllerPause();
        }
        else
        {
            invBackground.SetActive(false);
            inventoryParent.SetActive(false);
            binUI.SetActive(false);
            interactText.SetActive(false);
            player.ControllerPause();
        }
    }

    public void ShowBinUI()
    {
        if (binUI.activeInHierarchy == false)
        {
            invBackground.SetActive(true);
            inventoryParent.SetActive(true);
            binUI.SetActive(true);
            interactText.SetActive(false);
            player.ControllerPause();
        }
        else
        {
            invBackground.SetActive(false);
            inventoryParent.SetActive(false);
            binUI.SetActive(false);
            interactText.SetActive(false);
            player.ControllerPause();
        }
    }

    public void HideUIs()
    {
        invBackground.SetActive(false);
        inventoryParent.SetActive(false);
        binUI.SetActive(false);
        interactText.SetActive(false);
        BrochureScript.instance.brochureUp = false;
    }

    public void AddNewItem(GameObject newItem)
    {
        if (CheckForEmptySlot() >= 0)
        {
            //print("Slot " + CheckForEmptySlot() + " is the first empty slot");
            GameObject clone = Instantiate(newItem, playetInv.transform);
            slots[CheckForEmptySlot()].contents = clone;
        }
        else
        {
            StartCoroutine(Flash(invFullText, 3, .4f));
        }
    }

    public int CheckForEmptySlot() //checks for the first empty slot
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].contents == null)
            {
                return i;
            }
        }
        return -1;
    }

    public IEnumerator Flash(GameObject obj, int flashTimes, float flashDuration)
    {
        for (int i = 0; i < flashTimes; i++)
        {
            obj.SetActive(false);
            yield return new WaitForSeconds(flashDuration);
            obj.SetActive(true);
            yield return new WaitForSeconds(flashDuration);
        }
        obj.SetActive(false);
    }
}
