using System;
using System.Collections.Generic;
using System.Threading;
using System.Text;
using System.Diagnostics;

namespace OneButtonGame
{
    class Player
    {
        public String playerName;
        public List<Dictionary<string, int>> playerPosition = new List<Dictionary<string, int>> ();
        public long playerPoints = 0;
        public string playerChar = "|"; 
        public Player()
        {
            //    this.playerName = playerName;
            
        }
        private void addPosition(int valuex, int valuey)
        {
            Dictionary<string, int> bufforDictionary = new Dictionary<string, int>();
            bufforDictionary.Add("x", valuex);
            bufforDictionary.Add("y", valuey);
            this.playerPosition.Add(bufforDictionary);
        }
        public async void Jump(long gotBeginTime, bool jumpFlag)
        {
            bool beginJump = true;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            Console.WriteLine("JUMP");
            Console.WriteLine(jumpFlag);
            if (jumpFlag)
            {
                Console.WriteLine("BEGINJUMP");
                if (beginJump)
                {
                    for (int i = 0; i < this.playerPosition.Count; i++)
                    {
                        this.playerPosition[i]["y"] = this.playerPosition[i]["y"] + 2;
                    }
                    beginJump = false;
                    
                }

                if (gotBeginTime + 1000 <= stopwatch.ElapsedMilliseconds)
                {
                    for (int i = 0; i < this.playerPosition.Count; i++)
                    {
                        this.playerPosition[i]["y"] = this.playerPosition[i]["y"] - 2;
                    }   
                }  
            }
        }
        private bool InitPlayer(string newPlayerName)
        {
            this.playerName = newPlayerName;
            Console.WriteLine("INIT PLAYER");
            addPosition(3, 11);
            addPosition(3, 12);
            addPosition(3, 13);
            addPosition(3, 14);
            addPosition(4, 11);
            addPosition(4, 12);
            addPosition(4, 13);
            addPosition(4, 14);
            addPosition(5, 11);
            addPosition(5, 12);
            addPosition(5, 13);
            addPosition(5, 14);
            return true;
        }
        public bool AskPlayerForName()
        {
            while (true)
            {
                Console.Write("Write your name:  ");
                string newName = Console.ReadLine();
                if(newName == "")
                {
                    Console.WriteLine("You did not write your name");
                    continue;
                }
                if(this.InitPlayer(newName) == true)
                {
                    return true;
                    //break;
                }
            }
            
        }

    }
}
