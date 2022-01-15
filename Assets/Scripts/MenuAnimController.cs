using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuAnimController : MonoBehaviour
{
    public RectTransform MenuParent;
    public void ShowMenu() 
    {
        MenuParent.GetComponent<Animator>().Play("MenuAnim");
    }
    public void HideMenu() 
    {
        MenuParent.GetComponent<Animator>().Play("MenuAnimBack");
    }

    public void ShowOption() 
    {
        MenuParent.GetComponent<Animator>().Play("MenuOptionAnim");
    }
    public void HideOption() 
    {
        MenuParent.GetComponent<Animator>().Play("MenuOptionAnimBack");
    }
}
