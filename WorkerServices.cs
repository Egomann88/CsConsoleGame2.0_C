using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CsConsoleGame
{
    public static class WorkerServices
    {
        // class
        private const byte MAXLVL = 5;

        // meth
        /// <summary>
        /// Asks the Player if he wants to upgrade his worker<br />
        /// </summary>
        /// <param name="ws">worker</param>
        /// <returns>1 or 0</returns>
        public static byte IncreaseService(IWorkerServices ws, int playerGold) {
            if (ws.Lvl != MAXLVL) {
                Console.WriteLine("{0} ist Stufe {1}.\nEine Verbesserung kostet {2}.\n" +
                "Stufe aufsteigen lassen? [j/n]", ws.Name, ws.Lvl, ws.UpgradeCost);
                if (Console.ReadKey(false).Key == ConsoleKey.J && playerGold >= ws.UpgradeCost) return 1;
            }
            Console.WriteLine("{0} ist auf der höchsten Stufe", ws.Name);
            return 0;
        }

        /// <summary>
        /// Sets the cost for upgrade to the next lvl
        /// </summary>
        /// <param name="ws">worker</param>
        /// <returns>upgrade costs for next lvl</returns>
        public static ushort SetUpgradeCost(IWorkerServices ws) {
            Random r = new();
            switch (ws.Lvl) {
                case 1:
                    ws.UpgradeCost = (ushort)(500 + r.Next(1, 777));
                    break;
                case 2:
                    ws.UpgradeCost = (ushort)(800 + r.Next(1, 777));
                    break;
                case 3:
                    ws.UpgradeCost = (ushort)(1357 + r.Next(1, 999));
                    break;
                case 4:
                    ws.UpgradeCost = (ushort)(2222 + r.Next(1, 1111));
                    break;
            }
            return ws.UpgradeCost;
        }
    }
}
