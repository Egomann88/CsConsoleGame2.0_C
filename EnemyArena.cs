using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsConsoleGame
{
    internal class EnemyArena : Enemy
    {
        // class

        // mem

        // const
        public EnemyArena(byte pLvl, byte eId, bool isHard = false) :base(pLvl, eId, isHard) {
            SetEnemyStats(pLvl, eId, isHard);
        }

        // meth

        /// <summary>
        /// chooses enemy and increases its starts with player level and hardmode
        /// </summary>
        /// <param name="pLvl">player level</param>
        /// <param name="eId">enemy id</param>
        /// <param name="isHard">hardmode enemy true/false</param>
        protected override void SetEnemyStats(byte pLvl, byte eId, bool isHard) {
            float multiplier = 1.0F;

            // increase difficulty by increasing stat multiplier
            if (isHard) multiplier += 0.75f;
            while (pLvl - 10 >= 0) {
                multiplier += 0.2F;
                pLvl -= 10;
            }

            switch (eId) {
                case 1:
                    Name = "Gladiator";
                    Strength = Convert.ToUInt16(Math.Round(12 * multiplier));
                    Intelligents = Convert.ToUInt16(Math.Round(10 * multiplier));
                    Dexterity = Convert.ToUInt16(Math.Round(11 * multiplier));
                    CritChance = 0.10F * multiplier;
                    CritMult = base.MaxMultiplier(1.1F, multiplier, true);
                    Health = new short[] {
                        Convert.ToInt16(Math.Round(60 * multiplier)),
                        Convert.ToInt16(Math.Round(60 * multiplier))
                    };
                    Gold = Convert.ToInt32(Math.Round(334 * multiplier));
                    Exp = new uint[] {
                        Convert.ToUInt16(Math.Round(227 * multiplier)),
                        Convert.ToUInt16(Math.Round(227 * multiplier))
                    };
                    IsDmgUlt = true;
                    break;
                case 2:
                    Name = "Aurelion";
                    Strength = Convert.ToUInt16(Math.Round(8.4 * multiplier));
                    Intelligents = Convert.ToUInt16(Math.Round(14.1 * multiplier));
                    Dexterity = Convert.ToUInt16(Math.Round(11.2 * multiplier));
                    CritChance = 0.11F * multiplier;
                    CritMult = base.MaxMultiplier(1.15F, multiplier, true);
                    Health = new short[] {
                        Convert.ToInt16(Math.Round(56.4 * multiplier)),
                        Convert.ToInt16(Math.Round(56.4 * multiplier))
                    };
                    Gold = Convert.ToInt32(Math.Round(452 * multiplier));
                    Exp = new uint[] {
                        Convert.ToUInt16(Math.Round(172 * multiplier)),
                        Convert.ToUInt16(Math.Round(172 * multiplier))
                    };
                    IsDmgUlt = false;
                    break;
                case 3:
                    Name = "Gold Hexe";
                    Strength = Convert.ToUInt16(Math.Round(5 * multiplier));
                    Intelligents = Convert.ToUInt16(Math.Round(26.8 * multiplier));
                    Dexterity = Convert.ToUInt16(Math.Round(9 * multiplier));
                    CritChance = 0;
                    CritMult = 0;
                    Health = new short[] {
                        Convert.ToInt16(Math.Round(55 * multiplier)),
                        Convert.ToInt16(Math.Round(150 * multiplier))
                    };
                    Gold = Convert.ToInt32(Math.Round(384 * multiplier));
                    Exp = new uint[] {
                        Convert.ToUInt16(Math.Round(207 * multiplier)),
                        Convert.ToUInt16(Math.Round(207 * multiplier))
                    };
                    IsDmgUlt = false;
                    break;
                case 4:
                    Name = "Todesritter";
                    Strength = Convert.ToUInt16(Math.Round(15.4 * multiplier));
                    Intelligents = Convert.ToUInt16(Math.Round(10 * multiplier));
                    Dexterity = Convert.ToUInt16(Math.Round(12 * multiplier));
                    CritChance = 0.08F * multiplier;
                    CritMult = base.MaxMultiplier(1.66F, multiplier, true);
                    Health = new short[] {
                        Convert.ToInt16(Math.Round(66 * multiplier)),
                        Convert.ToInt16(Math.Round(75.4 * multiplier))
                    };
                    Gold = Convert.ToInt32(Math.Round(234 * multiplier));
                    Exp = new uint[] {
                        Convert.ToUInt16(Math.Round(427 * multiplier)),
                        Convert.ToUInt16(Math.Round(427 * multiplier))
                    };
                    IsDmgUlt = true;
                    break;
                case 5:
                    Name = "Nachtklinge";
                    Strength = Convert.ToUInt16(Math.Round(11.4 * multiplier));
                    Intelligents = Convert.ToUInt16(Math.Round(8.4 * multiplier));
                    Dexterity = Convert.ToUInt16(Math.Round(17.4 * multiplier));
                    CritChance = 0.20F * multiplier;
                    CritMult = base.MaxMultiplier(1.11F, multiplier, true);
                    Health = new short[] {
                        Convert.ToInt16(Math.Round(55 * multiplier)),
                        Convert.ToInt16(Math.Round(55 * multiplier))
                    };
                    Gold = Convert.ToInt32(Math.Round(350 * multiplier));
                    Exp = new uint[] {
                        Convert.ToUInt16(Math.Round(350 * multiplier)),
                        Convert.ToUInt16(Math.Round(350 * multiplier))
                    };
                    IsDmgUlt = true;
                    break;
            }
        }
    }
}
