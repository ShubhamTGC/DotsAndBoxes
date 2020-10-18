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

    public List<GameObject> Player1Token, Player2Token, Player3Token;
    public Sprite Clicked, NotClicked;
    public List<GameObject> playersButtons,PlayerSelectionboard;
    public List<Toggle> TwoPlayertoggle;
    public List<GameObject> ModeHide;
    private int player1index, player2index, player3index;
    private GameObject Player1indexObj, Player2indexObj, Player3indexObj;
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
        if (Gamemode.Equals("Single", System.StringComparison.OrdinalIgnoreCase))
        {
            SinglePlayerSetup();
        }
        else
        {
            TeamPlayerSetup();
        }
    }

    void SinglePlayerSetup()
    {
        
        for (int a=0;a< playersButtons.Count; a++)
        {
            if (a == 0)
            {
                playersButtons[a].GetComponent<Image>().sprite = Clicked;
                PlayerSelectionboard[a].SetActive(true);
            }
            else
            {
                playersButtons[a].GetComponent<Image>().sprite = NotClicked;
                PlayerSelectionboard[a].SetActive(false);
            }
        }

        for(int b =0;b < TwoPlayertoggle.Count; b++)
        {
            if (b == 0)
            {
                TwoPlayertoggle[b].isOn = true;
                ModeHide[b].SetActive(false);
            }
            else
            {
                TwoPlayertoggle[b].isOn = false;
                ModeHide[b].SetActive(true);
            }
        }
    }

    public void PlayerSelectionMode(GameObject Button)
    {
        for(int a=0;a< playersButtons.Count; a++)
        {
            if (playersButtons[a].name.Equals(Button.name, System.StringComparison.OrdinalIgnoreCase))
            {
                playersButtons[a].GetComponent<Image>().sprite = Clicked;
                PlayerSelectionboard[a].SetActive(true);
            }
            else
            {
                playersButtons[a].GetComponent<Image>().sprite = NotClicked;
                PlayerSelectionboard[a].SetActive(false);
            }
        }

        if (Button.name.Equals("3p", System.StringComparison.OrdinalIgnoreCase))
        {
            ThreePlayerSetup();
        }


    }

    public void TwoPlayerTilesSelection(Toggle toggle)
    {
        for (int a = 0; a < TwoPlayertoggle.Count; a++)
        {
            if (TwoPlayertoggle[a].name == toggle.name)
            {
                ModeHide[a].SetActive(!TwoPlayertoggle[a].isOn);
            }
            else
            {
                ModeHide[a].SetActive(true);
            }
        }
    }

    public void ThreePlayerSetup()
    {
        player1index = 0;
        player2index = 1;
        player3index = 2;
        Player1indexObj = Player1Token[0];
        Player2indexObj = Player2Token[1];
        Player3indexObj = Player3Token[2];
        for (int a = 0; a < Player1Token.Count; a++)
        {
            Player1Token[a].GetComponent<RectTransform>().localScale = a == 0 ? new Vector3(1.4f, 1.4f, 1f) : Vector3.one;
            Player1Token[a].transform.GetChild(1).gameObject.SetActive(a == 0 ? true : false);

            Player2Token[a].GetComponent<RectTransform>().localScale = a == 1 ? new Vector3(1.4f, 1.4f, 1f) : Vector3.one;
            Player2Token[a].transform.GetChild(1).gameObject.SetActive(a == 1 ? true : false);

            Player3Token[a].GetComponent<RectTransform>().localScale = a == 2 ? new Vector3(1.4f, 1.4f, 1f) : Vector3.one;
            Player3Token[a].transform.GetChild(1).gameObject.SetActive(a == 2 ? true : false);
        }
    }

    public void ThreePlayerTokenSelection()
    {
        int currentIndex;
        GameObject gb = EventSystem.current.currentSelectedGameObject;
        if (Player1Token.Contains(gb))
        {
            currentIndex = Player1Token.FindIndex(x => x.name == gb.name);
            tokenSetup(Player1Token, player1index, currentIndex);
            player1index = currentIndex;
        }
       if (Player2Token.Contains(gb))
        {
            currentIndex = Player2Token.FindIndex(x => x.name == gb.name);
            tokenSetup(Player2Token, player2index, currentIndex);
            player2index = currentIndex;
        }
       if (Player3Token.Contains(gb))
        {
            currentIndex = Player3Token.FindIndex(x => x.name == gb.name);
            tokenSetup(Player3Token, player3index, currentIndex);
            player3index = currentIndex;
        }

    }

    void tokenSetup(List<GameObject> tokens,int lastindex,int newindex)
    {
        tokens[lastindex].GetComponent<RectTransform>().localScale = Vector3.one;
        tokens[lastindex].transform.GetChild(1).gameObject.SetActive(false);
        tokens[newindex].GetComponent<RectTransform>().localScale = new Vector3(1.4f, 1.4f, 1f);
        tokens[newindex].transform.GetChild(1).gameObject.SetActive(true);
    }

    void TeamPlayerSetup()
    {

    }

    //public void RedSelection()
    //{
    //    GameObject button = EventSystem.current.currentSelectedGameObject;
    //    Red.ForEach(x =>
    //    {
    //        if (x.name == button.name)
    //        {
    //            button.GetComponent<RectTransform>().localScale = new Vector3(1.4f, 1.4f, 1f);
    //            button.transform.GetChild(1).gameObject.SetActive(true);
    //        }
    //        else
    //        {
    //            x.GetComponent<Button>().interactable = false;
    //        }
    //    });
    //}

  
}
