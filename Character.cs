using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CsConsoleGame
{
    abstract class Character
    {
        // meth
        public string Name { get; set; }

        virtual public ushort Strength { get; set; }

        virtual public ushort Intelligents { get; set; }

        virtual public ushort Dexterity { get; set; }

        virtual public float CritChance { get; set; }

        virtual public float CritMult { get; set; }

        /// <summary>
        /// 0 = current hp<br />
        /// 1 = max hp
        /// </summary>
        virtual public short[] Health { get; set; }

        public int Gold { get; set; }

        /// <summary>
        /// 0 = current exp<br />
        /// 1 = needed exp (for lvl up)
        /// </summary>
        public uint[] Exp { get; set; }

        public byte Lvl { get; set; }

        /// <summary>
        /// Checks if in Character Name is an InValid Sign
        /// </summary>
        /// <param name="input">inserted text</param>
        /// <returns>true - if wrong sign / false - if all correct</returns>
        public static bool InValidSign(string input) {
            Regex regex = new("[\\\\/:\\*\\?\"<>\\|]", RegexOptions.IgnoreCase);
            return regex.IsMatch(input);
        }

        /// <summary>
        /// de- / increases max HP
        /// </summary>
        /// <param name="health">value with which max HP is increased or not</param>
        public void ChangeMaximumHealth(short health) {
            Health[1] += health;
        }

        /// <summary>
        /// de- / increases HP
        /// </summary>
        /// <param name="health">value with which HP is increased or not</param>
        /// <param name="overheal">allows to heal over max HP</param>
        public void ChangeCurrentHealth(short health, bool overheal = false) {
            Health[0] += health;
            if (Health[0] > Health[1] && !overheal) Health[0] = Health[1];
        }

        /// <summary>
        /// Sets Characters current HP to max
        /// </summary>
        public void FullHeal() {
            ChangeCurrentHealth(Health[1], false);
        }

        /// <summary>
        /// de- / increases amout of Gold
        /// </summary>
        /// <param name="gold">value with which Gold is increased or not</param>
        public void ChangeAmoutOfGold(int gold) {
            Gold += gold;
        }
    }
}
