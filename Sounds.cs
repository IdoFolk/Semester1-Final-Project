using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDungeonCrawler
{
    class Sounds
    {
        public SoundPlayer HitSFX;
        public SoundPlayer MenuMusic;
        //public static SoundPlayer ChestSFX = new SoundPlayer("Sounds\\Chest_SFX.wav");
        //public static SoundPlayer CoinSFX = new SoundPlayer("Sounds\\Coin_SFX.wav");
        //public static SoundPlayer MissSFX = new SoundPlayer("Sounds\\Miss_SFX.wav");
        //public static SoundPlayer DoorSFX = new SoundPlayer("Sounds\\Door_SFX.wav");
        //public static SoundPlayer KeySFX = new SoundPlayer("Sounds\\Key_SFX.wav");
        //public static SoundPlayer ExitSFX = new SoundPlayer("Sounds\\Exit_SFX.wav");
        //public static SoundPlayer DyingSFX = new SoundPlayer("Sounds\\Dying_SFX.wav");
        //public static SoundPlayer DeadSFX = new SoundPlayer("Sounds\\Dead_SFX.wav");
        public Sounds()
        {
            HitSFX = new SoundPlayer("Sounds\\Hit_SFX.wav");
            MenuMusic = new SoundPlayer("Sounds\\Menu_Music.wav");
        }
    }
}
