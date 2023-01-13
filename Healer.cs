using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsConsoleGame
{
	public class Healer : Worker, IWorkerServices
	{
		//const
		public Healer(string name) : base(name) {
			Lvl = 1;

			UpgradeCost = WorkerServices.SetUpgradeCost(this);
        }

        //meth
        public byte Lvl { get; set; }

		public ushort UpgradeCost { get; set; }

        /// <summary>
        /// rolls a number between 1 and 20<br />
        /// the higher the number is, the better is the heal
        /// </summary>
        /// <returns>ushort -> gained money</returns>
        public short UseService() {
			Random r = new();
			byte roll = (byte)r.Next(1, 21);
			byte[] healPerLevel = new byte[5] { 5, 10, 15, 20, 30 };
			byte heal = 0;

			// convert to byte -> cannot be bigger than byte
			if (roll == 1) heal = (byte)(healPerLevel[Lvl - 1] * 0.4);	// 40 %
			else if (roll < 11) heal = (byte)(healPerLevel[Lvl - 1] * 0.7);	// 70 %
			else if (roll < 20) heal = healPerLevel[Lvl - 1];	// 100 %
			else heal = (byte)(healPerLevel[Lvl - 1] * 1.3);	// 130 %

			return heal;
		}
	}
}
