using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public int value;
    public enum Type { General, Recycling, Compost };
    public Type type;
    public GameObject uiElement;

    public void Destroyed()
    {
        if(uiElement != null)
        InventoryManager.instance.AddNewItem(uiElement);
    }
}
