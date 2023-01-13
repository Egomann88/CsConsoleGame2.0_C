using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsConsoleGame
{
    internal class Enemy : Character
    {
        // class

        // mem

        // const
        public Enemy(byte pLvl, byte eId, bool ishard = false) {
            SetEnemyStats(pLvl, eId, ishard);
        }

        // meth
        public byte EnemyId { get; set; }

        public bool IsDmgUlt { get; set; }

        /// <summary>
        /// sets an max Multiplier (300 % / 250 %)<br />
        /// the normal multiplicater and the multiplier cannot be bigger than the max multiplier
        /// </summary>
        /// <param name="mutliplicator">enemy normal CritMult</param>
        /// <param name="multiplier">increasing multiplier</param>
        /// <param name="boss">bossenemy?</param>
        /// <returns></returns>
        protected float MaxMultiplier(float mutliplicator, float multiplier, bool boss = false) {
            float maxMultiplier = boss ? 3F : 2.5F; // if enemy is strong, use bigger multiplier for max crit
            float result = mutliplicator * multiplier;

            return result > maxMultiplier ? maxMultiplier : result;
        }

        /// <summary>
        /// chooses enemy and increases its starts with player level and hardmode
        /// </summary>
        /// <param name="pLvl">player level</param>
        /// <param name="eId">enemy id</param>
        /// <param name="isHard">hardmode enemy true/false</param>
        virtual protected void SetEnemyStats(byte pLvl, byte eId, bool isHard) {
            float multiplier = 1.0F;

            // increase difficulty by increasing stat multiplier
            if(isHard) multiplier += 0.75f;
            while(pLvl - 10 >= 0) {
                multiplier += 0.2F;
                pLvl -= 10;
            }

            // create Enemy with Id
            switch(eId) {
                // goblin
                case 1:
                    Name = "Golbin";
                    Strength = Convert.ToUInt16(Math.Round(2 * multiplier));
                    Intelligents = Convert.ToUInt16(Math.Round(0.4 * multiplier));
                    Dexterity = Convert.ToUInt16(Math.Round(1 * multiplier));
                    CritChance = 0.02F * multiplier;
                    CritMult = MaxMultiplier(1.2F, multiplier);
                    Health = new short[] {
                        Convert.ToInt16(Math.Round(10 * multiplier)),
                        Convert.ToInt16(Math.Round(10 * multiplier))
                    };
                    Gold = Convert.ToInt32(Math.Round(10 * multiplier));
                    Exp = new uint[] {
                        Convert.ToUInt16(Math.Round(8 * multiplier)),
                        Convert.ToUInt16(Math.Round(8 * multiplier))
                    };
                    IsDmgUlt = true;
                    break;
                // assasin
                case 2:
                    Name = "Assasine";
                    Strength = Convert.ToUInt16(Math.Round(2 * multiplier));
                    Intelligents = Convert.ToUInt16(Math.Round(1.4 * multiplier));
                    Dexterity = Convert.ToUInt16(Math.Round(3 * multiplier));
                    CritChance = 0.12F * multiplier;
                    CritMult = MaxMultiplier(1.25F, multiplier);
                    Health = new short[] {
                        Convert.ToInt16(Math.Round(15.4 * multiplier)),
                        Convert.ToInt16(Math.Round(15.4 * multiplier))
                    };
                    Gold = Convert.ToInt32(Math.Round(19 * multiplier));
                    Exp = new uint[] {
                        Convert.ToUInt16(Math.Round(14 * multiplier)),
                        Convert.ToUInt16(Math.Round(14 * multiplier))
                    };
                    IsDmgUlt = true;
                    break;
                // witch
                case 3:
                    Name = "Hexe";
                    Strength = Convert.ToUInt16(Math.Round(1.3 * multiplier));
                    Intelligents = Convert.ToUInt16(Math.Round(4 * multiplier));
                    Dexterity = Convert.ToUInt16(Math.Round(2.1 * multiplier));
                    CritChance = 0.09F * multiplier;
                    CritMult = MaxMultiplier(1.50F, multiplier);
                    Health = new short[] {
                        Convert.ToInt16(Math.Round(13 * multiplier)),
                        Convert.ToInt16(Math.Round(13 * multiplier))
                    };
                    Gold = Convert.ToInt32(Math.Round(13 * multiplier));
                    Exp = new uint[] {
                        Convert.ToUInt16(Math.Round(23 * multiplier)),
                        Convert.ToUInt16(Math.Round(23 * multiplier))
                    };
                    IsDmgUlt = true;
                    break;
                // bandit
                case 4:
                    Name = "Bandit";
                    Strength = Convert.ToUInt16(Math.Round(3 * multiplier));
                    Intelligents = Convert.ToUInt16(Math.Round(0.8 * multiplier));
                    Dexterity = Convert.ToUInt16(Math.Round(2.4 * multiplier));
                    CritChance = 0.02F * multiplier;
                    CritMult = MaxMultiplier(1.25F, multiplier);
                    Health = new short[] {
                        Convert.ToInt16(Math.Round(16 * multiplier)),
                        Convert.ToInt16(Math.Round(18 * multiplier))
                    };
                    Gold = Convert.ToInt32(Math.Round(21 * multiplier));
                    Exp = new uint[] {
                        Convert.ToUInt16(Math.Round(16 * multiplier)),
                        Convert.ToUInt16(Math.Round(16 * multiplier))
                    };
                    IsDmgUlt = true;
                    break;
                // mercenary
                case 5:
                    Name = "Söldner";
                    Strength = Convert.ToUInt16(Math.Round(3.3 * multiplier));
                    Intelligents = Convert.ToUInt16(Math.Round(1.4 * multiplier));
                    Dexterity = Convert.ToUInt16(Math.Round(2.4 * multiplier));
                    CritChance = 0.04F * multiplier;
                    CritMult = MaxMultiplier(1.30F, multiplier);
                    Health = new short[] {
                        Convert.ToInt16(Math.Round(16 * multiplier)),
                        Convert.ToInt16(Math.Round(19 * multiplier))
                    };
                    Gold = Convert.ToInt32(Math.Round(33 * multiplier));
                    Exp = new uint[] {
                        Convert.ToUInt16(Math.Round(26 * multiplier)),
                        Convert.ToUInt16(Math.Round(26 * multiplier))
                    };
                    IsDmgUlt = true;
                    break;
                // paladin
                case 6:
                    Name = "Paladin";
                    Strength = Convert.ToUInt16(Math.Round(2 * multiplier));
                    Intelligents = Convert.ToUInt16(Math.Round(5 * multiplier));
                    Dexterity = Convert.ToUInt16(Math.Round(3 * multiplier));
                    CritChance = 0.09F * multiplier;
                    CritMult = MaxMultiplier(1.25F, multiplier);
                    Health = new short[] {
                        Convert.ToInt16(Math.Round(26 * multiplier)),
                        Convert.ToInt16(Math.Round(26 * multiplier))
                    };
                    Gold = Convert.ToInt32(Math.Round(38 * multiplier));
                    Exp = new uint[] {
                        Convert.ToUInt16(Math.Round(26 * multiplier)),
                        Convert.ToUInt16(Math.Round(26 * multiplier))
                    };
                    IsDmgUlt = false;
                    break;
                // plantara
                case 7:
                    Name = "Plantara";
                    Strength = Convert.ToUInt16(Math.Round(6 * multiplier));
                    Intelligents = Convert.ToUInt16(Math.Round(2 * multiplier));
                    Dexterity = Convert.ToUInt16(Math.Round(2 * multiplier));
                    CritChance = 0.03F * multiplier;
                    CritMult = MaxMultiplier(1.1F, multiplier);
                    Health = new short[] {
                        Convert.ToInt16(Math.Round(31 * multiplier)),
                        Convert.ToInt16(Math.Round(31 * multiplier))
                    };
                    Gold = Convert.ToInt32(Math.Round(12 * multiplier));
                    Exp = new uint[] {
                        Convert.ToUInt16(Math.Round(38 * multiplier)),
                        Convert.ToUInt16(Math.Round(38 * multiplier))
                    };
                    IsDmgUlt = false;
                    break;
                // beserk
                case 8:
                    Name = "Beserker";
                    Strength = Convert.ToUInt16(Math.Round(8.4 * multiplier));
                    Intelligents = Convert.ToUInt16(Math.Round(5 * multiplier));
                    Dexterity = Convert.ToUInt16(Math.Round(4 * multiplier));
                    CritChance = 0.03F * multiplier;
                    CritMult = MaxMultiplier(1.5F, multiplier, true);
                    Health = new short[] {
                        Convert.ToInt16(Math.Round(43 * multiplier)),
                        Convert.ToInt16(Math.Round(43 * multiplier))
                    };
                    Gold = Convert.ToInt32(Math.Round(68 * multiplier));
                    Exp = new uint[] {
                        Convert.ToUInt16(Math.Round(74 * multiplier)),
                        Convert.ToUInt16(Math.Round(74 * multiplier))
                    };
                    IsDmgUlt = true;
                    break;
                // Archmage
                case 9:
                    Name = "Erzmagier";
                    Strength = Convert.ToUInt16(Math.Round(3 * multiplier));
                    Intelligents = Convert.ToUInt16(Math.Round(12.4 * multiplier));
                    Dexterity = Convert.ToUInt16(Math.Round(6 * multiplier));
                    CritChance = 0.23F * multiplier;
                    CritMult = MaxMultiplier(1.1F, multiplier);
                    Health = new short[] {
                        Convert.ToInt16(Math.Round(35 * multiplier)),
                        Convert.ToInt16(Math.Round(35 * multiplier))
                    };
                    Gold = Convert.ToInt32(Math.Round(70 * multiplier));
                    Exp = new uint[] {
                        Convert.ToUInt16(Math.Round(86 * multiplier)),
                        Convert.ToUInt16(Math.Round(86 * multiplier))
                    };
                    IsDmgUlt = false;
                    break;
                // dragon
                case 10:
                    Name = "Drache";
                    Strength = Convert.ToUInt16(Math.Round(10.4 * multiplier));
                    Intelligents = Convert.ToUInt16(Math.Round(7.8 * multiplier));
                    Dexterity = Convert.ToUInt16(Math.Round(8 * multiplier));
                    CritChance = 0.05F * multiplier;
                    CritMult = MaxMultiplier(1.35F, multiplier);
                    Health = new short[] {
                        Convert.ToInt16(Math.Round(66 * multiplier)),
                        Convert.ToInt16(Math.Round(66 * multiplier))
                    };
                    Gold = Convert.ToInt32(Math.Round(160 * multiplier));
                    Exp = new uint[] {
                        Convert.ToUInt16(Math.Round(140 * multiplier)),
                        Convert.ToUInt16(Math.Round(140 * multiplier))
                    };
                    IsDmgUlt = true;
                    break;
                // demon
                case 11:
                    Name = "Dämon";
                    Strength = Convert.ToUInt16(Math.Round(11.6 * multiplier));
                    Intelligents = Convert.ToUInt16(Math.Round(9 * multiplier));
                    Dexterity = Convert.ToUInt16(Math.Round(10 * multiplier));
                    CritChance = 0.06F * multiplier;
                    CritMult = MaxMultiplier(1.3F, multiplier);
                    Health = new short[] {
                        Convert.ToInt16(Math.Round(74.4 * multiplier)),
                        Convert.ToInt16(Math.Round(74.4 * multiplier))
                    };
                    Gold = Convert.ToInt32(Math.Round(140 * multiplier));
                    Exp = new uint[] {
                        Convert.ToUInt16(Math.Round(160 * multiplier)),
                        Convert.ToUInt16(Math.Round(160 * multiplier))
                    };
                    IsDmgUlt = true;
                    break;
                // grifin
                case 12:
                    Name = "Grifin";
                    Strength = Convert.ToUInt16(Math.Round(11.4 * multiplier));
                    Intelligents = Convert.ToUInt16(Math.Round(10 * multiplier));
                    Dexterity = Convert.ToUInt16(Math.Round(14 * multiplier));
                    CritChance = 0.10F * multiplier;
                    CritMult = MaxMultiplier(1.5F, multiplier, true);
                    Health = new short[] {
                        Convert.ToInt16(Math.Round(76 * multiplier)),
                        Convert.ToInt16(Math.Round(76 * multiplier))
                    };
                    Gold = Convert.ToInt32(Math.Round(36 * multiplier));
                    Exp = new uint[] {
                        Convert.ToUInt16(Math.Round(225 * multiplier)),
                        Convert.ToUInt16(Math.Round(225 * multiplier))
                    };
                    IsDmgUlt = false;
                    break;
                // ashura
                case 13:
                    Name = "Ashura";
                    Strength = Convert.ToUInt16(Math.Round(12 * multiplier));
                    Intelligents = Convert.ToUInt16(Math.Round(9.4 * multiplier));
                    Dexterity = Convert.ToUInt16(Math.Round(22 * multiplier));
                    CritChance = 0.15F * multiplier;
                    CritMult = MaxMultiplier(1.35F, multiplier, true);
                    Health = new short[] {
                        Convert.ToInt16(Math.Round(60 * multiplier)),
                        Convert.ToInt16(Math.Round(60 * multiplier))
                    };
                    Gold = Convert.ToInt32(Math.Round(150 * multiplier));
                    Exp = new uint[] {
                        Convert.ToUInt16(Math.Round(270 * multiplier)),
                        Convert.ToUInt16(Math.Round(270 * multiplier))
                    };
                    IsDmgUlt = true;
                    break;
            }
        }
    }
}
