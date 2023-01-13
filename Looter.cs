using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsConsoleGame
{
    public class Looter : Worker, IWorkerServices
    {
		//const
		public Looter(string name) : base(name) {
            Lvl = 1;

            UpgradeCost = WorkerServices.SetUpgradeCost(this);
		}

		//meth
		public byte Lvl { get; set; }

        public ushort UpgradeCost { get; set; }

        /// <summary>
        /// rolls a number between 1 and 20<br />
        /// the higher the number is, the more money the player gets
        /// </summary>
        /// <returns>ushort -> gained money</returns>
        public ushort UseService() {
            Random r = new();
            byte roll = (byte)r.Next(1, 21);
            byte[] gainPerLevel = new byte[5] { 40, 60, 80, 140, 220 };
            ushort gain = 0;

			// convert to byte -> cannot be bigger than byte
			if (roll == 1) gain = (byte)(gainPerLevel[Lvl - 1] / 3);
            else if (roll < 11) gain = (byte)(gainPerLevel[Lvl - 1] / 1.5);
			else if (roll < 20) gain = gainPerLevel[Lvl - 1];
            else gain = (ushort)(gainPerLevel[Lvl - 1] * 2.5);

            return gain;
        }
    }
}
