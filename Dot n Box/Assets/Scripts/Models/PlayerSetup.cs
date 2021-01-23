using System.Collections.Generic;
using System.Linq;

public class PlayerSetup
{
    public Dia Dia { get; set; }
    public string PlayerName { get; set; }
}

public class GameInitialSetup
{
    
    public List<PlayerSetup> Players { get; set; }
    public int PlayerCount => Players.Count;
    public GameInitialSetup()
    {
        Players = DefaultSetup();
    }


    public List<PlayerSetup> DefaultSetup()
    {
        return new List<PlayerSetup>()
            {
                new PlayerSetup()
                {
                    Dia = Dia.Blue,
                    PlayerName   = "Player 1"
                },
                new PlayerSetup()
                {
                    Dia = Dia.Red,
                    PlayerName  = "Player 2"
                }
            };
    }

    public void Add(Dia dia, string playerName)
    {
        if (!Players.Any(x => x.Dia == dia && x.PlayerName.Equals(playerName, System.StringComparison.OrdinalIgnoreCase)))
        {

            Players.Add(new PlayerSetup()
            {
                Dia = dia,
                PlayerName = playerName
            });
        }
    }

   
}

public enum Dia
{
    Red,
    Blue,
    Purple,
    Pink
}
