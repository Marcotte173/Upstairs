using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Text;

public class Manage
{
    static Owner player = Create.player;
    public static List<string> managerNames = new List<string> { };
    public static void Gladiators()
    {
        Console.Clear();
        Return.PlayerInfo();
        Return.Roster(player,5);
        Write.Line(0, 21, "[1] " + Color.OFFENCE + "Assign your main gladiator\n" + Color.RESET);
        Write.Line("[2] " + Color.ENDURANCE + "Train a Gladiator" + Color.RESET);
        Write.Line("[3] " + Color.HEALTH + "Rest a Gladiator" + Color.RESET);
        Write.Line("[4] " + Color.ITEM + "Repair Equipment" + Color.RESET);
        Write.Line("[5] " + Color.GOLD + "Hire Managers" + Color.RESET);
        Write.Line("[6] " + Color.DEFENCE + "Release a Gladiator" + Color.RESET);
        Write.Line(0,28,"[0] Return to the Main Hub" + Color.RESET);
        string choice = Return.Option();
        if (choice == "1")
        {
            Console.Clear();
            Return.PlayerInfo();
            Return.Roster(player, 5);
            Write.Line(0, 20, "Who would your like to be your "+Color.OFFENCE+"main "+Color.RESET+"Gladiator?");
            for (int i = 0; i < player.roster.Count; i++)
            {
                Write.Line(0,22 +i,$"[{i + 1}] "+ Color.NAME+ player.roster[i].name +Color.RESET);
            }
            Write.Line(0, 28, "[0] to Return");
            Gladiator g = new Gladiator(1);
            int glad = Return.Int();
            if (glad > 0 && glad <= player.roster.Count)
            {
                g = player.roster[glad - 1];
                Gladiator temp = new Gladiator(1);
                temp = player.roster[0];
                player.roster[0] = g;
                player.roster[glad - 1] = temp;
            }
            else if (glad == 0) Gladiators();
        }
        else if (choice == "2")
        {
            Console.Clear();
            Return.PlayerInfo();
            Return.Roster(player, 5);
            Write.Line(0, 20, "Who would your like to " + Color.ENDURANCE + "train" + Color.RESET + "?");
            for (int i = 0; i < player.roster.Count; i++)
            {
                Write.Line(0, 22 + i, $"[{i + 1}] " + Color.NAME + player.roster[i].name + Color.RESET);
            }
            Write.Line(0, 28, "[0] to Return");
            Gladiator g = new Gladiator(1);
            int glad = Return.Int();
            if (glad > 0 && glad <= player.roster.Count)
            {
                g = player.roster[glad - 1];
                Train(g);
            }
        }
        else if (choice == "3")
        {
            Console.Clear();
            Return.PlayerInfo();
            Return.Roster(player, 5);
            Injuries();
            Write.Line(0, 20, "Who would your like to " + Color.HEALTH + "rest" + Color.RESET + "?");
            for (int i = 0; i < player.roster.Count; i++)
            {
                Write.Line(0, 22 + i, $"[{i + 1}] " + Color.NAME + player.roster[i].name + Color.RESET);
            }
            Write.Line(0, 28, "[0] to Return");
            Gladiator g = new Gladiator(1);
            int glad = Return.Int();
            if (glad > 0 && glad <= player.roster.Count)
            {
                g = player.roster[glad - 1];
                Rest(g);
            }
        }
        else if (choice == "4")
        {
            Console.Clear();
            Return.PlayerInfo();
            Return.Roster(player, 5);
            Write.Line(0, 20, "Who's " + Color.ITEM + "equipment " + Color.RESET + "would you like to repair?");
            for (int i = 0; i < player.roster.Count; i++)
            {
                Write.Line(0, 22 + i, $"[{i + 1}] " + Color.NAME + player.roster[i].name + Color.RESET);
            }
            Write.Line(0, 28, "[0] to Return");
            Gladiator g = new Gladiator(1);
            int glad = Return.Int();
            if (glad > 0 && glad <= player.roster.Count)
            {
                g = player.roster[glad - 1];
                Repair(g);
            }
        }
        else if (choice == "5")
        {
            int e = 50;
            int p = 100;
            Console.Clear();
            Return.PlayerInfo();
            Write.Line(5, 2, "Name");
            Write.Line(e, 2, "Effect");
            Write.Line(p, 2, "Price");
            if (player.equipmentManager == false)
            {
                Write.Line(0, 4, "[1] " + Color.NAME + managerNames[0] + Color.RESET + " the" + Color.ITEM + " equipment manager" + Color.RESET);
                Write.Line(e, 4, Color.ITEM + "Repairs/Upgrades Armor" + Color.RESET);
                Write.Line(p, 18, Color.GOLD + "" + Color.RESET);
            }
            else Write.Line(0, 4, "[X] " + Color.MITIGATION + "This manager is not available" + Color.RESET);
            if (player.healthManager == false)  
            {
                Write.Line(0, 6, "[2] " + Color.NAME + managerNames[1] + Color.RESET + " the" + Color.HEALTH + " health manager" + Color.RESET);
                Write.Line(e, 6, Color.HEALTH + "Resting a gladiator heals all severe injuries" + Color.RESET);
                Write.Line(p, 18, Color.GOLD + "" + Color.RESET);
            }
            else Write.Line(0, 6, "[X] " + Color.MITIGATION + "This manager is not available" + Color.RESET);
            if (player.strengthManager == false)
            {
                Write.Line(0, 8, "[3] " + Color.NAME + managerNames[2] + Color.RESET + " the" + Color.STRENGTH + " fitness instructor" + Color.RESET);
                Write.Line(e, 8, Color.STRENGTH + "Increased chance of strength gain" + Color.RESET);
                Write.Line(p, 18, Color.GOLD + "" + Color.RESET);
            }
            else Write.Line(0, 8, "[X] " + Color.MITIGATION + "This manager is not available" + Color.RESET);
            if (player.offenceManager == false)
            {
                Write.Line(0, 10, "[4] " + Color.NAME + managerNames[3] + Color.RESET + " the" + Color.OFFENCE + " offence coordinator" + Color.RESET);
                Write.Line(e, 10, Color.OFFENCE + "Increased chance of offence gain" + Color.RESET);
                Write.Line(p, 18, Color.GOLD + "" + Color.RESET);
            }
            else Write.Line(0, 10, "[X] " + Color.MITIGATION + "This manager is not available" + Color.RESET);
            if (player.defenceManager == false)
            {
                Write.Line(0, 12, "[5] " + Color.NAME + managerNames[4] + Color.RESET + " the" + Color.DEFENCE + " defence coordinator" + Color.RESET);
                Write.Line(e, 12, Color.DEFENCE + "Increased chance of defence gain" + Color.RESET);
                Write.Line(p, 18, Color.GOLD + "" + Color.RESET);
            }
            else Write.Line(0, 12, "[X] " + Color.MITIGATION + "This manager is not available" + Color.RESET);
            if (player.enduranceManager == false)
            {
                Write.Line(0, 14, "[6] " + Color.NAME + managerNames[5] + Color.RESET + " the" + Color.ENDURANCE + " conditioning coach" + Color.RESET);
                Write.Line(e, 14, Color.ENDURANCE + "Increased chance of endurance gain" + Color.RESET);
                Write.Line(p, 18, Color.GOLD + "" + Color.RESET);
            }
            else Write.Line(0, 14, "[X] " + Color.MITIGATION + "This manager is not available" + Color.RESET);
            if (player.prestigeManager == false)
            {
                Write.Line(0, 16, "[7] " + Color.NAME + managerNames[6] + Color.RESET + " the" + Color.XP + " publicity manager" + Color.RESET);
                Write.Line(e, 16, Color.XP + "Increases prestige gain after arena win" + Color.RESET);
                Write.Line(p, 18, Color.GOLD + "" + Color.RESET);
            }
            else Write.Line(0, 16, "[X] " + Color.MITIGATION + "This manager is not available" + Color.RESET);
            if (player.moneyManager == false)
            {
                Write.Line(0, 18, "[8] " + Color.NAME + managerNames[7] + Color.RESET + " the" + Color.GOLD + " fiscal planner" + Color.RESET);
                Write.Line(e, 18, Color.GOLD + "Increases gold won in arena, allows gambling" + Color.RESET);
                Write.Line(p, 18, Color.GOLD + "" + Color.RESET);
            }
            else Write.Line(0, 18, "[X] " + Color.MITIGATION + "This manager is not available" + Color.RESET);
            Write.KeyPress(0, 28);
        }
        else if (choice == "6")
        {
            Console.Clear();
            Return.PlayerInfo();
            Return.Roster(player,5);
            Write.Line(0, 20, "Who would your like to " + Color.DEFENCE + "release" + Color.RESET+"?");
            for (int i = 0; i < player.roster.Count; i++)
            {
                Write.Line(0, 22 + i, $"[{i + 1}] " + Color.NAME + player.roster[i].name + Color.RESET);
            }
            Write.Line(0, 28, "[0] to Return");
            Gladiator g = new Gladiator(1);
            int glad = Return.Int();
            if (glad > 0 && glad <= player.roster.Count)
            {
                g = player.roster[glad - 1];
                player.roster.Remove(g);
                Console.Clear();
                Return.Roster(player, 5);
                Write.Line(0,20,g.name + " has been " + Color.DEFENCE + "released " + Color.RESET +"from your team.");
                Write.KeyPress(0,28);
            }
            else if (glad == 0) Gladiators();
        }
        else if (choice == "0") Hub.Menu();
        Gladiators();
    }

