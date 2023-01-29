using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dart.Model;

public class Player
{
    public string Name { get; set; }

    public List<Round> ResultList { get; set; }

    public Player(string name)
    {
        Name = name;
        ResultList = new List<Round>();
    }

    public override string ToString()
    {
        return Name;
    }
}
