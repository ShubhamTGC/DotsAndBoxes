using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
    public static GameMenu MainGame { get; set; }
    [HideInInspector] public int width, Height;
    [HideInInspector] public bool Player1,Player2,Player3,Player4;
     public bool Red, Blue, Pink, Purple;
     public string Player1name, player2name, player3name, player4name;
    [HideInInspector] public string player1Die, Player2Die, Player3Die, Player4Die;
    public PassnPlayMode PlayerSetupdata;
    public Button PlayerBtn;

    void Start()
    {
        if(MainGame != null)
        {
            width = 0;
            Height = 0;
            Debug.Log("destory");
            Destroy(MainGame);
        }
        else
        {
            Debug.Log("created");
            MainGame = this;
        }
        DontDestroyOnLoad(MainGame);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectGridValue(string value)
    {
        string[] boardvalue = value.Split("x"[0]);
        width = int.Parse(boardvalue[0]);
        Height = int.Parse(boardvalue[1]);
        Debug.Log("values are " + width + " " + Height);
    }

    public void GamePlay()
    {
        PlayerBtn.interactable = false;
        StartCoroutine(PlayGame());
     
    }
    IEnumerator PlayGame()
    {
        PlayerSetupdata.OnUserInput();
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(1);
    }
    public void ExitApp()
    {
       
        Application.Quit();
    }
}
