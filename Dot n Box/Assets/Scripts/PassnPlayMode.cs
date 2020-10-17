using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PassnPlayMode : MonoBehaviour
{
    public Toggle singlemode, teammode;
    private string Gamemode;

    public List<GameObject> Red, Blue, pink, purple;
    void Start()
    {
        
    }

    private void OnEnable()
    {
        singlemode.isOn = true;
        teammode.isOn = false;
    }

    
    void Update()
    {
        
    }

    public void GameModeSelection()
    {
        Gamemode = singlemode.isOn == true ? "Single" : "Team";
    }

    public void RedSelection()
    {
        GameObject button = EventSystem.current.currentSelectedGameObject;
        Red.ForEach(x =>
        {
            if (x.name == button.name)
            {
                button.GetComponent<RectTransform>().localScale = new Vector3(1.4f, 1.4f, 1f);
                button.transform.GetChild(1).gameObject.SetActive(true);
            }
            else
            {
                x.GetComponent<Button>().interactable = false;
            }
        });
    }
}
