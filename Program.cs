using System.Security.Cryptography;

char[] colorsStr = { 'r', 'b', 'g', 'y' };

ConsoleColor[] colors = 
{
    ConsoleColor.Red,
    ConsoleColor.Blue,
    ConsoleColor.Green,
    ConsoleColor.Yellow
};

string machineSequence = "", humanSequence = "";
int rounds = 0, limitRounds = 8, speed = 500, frequency = 0;
bool gameOver = false;

int[] colorSequence = new int[limitRounds];

Console.CursorVisible = false;

Console.Clear();

Console.ForegroundColor = colors[0];
Console.Write("---- ");
Console.ForegroundColor = colors[1];
Console.Write("GEN");
Console.ForegroundColor = colors[2];
Console.Write("IUS ");
Console.ForegroundColor = colors[3];
Console.WriteLine("----\n");

Console.ResetColor();

Console.WriteLine("Pressione espaço para jogar...");

while (true)
{
    var key = Console.ReadKey(intercept: true);
    if (key.Key == ConsoleKey.Spacebar)
    {
        break;
    }
}

Console.Clear();

Loop(ref gameOver);

Console.CursorVisible = true;

if (gameOver)
{
    Console.WriteLine("Você perdeu!");
}
else
{
    Console.WriteLine("Você venceu!");
}

void Loop(ref bool gameOver)
{
    rounds++;

    if (rounds > limitRounds)
    {
        return;
    }

    MachineGeneratorNumber(colorsStr, colorSequence);

    ShowColor(colors, ref machineSequence, ref frequency, speed);

    gameOver = Compare(ref humanSequence, machineSequence);

    if (gameOver)
    {
        return;
    }

    Loop(ref gameOver);
}

void MachineGeneratorNumber(char[] colors, int[] sequence)
{
    int number = RandomNumberGenerator.GetInt32(0, colorsStr.Length);

    sequence[rounds - 1] = number;

    MachineResult.CharValue = colors[number];
    MachineResult.ColorSequence = sequence;
}

void ShowColor( ConsoleColor[] colors, ref string machine, ref int freq, int sp)
{
    machine += MachineResult.CharValue;

    if (rounds >= 5)
    {
        sp = 350;
    }

    for (int i = 0; i < machine.Length; i++)
        {
            Console.Clear();

            ConsoleColor c = colors[MachineResult.ColorSequence[i]];
            Console.BackgroundColor = c;

            Console.WriteLine("  ");

            Console.ResetColor();

            switch (c)
            {
                case ConsoleColor.Red:
                    freq = 523;
                    break;
                case ConsoleColor.Blue:
                    freq = 784;
                    break;
                case ConsoleColor.Green:
                    freq = 659;
                    break;
                case ConsoleColor.Yellow:
                    freq = 988;
                    break;
            }

            Console.Beep(freq, sp);

            Thread.Sleep(sp);
        }

    Console.Clear();
    Console.WriteLine();

}

bool Compare(ref string human, string machine)
{
    human = "";

    for (int i = 0; i < machine.Length; i++)
    {
        human += Console.ReadKey(intercept: true).KeyChar.ToString().ToLower();

        if (human[i] != machine[i])
        {
            return true;
        }
    }
    Thread.Sleep(500);

    return false;
}

public static class MachineResult
{
    public static char CharValue { get; set; } 
    public static int[] ColorSequence { get; set; } = null!;
}