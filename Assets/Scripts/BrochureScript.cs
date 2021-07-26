using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrochureScript : MonoBehaviour
{
    #region Singleton
    public static BrochureScript instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of BrochureScript found");
            return;
        }
        instance = this;
    }
    #endregion

    public GameObject brochureUI;

    public bool brochureUp;

    void Start()
    {
        ShowBrochure();
    }

    void Update()
    {
        InventoryManager.instance.enabled = true;
        brochureUI.SetActive(brochureUp);

        if(brochureUp)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        
        if(Input.GetKeyDown(KeyCode.B) && InventoryManager.instance.invBackground.active == false)
        {
            if (brochureUp)
            {
                HideBrochure();
            }
            else
            {
                ShowBrochure();
            }
        }
    }

    public void HideBrochure()
    {
        brochureUp = false;
        FirstPersonAIO.instance.ControllerPause();
    }

    public void ShowBrochure()
    {
        brochureUp = true;
        FirstPersonAIO.instance.ControllerPause();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
