using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PassnPlayMode : MonoBehaviour
{
    public GameMenu MainMenu;
    public Toggle singlemode, teammode;
    private string Gamemode;

    public List<GameObject> Player1Token, Player2Token, Player3Token;
    public Sprite Clicked, NotClicked;
    public List<GameObject> playersButtons,PlayerSelectionboard;
    public List<Toggle> TwoPlayertoggle;
    public List<InputField> TwoPlayerName,otherplayername;
    public List<GameObject> ModeHide;
    private int player1index, player2index, player3index;
    private GameObject Player1indexObj, Player2indexObj, Player3indexObj;
    public List<GameObject> Gridbuttons;
    public GameObject GameModesToggles, updateMsg, PlayerSetupPage, GamesetuPage;
    private bool TwoPlayer, ThreePlayer, FourPlayer;

    void Start()
    {
        
    }

    private void OnEnable()
    {
        for(int a=0;a< Gridbuttons.Count; a++)
        {
            Gridbuttons[a].GetComponent<Image>().sprite = a == 0 ? Clicked : NotClicked;
        }
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
        GamesetuPage.SetActive(false);
        PlayerSetupPage.SetActive(true);
        MainMenu.Player1 = true;
        MainMenu.Player2 = true;
        MainMenu.Player3 = false;
        MainMenu.Player4 = false;
        string gridsize = "3x4";
        TwoPlayer = true;
        ThreePlayer = false;
        FourPlayer = false;
        string[] gridvalue = gridsize.Split("x"[0]);
        MainMenu.width = int.Parse(gridvalue[0]);
        MainMenu.Height = int.Parse(gridvalue[1]);
        Debug.Log(" width " + MainMenu.width + " height " + MainMenu.Height);
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
                MainMenu.Red = true;
                MainMenu.Blue = true;
                // ModeHide[b].SetActive(false);
                TwoPlayerName.ForEach(x =>
                {
                    x.interactable = false;
                });
                otherplayername.ForEach(x =>
                {
                    x.interactable = true;
                });
              
                MainMenu.Player1name = TwoPlayerName[0].text == "" ? "Player1" : TwoPlayerName[0].text;
                MainMenu.player2name = TwoPlayerName[1].text == "" ? "Player2" : TwoPlayerName[1].text;

            }
            else
            {
                MainMenu.Pink = false;
                MainMenu.Purple = false;
                TwoPlayertoggle[b].isOn = false;
                //ModeHide[b].SetActive(true);
                TwoPlayerName.ForEach(x =>
                {
                    x.interactable = true;

                });
                otherplayername.ForEach(x =>
                {
                    x.interactable = false;
                });
                MainMenu.Player1name = otherplayername[0].text == "" ? "Player1" : TwoPlayerName[0].text;
                MainMenu.player2name = otherplayername[1].text == "" ? "Player2" : TwoPlayerName[1].text;
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
                TwoPlayer = true;
                ThreePlayer = false;
                FourPlayer = false;
                TwoPlayerdataSetup();
            }
            else
            {
                playersButtons[a].GetComponent<Image>().sprite = NotClicked;
                PlayerSelectionboard[a].SetActive(false);
            }
        }
        if (Button.name.Equals("3p", System.StringComparison.OrdinalIgnoreCase))
        {
            TwoPlayer = false;
            ThreePlayer = true;
            FourPlayer = false;
            TherePlayerdataSetup();
            ThreePlayerSetup();
        }
        if (Button.name.Equals("4p", System.StringComparison.OrdinalIgnoreCase))
        {
            TwoPlayer = false;
            ThreePlayer = false;
            FourPlayer = true;
            FourPlayerdataSetup();
            //ThreePlayerSetup();
        }
    }

   
    void TwoPlayerdataSetup()
    {
        MainMenu.Player1 = true;
        MainMenu.Player2 = true;
        MainMenu.Player3 = false;
        MainMenu.Player4 = false;
        MainMenu.Red = true;
        MainMenu.Blue = true;
        MainMenu.Pink = false;
        MainMenu.Purple = false;
        MainMenu.Player1name = "Player1";
        MainMenu.player2name = "player2";
        MainMenu.player3name = "";
        MainMenu.player4name = "";
    }

    void TherePlayerdataSetup()
    {
        MainMenu.Player1 = true;
        MainMenu.Player2 = true;
        MainMenu.Player3 = true;
        MainMenu.Player4 = false;
        MainMenu.Red = true;
        MainMenu.Blue = true;
        MainMenu.Pink = true;
        MainMenu.Purple = false;
        MainMenu.Player1name = "Player1";
        MainMenu.player2name = "Player2";
        MainMenu.player3name = "Player3";
        MainMenu.player4name = "";
    }

    void FourPlayerdataSetup()
    {
        MainMenu.Player1 = true;
        MainMenu.Player2 = true;
        MainMenu.Player3 = true;
        MainMenu.Player4 = true;
        MainMenu.Red = true;
        MainMenu.Blue = true;
        MainMenu.Pink = true;
        MainMenu.Purple = true;
        MainMenu.Player1name = "Player1";
        MainMenu.player2name = "Player2";
        MainMenu.player3name = "Player3";
        MainMenu.player4name = "Player4";
    }

    public void TwoPlayerTilesSelection(Toggle toggle)
    {
        for (int a = 0; a < TwoPlayertoggle.Count; a++)
        {
            if (TwoPlayertoggle[a].name == toggle.name)
            {
                MainMenu.Red = false;
                MainMenu.Blue = false;
                MainMenu.Pink = true;
                MainMenu.Purple = true;
            
                TwoPlayerName.ForEach(x =>
                {
                    x.interactable = false;
                });
                otherplayername.ForEach(x =>
                {
                    x.interactable = true;
                });
           

            }
            else
            {
                MainMenu.Red = true;
                MainMenu.Blue = true;
                MainMenu.Pink = false;
                MainMenu.Purple = false;
               
                TwoPlayerName.ForEach(x =>
                {
                    x.interactable = true;
                });
                otherplayername.ForEach(x =>
                {
                    x.interactable = false;
                });
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
            PlayerTokentaken(currentIndex, player1index, Player2Token, Player3Token);
            player1index = currentIndex;
            
        }
        if (Player2Token.Contains(gb))
        {
            currentIndex = Player2Token.FindIndex(x => x.name == gb.name);
            tokenSetup(Player2Token, player2index, currentIndex);
            PlayerTokentaken(currentIndex, player2index, Player1Token, Player3Token);
            player2index = currentIndex;
        }
        if (Player3Token.Contains(gb))
        {
            currentIndex = Player3Token.FindIndex(x => x.name == gb.name);
            tokenSetup(Player3Token, player3index, currentIndex);
            PlayerTokentaken(currentIndex, player3index, Player1Token, Player2Token);
            player3index = currentIndex;
        }
    }


    void tokenSetup(List<GameObject> tokens,int lastindex,int newindex)
    {
        Debug.Log("Index of click " + lastindex + " ==  " + newindex);
        tokens[lastindex].GetComponent<RectTransform>().localScale = Vector3.one;
        tokens[lastindex].transform.GetChild(1).gameObject.SetActive(false);
        tokens[newindex].GetComponent<RectTransform>().localScale = new Vector3(1.4f, 1.4f, 1f);
        tokens[newindex].transform.GetChild(1).gameObject.SetActive(true);
       
    }

    void PlayerTokentaken(int NewIndex,int prevuiousIndex,List<GameObject> secondList,List<GameObject> ThirdList)
    {
        if(secondList[NewIndex].gameObject.GetComponent<RectTransform>().localScale != Vector3.one)
        {
            tokenSetup(secondList, NewIndex, prevuiousIndex);
        }
        if (ThirdList[NewIndex].gameObject.GetComponent<RectTransform>().localScale != Vector3.one)
        {
            tokenSetup(ThirdList, NewIndex, prevuiousIndex);
        }
     
    }


    void TeamPlayerSetup()
    {
        StartCoroutine(TeamplayerTask());
    }

    IEnumerator TeamplayerTask()
    {
        GameModesToggles.SetActive(false);
        updateMsg.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        updateMsg.SetActive(false);
        GameModesToggles.SetActive(true);

    }

    public void SelectGrid(GameObject grid)
    {
        Gridbuttons.ForEach(x =>
        {
            x.GetComponent<Image>().sprite = x.name == grid.name ? Clicked : NotClicked;
        });
        string gridsize = grid.name;
        string[] gridvalue = gridsize.Split("x"[0]);
        MainMenu.width =int.Parse(gridvalue[0]);
        MainMenu.Height =int.Parse(gridvalue[1]);
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

    public void OnUserInput()
    {
        if (TwoPlayer)
        {
            if (TwoPlayertoggle[0].isOn)
            {
                MainMenu.Player1name = TwoPlayerName[0].text == "" ? "Player 1" : TwoPlayerName[0].text;
                MainMenu.player2name = TwoPlayerName[1].text == "" ? "player 2" : TwoPlayerName[1].text;
            }
            else
            {
                MainMenu.Player1name = otherplayername[0].text == "" ? "Player 1" : otherplayername[0].text;
                MainMenu.player2name = otherplayername[1].text == "" ? "Player 2" : otherplayername[1].text;
            }
            
        }

        
    }

}
