using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDungeonCrawler
{
    static class Sounds
    {
        public static SoundPlayer HitSFX = new SoundPlayer("Sounds\\Hit_SFX.wav");
        public static SoundPlayer MissSFX = new SoundPlayer("Sounds\\Miss_SFX.wav");
        public static SoundPlayer DyingSFX = new SoundPlayer("Sounds\\Dying_SFX.wav");
        public static SoundPlayer BossDyingSFX = new SoundPlayer("Sounds\\BossDying_SFX.wav");
        public static SoundPlayer DeadSFX = new SoundPlayer("Sounds\\Dead_SFX.wav");
        public static SoundPlayer WinSFX = new SoundPlayer("Sounds\\Win_SFX.wav");
        public static SoundPlayer EnemyDieSFX = new SoundPlayer("Sounds\\EnemyDie_SFX.wav");
        public static SoundPlayer EnterSFX = new SoundPlayer("Sounds\\Enter_SFX.wav");
        public static SoundPlayer CoinSFX = new SoundPlayer("Sounds\\Coin_SFX.wav");
        public static SoundPlayer ChestSFX = new SoundPlayer("Sounds\\Chest_SFX.wav");
        public static SoundPlayer DoorSFX = new SoundPlayer("Sounds\\Door_SFX.wav");
        public static SoundPlayer KeySFX = new SoundPlayer("Sounds\\Key_SFX.wav");
        public static SoundPlayer PotionSFX = new SoundPlayer("Sounds\\Potion_SFX.wav");
        public static SoundPlayer ScriptSFX = new SoundPlayer("Sounds\\Script_SFX.wav");
        public static SoundPlayer EquipSFX = new SoundPlayer("Sounds\\Equip_SFX.wav");
        public static SoundPlayer WeaponBreakSFX = new SoundPlayer("Sounds\\WeaponBreak_SFX.wav");
        public static SoundPlayer ShirtTearSFX = new SoundPlayer("Sounds\\ShirtTear_SFX.wav");
        public static SoundPlayer OpenInventorySFX = new SoundPlayer("Sounds\\OpenInventory_SFX.wav");
        public static SoundPlayer CloseInventorySFX = new SoundPlayer("Sounds\\CloseInventory_SFX.wav");
        public static SoundPlayer BuySFX = new SoundPlayer("Sounds\\Buy_SFX.wav");
        public static SoundPlayer EvilLaughSFX = new SoundPlayer("Sounds\\EvilLaugh_SFX.wav");
        public static SoundPlayer MenuNav1SFX = new SoundPlayer("Sounds\\MenuNav1_SFX.wav");
        public static SoundPlayer MenuNav2SFX = new SoundPlayer("Sounds\\MenuNav2_SFX.wav");
        public static SoundPlayer MenuNav3SFX = new SoundPlayer("Sounds\\MenuNav3_SFX.wav");
        public static SoundPlayer MenuMusic = new SoundPlayer("Sounds\\Menu_Music.wav");
        public static SoundPlayer IntroMusic = new SoundPlayer("Sounds\\Intro_Music.wav");
        public static SoundPlayer ClimaxMusic = new SoundPlayer("Sounds\\Climax_Music.wav");

        private static bool _levelMusic = false;
        public static void PlaySFX(SoundPlayer sound)
        {
            if (Game.SFXon && !_levelMusic)
            {
                sound.Play();
            }
        }
        public static void StopSFX(SoundPlayer sound)
        {
            if (Game.SFXon)
            {
                sound.Stop();
            }
        }
        public static void PlayMusic(SoundPlayer sound)
        {
            if (Game.Musicon)
            {
                sound.PlayLooping();
            }
        }
        public static void PlayLevelMusic(SoundPlayer sound)
        {
            if (Game.Musicon)
            {
                _levelMusic = true;
                sound.PlayLooping();
            }
        }
        public static void StopMusic(SoundPlayer sound)
        {
            if (Game.Musicon)
            {
                sound.Stop();
            }
        }
        public static void StopLevelMusic(SoundPlayer sound)
        {
            if (Game.Musicon)
            {
                _levelMusic = false;
                sound.Stop();
            }
        }
        public static void StopAllMusic()
        {
            MenuMusic.Stop();
            IntroMusic.Stop();
        }
        
    }
}
