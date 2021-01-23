using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;

public class GameAuthentication : MonoBehaviour
{
    public static PlayGamesPlatform platform;
    public Text status;
    // Start is called before the first frame update
    void Start()
    {
        if(platform == null)
        {
            PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
            PlayGamesPlatform.InitializeInstance(config);
            PlayGamesPlatform.DebugLogEnabled = true;
            platform = PlayGamesPlatform.Activate();

        }

        Social.Active.localUser.Authenticate(success =>
        {
            if (success)
            {
                Debug.Log(" logged in successfully ");
                status.text = "logged in successfully";

            }
            else
            {
                Debug.Log(" Failed to login ");
                status.text = "Failed to login ";
            }
        });
    }

  
}
