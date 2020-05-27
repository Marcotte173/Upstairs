using System;
using System.Threading;

public class Combat
{
    static int momentum;
    public static Gladiator winner;
    public static Gladiator loser;
    public static bool surrender;
    public static void Start(Gladiator a, Gladiator b)
    {
        Console.Clear();
        if (a.Owner.player || b.Owner.player)
        {
            Write.Line(50, 12, "Welcome to the " + Color.BLOOD + "Arena" + Color.RESET + "!");
            Write.Line(30, 14, $"The next match to take place will be between {Color.NAME + a.name + Color.RESET} and {Color.NAME + b.name + Color.RESET}");
            Write.KeyPress(0, 28);
        }        
        while (winner == null)
        {
            Console.Clear();
            Event();
            for (int i = 0; i < 6; i++)
            {
                Console.Clear();
                Attack(true, a, b);
                if (a.Owner.player || b.Owner.player) Return.CombatDisplay(a, b);
                if (a.Disabled(a.head) || a.Disabled(a.torso)) break;
                if (b.Disabled(b.head) || b.Disabled(b.torso)) break;
                if (a.Owner.player || b.Owner.player) Thread.Sleep(1500);                
            }
            if (a.SurrenderCheck())
            {
                winner = b;
                loser = a;
                surrender = true;
            }
            else if (b.SurrenderCheck())
            {
                winner = a;
                loser = b;
                surrender = true;
            }
            else if (a.Disabled(a.head) || a.Disabled(a.torso))
            {
                winner = b;
                loser = a;
            }
            else if (b.Disabled(b.head) || b.Disabled(b.torso))
            {
                loser = b;
                winner = a;
            }
        }
        if (a.Owner.player || b.Owner.player) Write.KeyPress(0, 26);
        Recap(winner, loser);
        winner = null;
        loser = null;
        surrender = false;
    }

    private static void Attack(bool player, Gladiator a, Gladiator b)
    {
        int attackRoll = Return.RandomInt(0, 101);
        //A crits
        if (attackRoll < 6)
        {
            if (momentum < 0) b.TakeDamage(a, momentum, 1);
            else b.TakeDamage(a, Math.Abs(momentum), 1);
            momentum -= 3;
        }
        //B Crits
        else if (attackRoll > 95)
        {
            if (momentum > 0) a.TakeDamage(b, momentum, 1);
            else a.TakeDamage(b, -momentum, 1);
            momentum += 3;
        }
        //Up for grabs!
        else
        {
            //Modify based on offence
            attackRoll -= a.Offence;
            attackRoll += b.Offence;
            //If less than 50, a wins, but b's defence can mitigate
            if (attackRoll < 50) attackRoll = (attackRoll + b.Defence > 50) ? 50 : attackRoll + b.Defence;
            //If not, b wins, but a's defence can mitigate
            else attackRoll = (attackRoll - a.Defence < 50) ? 50 : attackRoll - a.Defence;
            if (attackRoll < 41 && attackRoll >= 25)
            {
                if (momentum < 0) b.TakeDamage(a, momentum, 3);
                else b.TakeDamage(a, Math.Abs(momentum), 3);
                momentum -= 1;
            }
            else if (attackRoll < 25 && attackRoll >= 11)
            {
                if (momentum < 0) b.TakeDamage(a, momentum, 2);
                else b.TakeDamage(a, Math.Abs(momentum), 2);
                momentum -= 2;
            }
            else if (attackRoll < 11)
            {
                if (momentum < 0) b.TakeDamage(a, momentum, 1);
                else b.TakeDamage(a, Math.Abs(momentum), 1);
                momentum -= 3;
            }
            else if (attackRoll > 59 && attackRoll <= 75)
            {
                if (momentum > 0) a.TakeDamage(b, momentum, 3);
                else a.TakeDamage(b, -momentum, 3);
                momentum += 1;
            }
            else if (attackRoll > 75 && attackRoll <= 90)
            {
                if (momentum > 0) a.TakeDamage(b, momentum, 2);
                else a.TakeDamage(b, -momentum, 2);
                momentum += 2;
            }
            else if (attackRoll > 90)
            {
                if (momentum > 0) a.TakeDamage(b, momentum, 1);
                else a.TakeDamage(b, -momentum, 1);
                momentum += 3;
            }
            else
            {
                int r = Return.RandomInt(0, 3);
                string x = (r == 0) ? "The two gladiators circle each other, neither gaining an advantage" : (r == 1) ? $"{a.name} makes a feint, but {b.name} doesn't fall for it" : $"{b.name} makes a feint, but {a.name} doesn't fall for it";
                Write.Line(0, 24, x);
            }
        }
    }

