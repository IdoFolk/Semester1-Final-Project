using ConsoleDungeonCrawler.Character;
using ConsoleDungeonCrawler.Level_Elements;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDungeonCrawler.Printer
{
    static class HUD
    {
        private static ConsoleColor DefaultForeground = ConsoleColor.Gray;
        private static ConsoleColor DefaultBackground = ConsoleColor.Black;
        private static bool _skipCutscene;
        private static Queue<string> _logs = new Queue<string>();
        private static Queue<ConsoleColor> _logColors = new Queue<ConsoleColor>();
        public static void GameState(Level level, Player player)
        {
            Map.PrintMap(level, player);
            LevelStats(level);
            PlayerStats(player);
            PrintInventory(player, level);
            PrintLog();
        }
        public static void GameStatePaused(Level level, Player player)
        {
            LevelStats(level);
            PlayerStats(player);
            PrintInventory(player, level);
        }
        private static void PrintLog()
        {
            int i = 0;
            foreach (string logText in _logs)
            {
                int c = 0;
                foreach (var color in _logColors)
                {
                    if (i == c) Console.ForegroundColor = color;
                    c++;
                }
                Console.SetCursorPosition(UI.LogBox.PosX + 1, UI.LogBox.PosY + 3 + i);
                Console.WriteLine(logText);
                i++;
            }
            Console.ForegroundColor = DefaultForeground;
            i = 0;
            foreach (string logText in _logs)
            {
                int posX = UI.LogBox.PosX + 1 + logText.Length;
                int logBoxEnd = UI.LogBox.PosX + UI.LogBox.Width;
                Console.SetCursorPosition(posX, UI.LogBox.PosY + 3 + i);
                for (int j = 0; j < logBoxEnd - posX; j++)
                {
                    Console.Write(" ");
                }
                i++;
            }
        }
        public static void Log(string logText, ConsoleColor color)
        {
            _logs.Enqueue(logText);
            _logColors.Enqueue(color);
            if (_logs.Count > 29)
                _logs.Dequeue();
            if (_logColors.Count > 29)
                _logColors.Dequeue();
        }
        public static void Log()
        {
            _logs.Enqueue("");
            _logColors.Enqueue(ConsoleColor.White);
            if (_logs.Count > 29)
                _logs.Dequeue();
            if (_logColors.Count > 29)
                _logColors.Dequeue();
        }
        public static void LogReset()
        {
            _logs.Clear();
            _logColors.Clear();
        }
        public static void CombatLog(Player player, Enemy enemy, int attack)
        {
            if (attack == 0)
            {
                Log($"{player.Name} Misses...",DefaultForeground);
                return;

            }
            Log($"{player.Name} attacks {enemy.Name} for {attack} damage.", ConsoleColor.Red);
        }
        public static void CombatLog(Enemy enemy, Player player, int attack)
        {
            if (attack == 0)
            {
                Log($"{enemy.Name} Misses...", DefaultForeground);
                Log();
                return;
            }
            Log($"{enemy.Name} attacks {player.Name} for {attack} damage.", ConsoleColor.Red);
            Log();
        }
        public static void CombatLog(Player player, Boss boss, int attack)
        {
            if (attack == 0)
            {
                Log($"{player.Name} Misses...", DefaultForeground);
                return;

            }
            Log($"{player.Name} attacks {boss.Name} for {attack} damage.", ConsoleColor.Red);
        }
        public static void CombatLog(Boss boss, Player player, int attack)
        {
            if (attack == 0)
            {
                Log($"{boss.Name} Misses...", DefaultForeground);
                Log();
                return;
            }
            Log($"{boss.Name} attacks {player.Name} for {attack} damage.", ConsoleColor.Red);
            Log();
        }
        public static void OpenDoorLog(Door door)
        {
            Log("You have opened a door", door.Color);
            Log();
        }
        public static void GotKeyLog(Key key)
        {
            Log("You got a key", key.Color);
            Log();
        }
        public static bool BuyKey(Key key)
        {
            int indicator = 0;
            bool optionChoosen = false;
            Console.SetCursorPosition(47 + UI.StartingPosX, PrintMenu.ButtonPosY +3);
            Console.Write($"Key price is {key.Price}");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"♣");
            Console.ForegroundColor = DefaultForeground;
            Console.SetCursorPosition(42 + UI.StartingPosX, PrintMenu.ButtonPosY +4);
            Console.Write("Do you want to buy the key?");
            while (!optionChoosen)
            {
                Console.CursorVisible = false;
                BuyingOptions(indicator);
                ConsoleKey input = Console.ReadKey(true).Key;
                switch (input)
                {
                    case ConsoleKey.LeftArrow:
                        indicator--;
                        break;
                    case ConsoleKey.RightArrow:
                        indicator++;
                        break;
                    case ConsoleKey.Enter:
                        switch (indicator)
                        {
                            case 0:
                                return true;
                            case 1:
                                return false;
                        }
                        break;
                }
                if (indicator < 0) indicator = 0;
                if (indicator > 1) indicator = 1;
            }
            return false;
        }
        public static void BuyingOptions(int indicator)
        {
            IfSelected(0, indicator);
            Console.SetCursorPosition(52 + UI.StartingPosX, PrintMenu.ButtonPosY +5);
            Console.Write("YES");
            IfSelected(1, indicator);
            Console.SetCursorPosition(57 + UI.StartingPosX, PrintMenu.ButtonPosY +5);
            Console.Write("NO");
            Console.BackgroundColor = DefaultBackground;
            Console.ForegroundColor = DefaultForeground;
        }
        private static void IfSelected(int pos, int indicator)
        {
            if (indicator == pos)
            {
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Black;
            }
            else
            {
                Console.BackgroundColor = DefaultBackground;
                Console.ForegroundColor = DefaultForeground;
            }
        }
        public static void NotEnoughCoinsLog()
        {
            Log("You Dont have enough coins...", DefaultForeground);
            Log();
        }
        public static void SteppedOnTrapLog(Trap trap)
        {
            Log($"{Game.PlayersName} stepped on a piece of art and", ConsoleColor.Red);
            Log($"received {trap.Damage} damage", ConsoleColor.Red);
            Log();
        }
        public static void GotCoinLog(int amount)
        {
            Log($"You got {amount} Clover!", ConsoleColor.Green);
            Log();
        }
        public static void GotScriptLog()
        {
            Log($"You got a C# Script!", ConsoleColor.Cyan);
            Log();
        }
        public static void AlreadyGotScriptLog()
        {
            Log($"You already have a C# Script ...", ConsoleColor.Cyan);
            Log();
        }
        public static void OpenChestLog()
        {
            Log("You have opened a bag and got:", DefaultForeground);
        }
        public static void GotWeaponLog(Weapon weapon)
        {
            Log($"A {weapon.Name}!", DefaultForeground);
            Log();
        }
        public static void GotPotionLog(Potion potion)
        {
            if (potion.Name == "Water")
                Log($"A bottle of {potion.Name}!", potion.Color);
            else Log($"A {potion.Name}!", potion.Color);
            Log();
        }
        public static void GotArmorLog(Armor armor)
        {
            Log($"A {armor.Name}!", DefaultForeground);
            Log();
        }
        public static void WeaponBreakLog(Weapon weapon)
        {
            Log($"{weapon.Name} broke...", DefaultForeground);
            Log();
        }
        public static void UsePotionLog(Potion potion)
        {
            switch (potion.Name)
            {
                case "Water":
                    Log("You drank some water and healed for", potion.Color);
                    break;
                case "Snack":
                    Log("You ate a snack and healed for", potion.Color);
                    break;
                case "Sandwich":
                    Log("You ate a sandwich and healed for", potion.Color);
                    break;
            }
            Log($"{potion.Heal} Hearts", potion.Color);
            Log();
        }
        public static void DropWeaponLog(Weapon weapon)
        {
            Log($"{weapon.Name} Dropped...", DefaultForeground);
            Log();
        }
        public static void ArmorBreakLog(Armor armor)
        {
            if (armor.Name == "Plate Armor") Log($"{armor.Name} Broke...", DefaultForeground);
            else Log($"{armor.Name} Ripped...", DefaultForeground);
            Log();
        }
        public static void DropArmorLog(Armor armor)
        {
            Log($"{armor.Name} Dropped...", DefaultForeground);
            Log();
        }
        public static void DropPotionLog(Potion potion)
        {
            Log($"{potion.Name} Potion Dropped...", potion.Color);
            Log();
        }
        public static void ItemCappedLog(ItemType itemType)
        {
            switch (itemType)
            {
                case ItemType.Weapon:
                    Log("Weapon Limit Reached...", DefaultForeground);
                    break;
                case ItemType.Potion:
                    Log("Potion Limit Reached...", DefaultForeground);
                    break;
                case ItemType.Armor:
                    Log("Armor Limit Reached...", DefaultForeground);
                    break;
            }
            Log();
        }
        public static void ChangeCodeLog(Player player)
        {
            if (player.HasScript)
            {

                Log("Success! You've changed the code!", ConsoleColor.DarkCyan);
                Log("Dor's HP is now 10", ConsoleColor.DarkCyan);

            }
            else
            {
                Log("You dont have the necessary Script", ConsoleColor.DarkCyan);
                Log("in order to change the code", ConsoleColor.DarkCyan);
            }
            Log();
        }
        public static void LevelStats(Level level)
        {
            Console.SetCursorPosition(UI.LevelBox.PosX + 1, UI.LevelBox.PosY + 3);
            Console.WriteLine($"Level: {level.Name}");
            Console.SetCursorPosition(UI.LevelBox.PosX + 1, UI.LevelBox.PosY + 4);
            Console.WriteLine($"Students Killed: {level.EnemiesKilled} / {level.EnemiesAmount}");
            Console.SetCursorPosition(UI.LevelBox.PosX + 1, UI.LevelBox.PosY + 5);
            Console.WriteLine($"Bags Opened: {level.ChestsOpened} / {level.ChestAmount}");
        }
        public static void PlayerStats(Player player)
        {
            //HP
            Console.SetCursorPosition(UI.PlayerStatBox.PosX + 1, UI.PlayerStatBox.PosY + 3);
            Console.Write("HP: ");
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write("♥♥♥♥♥♥♥♥♥♥");
            Console.SetCursorPosition(UI.PlayerStatBox.PosX + 5, UI.PlayerStatBox.PosY + 3);
            Console.ForegroundColor = ConsoleColor.Red;
            for (int i = 0; i < player.CurrentHP; i++)
            {
                Console.Write('♥');
            }
            Console.ForegroundColor = DefaultForeground;
            //Shield
            Console.SetCursorPosition(UI.PlayerStatBox.PosX + 1, UI.PlayerStatBox.PosY + 4);
            Console.Write($"Shield: ");
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write("♦♦♦♦♦");
            Console.SetCursorPosition(UI.PlayerStatBox.PosX + 8, UI.PlayerStatBox.PosY + 4);
            Console.ForegroundColor = ConsoleColor.Blue;
            for (int i = 0; i < player.EquippedArmor.ArmorPoints; i++)
            {
                Console.Write('♦');
            }
            Console.ForegroundColor = DefaultForeground;

            for (int i = 0; i < 5; i++)
            {
                Console.SetCursorPosition(UI.PlayerStatBox.PosX + 13, UI.PlayerStatBox.PosY + 5 + i);
                Console.Write("|");
            }
            for (int i = 0; i < UI.PlayerStatBox.Width-1; i++)
            {
                Console.SetCursorPosition(UI.PlayerStatBox.PosX + 1 + i, UI.PlayerStatBox.PosY + 5);
                Console.Write("─");
            }
            Console.SetCursorPosition(UI.PlayerStatBox.PosX + 13, UI.PlayerStatBox.PosY + 5);
            Console.Write("┬");
            Console.SetCursorPosition(UI.PlayerStatBox.PosX + 13, UI.PlayerStatBox.PosY + 10);
            Console.Write("┴");

            Console.SetCursorPosition(UI.PlayerStatBox.PosX + 4, UI.PlayerStatBox.PosY + 5);
            Console.Write("Weapon");

            Console.SetCursorPosition(UI.PlayerStatBox.PosX + 17, UI.PlayerStatBox.PosY + 5);
            Console.Write("Armor");
            
            Console.SetCursorPosition(UI.PlayerStatBox.PosX + 4, UI.PlayerStatBox.PosY + 6);
            Console.Write($"{player.EquippedWeapon.Name}");

            Console.SetCursorPosition(UI.PlayerStatBox.PosX + 1, UI.PlayerStatBox.PosY + 7);
            Console.Write($"Damage: {player.EquippedWeapon.Damage}");

            Console.SetCursorPosition(UI.PlayerStatBox.PosX + 1, UI.PlayerStatBox.PosY + 8);
            Console.Write($"Hit: {(player.EquippedWeapon.HitChance * 10) * 10}%");

            Console.SetCursorPosition(UI.PlayerStatBox.PosX + 1, UI.PlayerStatBox.PosY + 9);
            Console.Write($"Status: {player.EquippedWeapon.Durability * 20}%");

            Console.SetCursorPosition(UI.PlayerStatBox.PosX + 15, UI.PlayerStatBox.PosY + 6);
            Console.Write($"{player.EquippedArmor.Name}");
            
            Console.SetCursorPosition(UI.PlayerStatBox.PosX + 14, UI.PlayerStatBox.PosY + 8);
            Console.Write($"Evasion: {(player.EquippedArmor.Evasion*10)*10}%");

            Console.SetCursorPosition(UI.PlayerStatBox.PosX + 14, UI.PlayerStatBox.PosY + 9);
            Console.Write($"Status: {player.EquippedArmor.ArmorPoints * 20}%");


        }
        public static void EnemyStats(Enemy enemy)
        {


            Console.SetCursorPosition(UI.EnemyStatBox.PosX + 1, UI.EnemyStatBox.PosY + 3);
            Console.Write($"{enemy.Name} HP: ");
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write("♥♥♥♥♥♥♥♥♥♥");
            Console.SetCursorPosition(UI.EnemyStatBox.PosX + 5 + enemy.Name.Length, UI.EnemyStatBox.PosY + 3);
            Console.ForegroundColor = ConsoleColor.Red;
            for (int i = 0; i < enemy.CurrentHP; i++)
            {
                Console.Write('♥');
            }
            Console.ForegroundColor = DefaultForeground;

        }
        public static void EnemyStats(Boss boss)
        {


            Console.SetCursorPosition(UI.EnemyStatBox.PosX + 1, UI.EnemyStatBox.PosY + 3);
            Console.Write($"{boss.Name} HP: ");
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write("♥♥♥♥♥♥♥♥♥♥");
            Console.SetCursorPosition(UI.EnemyStatBox.PosX + boss.Name.Length + 5, UI.EnemyStatBox.PosY + 3);
            Console.ForegroundColor = ConsoleColor.Red;
            
            if (boss.CurrentHP > 10)
            {
                Console.Write("♥");
                Console.ForegroundColor = DefaultForeground;
                Console.Write($"x{ boss.CurrentHP}"); 
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                for (int i = 0; i < boss.CurrentHP; i++)
                {
                    Console.Write("♥");
                }
            } 
            Console.ForegroundColor = DefaultForeground;
        }
        public static void PrintInventory(Player player, Level level)
        {
            InventoryMenu();
            OwnedWeapons(player);
            OwnedPotions(player);
            OwnedArmor(player);
            OwnedKeys(level);
            OwnedCoins(player);
            OwnedScript(player);
        }
        public static void InventoryMenu()
        {
            Console.SetCursorPosition(UI.InventoryBox.PosX + 1, UI.InventoryBox.PosY + 1);
            Console.Write("Inventory:                  ");
            Console.SetCursorPosition(UI.InventoryBox.PosX + 1, UI.InventoryBox.PosY + 3);
            Console.Write("WEAPONS:");
            Console.SetCursorPosition(UI.InventoryBox.PosX + 1, UI.InventoryBox.PosY + 8);
            Console.Write("POTIONS:");
            Console.SetCursorPosition(UI.InventoryBox.PosX + 1, UI.InventoryBox.PosY + 11);
            Console.Write("ARMOR:");
        }
        public static void OwnedWeapons(Player player)
        {
            int w = 1;
            for (int j = 0; j < 15; j+=14)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (w > player.PlayerWeapons.Count-1) return;
                    Weapon weapon = player.PlayerWeapons[w];
                    if (weapon.IsEquipped)
                    {
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }
                    Console.SetCursorPosition(UI.InventoryBox.PosX + 1 + j, UI.InventoryBox.PosY + 4 + i);
                    Console.Write($"{w}. {weapon.Name}");
                    Console.BackgroundColor = DefaultBackground;
                    Console.ForegroundColor = DefaultForeground;
                    w++;
                }
            }
        }
        public static void OwnedPotions(Player player)
        {
            Console.SetCursorPosition(UI.InventoryBox.PosX + 1, UI.InventoryBox.PosY + 9);
            Console.WriteLine("[                          ]");
            int i = 4;
            foreach (Potion potion in player.PlayerPotions)
            {
                Console.SetCursorPosition(UI.InventoryBox.PosX + i, UI.InventoryBox.PosY + 9);
                Console.ForegroundColor = potion.Color;
                Console.Write("♠");
                Console.ForegroundColor = DefaultForeground;
                i += 3;
            }
        }
        public static void OwnedArmor(Player player)
        {
            for (int i = 1; i < player.PlayerArmors.Count; i++)
            {
                Armor armor = player.PlayerArmors[i];
                if (armor.IsEquipped)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                Console.SetCursorPosition(UI.InventoryBox.PosX + 1, UI.InventoryBox.PosY + 11 + i);
                Console.Write($"{i}. {armor.Name}");
                Console.BackgroundColor = DefaultBackground;
                Console.ForegroundColor = DefaultForeground;
            }
        }
        public static void OwnedKeys(Level level)
        {
            Console.SetCursorPosition(UI.InventoryBox.PosX+1, UI.InventoryBox.PosY+16);
            Console.Write("KEYS:");
            Console.SetCursorPosition(UI.InventoryBox.PosX+1, UI.InventoryBox.PosY+17);
            Console.WriteLine("[                          ]");
            int j = 4;  
            foreach (Key key in level.PlayerKeys)
            {
                Console.SetCursorPosition(UI.InventoryBox.PosX + j, UI.InventoryBox.PosY + 17);
                if (key.Used)
                {
                    Console.ForegroundColor = key.UsedColor;
                    Console.Write("¶");
                    Console.BackgroundColor = DefaultBackground;
                }
                else
                {
                    Console.ForegroundColor = key.Color;
                    Console.Write("¶");
                }
                j += 3;
            }
            
            Console.BackgroundColor = DefaultBackground;
            Console.ForegroundColor = DefaultForeground;
        }
        public static void OwnedCoins(Player player)
        {
            Console.SetCursorPosition(UI.InventoryBox.PosX + 1, UI.InventoryBox.PosY + 19);
            Console.Write("CLOVERS:");
            Console.SetCursorPosition(UI.InventoryBox.PosX + 1, UI.InventoryBox.PosY + 20);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("♣");
            Console.ForegroundColor = DefaultForeground;
            Console.Write($"x{player.Coins}");
        }
        public static void OwnedScript(Player player)
        {
            if (player.HasScript)
            {
                Console.SetCursorPosition(UI.InventoryBox.PosX + 19, UI.InventoryBox.PosY + 20);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("C# Script");
                Console.ForegroundColor = DefaultForeground;
            }
            else
            {
                Console.SetCursorPosition(UI.InventoryBox.PosX + 19, UI.InventoryBox.PosY + 20);
                Console.Write("         ");
            }
        }
        public static void ResetCoins()
        {
            Console.SetCursorPosition(UI.InventoryBox.PosX + 1, UI.InventoryBox.PosY + 20);
            Console.Write("        ");
        }
        public static void InventoryNav()
        {
            Console.SetCursorPosition(UI.InventoryBox.PosX + 20, UI.InventoryBox.PosY + 1);
            Console.Write("Opened");
            Console.SetCursorPosition(UI.InventoryBox.PosX + 1, UI.InventoryBox.PosY + 3);
            Console.Write("WEAPONS:");
            Console.SetCursorPosition(UI.InventoryBox.PosX + 1, UI.InventoryBox.PosY + 8);
            Console.Write("POTIONS:");
            Console.SetCursorPosition(UI.InventoryBox.PosX + 1, UI.InventoryBox.PosY + 11);
            Console.Write("ARMOR:");
            switch (Inventory.MenuIndicator)
            {
                case 0:
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.SetCursorPosition(UI.InventoryBox.PosX + 1, UI.InventoryBox.PosY + 3);
                    Console.Write("WEAPONS:"); 
                    Console.BackgroundColor = DefaultBackground;
                    Console.ForegroundColor = DefaultForeground;
                    break;
                case 1:
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.SetCursorPosition(UI.InventoryBox.PosX + 1, UI.InventoryBox.PosY + 8);
                    Console.Write("POTIONS:");
                    Console.BackgroundColor = DefaultBackground;
                    Console.ForegroundColor = DefaultForeground;
                    break;
                case 2:
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.SetCursorPosition(UI.InventoryBox.PosX + 1, UI.InventoryBox.PosY + 11);
                    Console.Write("ARMOR:");
                    Console.BackgroundColor = DefaultBackground;
                    Console.ForegroundColor = DefaultForeground;
                    break;
            }
        }
        public static void WeaponNav(Player player)
        {
            int w = 1;
            for (int j = 0; j < 15; j += 14)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (w > player.PlayerWeapons.Count - 1) return;
                    Weapon weapon = player.PlayerWeapons[w];
                    if (Inventory.SubMenuIndicator == w)
                    {
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }
                    Console.SetCursorPosition(UI.InventoryBox.PosX + 1 + j, UI.InventoryBox.PosY + 4 + i);
                    Console.Write($"{w}. {weapon.Name}");
                    Console.BackgroundColor = DefaultBackground;
                    Console.ForegroundColor = DefaultForeground;
                    w++;
                }
            }
        }
        public static void PotionNav(Player player)
        {
            int i = 0;
            int j = 4;
            foreach (Potion potion in player.PlayerPotions)
            {
                if (Inventory.SubMenuIndicator == i)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = potion.Color;
                }
                Console.SetCursorPosition(UI.InventoryBox.PosX + j, UI.InventoryBox.PosY + 9);
                Console.ForegroundColor = potion.Color;
                Console.Write("♠");
                Console.BackgroundColor = DefaultBackground;
                i ++;
                j += 3;
            }
                Console.ForegroundColor = DefaultForeground;
        }
        public static void ArmorNav(Player player)
        {
            for (int i = 1; i < player.PlayerArmors.Count; i++)
            {
                Armor armor = player.PlayerArmors[i];
                if (Inventory.SubMenuIndicator == i)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                Console.SetCursorPosition(UI.InventoryBox.PosX + 1, UI.InventoryBox.PosY + 11 + i);
                Console.Write($"{i}. {armor.Name}");
                Console.BackgroundColor = DefaultBackground;
                Console.ForegroundColor = DefaultForeground;
            }
        }
        public static void ClearInventory()
        {
            //Inventory Weapons Clear
            HUD.Clear(UI.InventoryBox.PosX + 1, UI.InventoryBox.PosY + 4, UI.InventoryBox.Width - 1, 3);
            //Inventory Armor Clear
            HUD.Clear(UI.InventoryBox.PosX + 1, UI.InventoryBox.PosY + 12, UI.InventoryBox.Width - 1, 3);
        }
        public static void ClearPlayerStats()
        {
            HUD.Clear(UI.PlayerStatBox.PosX + 1, UI.PlayerStatBox.PosY + 5, UI.PlayerStatBox.Width - 1, 5);
        }
        public static void Clear(int posX, int posY, int width, int height)
        {
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    Console.SetCursorPosition(posX + j, posY + i);
                    Console.Write(" ");
                }
            }
        }
        public static void StartingCutscene()
        {
            Console.Clear();
            _skipCutscene = false;
            bool starting = true;
            CutsceneDialog("Once Upon A Time...", starting,0);
            CutsceneDialog("Tiltan Hosted Her Annual Global GameJam", starting,-10);
            CutsceneDialog($"{Game.PlayersName} Was So Exited To Finally Participate", starting,-12);
            if (Game.PlayerIsMale)
                CutsceneDialog("That He Could Not Sleep All Night", starting,-7);
            else
                CutsceneDialog("That She Could Not Sleep All Night", starting,-7);
            CutsceneDialog("And When The GameJam Finally Started", starting,-9);
            CutsceneDialog($"{Game.PlayersName} Fell Asleep After 2 Hours of Work", starting,-10);
            CutsceneDialog("Only Then...", starting,4);
            if (Game.PlayerIsMale)
                CutsceneDialog("He Woke Up Into His Worst Nightmare", starting,-8);
            else
                CutsceneDialog("She Woke Up Into Her Worst Nightmare", starting,-8);
            CutsceneDialog("All of The Students Turned Into Zombies!", starting,-10);
            CutsceneDialog("You Have To Escape The College NOW!", starting,-9);
        }
        public static void BossCutscene(Boss dor)
        {
            _skipCutscene = false;
            bool ending = false;
            Console.SetCursorPosition(UI.MapBox.PosX + 26, UI.MapBox.PosY + 17);
            Map.Enemy(dor);
            Console.SetCursorPosition(50 + UI.StartingPosX, PrintMenu.ButtonPosY - 2);
            Console.Write("Dor Ben Dor:");
            CutsceneDialog("Ha Ha Ha", ending, 2);
            CutsceneDialog("You Fool!", ending, 1);
            CutsceneDialog("You Can't Escape Me!", ending, -4);
            
        }
        public static void BossDefeatedCutscene()
        {
            _skipCutscene = false;
            bool ending = false;
            Console.SetCursorPosition(50 + UI.StartingPosX, PrintMenu.ButtonPosY - 2);
            Console.Write("Dor Ben Dor:");
            CutsceneDialog("Ahhhhhh!", ending,2);
            CutsceneDialog("You Defeated Me!", ending, -2);
            CutsceneDialog("I Guess...", ending,2);
            CutsceneDialog("You Are a Better Programmer Than i Thought", ending, -14);
            CutsceneDialog("I Will Give You an A- for the Effort...", ending, -12);

        }
        private static void CutsceneDialog(string text, bool starting, int posX)
        {
            if (_skipCutscene) return;
            if (starting) Console.SetCursorPosition(47 + posX + UI.StartingPosX, PrintMenu.ButtonPosY - 1);
            else Console.SetCursorPosition(50 + posX + UI.StartingPosX, PrintMenu.ButtonPosY - 1);
            Console.Write(text);
            if (SkipCutsceneDialog())
            {
                if (starting) Console.SetCursorPosition(47 + posX + UI.StartingPosX, PrintMenu.ButtonPosY - 1);
                else Console.SetCursorPosition(50 + posX + UI.StartingPosX, PrintMenu.ButtonPosY - 1);
                for (int i = 0; i < text.Length; i++)
                {
                    Console.Write(" ");
                }
                return;
            }
        }
        private static bool SkipCutsceneDialog()
        {
            while (!_skipCutscene)
            {
                ConsoleKey key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.Enter) return true;
                else if (key == ConsoleKey.Escape) _skipCutscene = true;
                Console.SetCursorPosition(47 + UI.StartingPosX, PrintMenu.ButtonPosY + 5);
                Console.Write("Press Enter To Skip");
            }
            return true;
        }

    }
}
