using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Juicysheets : MonoBehaviour
{
    //public int nbButton;
    //public GameObject boxPrefab;
    //public List<GameObject> buttons = new List<GameObject>();
    //public List<string> buttonsName = new List<string>();
    //public Transform parentSheet;
    // Start is called before the first frame update
    void Start()
    {
        //if(nbButton == buttonsName.Count)
        //{
        //    for (int i = 0; i < nbButton; i++)
        //    {
        //        buttons.Add(Instantiate(boxPrefab, parentSheet.position, parentSheet.rotation, parentSheet));
        //        buttons[i].transform.GetChild(0).GetComponent<TMP_Text>().text = buttonsName[i];
        //    }
        //}
        
    }

    public void SwapEffect(string name)
    {
        switch (name)
        {
            case "particules":
                //fonction desactive particules*

                break;
            case "ui":
                //fonction desactive ui
                break;
            case "lights":
                //fonction desactive light
                break;
            case "anims":
                //fonction desactive animations
                break;
            case "cam":
                //fonction desactive cam movement
                break;
            case "all":
                //fonction desactive tout
                break;
            default:
                break;
        }
    }
}
