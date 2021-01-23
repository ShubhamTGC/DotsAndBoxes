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
    public List<InputField> TwoPlayerName,otherplayername,ThreePlayerNameInput;
    public List<GameObject> ModeHide;
    [SerializeField]private int player1index, player2index, player3index;
    private GameObject Player1indexObj, Player2indexObj, Player3indexObj;
    public List<GameObject> Gridbuttons;
    public GameObject GameModesToggles, updateMsg, PlayerSetupPage, GamesetuPage;
    [SerializeField]private bool TwoPlayer, ThreePlayer, FourPlayer;

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
        var setup = new GameInitialSetup();
        CommonPlayerSetup(setup);
        //setup.Players[0].Dia = Dia.Blue;
        //setup.Players[1].Dia = Dia.Red;
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
                TwoPlayerName.ForEach(x =>
                {
                    x.interactable = false;
                });
                otherplayername.ForEach(x =>
                {
                    x.interactable = true;
                });
                setup.Players[0].Dia = Dia.Blue;
                setup.Players[1].Dia = Dia.Red;


            }
            else
            {
                TwoPlayertoggle[b].isOn = false;
                TwoPlayerName.ForEach(x =>
                {
                    x.interactable = true;

                });
                otherplayername.ForEach(x =>
                {
                    x.interactable = false;
                });
                setup.Players[0].Dia = Dia.Pink;
                setup.Players[1].Dia = Dia.Purple;
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
                //SinglePlayerSetup();
                TwoPlayerdataSetup();
                for (int b = 0; b < TwoPlayertoggle.Count; b++)
                {
                    if (b == 0)
                    {
                        TwoPlayertoggle[b].isOn = true;
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
                        TwoPlayertoggle[b].isOn = false;
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
        }
    }

   
    void TwoPlayerdataSetup()
    {
        GameInitialSetup setup = new GameInitialSetup();
        CommonPlayerSetup(setup);
    }

    void TherePlayerdataSetup()
    {
        GameInitialSetup setup = new GameInitialSetup();
        setup.Add(Dia.Pink, "Player 3");
        CommonPlayerSetup(setup);
    }

    void FourPlayerdataSetup()
    {
        GameInitialSetup setup = new GameInitialSetup();
        setup.Add(Dia.Pink, "Player 3");
        setup.Add(Dia.Purple, "Player 4");
        CommonPlayerSetup(setup);
    }

    void CommonPlayerSetup(GameInitialSetup setup)
    {
        MainMenu.Player1 = setup.Players.Count > 0;
        MainMenu.Player2 = setup.Players.Count > 1;
        MainMenu.Player3 = setup.Players.Count > 2;
        MainMenu.Player4 = setup.Players.Count > 3;
        MainMenu.Red = setup.Players.Any(x => x.Dia == Dia.Red);
        MainMenu.Blue = setup.Players.Any(x => x.Dia == Dia.Blue);
        MainMenu.Pink = setup.Players.Any(x => x.Dia == Dia.Pink);
        MainMenu.Purple = setup.Players.Any(x => x.Dia == Dia.Purple);
        MainMenu.player1Die = setup.Players.Any(x => x.Dia == Dia.Red).ToString();
        MainMenu.Player1name = setup.Players.Count > 0 ? setup.Players[0].PlayerName : "";
        MainMenu.player2name = setup.Players.Count > 1 ? setup.Players[1].PlayerName : "";
        MainMenu.player3name = setup.Players.Count > 2 ? setup.Players[2].PlayerName : "";
        MainMenu.player4name = setup.Players.Count > 3 ? setup.Players[3].PlayerName : "";
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
            int tempindex1 = 0;
            int tempindex2 = 0;
            currentIndex = Player1Token.FindIndex(x => x.name == gb.name);  
            tokenSetup(Player1Token, player1index, currentIndex);
            (tempindex1, tempindex2) = PlayerTokentaken(currentIndex, player1index, Player2Token, Player3Token);
            player2index = tempindex1 == 6 ? player2index : tempindex1;
            player3index = tempindex2 == 6 ? player3index : tempindex2;
            player1index = currentIndex;
            
        }
        if (Player2Token.Contains(gb))
        {
            int tempindex1 = 0;
            int tempindex2 = 0;
            currentIndex = Player2Token.FindIndex(x => x.name == gb.name);
            tokenSetup(Player2Token, player2index, currentIndex);
            (tempindex1, tempindex2) = PlayerTokentaken(currentIndex, player2index, Player1Token, Player3Token) ;
            player1index = tempindex1 == 6 ? player1index :tempindex1;
            player3index = tempindex2 == 6 ? player3index : tempindex2;
            player2index = currentIndex;
        }
        if (Player3Token.Contains(gb))
        {
            int tempindex1 = 0;
            int tempindex2 = 0;
            currentIndex = Player3Token.FindIndex(x => x.name == gb.name);
            tokenSetup(Player3Token, player3index, currentIndex);
            (tempindex1, tempindex2) =PlayerTokentaken(currentIndex, player3index, Player1Token, Player2Token);
            player1index = tempindex1 == 6 ? player1index : tempindex1;
            player2index = tempindex2 == 6 ? player2index : tempindex2;
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

    (int,int) PlayerTokentaken(int NewIndex,int prevuiousIndex,List<GameObject> secondList,List<GameObject> ThirdList)
    {
        int lastvalue=6;
        int lastvalue2=6;
        if(secondList[NewIndex].gameObject.GetComponent<RectTransform>().localScale != Vector3.one)
        {
            lastvalue = prevuiousIndex;
            tokenSetup(secondList, NewIndex, prevuiousIndex);
        }
        if (ThirdList[NewIndex].gameObject.GetComponent<RectTransform>().localScale != Vector3.one)
        {
            lastvalue2 = prevuiousIndex;
            tokenSetup(ThirdList, NewIndex, prevuiousIndex);
           
        }
        return (lastvalue,lastvalue2);



    }


    //TEAM PLAYER GAME SETUP COMING SOON
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

 

    public void OnUserInput()
    {
        if (TwoPlayer)
        {
            MainMenu.Player1 = true;
            MainMenu.Player2 = true;
            MainMenu.Player3 = false;
            MainMenu.Player4 = false;
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
        if (ThreePlayer)
        {
            MainMenu.Player1 = true;
            MainMenu.Player2 = true;
            MainMenu.Player3 = true;
            MainMenu.Player4 = false;
            MainMenu.Player1name = ThreePlayerNameInput[0].text == "" ? "Player 1" : ThreePlayerNameInput[0].text;
            MainMenu.player2name = ThreePlayerNameInput[1].text == "" ? "Player 2" : ThreePlayerNameInput[1].text;
            MainMenu.player3name = ThreePlayerNameInput[1].text == "" ? "Player 3" : ThreePlayerNameInput[2].text;
        }
    }
}
