using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    public GameObject contents;

    void Start()
    {
        if (contents != null)
            contents.GetComponent<DragDrop>().inSlot = this.gameObject;
    }

    void Update()
    {
        if (contents != null)
        {
            contents.transform.position = transform.position;

            string contentsType = contents.GetComponent<Item>().type.ToString();

            if (gameObject.tag != "Slot") // if this is not a normal slot
            {
                if (contentsType == gameObject.tag) //if the contents type is the same as the tag
                {
                    StartCoroutine(InventoryManager.instance.Flash(InventoryManager.instance.tickImage.gameObject, 3, .1f));
                    ScoreTracker.instance.score += contents.GetComponent<Item>().value;
                    //Play 'Ding' sound here
                    //print("Yay you put it in the right bin!");
                }
                else //the contents was not in the correct bin
                {
                    StartCoroutine(InventoryManager.instance.Flash(InventoryManager.instance.crossImage.gameObject, 3, .1f));
                    //Play 'err' sound here
                    //print("Wrong bin! That was supposed to go in the " + contentsType + " bin!");                    
                }
                Destroy(contents);
                contents = null;
            }

            if (InventoryManager.instance.dragging == contents)
            {
                contents = null;
            }
        }
    }
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            if (contents == null)
            {
                contents = InventoryManager.instance.dragging;
                contents.GetComponent<DragDrop>().inSlot = this.gameObject;
                eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
            }
            else
            {
                print("Slot already full");
                eventData.pointerDrag.transform.position = eventData.pointerDrag.GetComponent<DragDrop>().initialPosition;
               // contents = eventData.pointerDrag;
            }
        }
    }
}
