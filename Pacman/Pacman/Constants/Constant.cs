namespace Pacman.Constants
{
    static class Constant
    {
        // Team helps to recognize opponents, walls, scores and etc..
        public const int CharacterTeam = 1;
        public const int OpponentTeam = 2;
        public const int ScoreTeam = 3;
        public const int WallTeam = 4;
        public const int BonusTeam = 5;

        public const string MapPath = "../../Maps/map1.txt";

        public const char WallSymbol = '*';
        public const char ScoreSymbol = '\'';
        public const char BonusSymbol = 'H';
        public const char EmptySymbol = ' ';

        public const int NormalScoreValue = 1;
        public const int BonusScoreValue = 2;

        public const int AdditionalColumns = 1; // 1 because thats the way console works.
        public const int AdditionalRows = 2; // if you put 1 more Console.WriteLine() in engine you should icreate the variable with 1 more
    }
}
