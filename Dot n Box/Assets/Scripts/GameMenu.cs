using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    public static GameMenu MainGame { get; set; }
    [HideInInspector] public int width, Height;
    public bool Player1,Player2,Player3,Player4;
    public bool Red, Blue, Pink, Purple;


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
        SceneManager.LoadScene(1);
    }
    public void ExitApp()
    {
       
        Application.Quit();
    }
}
