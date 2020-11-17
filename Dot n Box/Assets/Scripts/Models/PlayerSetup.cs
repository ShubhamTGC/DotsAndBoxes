using System.Collections.Generic;
public class PlayerSetup
{
    public Dia Dia { get; set; }
    public string PlayerName { get; set; }
}

public class GameInitialSetup
{
    public List<PlayerSetup> Players { get; set; }
    public int PlayerCount => Players.Count;
}

public class Sample
{
    public static GameInitialSetup GetSetupCriteria()
    {
        var gameSetup = new GameInitialSetup();
        var setup = new List<PlayerSetup>() { new PlayerSetup()
        {
            Dia = Dia.Blue,
            PlayerName  = "Shubham"
        },
        new PlayerSetup()
        {
            Dia = Dia.Red,
            PlayerName  = "Pradeep"
        }

        };
        gameSetup.Players = setup;
        return gameSetup;
    }
}

public enum Dia
{
    Red,
    Blue,
    Purple,
    Pink
}
