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
        private List<Pacman.GameObjects.Path> allPaths;
        private string path;

        public Map(string path)
        {
            this.allObjects = new List<GameObject>();
            this.allScore = new List<Score>();
            this.allWalls = new List<Wall>();
            this.allPaths = new List<Pacman.GameObjects.Path>();
            this.path = path;
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
                        Pacman.GameObjects.Path newPath = new Pacman.GameObjects.Path(Constant.PathSymbol, new MatrixCoords(row, col));
                        this.allPaths.Add(newPath);
                        this.allObjects.Add(newScore);
                        this.allScore.Add(newScore as NormalScore);
                        break;
                    case Constant.BonusSymbol:
                        Score newBonus = new BonusScore(Constant.BonusSymbol, new MatrixCoords(row, col));
                        Pacman.GameObjects.Path newPath2 = new Pacman.GameObjects.Path(Constant.PathSymbol, new MatrixCoords(row, col));
                        this.allPaths.Add(newPath2);
                        this.allObjects.Add(newBonus);
                        this.allScore.Add(newBonus as BonusScore);
                        break;
                    case Constant.EmptySymbol:
                        Wall newWall2 = new Wall(Constant.EmptySymbol, new MatrixCoords(row, col));
                        this.allObjects.Add(newWall2);
                        this.allWalls.Add(newWall2);
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

        public List<Score> GiveMeAllScores()
        {
            return this.allScore;
        }

        public List<Pacman.GameObjects.Path> GiveMeAllPaths()
        {
            return this.allPaths;
        } 

        public object GiveMeMapAgain()
        {
            return new Map(this.path);
        }
    }
}
