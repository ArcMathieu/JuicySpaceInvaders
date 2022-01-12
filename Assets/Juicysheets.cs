using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Juicysheets : MonoBehaviour
{
    public List<GameObject> buttons = new List<GameObject>();
    public List<string> buttonsName = new List<string>();
    public Dictionary<string, GameObject> BtnDico = new Dictionary<string, GameObject>();
    public Transform parentSheet;
    // Start is called before the first frame update
    void Start()
    {
        if(buttons.Count == buttonsName.Count)
        {
            for (int i = 0; i < buttons.Count; i++)
            {
                Instantiate(buttons[i], parentSheet.position, parentSheet.rotation, parentSheet);
            }
            for (int i = 0; i < buttonsName.Count; i++)
            {
                Debug.Log(buttons[i].GetComponentInChildren<GameObject>().name);
                buttons[i].GetComponentInChildren<TextMeshPro>().text = buttonsName[i];
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
