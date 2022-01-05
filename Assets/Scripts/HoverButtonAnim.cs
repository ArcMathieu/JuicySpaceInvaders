using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverButtonAnim : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public RectTransform Button;
    public bool canTurn;
    // Start is called before the first frame update
    void Start()
    {
        Button.GetComponent<Animator>().Play("HoverOff");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Button.GetComponent<Animator>().Play("HoverOn");
        if(canTurn)
            Button.GetComponent<Animator>().Play("Turn");
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        Button.GetComponent<Animator>().Play("HoverOff");
    }
}
