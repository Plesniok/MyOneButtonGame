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
            renderInstance.RenderDisplay();
        }
    }
}