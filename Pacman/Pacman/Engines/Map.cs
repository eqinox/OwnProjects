namespace Pacman.Engines
{
    using Pacman.GameObjects;
    using System.Collections.Generic;
    using System.IO;
    using Pacman.Constants;
    using Pacman.ConsoleThings;
    using Pacman.GameObjects.Scores;

    class Map
    {
        public int AllRows;
        public int AllColls;
        private List<GameObject> allObjects;
        private List<Wall> allWalls;
        private List<Score> allScore;
        private List<BonusScore> allBonus;

        public Map(string path)
        {
            this.allObjects = new List<GameObject>();
            this.allScore = new List<Score>();
            this.allWalls = new List<Wall>();
            this.allBonus = new List<BonusScore>();
            ParseMap(path);
        }

        private void ParseMap(string path)
        {
            StreamReader reader = new StreamReader(path);

            using (reader)
            {
                string currentLine = reader.ReadLine();
                int row = 0;

                while (currentLine != null)
                {
                    FillAllObjects(row, currentLine);

                    InitializeRowsAndCols(currentLine);

                    row++;
                    currentLine = reader.ReadLine();
                }

            }
        }

        private void InitializeRowsAndCols(string currentLine)
        {
            if (AllColls < currentLine.Trim().Length)
            {
                AllColls = currentLine.Trim().Length;
            }
            AllRows++;
        }

        private void FillAllObjects(int row, string currentLine)
        {
            for (int col = 0; col < currentLine.Length; col++)
            {
                char symbol = currentLine[col];
                switch (symbol)
                {
                    case Constant.WallSymbol:
                        Wall newWall = new Wall(Constant.WallSymbol, new MatrixCoords(row, col));
                        this.allObjects.Add(newWall);
                        this.allWalls.Add(newWall as Wall);
                        break;
                    case Constant.ScoreSymbol:
                        Score newScore = new NormalScore(Constant.ScoreSymbol, new MatrixCoords(row, col));
                        this.allObjects.Add(newScore);
                        this.allScore.Add(newScore as Score);
                        break;
                    case Constant.BonusSymbol:
                        BonusScore newBonus = new BonusScore(Constant.BonusSymbol, new MatrixCoords(row, col));
                        this.allObjects.Add(newBonus);
                        this.allBonus.Add(newBonus as BonusScore);
                        break;
                    default:
                        break;
                }
            }
        }

        public List<GameObject> GiveMeMap()
        {
            return this.allObjects;
        }

        public List<Wall> GiveMeAllWalls()
        {
            return this.allWalls;
        }

        public List<Score> GiveMeAllScore()
        {
            return this.allScore;
        }

        public List<BonusScore> GiveMeAllBonuses()
        {
            return this.allBonus;
        }
    }
}
