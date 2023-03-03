using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dart.Model;

public class GameModel
{
    public List<Player> PlayersList { get; set; }

    public int GameVariant { get; set; }

    public bool FinalWithDouble { get; set; }

    public GameModel(List<Player> playersList, int gameVariant = 501, bool finalWithDouble = true)
    {
        PlayersList = playersList;
        GameVariant = gameVariant;
        FinalWithDouble = finalWithDouble;
    }

    public override string ToString()
    {
        if (FinalWithDouble)
            return string.Format("Gramy do {0}, musisz skończyć doublem", GameVariant);
        else
            return string.Format("Gramy do {0}, nie musisz skończyć doublem", GameVariant);
    }
}

public class PlayersList : ObservableCollection<Player>
{
    public PlayersList(List<Player> playersList) : base()
    {
        foreach(Player player in playersList)
        {
            Add(player);
        }
    }
}

