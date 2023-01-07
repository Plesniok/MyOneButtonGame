﻿using System;
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
        Player player = new Player();
        Enemy mainEnemy = new Enemy();
        int beginMap = 45;
        int endMap = 49;

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
        private async void GeneratePlayer(List<Dictionary<string, int>> playerPosition, string objectChar)
        {
            for (int i = 0; i <playerPosition.Count; i++)
            {
                Dictionary<string, int> position = playerPosition[i];
                this.view[position["y"]].RemoveAt(position["x"]);

                this.view[position["y"]].Insert(position["x"], objectChar);

            }
        }
        private async void RemovePlayer(List<Dictionary<string, int>>  playerPosition)
        {
            for (int i = 0; i < playerPosition.Count; i++)
            {
                Dictionary<string, int> position = playerPosition[i];
                this.view[position["y"]].RemoveAt(position["x"]);

                this.view[position["y"]].Insert(position["x"], " ");

            }
        }
        
        private async void JumpPlayerHandler(List<Dictionary<string, int>> playerPosition)
        {
            for (int i = 0; i < player.playerPosition.Count; i++)
            {
                //Console.WriteLine(player.playerPosition[i]["y"]);
                player.playerPosition[i]["y"] = player.playerPosition[i]["y"] - 2;

                //Console.WriteLine(player.playerPosition[i]["y"]);
            }

            Thread.Sleep(5000);
            for (int i = 0; i < player.playerPosition.Count; i++)
            {
                //Console.WriteLine(player.playerPosition[i]["y"]);
                player.playerPosition[i]["y"] = player.playerPosition[i]["y"] + 2;

                //Console.WriteLine(player.playerPosition[i]["y"]);
            }
        }

        private async void moveEnemyLeft(List<Dictionary<string, int>> enemyPosition)
        {
            for (int i = 0; i < enemyPosition.Count; i++)
            {
                Console.WriteLine(i);
                RemovePlayer(enemyPosition);
                if(enemyPosition[i]["x"] - 1 < 0)
                {
                    enemyPosition[i]["x"] = 49;
                }
                else
                {
                    enemyPosition[i]["x"] = enemyPosition[i]["x"] - 1;
                }
                GeneratePlayer(enemyPosition, "X");
            }
        }

        public async void RenderDisplay()
        {
            GenerateGround();
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
                GeneratePlayer(this.player.playerPosition, "|");
                GeneratePlayer(this.mainEnemy.enemyPosition, "X");




                long currentTime = stopwatch.ElapsedMilliseconds;
                Console.WriteLine(currentTime);
                Console.WriteLine(jumpBeginTime);
                for (int rowIndex = 0; rowIndex <= this.view.Count - 1; rowIndex++)
                {
                    string rowString = "";
                    //Console.WriteLine(this.view.Count);
                    for (int widthIndex = 0; widthIndex <= this.view[rowIndex].Count - 1; widthIndex++)
                    {
                        
                        //Console.WriteLine(this.view.Count);
                        rowString += this.view[rowIndex][widthIndex];
                        
                    }
                    Console.WriteLine(rowString);

                }
                //ConsoleKeyInfo key = Console.ReadKey();

                // Check if the key pressed was the space key
                // Check if the key pressed was the space key
                if (Console.KeyAvailable)
                {
                    key = Console.ReadKey();
                    if (key.Key == ConsoleKey.Spacebar && jumpFlag == false)
                    {

                        jumpBeginTime = stopwatch.ElapsedMilliseconds;
                        RemovePlayer(player.playerPosition);
                        jumpFlag = true;
                        jumpBeginFlag = true;

                        //GeneratePlayer(this.player.playerPosition);
                        key = new ConsoleKeyInfo('A', ConsoleKey.Escape, false, false, false);
                        //break;
                    }
                }


                if (jumpFlag)
                {
                    //JumpPlayerHandler(player.playerPosition);
                    if(jumpBeginFlag)
                    {
                        for (int i = 0; i < player.playerPosition.Count; i++)
                        {
                            RemovePlayer(player.playerPosition);

                            player.playerPosition[i]["y"] = player.playerPosition[i]["y"] - 6;
                            GeneratePlayer(player.playerPosition, "|");
                        }
                        jumpBeginFlag = false;
                    }

                    if (currentTime - 1000 >= jumpBeginTime)
                    {
                        for (int i = 0; i < player.playerPosition.Count; i++)
                        {
                            RemovePlayer(player.playerPosition);
                            player.playerPosition[i]["y"] = player.playerPosition[i]["y"] + 6;
                            GeneratePlayer(player.playerPosition, "|");
                        }
                        jumpBeginTime = 0;
                        jumpFlag = false;
                    }
                    

                }
                if(stopwatch.ElapsedMilliseconds > 3000 && currentTime - 20 > enemyMoveTime)
                {
                    endMap = endMap - 1;
                    beginMap = beginMap- 1;
                    Console.WriteLine(beginMap);
                    Console.WriteLine("d");

                    for (int i = 0; i < this.mainEnemy.enemyPosition.Count; i++)
                    {
                        RemovePlayer(this.mainEnemy.enemyPosition);
                        if (this.mainEnemy.enemyPosition[i]["x"] - 1 < 0)
                        {
                            this.mainEnemy.enemyPosition[i]["x"] = 49;
                        }
                        else
                        {
                            this.mainEnemy.enemyPosition[i]["x"] = this.mainEnemy.enemyPosition[i]["x"] - 1;
                        }
                        GeneratePlayer(this.mainEnemy.enemyPosition, "X");
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
                            Console.WriteLine("END GAME");
                            endGameFlag = true;
                            break;

                        }
                    }

                }
                
                Thread.Sleep(20);
                RemovePlayer(this.mainEnemy.enemyPosition);

                RemovePlayer(player.playerPosition);
                Console.Clear();
                if (endGameFlag)
                {
                    break;
                }
                //break;
            }
        }
    }
}