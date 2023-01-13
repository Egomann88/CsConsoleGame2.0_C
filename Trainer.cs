namespace CsConsoleGame
{
    internal class Trainer : Worker
    {
        //const
        public Trainer(string name, char focus) :base(name) {
            Focus = focus;
        }

        //meth
        public char Focus { get; } // S = Strength; I = Inteligents; D = Dex; E = Exp

        /// <summary>
        /// returns skill / exp increase 
        /// </summary>
        /// <returns>Leveled skill or exp</returns>
        public byte UseService() {
            Random r = new();

            if (Focus == 'E') return IncreasePlayerExp((byte)r.Next(1, 51));
            else return IncreasePlayerStats((byte)r.Next(1, 51));
        }

        /// <summary>
        /// returns skill increasement
        /// </summary>
        /// <param name="roll">rnd roll</param>
        /// <returns>0, 1 or 2</returns>
        private static byte IncreasePlayerStats(byte roll) {
            if (roll == 1) return 0;    // bad training
            else if (roll == 50) return 2;  // perfect training
            else return 1;
        }

        /// <summary>
        /// returns exp increasement
        /// </summary>
        /// <param name="roll">rnd roll</param>
        /// <returns>10, 100 or 255</returns>
        private static byte IncreasePlayerExp(byte roll) {
            if (roll == 1) return 10;    // bad training
            else if (roll == 50) return 255;  // perfect training
            else return 100;
        }
    }
}
