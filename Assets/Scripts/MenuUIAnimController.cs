using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUIAnimController : MonoBehaviour
{
    public RectTransform MenuParent;
    public void ShowStartMenu()
    {
        MenuParent.GetComponent<Animator>().Play("UIStartAnim");
    }
    public void ShowMenu()
    {
        MenuParent.GetComponent<Animator>().Play("UIShowAnim");
    }
    public void HideMenu()
    {
        MenuParent.GetComponent<Animator>().Play("UIHideAnim");
    }
    public void AddScoreAnim()
    {
        MenuParent.GetComponent<Animator>().Play("AddScore");
    }
    public void GameOverAnim()
    {
        MenuParent.GetComponent<Animator>().Play("GameOverScore");
    }
}
