using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//BUG closing inventory while dragging

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField]
    private Canvas canvas;
    private CanvasGroup canvasGroup;

    public Image icon;
    private RectTransform rectTransform;

    public GameObject inSlot;

    public Vector3 initialPosition; //the first position before dragging

    GraphicRaycaster m_Raycaster;
    PointerEventData m_PointerEventData;
    EventSystem m_EventSystem;


    private void Update()
    {
    }

    private void Awake()
    {
        canvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>();
        icon = GetComponent<Image>();

        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();
    }

    private void Start()
    {
        //Fetch the Raycaster from the GameObject (the Canvas)
        m_Raycaster = canvas.GetComponent<GraphicRaycaster>();
        //Fetch the Event System from the Scene
        m_EventSystem = GetComponent<EventSystem>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        InventoryManager.instance.dragging = this.gameObject;
        canvasGroup.alpha = .6f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        print("A");
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
        InventoryManager.instance.dragging = null;

        if (CheckWhatUIMouseIsOver().tag != "Slot")
        {
            transform.position = initialPosition;
            //print("Must place over a slot");
        }
        else
        {
            CheckWhatUIMouseIsOver().GetComponent<InventorySlot>().contents = this.gameObject;
        }

        //if(CheckWhatUIMouseIsOver().tag == "Slot" && CheckWhatUIMouseIsOver().GetComponent<InventorySlot>().contents != null)
        //{
        //    print("S");
        //}
    }

    public void CancelDrag()
    {
        if(m_PointerEventData == null)
        {
            m_PointerEventData.pointerDrag = null; 
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        initialPosition = transform.position;
    }

    public string CheckWhatMouseIsOver()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (hit != false)
        {
            return hit.transform.tag;
        }
        return null;
    }

    public GameObject CheckWhatUIMouseIsOver()
    {
        //Set up the new Pointer Event
        m_PointerEventData = new PointerEventData(m_EventSystem);
        //Set the Pointer Event Position to that of the mouse position
        m_PointerEventData.position = Input.mousePosition;

        //Create a list of Raycast Results
        List<RaycastResult> results = new List<RaycastResult>();

        //Raycast using the Graphics Raycaster and mouse click position
        m_Raycaster.Raycast(m_PointerEventData, results);

        //For every result returned, output the name of the GameObject on the Canvas hit by the Ray
        if (results.Count > 0)
        {

            //print(results[0].gameObject.tag);
        }


        return results[0].gameObject;
    }
}
