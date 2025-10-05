using System;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine.Events;

public class SpriteButton: MonoBehaviour, IPointerClickHandler,
    IPointerDownHandler, IPointerEnterHandler,
    IPointerUpHandler, IPointerExitHandler
{

    public UnityEvent OnClickAction;
    void Start()
    {
        //Attach Physics2DRaycaster to the Camera
        Camera.main.gameObject.AddComponent<Physics2DRaycaster>();

        AddEventSystem();
    }

    public virtual void OnPointerClick(PointerEventData eventData)
    {
        OnClickAction?.Invoke();
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        
    }

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        
    }

    public virtual void OnPointerUp(PointerEventData eventData)
    {
        
    }
    public virtual void OnPointerExit(PointerEventData eventData)
    {
        
    }

    //Add Event System to the Camera
    void AddEventSystem()
    {
        GameObject eventSystem = null;
        GameObject tempObj = GameObject.Find("EventSystem");
        if (tempObj == null)
        {
            eventSystem = new GameObject("EventSystem");
            eventSystem.AddComponent<EventSystem>();
            eventSystem.AddComponent<StandaloneInputModule>();
        }
        else
        {
            if ((tempObj.GetComponent<EventSystem>()) == null)
            {
                tempObj.AddComponent<EventSystem>();
            }

            if ((tempObj.GetComponent<StandaloneInputModule>()) == null)
            {
                tempObj.AddComponent<StandaloneInputModule>();
            }
        }
    }

}