    private static void Injuries()
    {
        int x = 5;
        int n = 16;
        for (int i = 0; i < player.roster.Count; i++)
        {
            if (player.roster[i].SeverelyInjured(player.roster[i].head) || player.roster[i].Injured(player.roster[i].head))
            {
                Write.Line(x, n, $"Head      {player.roster[i].head.StatusString()}");
                n++;
            }
            if (player.roster[i].SeverelyInjured(player.roster[i].torso) || player.roster[i].Injured(player.roster[i].torso))
            {
                Write.Line(x, n, $"Torso     {player.roster[i].torso.StatusString()}");
                n++;
            }
            if (player.roster[i].SeverelyInjured(player.roster[i].leftArm) || player.roster[i].Injured(player.roster[i].leftArm))
            {
                Write.Line(x, n, $"Left Arm  {player.roster[i].leftArm.StatusString()}");
                n++;
            }
            if (player.roster[i].SeverelyInjured(player.roster[i].rightArm) || player.roster[i].Injured(player.roster[i].rightArm))
            {
                Write.Line(x, n, $"Right Arm  {player.roster[i].rightArm.StatusString()}");
                n++;
            }
            if (player.roster[i].SeverelyInjured(player.roster[i].legs) || player.roster[i].Injured(player.roster[i].legs))
            {
                Write.Line(x, n, $"Legs       {player.roster[i].legs.StatusString()}");
                n++;
            }
            x += 25;
            n = 16;
        }
    }

