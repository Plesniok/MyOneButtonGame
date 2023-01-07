using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;

namespace OneButtonGame
{
    class Enemy
    {
        public List<Dictionary<string, int>> enemyPosition = new List<Dictionary<string, int>>();
        public Enemy()
        {
            //    this.playerName = playerName;

        }
        private void addPosition(int valuex, int valuey)
        {
            Dictionary<string, int> bufforDictionary = new Dictionary<string, int>();
            bufforDictionary.Add("x", valuex);
            bufforDictionary.Add("y", valuey);
            this.enemyPosition.Add(bufforDictionary);
        }
        private void removePositions()
        {
            this.enemyPosition.Clear();
        }
        

        public bool InitEnemy(int beginRender, int endRender)
        {
            Console.WriteLine("INIT ENEMY");
            addPosition(beginRender, 13);
            addPosition(beginRender, 14);
            addPosition(beginRender + 1, 13);
            addPosition(beginRender + 1, 14);
            addPosition(endRender, 13);
            addPosition(endRender, 14);

            return true;
        }
    }
}
