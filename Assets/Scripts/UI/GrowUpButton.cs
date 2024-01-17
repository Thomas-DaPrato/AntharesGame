using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GrowUpButton : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    public float scale;


    public void OnSelect(BaseEventData eventData) {
        gameObject.transform.localScale = new Vector3(scale, scale, scale);
    }

    public void OnDeselect(BaseEventData eventData) {
        gameObject.transform.localScale = new Vector3(1, 1, 1);
    }
    
}
