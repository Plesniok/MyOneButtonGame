using System;

namespace OneButtonGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Player dinosaur = new Player();
            Render renderInstance = new Render(dinosaur);

            dinosaur.AskPlayerForName();
            renderInstance.DrawView();
            Console.WriteLine(dinosaur.playerName);
            Console.WriteLine("YOUR POINTS: " + dinosaur.playerPoints.ToString());
            
        }
    }
}