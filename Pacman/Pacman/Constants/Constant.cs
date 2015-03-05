using Pacman.ConsoleThings;
namespace Pacman.Constants
{
    static class Constant
    {
        // Team helps to recognize opponents, walls, scores and etc..
        public const int CharacterTeam = 1;
        public const int OpponentTeam = 2;
        public const int ScoreTeam = 3;
        public const int WallTeam = 4;

        public const string MapPath = "../../Maps/map1.txt";

        public const char WallSymbol = '*';
        public const char ScoreSymbol = '\'';
        public const char BonusSymbol = 'H';
        public const char BonusLifeSymbol = 'L';
        public const char SuperBonusScoreSymbol = 'B';
        public const char EmptySymbol = ' ';

        public const int NormalScoreValue = 2;
        public const int BonusScoreValue = 4;
        public const int SuperBonusScoreValue = 50;
        public const int OpponentScore = 100;

        public const int AdditionalColumns = 1; // 1 because thats the way console works.
        public const int AdditionalRows = 2; // if you put 1 more Console.WriteLine() in engine you should icreate the variable with 1 more


        public const int OpponentRowStartPosition = 16;
        public const int OpponentColStartPosition = 29;
        public const int PacmanRowStartPosition = 21;
        public const int PacmanColStartPosition = 29;

        public const string ExitCommand = "N";
        public const string RestartCommand = "Y";

        public const int CharacterDefaultLives = 3;
        public const int EnoughScoreForeBonusLife = 800;

        public const string EatPillPath = @"..\..\sound\EatPill.wav";
        public const string ExtraLifePath = @"..\..\sound\extralife.wav";
        public const string GhostEatPath = @"..\..\sound\ghosteat.wav";
        public const string StartMusicPath = @"..\..\sound\StartMusic.wav";
        public const string KilledPath = @"..\..\sound\killed.wav";
    }
}
