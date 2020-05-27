using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

public class Hub
{
    static Owner player = Create.player;
    static List<Owner> owners = Create.owners;
    public static int day = 1;
    static bool audience;
    static bool fightsToday = true;   

    public static void Start()
    {
        Slaver.NewStock();
        for (int i = 0; i < 8; i++) Manage.managerNames.Add(Return.Name());
        Menu();
    }

    public static void Menu()
    {
        Console.Clear();
        Return.PlayerInfo();
        Return.Managers();
        Return.Roster(player,5);
        while (day < 11)
        {
            Write.Line(0, 18, "[1] " + Color.GOLD + "Hire Gladiators\n" + Color.RESET);
            Write.Line("[2] " + Color.ENERGY + "Manage Gladiators" + Color.RESET);
            Write.Line("[3] " + Color.ITEM + "Purchase Equipment" + Color.RESET);
            Write.Line("[4] " + Color.ENERGY + "Jobs" + Color.RESET);
            Write.Line("[5] " + Color.ENERGY + "Shady Jobs" + Color.RESET);
            if (Graveyard.graveyard.Count > 0) Write.Line("[6] " + Color.DURABILITY + "Graveyard" + Color.RESET);
            else Write.Line("[X] " + Color.MITIGATION + "The graveyard is empty" + Color.RESET);
            Write.Line("[7] " + Color.XP + "Owner Rankings" + Color.RESET);
            if (audience) Write.Line("[8] " + Color.XP + "Audience with the emperor" + Color.RESET);
            else Write.Line("[X] " + Color.MITIGATION + "The emperor has no interest in you" + Color.RESET);
            if (fightsToday) Write.Line("[A] " + Color.STRENGTH + "The Arena!" + Color.RESET);
            else Write.Line("[X] " + Color.MITIGATION + "There are no other fights today" + Color.RESET);
            Write.Line("[0] " + Color.TIME + "Next day" + Color.RESET);
            Write.Line("[Q] Quit");
            string choice = Return.Option();
            if (choice == "1") Slaver.Hire();
            else if (choice == "2") Manage.Gladiators();
            else if (choice == "3") Purchase.Equipment();
            else if (choice == "6" && Graveyard.graveyard.Count > 0) Graveyard.Visit();
            else if (choice == "7") 
            else if (choice == "a")
            {
                fightsToday = false;
                //Figure out how fights will happen
            }
            else if (choice == "0")
            {
                day++;
                player.actions = 3;
                if (day % 3 == 0) Slaver.NewStock();
                fightsToday = true;
            }
            Menu();
        }
        // Torunament
    }
}