    private static void Repair(Gladiator g)
    {
        g.action = Actions.Repairing;
        Console.Clear();
        Return.Roster(player, 5);
        Write.Line(0, 20, Color.NAME + g.name + Color.RESET + " is " + Color.ITEM + "repairing" + Color.RESET);
        Write.KeyPress(0, 28);
    }

    private static void Rest(Gladiator g)
    {
        g.action = Actions.Resting;
        Console.Clear();
        Return.Roster(player, 5);
        Write.Line(0, 20, Color.NAME + g.name + Color.RESET + " is " + Color.HEALTH + "resting" + Color.RESET);
        Write.KeyPress(0,28);        
    }

    private static void Train(Gladiator g)
    {
        Console.Clear();
        Return.GladiatorInfo(g,5, 6);
        Write.Line(0,18,"What would you like to train?");
        Write.Line(0, 23, "[1] "+Color.STRENGTH + "Strength"+ Color.RESET);
        Write.Line(0, 24, "[2] "+Color.OFFENCE + "Offence" + Color.RESET);
        Write.Line(0, 25, "[3] "+Color.DEFENCE + "Defence" + Color.RESET);
        Write.Line(0, 26, "[4] "+Color.ENDURANCE + "Endurance" + Color.RESET);
        Write.Line(0, 28, "[0] to Return");
        string choice = Return.Option();
        if (choice == "0") Gladiators();
        else if (choice == "1")
        {
            g.action = Actions.Strength;
            Console.Clear();
            Return.GladiatorInfo(g, 5, 6);
            Write.Line(0, 20, Color.NAME + g.name + Color.RESET + " is training " + Color.STRENGTH + "strength" + Color.RESET);
            Write.KeyPress(0, 28);
        }
        else if (choice == "2")
        {
            g.action = Actions.Offence;
            Console.Clear();
            Return.GladiatorInfo(g, 5, 6);
            Write.Line(0, 20, Color.NAME + g.name + Color.RESET + " is training " + Color.OFFENCE + "offence" + Color.RESET);
            Write.KeyPress(0, 28);
        }
        else if (choice == "3")
        {
            g.action = Actions.Defence;
            Console.Clear();
            Return.GladiatorInfo(g, 5, 6);
            Write.Line(0, 20, Color.NAME + g.name + Color.RESET + " is training " + Color.DEFENCE + "defence" + Color.RESET);
            Write.KeyPress(0, 28);
        }
        else if (choice == "4")
        {
            g.action = Actions.Endurance;
            Console.Clear();
            Return.GladiatorInfo(g, 5, 6);
            Write.Line(0, 20, Color.NAME + g.name + Color.RESET + " is training " + Color.ENDURANCE + "endurance" + Color.RESET);
            Write.KeyPress(0, 28);
        }
        else Train(g);
    }
}