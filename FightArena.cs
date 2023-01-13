using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsConsoleGame
{
    internal class FightArena : Fight
    {
        // class

        // mem

        // const
        public FightArena(Player p, Enemy e) : base(p, e) { }

        // meth
        override public Player FightIn() {
            bool fightOver = false, giveUp = false;
            bool isPlayerFirst = GetFirstMove();
            byte playerTurns = GetNumOfTurns(true);
            byte enemyTurns = GetNumOfTurns(false);

            Console.Clear();
            Console.WriteLine("Ein {0} seit auf der Hut.", Enemy.Name);
            Thread.Sleep(SHORTTIMEOUT);

            do {
                Console.Clear();    // clear all fighting texts

                if (isPlayerFirst) {
                    for (byte i = 0; i < playerTurns; i++) {    // repeat as long as Player still has turns
                        fightOver = giveUp = PlayerTurn();
                        if (Enemy.Health[0] <= 0) {
                            fightOver = true;
                            break;
                        }
                    }
                    if (fightOver) continue;    // go to end of while-loop -> enemy died
                }

                for (byte i = 0; i < enemyTurns; i++) { // repeat as long as Enemy still has turns
                    EnemyTurn();
                    if (Player.Health[0] <= 0) {
                        Player.Health[0] = 1; // survive with 1 hp
                        fightOver = true;
                        break;  // break out of for-loop
                    }
                }
                if (fightOver) continue;    // go to end of while-loop -> Player defeated

                if (!isPlayerFirst) {
                    for (byte i = 0; i < playerTurns; i++) {    // repeat as long as Player still has turns
                        fightOver = giveUp = PlayerTurn(); // if player fled, jump direct to end
                        if (Enemy.Health[0] <= 0) {
                            fightOver = true;
                            break;
                        }
                    }
                    if (fightOver) continue;    // go to end of while-loop -> enemy died
                }

                RoundCount++;

            } while (!fightOver);

            Console.Clear();
            if (giveUp) Console.WriteLine("{0} hat aufgegeben!", Player.Name);
            else if (Player.Health[0] == 1) Console.WriteLine("{0} hat verloren.", Player.Name);
            else {  // defeated enemy
                Console.WriteLine("{0} war siegreich!\n{1} Exp erhalten.\n{2} Gold erhalten", Player.Name, Enemy.Exp[0], Enemy.Gold);
                // get enemy gold and exp
                Player.Exp[0] += Enemy.Exp[0];
                Player.ChangeAmoutOfGold(Enemy.Gold);

                // player lvl up
                Player.IncreaseLvl();
            }

            Console.WriteLine("\n\nDrücken Sie auf eine Taste, um fortzufahren...");
            Console.ReadKey(true);

            Player.SavePlayer(Player); // autosave
            return Player;
        }

        override protected bool PlayerTurn() {
            short[] coolDown = GetCoolDown(true);   // cooldown of abilitys
            string ultimateName = GetUltimateName(), actionText = ""; // what player will do
            ushort damage = 0;  // players dmg
            ushort chance2Hit = (ushort)(75 + Player.Dexterity - Enemy.Dexterity); // 75 % base value + char dex - enemy dex (dodge chance)
            char input = '0';   // player input
            bool giveUp = false;

            do {
                Console.Clear();
                Console.WriteLine("{0}, was wollt ihr machen?\nLeben: {1} / {2}",
                  Player.Name, Player.Health[0], Player.Health[1]);
                Console.Write("1) Angreifen\n2) Heilen (Abklingzeit: {0} Runden)\n" +
                  "3) {1} (Abklingzeit: {2} Runden)\n4) Aufgeben", coolDown[0], ultimateName, coolDown[1]);
                input = Console.ReadKey(true).KeyChar; // do not show input in console
                Console.Clear();
                switch (input) {
                    case '1':
                        damage = Player.Strength;

                        actionText = $"{Player.Name} greift an.\n";

                        if (CritDodge(Player.CritChance)) {
                            damage = Convert.ToUInt16(Math.Round(damage * Player.CritMult));
                            actionText += "Kritischer Treffer!\n";
                            chance2Hit = 100; // Crit is always an hit
                        }

                        if (!CritDodge(chance2Hit)) {
                            actionText += $"{Enemy.Name} ist ausgewichen!\n";
                            damage = 0;
                        } else actionText += $"{damage} Schaden!";


                        Enemy.ChangeCurrentHealth(Convert.ToInt16(-damage));
                        break;
                    case '2':
                        // if abilty is still on cooldown, go back to start
                        if (CharacterOnCoolDown(coolDown[0])) continue;

                        damage = Player.Intelligents;

                        actionText = $"{Player.Name} heilt sich.\n{damage} Leben wiederhergestellt";

                        Player.ChangeCurrentHealth(Convert.ToInt16(damage));

                        coolDown[0] = HEALCOOLDOWN;    // set heal cooldown
                        break;
                    case '3':
                        // if abilty is still on cooldown, go back to start
                        if (CharacterOnCoolDown(coolDown[1])) continue;

                        damage = GetCharacterUltimate();

                        actionText = $"{Player.Name} nutzt seine Ultimatie Fähigkeit \"{ultimateName}\".\n";

                        if (CritDodge(Player.CritChance)) {
                            damage = Convert.ToUInt16(Math.Round(damage * Player.CritMult));
                            actionText += "Kritischer Treffer!\n";
                            chance2Hit = 100; // Crit is always an hit
                        }

                        if (!CritDodge(chance2Hit + ULTHITBONUS)) { // ultimate has extra hit chance
                            actionText += $"{Enemy.Name} ist ausgewichen!\n";
                            damage = 0;
                        } else actionText += $"{damage} Schaden!";

                        Enemy.ChangeCurrentHealth(Convert.ToInt16(-damage));
                        coolDown[1] = ULTIMATECOOLDOWN;    // set ulti cooldown
                        break;
                    case '4':
                        actionText = $"{Player.Name} gibt auf.\n";
                        giveUp = true;
                        break;
                    default: continue;  // must give new input
                }

                Console.WriteLine(actionText);

                Thread.Sleep(TIMEOUT);

                break;  // break out of loop
            } while (true);

            coolDown = coolDown.Select(x => --x).ToArray();   // decrease cooldowns by one

            PLAYERCOOLDOWN = coolDown;  // save cooldowns for next round

            return giveUp;
        }
    }
}
