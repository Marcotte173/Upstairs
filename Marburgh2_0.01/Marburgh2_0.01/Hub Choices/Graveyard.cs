using System;
using System.Collections.Generic;
using System.Text;

public class Graveyard
{
    public static List<Gladiator> graveyard = new List<Gladiator> { };
    public static List<Gladiator> killedBy = new List<Gladiator> { };
    public static List<int> dayOfDeath = new List<int> { };
    public static void Visit()
    {
        Console.Clear();
        Return.PlayerInfo();
        Write.Line(0, 2, Color.NAME + "Name" + Color.RESET );
        Write.Line(20, 2, Color.DAMAGE + "Killed By");
        Write.Line(35, 2, Color.TIME + "Day");
        Write.Line(50, 2, Color.HEALTH + "Wins");
        for (int i = 0; i < graveyard.Count; i++)
        {
            Write.Line(1, i + 4, (i + 1).ToString());
            Write.Line(5, i + 4, Color.NAME + graveyard[i].name + Color.RESET);
            Write.Line(23, i + 4, killedBy[i].name);
            Write.Line(38, i + 4, dayOfDeath[i].ToString());
            Write.Line(53, i + 4, graveyard[i].wins.ToString());
        }
    }
}