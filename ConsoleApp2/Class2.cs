using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;

namespace OneButtonGame
{
    class Render
    {
        private List<List<string>> view = new List<List<string>>();
        private Player player = new Player();
        private Enemy mainEnemy = new Enemy();
        private int beginMap = 45;
        private int endMap = 49; 
        private int drawViewDelay = 20;
    public Render(Player newPlayer)
        {
            this.player = newPlayer;
        }

        private void GenerateGround()
        {
            for(int height = 0; height < 20; height++)
            {
                List<string> bufforRow = new List<string>();
                if (height >= 15)
                {
                    for (int width = 0; width < 50; width++)
                    {
                        bufforRow.Add("_");
                    }
                }
                else
                {
                    for (int width = 0; width < 50; width++)
                    {
                        bufforRow.Add(" ");
                    }
                }
                this.view.Add(bufforRow);
            }
        }
        private async void DrawObject(List<Dictionary<string, int>> playerPosition, string objectChar)
        {
            for (int i = 0; i <playerPosition.Count; i++)
            {
                Dictionary<string, int> position = playerPosition[i];
                this.view[position["y"]].RemoveAt(position["x"]);

                this.view[position["y"]].Insert(position["x"], objectChar);

            }
        }
        private async void RemoveObject(List<Dictionary<string, int>>  playerPosition)
        {
            for (int i = 0; i < playerPosition.Count; i++)
            {
                Dictionary<string, int> position = playerPosition[i];
                this.view[position["y"]].RemoveAt(position["x"]);

                this.view[position["y"]].Insert(position["x"], " ");

            }
        }
        


        public async void DrawView()
        {
            GenerateGround();
            Console.WriteLine("Press spacebar to jump!");
            ConsoleKeyInfo key = Console.ReadKey();
            bool jumpFlag = false;
            bool jumpBeginFlag = false;
            long jumpBeginTime = 0;

            this.mainEnemy.InitEnemy(47, 49);
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            long enemyMoveTime = stopwatch.ElapsedMilliseconds;
            bool endGameFlag = false;
            while (true)
            {
                DrawObject(this.player.playerPosition, "|");
                DrawObject(this.mainEnemy.enemyPosition, "X");




                long currentTime = stopwatch.ElapsedMilliseconds;
                Console.WriteLine(currentTime);
                Console.WriteLine(jumpBeginTime);
                for (int rowIndex = 0; rowIndex <= this.view.Count - 1; rowIndex++)
                {
                    string rowString = "";

                    for (int widthIndex = 0; widthIndex <= this.view[rowIndex].Count - 1; widthIndex++)
                    {
                        

                        rowString += this.view[rowIndex][widthIndex];
                        
                    }
                    Console.WriteLine(rowString);

                }

                if (Console.KeyAvailable)
                {
                    key = Console.ReadKey();
                    if (key.Key == ConsoleKey.Spacebar && jumpFlag == false)
                    {

                        jumpBeginTime = stopwatch.ElapsedMilliseconds;
                        RemoveObject(player.playerPosition);
                        jumpFlag = true;
                        jumpBeginFlag = true;

                        key = new ConsoleKeyInfo('A', ConsoleKey.Escape, false, false, false);
                    }
                }


                if (jumpFlag)
                {
                    if(jumpBeginFlag)
                    {
                        for (int i = 0; i < player.playerPosition.Count; i++)
                        {
                            RemoveObject(player.playerPosition);

                            player.playerPosition[i]["y"] = player.playerPosition[i]["y"] - 6;
                            DrawObject(player.playerPosition, "|");
                        }
                        jumpBeginFlag = false;
                    }

                    if (currentTime - 1000 >= jumpBeginTime)
                    {
                        for (int i = 0; i < player.playerPosition.Count; i++)
                        {
                            RemoveObject(player.playerPosition);
                            player.playerPosition[i]["y"] = player.playerPosition[i]["y"] + 6;
                            DrawObject(player.playerPosition, "|");
                        }
                        jumpBeginTime = 0;
                        jumpFlag = false;
                    }
                    

                }
                if(stopwatch.ElapsedMilliseconds > 3000 && currentTime - 20 > enemyMoveTime)
                {
                    endMap = endMap - 1;
                    beginMap = beginMap- 1;

                    for (int i = 0; i < this.mainEnemy.enemyPosition.Count; i++)
                    {
                        RemoveObject(this.mainEnemy.enemyPosition);
                        if (this.mainEnemy.enemyPosition[i]["x"] - 1 < 0)
                        {
                            this.mainEnemy.enemyPosition[i]["x"] = 49;
                        }
                        else
                        {
                            this.mainEnemy.enemyPosition[i]["x"] = this.mainEnemy.enemyPosition[i]["x"] - 1;
                        }
                        DrawObject(this.mainEnemy.enemyPosition, "X");
                    }
                    enemyMoveTime = stopwatch.ElapsedMilliseconds;
                }
                //check if enemy is on player position
                for (int i = 0; i < this.player.playerPosition.Count; i++)
                {

                    for (int j = 0; j < this.mainEnemy.enemyPosition.Count; j++)
                    {
                        if(this.player.playerPosition[i]["x"] == this.mainEnemy.enemyPosition[j]["x"] && this.player.playerPosition[i]["y"] == this.mainEnemy.enemyPosition[j]["y"])
                        {
                            
                            endGameFlag = true;
                            break;

                        }
                    }

                }
                player.playerPoints += 1;
                Thread.Sleep(this.drawViewDelay);
                RemoveObject(this.mainEnemy.enemyPosition);
                
                RemoveObject(player.playerPosition);
                
                if (endGameFlag)
                {
                    break;
                }
                Console.Clear();
                Console.WriteLine("YOUR POINTS: " + player.playerPoints.ToString());
                
            }
        }
    }
}
