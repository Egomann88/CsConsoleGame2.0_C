using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CsConsoleGame
{
    internal class Player : Character {
        // class
        const ushort MAXSTATVALUE = 300;
        const float MAXCRITCHANCE = 60;
        const float MAXCRITMULT = 3;
        const short MAXHEALTH = 250;
        const byte MAXLVL = 100;

        // member
        ushort _Strength = 0;
        ushort _Intelligents = 0;
        ushort _Dexterity = 0;
        float _CritChance = 0;
        float _CritMult = 0;
        short[] _Health = new short[2];

        // const
        /// <summary>
        /// creates an new Player
        /// </summary>
        /// <param name="name">playername</param>
        /// <param name="cl">class of player</param>
        public Player(string name, byte cl) {
            if (InValidSign(name)) {
                Console.WriteLine("\nUnerlaubtes Zeichen im Namen!");
                return;
            }

            if(!(cl is 1 or 2 or 3)) {
                Console.WriteLine("\nEingegebene Klasse gibt es nicht!");
                return;
            }

            Name = name;
            Class = cl;
            Exp = new uint[2] { 0, 30 };
            Lvl = 1;

            switch (Class) {
                case 1: // warrior
                    Strength = 4;
                    Intelligents = 2;
                    Dexterity = 2;
                    CritChance = 2.4F;
                    CritMult = 1.25F;
                    Health = new short[2] { 30, 30 };
                    Gold = 29;
                    break;
                case 2: // mage
                    Strength = 2;
                    Intelligents = 4;
                    Dexterity = 2;
                    CritChance = 4.8F;
                    CritMult = 1.5F;
                    Health = new short[2] { 23, 23 };
                    Gold = 21;
                    break;
                case 3: // thief
                    Strength = 2;
                    Intelligents = 2;
                    Dexterity = 4;
                    CritChance = 3.2F;
                    CritMult = 1.75F;
                    Health = new short[2] { 25, 25 };
                    Gold = 36;
                    break;
                default:  break;
            }
        }
        
        public Player() { }

        // meth
        /// <summary>
        /// 1 = warrior, 2 = mage, 3 = thief
        /// </summary>
        public byte Class { get; set; }

        public override ushort Strength {
            get => _Strength; set {
                if (_Strength >= MAXSTATVALUE) _Strength = MAXSTATVALUE;
                _Strength = value;
            }
        }

        public override ushort Intelligents {
            get => _Intelligents; set {
                if (_Intelligents >= MAXSTATVALUE) _Intelligents = MAXSTATVALUE;
                _Intelligents = value;
            }
        }

        public override ushort Dexterity {
            get => _Dexterity; set {
                if (_Dexterity >= MAXSTATVALUE) _Dexterity = MAXSTATVALUE;
                _Dexterity = value;
            }
        }
        public override float CritChance {
            get => _CritChance; set {
                if (_CritChance >= MAXCRITCHANCE) _CritChance = MAXCRITCHANCE;
                _CritChance = value;
            }
        }
        public override float CritMult {
            get => _CritMult; set {
                if (_CritMult >= MAXCRITMULT) _CritMult = MAXCRITMULT;
                _CritMult = value;
            }
        }
        /// <summary>
        /// 0 = current Health, 1 = max Health
        /// </summary>
        public override short[] Health {
            get => _Health; set {
                for (byte i = 0; i < _Health.Length; i++) {
                    if (_Health[i] >= MAXHEALTH) _Health[i] = MAXHEALTH;
                    else _Health = value;
                }
            }
        }

        /// <summary>
        /// Creates an new Player with Name and Class.<br />
        /// </summary>
        /// <returns>Player</returns>
        public static Player CreatePlayer() {
            string name = "";
            byte cl = 0;

            name = ChangeName();

            do {
                Console.Clear();
                Console.WriteLine("Was ist die Klasse ihres Charakters?\n1) Krieger\n2) Magier\n3) Schurke");
                cl = Convert.ToByte(Console.ReadKey(false).KeyChar - 48);
            } while (cl < 1 || cl > 4);

            return new Player(name, cl);
        }

        /// <summary>
        /// overrides the old Player name
        /// </summary>
        /// <returns>(new) name</returns>
        public static string ChangeName() {
            bool valid = false;
            string name = "";

            while (!valid) {
                Console.Clear();
                Console.WriteLine("Geben Sie den Namen ihres Charakters ein:");
                name = Console.ReadLine();

                if (InValidSign(name)) {
                    Console.WriteLine("\nIm Namen ist ein unerlaubtes Zeichen enthalten!");
                    Thread.Sleep(500);
                    continue;
                } else if (IsDoubleName(name)) {
                    Console.WriteLine("\nIhr benutzt denselben Namen zweimal. Das ist nicht möglich.");
                    Thread.Sleep(500);
                    continue;
                } else if (name == "" || name == " ") {
                    Console.WriteLine("\nDer Name darf nicht leer sein!");
                    Thread.Sleep(500);
                    continue;
                }

                valid = true;
                // convert to char array of the string
                char[] letters = name.ToCharArray();
                // upper case the first char
                letters[0] = char.ToUpper(letters[0]);
                // put array back together
                name = new string(letters);
            }

            return name;
        }

        /// <summary>
        /// Returns Classname
        /// </summary>
        /// <returns>classname</returns>
        public string GetClassName() {
            string cl = "";
            if (Class == 1) cl = "Krieger";
            else if (Class == 2) cl = "Magier";
            else cl = "Schurke";

            return cl;
        }

        /// <summary>
        /// infoscreen for player
        /// </summary>
        public void ShowPlayer() {
            string cl = GetClassName();
            string[] stats = {
                $"Name:\t\t\t{Name}",
                $"Klasse:\t\t\t{cl}",
                $"Level:\t\t\t{Lvl}",
                $"Exp:\t\t\t{Exp[0]} / {Exp[1]}",
                $"Leben:\t\t\t{Health[0]} / {Health[1]}",
                $"Gold:\t\t\t{Gold}",
                $"Stärke:\t\t\t{Strength}",
                $"Inteligents:\t\t{Intelligents}",
                $"Geschwindigkeit:\t{Dexterity}",
                $"Krit. Chance:\t\t{Math.Round(CritChance, 3)} %",
                $"Krit. Schaden:\t\t{Math.Round((CritMult - 1.0F) * 100, 3)} %",
                "\nDrücken Sie <Enter> um zurückzukehren..."
            };

            do {
                Console.Clear();
                foreach (string stat in stats) {
                    Console.WriteLine(stat);
                }
            } while (Console.ReadKey(false).Key != ConsoleKey.Enter);
        }

        /// <summary>
        /// Increases Level and Exp (if possible), which is needed for next lvl<br />
        /// Reduces current Exp with the Exp need for this level
        /// </summary>
        public void IncreaseLvl() {
            // if lvl 100 is reached, no more leveling
            if (Lvl >= MAXLVL) {
                Lvl = MAXLVL;
                Exp = new uint[2] { 0, 0 };
            } else if (Exp[0] >= Exp[1]) { // allows multiple lvl ups
                Console.WriteLine("\n{0} ist ein Level aufgestiegen.\n{0} ist nun Level {1}.", Name, ++Lvl);
                Console.ReadKey(true);
                Exp[0] -= Exp[1];
                Exp[1] += (byte)(5 + Lvl);

                IncreaseStats();
                IncreaseLvl();
            }
        }

        /// <summary>
        /// Increases all stats by one (exept for class stat & Strength - increased by 2)<br />
        /// Strength is too strong, if its increased with every level
        /// Heal the Player to max HP
        /// </summary>
        private void IncreaseStats() {
            Intelligents++;
            Dexterity++;
            ChangeMaximumHealth(2);
            FullHeal();

            if (Lvl % 10 == 0) {
                CritChance += 0.3F;
                CritMult += 0.10F;
            }

            if (Lvl % 2 == 0 || Lvl % 3 == 0) Strength++;
            if (Lvl % 3 == 0) IncreaseClassStat();
        }

        /// <summary>
        /// Increases class specific stat by 1
        /// </summary>
        private void IncreaseClassStat() {
            switch (Class) {
                case 1: Strength++; break;
                case 2: Intelligents++; break;
                case 3: Dexterity++; break;
            }
        }

        // *************** saves *************** //

        /// <summary>
        /// creates the Player_save directory is it not exsists
        /// </summary>
        private static void CreateDirectory() {
            // create folder in current Directory
            string path = Directory.GetCurrentDirectory();
            string[] subDirectorys = Directory.GetDirectories(path);
            bool savegamesDirectory = false;

            foreach (string subDirectory in subDirectorys) {
                if (subDirectory == (path + @"\savegames")) {
                    savegamesDirectory = true;
                    break;
                }
            }

            // if savegame folder does not exist -> create it
            if (!savegamesDirectory) {
                Directory.CreateDirectory(path + @"\savegames\");            
            }
        }

        private static string GetSaveDirectory() {
            CreateDirectory();
            return Directory.GetCurrentDirectory() + @"\savegames\";
        }

        /// <summary>
        /// Saves Json File with current Player Stats<br />
        /// https://www.nuget.org/packages/System.Text.Json<br />
        /// </summary>
        /// <param name="p">current Player</param>
        public static void SavePlayer(Player p) {
            string path = GetSaveDirectory();  // current Path
            string json = JsonSerializer.Serialize(p);
            
            File.WriteAllText(path + p.Name + @".json", json);  // save in .json file
            Console.Clear();

            Thread.Sleep(600);
        }

        /// <summary>
        /// Deletes rageted json file
        /// </summary>
        /// <param name="name">Player / File name</param>
        public static void DeleteCharacer(string name) {
            string path = GetSaveDirectory();  // current Path
            File.Delete(path + name + ".json");
        }

        /// <summary>
        /// Checks if the new players Name is already existing<br />
        /// If its the case, than a new name must be choosen for the character
        /// </summary>
        /// <param name="playerName">name of current created character</param>
        /// <returns>true if name is doubled / false if name is unique</returns>
        public static bool IsDoubleName(string playerName) {
            List<Player> list = SaveList();
            Character[] players = list.ToArray();  // convert list to array

            for (byte i = 0; i < players.Length; i++) {
                if (playerName == players[i].Name) return true;
            }

            return false;
        }

        /// <summary>
        /// checsk whether there are any Player save files available to load<br />
        /// If there are same, they will return true
        /// </summary>
        /// <returns>true - if save files are available / false - if not</returns>
        public static bool HasPlayers() {
            // https://www.geeksforgeeks.org/c-sharp-program-for-listing-the-files-in-a-directory/
            string path = GetSaveDirectory();  // current Path
            DirectoryInfo PlayerSaves = new(path);
            FileInfo[] Files = PlayerSaves.GetFiles();

            // https://stackoverflow.com/questions/24518299/if-file-directory-is-empty-c-sharp
            if (Files.Length == 0) return false;
            return true;
        }

        /// <summary>
        /// gets all files from save directory, convert them to strings and fills them in a list
        /// </summary>
        /// <returns>list with all saves</returns>
        public static List<Player> SaveList() {
            // https://www.geeksforgeeks.org/c-sharp-program-for-listing-the-files-in-a-directory/
            string path = GetSaveDirectory();  // current Path
            DirectoryInfo PlayerSaves = new(path);
            FileInfo[] Files = PlayerSaves.GetFiles();
            List<Player> SavesList = new List<Player>();


            // fill Player list
            // https://www.tutorialsteacher.com/articles/convert-json-string-to-object-in-csharp
            foreach (FileInfo i in Files) { 
                string jsonPlayerData = File.ReadAllText(i.ToString());
                SavesList.Add(JsonSerializer.Deserialize<Player>(jsonPlayerData));
            }

            return SavesList;
        }

        /// <summary>
        /// Lists all available players (not more than 255) with Name, Class and Level
        /// </summary>
        /// <param name="players">list of / all players</param>
        private static void ShowPlayers(Player[] players) {
            Console.WriteLine("Wählen Sie einen Charakter aus:");

            for (byte i = 0; i < players.Length; i++) {
                if (i == 255) break;    // dont list more than 255 saves
                Console.WriteLine("{0}) {1}, {2} (Level: {3})",
                  i + 1, players[i].Name, players[i].GetClassName(), players[i].Lvl
                );
            }
        }

        /// <summary>
        /// Simple input field to tell which save should be loaded<br />
        /// User cannot enter higher num, than listed saves
        /// </summary>
        /// <param name="player">list of / all players</param>
        /// <returns>index of player in array</returns>
        private static byte ChoosePlayer(Player[] player) {
            byte input = 0;

            do {
                Console.Write("Eingabe: ");
            } while (!byte.TryParse(Console.ReadLine(), out input) || input >= player.Length + 1 || input == 0);

            return (byte)(input - 1);   // return array index
        }

        /// <summary>
        /// checks if save is correct and can be loaded
        /// </summary>
        /// <param name="player">save which should be loaded</param>
        /// <returns></returns>
        private static bool CanLoadPlayer(Player player) {
            if (PlayerValid(player)) return true;

            return false;
        }

        /// <summary>
        /// Checks if save can be loaded, if so it returns the save/player<br />
        /// </summary>
        /// <param name="playerId">player id/index in array</param>
        /// <param name="players">player array</param>
        /// <returns>player/save</returns>
        /// <exception cref="IndexOutOfRangeException">corrupt / edited savefile</exception>
        private static Player Prepare2Load(byte playerId, Player[] players) {
            if (CanLoadPlayer(players[playerId])) return LoadPlayer(players[playerId]);
            else {  // save file is edited
                string error = "Die geladene Charakterdatei ist korrput.";
                Console.WriteLine(error);
                Thread.Sleep(800);
                throw new IndexOutOfRangeException(error);
            }
        }

        /// <summary>
        /// Return the save
        /// </summary>
        /// <param name="player">loaded player/save</param>
        /// <returns>player/save</returns>
        private static Player LoadPlayer(Player player) {
            return player;
        }

        /// <summary>
        /// Gets an List of all saves and lets the user load or delete one
        /// </summary>
        /// <param name="delete">delete save - yes / no</param>
        /// <returns>savegame or error</returns>
        public static Player GetPlayers(bool delete) {
            List<Player> playersList = SaveList();
            byte choosenPlayerId = 0;

            // list all players
            Player[] players = playersList.ToArray(); // convert list to array
            ShowPlayers(players); // list all players

            choosenPlayerId = ChoosePlayer(players); // user input

            if(delete) {
                DeleteCharacer(players[choosenPlayerId].Name);
                return new Player(); // useless only for return value
            } else {
                return Prepare2Load(choosenPlayerId, players);
            }
        }

        /// <summary>
        /// Checks if the loaded save wasnt modified.<br />
        /// If class or name are modified, save cannot be loaded<br />
        /// </summary>
        /// <param name="c">characer object</param>
        /// <returns>true - if Player is valid / flase - if not</returns>
        private static bool PlayerValid(Player c) {
            bool nameValid = false, classValid = false;

            if (c.Name == "" || !InValidSign(c.Name)) nameValid = true;
            if (c.Class is 1 or 2 or 3) classValid = true;

            // overwrite invalid stats, if nessessary
            c.Strength = c.Strength;
            c.Intelligents = c.Intelligents;
            c.Dexterity = c.Dexterity;
            c.CritChance = c.CritChance;
            c.CritMult = c.CritMult;
            c.Health = c.Health;
            c.Gold = c.Gold;
            c.Exp = c.Exp;
            c.Lvl = c.Lvl;

            if (nameValid && classValid) return true;

            return false;
        }
    }
}