    private static void Event()
    {

    }

    private static void Recap(Gladiator winner, Gladiator loser)
    {
        Console.Clear();
        if (surrender) Write.Line(40, 1, $"{loser.name} has surrendered! {winner.name} has defeated {loser.name}");
        else Write.Line(45, 1, $"{winner.name} has defeated {loser.name}");
        Winner();
        Loser();
        Write.KeyPress(0, 28);
    }

    private static void Winner()
    {
        bool afraid = false;
        bool missingArm = false;
        bool missingLeg = false;
        string head = "";
        string torso = "";
        string leftArm = "";
        string rightArm = "";
        string legs = "";
        if (winner.Disabled(winner.head))
        {
            int chance = Return.RandomInt(0, 3);
            if (chance == 0)
            {
                winner.dead = true;
                head = Color.NAME + winner.name + Color.RESET + " succumbs to his head injuries";
            }
            else
            {
                head = Color.NAME + winner.name + Color.RESET + " will recover from his head wounds but this experience will scar him forever";
                afraid = true;
                winner.head.hp = 2;
            }
        }
        if (winner.Disabled(winner.torso))
        {
            int chance = Return.RandomInt(0, 3);
            if (chance == 0)
            {
                if (winner.dead == false) torso = Color.NAME + winner.name + Color.RESET + " succumbs to his chest injuries";
                winner.dead = true;
            }
            else
            {
                if (winner.dead == false) torso = Color.NAME + winner.name + Color.RESET + " will recover from his chest wounds in time";
                winner.torso.hp = 2;
            }
        }
        if (winner.Disabled(winner.leftArm))
        {
            int chance = Return.RandomInt(0, 3);
            if (chance == 0)
            {
                //One Handed Trait
                leftArm = Color.NAME + winner.name + Color.RESET + " loses his left arm";
                missingArm = true;
            }
            else
            {
                leftArm = Color.NAME + winner.name + Color.RESET + " will recover from his arm wounds in time";
                winner.leftArm.hp = 2;
            }
        }
        if (winner.Disabled(winner.rightArm))
        {
            int chance = Return.RandomInt(0, 3);
            if (chance == 0)
            {
                //One Handed Trait
                rightArm = Color.NAME + winner.name + Color.RESET + " loses his right arm";
                missingArm = true;
            }
            else
            {
                rightArm = Color.NAME + winner.name + Color.RESET + " will recover from his arm wounds in time";
                winner.rightArm.hp = 2;
            }
        }
        if (winner.Disabled(winner.legs))
        {
            int chance = Return.RandomInt(0, 3);
            if (chance == 0)
            {
                //One leg Trait
                legs = Color.NAME + winner.name + Color.RESET + " loses his leg";
                missingLeg = true;
            }
            else
            {
                legs = Color.NAME + winner.name + Color.RESET + " will recover from his leg wounds in time";
                winner.legs.hp = 2;
            }
        }
        Write.Line(20, 4, Color.NAME + winner.name + Color.HEALTH + " recovery" + Color.RESET);
        int number = 0;
        if (head != "")
        {
            number++;
            Write.Line(0, 6, head); 
        }
        if (torso != "")
        {
            number++;
            Write.Line(0, 6 + number, torso);
        }
        if (leftArm != "")
        {
            number++;
            Write.Line(0, 6 + number, leftArm);
        }
        if (rightArm != "")
        {
            number++;
            Write.Line(0, 6 + number, rightArm);
        }
        if (legs != "")
        {
            number++;
            Write.Line(0, 6 + number, legs);
        }
        number++;
        if (afraid)
        {
            number++;
            Write.Line(0, 6 + number, "TRAIT GAINED: AFRAID");
        }
        if (missingArm)
        {
            number++;
            Write.Line(0, 6 + number, "TRAIT GAINED: MISSING ARM");
        }
        if (missingLeg)
        {
            number++;
            Write.Line(0, 6 + number, "TRAIT GAINED: MISSING LEG");
        }
        if (winner.dead) Write.Line(0, 6 + number, Color.NAME + winner.name + Color.STRENGTH + " HAS DIED" + Color.RESET);
        if (head == "" && torso == "" && leftArm == "" && rightArm == "" && legs == "") Write.Line(0, 6 + number, Color.NAME + winner.name + Color.RESET + " is undamaged" );
    }

