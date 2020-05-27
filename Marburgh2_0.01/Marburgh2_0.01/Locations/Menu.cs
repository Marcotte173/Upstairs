using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

public class Menu
{
    internal static void Start()
    {
        Console.Clear();
        Write.Line(50, 10, "Gladiator Manager");
        Write.Line(0, Console.WindowHeight - 4, "[1] New Game");
        Write.Line(0, Console.WindowHeight - 3, "[2] Load Game");
        Write.Line(0, Console.WindowHeight - 2, "[?] How to play");
        Write.Line(100, Console.WindowHeight - 2, "Version 0.2");
        string choice = Return.Option();
        Console.Clear();
        if (choice == "1")
        {
            Create.Player();
            Create.Opponents();           
            Hub.Start();           
        }
        if (choice == "2")
        {
            Write.Line("Not implemented");
            Write.KeyPress(0, Console.WindowHeight - 2);
        }
        if (choice == "?")
        {
            Write.Line("Not implemented");
            Write.KeyPress(0, Console.WindowHeight - 2);
        }        
        Start();
    }
}