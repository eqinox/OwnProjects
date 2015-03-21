using Pacman.Constants;
using System.Media;
namespace Pacman.sound
{
    static class MusicPlayer
    {
        public static SoundPlayer EatPill = new SoundPlayer(Constant.EatPillPath);
        public static SoundPlayer extralife = new SoundPlayer(Constant.ExtraLifePath);
        public static SoundPlayer ghosteat = new SoundPlayer(Constant.GhostEatPath);
        public static SoundPlayer killed = new SoundPlayer(Constant.KilledPath);
        public static SoundPlayer StartMusic = new SoundPlayer(Constant.StartMusicPath);
    }
}