    private static void Loser()
    {
        bool afraid = false;
        bool missingArm = false;
        bool missingLeg = false;
        string head = "";
        string torso = "";
        string leftArm = "";
        string rightArm = "";
        string legs = "";
        if (loser.Disabled(loser.head))
        {
            int chance = Return.RandomInt(0, 3);
            if (chance == 0)
            {
                loser.dead = true;
                head = Color.NAME + loser.name + Color.RESET + " succumbs to his head injuries";
            }
            else
            {
                head = Color.NAME + loser.name + Color.RESET + " will recover from his head wounds but this experience will scar him forever";
                afraid = true;
                loser.head.hp = 2;
            }
        }
        if (loser.Disabled(loser.torso))
        {
            int chance = Return.RandomInt(0, 3);
            if (chance == 0)
            {
                if (loser.dead == false ) torso = Color.NAME + loser.name + Color.RESET + " succumbs to his chest injuries";
                loser.dead = true;                
            }
            else
            {
                if (loser.dead == false) torso = Color.NAME + loser.name + Color.RESET + " will recover from his chest wounds in time";
                loser.torso.hp = 2;
            }
        }
        if (loser.Disabled(loser.leftArm))
        {
            int chance = Return.RandomInt(0, 3);
            if (chance == 0)
            {
                //One Handed Trait
                leftArm = Color.NAME + loser.name + Color.RESET + " loses his left arm";
                missingArm = true;
            }
            else
            {
                leftArm = Color.NAME + loser.name + Color.RESET + " will recover from his arm wounds in time";
                loser.leftArm.hp = 2;
            }
        }
        if (loser.Disabled(loser.rightArm))
        {
            int chance = Return.RandomInt(0, 3);
            if (chance == 0)
            {
                //One Handed Trait
                rightArm = Color.NAME + loser.name + Color.RESET + " loses his right arm";
                missingArm = true;
            }
            else
            {
                rightArm = Color.NAME + loser.name + Color.RESET + " will recover from his arm wounds in time";
                loser.rightArm.hp = 2;
            }
        }
        if (loser.Disabled(loser.legs))
        {
            int chance = Return.RandomInt(0, 3);
            if (chance == 0)
            {
                //One leg Trait
                legs = Color.NAME + loser.name + Color.RESET + " loses his leg";
                missingLeg = true;
            }
            else
            {
                legs = Color.NAME + loser.name + Color.RESET + " will recover from his leg wounds in time";
                loser.legs.hp = 2;
            }
        }
        Write.Line(20, 15, Color.NAME + loser.name + Color.HEALTH + " recovery" + Color.RESET);
        int number = 0;
        if (head != "")
        {
            number++;
            Write.Line(0, 17, head);
        }
        if (torso != "")
        {
            number++;
            Write.Line(0, 17 + number, torso);
        }
        if (leftArm != "" && loser.dead == false)
        {
            number++;
            Write.Line(0, 17 + number, leftArm);
        }
        if (rightArm != "" && loser.dead == false)
        {
            number++;
            Write.Line(0, 17 + number, rightArm);
        }
        if (legs != "" && loser.dead == false)
        {
            number++;
            Write.Line(0, 17 + number, legs);
        }
        number++;
        if (afraid && loser.dead == false)
        {
            number++;
            Write.Line(0, 17 + number, "TRAIT GAINED: AFRAID");
        }
        if (missingArm && loser.dead == false)
        {
            number++;
            Write.Line(0, 17 + number, "TRAIT GAINED: MISSING ARM");
        }
        if (missingLeg && loser.dead == false)
        {
            number++;
            Write.Line(0, 17 + number, "TRAIT GAINED: MISSING LEG");
        }
        if (loser.dead) Write.Line(0, 17 + number, Color.NAME + loser.name + Color.STRENGTH +" HAS DIED" + Color.RESET);
        if (head == "" && torso == "" && leftArm == "" && rightArm == "" && legs == "") Write.Line(0, 17 + number, Color.NAME + winner.name + Color.RESET + " is undamaged");
    }
}