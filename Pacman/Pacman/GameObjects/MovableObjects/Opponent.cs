namespace Pacman.GameObjects.MovableObjects
{
    using Pacman.ConsoleThings;
    using Pacman.Constants;
    using Pacman.Enumerations;
    using System.Collections.Generic;
    using System.Linq;
    using System;

    class Opponent : MovableObject
    {
        private const int SmartestEnemy = 100;

        public bool StartFollow { get; set; }
        public bool Blink { get; set; }
        public int Score { get; set; }

        private int counterForSlowMotionMove = 1;
        private int counterForBlink = 1;
        private int smartPercentage;

        private bool startPosition;
        private bool slowMotionMove;
        private bool foundWay;
        private bool hitWall;

        private List<MatrixCoords> openList = new List<MatrixCoords>();
        private List<MatrixCoords> closedList = new List<MatrixCoords>();
        private List<KeyValuePair<decimal, MatrixCoords>> firstEqualFoundedWays = new List<KeyValuePair<decimal, MatrixCoords>>(); // first time when we met a junction, if there have more than 1 equal distance difference ways, will go in this variable
        private List<KeyValuePair<int, Direction>> shortestDirections = new List<KeyValuePair<int, Direction>>();

        public Opponent(char symbol, MatrixCoords position, int smartPercentage)
            :base(symbol, position)
        {
            base.team = Team.Opponent;
            this.SmartPercentage = smartPercentage;
            this.startPosition = true;
            this.StartFollow = false;
            this.Score = Constant.OpponentScore;
        }

        public override char Symbol
        {
            get
            {
                char symbolToReturn;
                if (this.Blink)
                {
                    if (this.counterForBlink == 2)
                    {
                        symbolToReturn = ' ';
                        this.counterForBlink = 1;
                    }
                    else
                    {
                        symbolToReturn = this.symbol;
                        this.counterForBlink++;
                    }
                }
                else
                {
                    symbolToReturn = this.symbol;
                }

                return symbolToReturn;
            }
            set { base.Symbol = value; }
        }

        public int SmartPercentage
        {
            get 
            {
                return this.smartPercentage; 
            }
            set 
            {
                if (value > SmartestEnemy)
                {
                    value = SmartestEnemy;
                }
                this.smartPercentage = value; 
            }
        }

        public void FollowPacman(Character pacman, List<Path> paths, List<Wall> walls )
        {
            this.Blink = pacman.AttackMode;
            this.slowMotionMove = false;
            if (this.StartFollow)
            {
                if (this.startPosition)
                {
                    this.waitingDirection = Direction.Up;
                    this.currentDirection = Direction.Up;
                    this.Move(Direction.Up);
                    this.Move(Direction.Up);
                    this.Move(Direction.Up);
                    this.startPosition = false;
                }
                else
                {
                    if (CheckForHitWall(walls))
                    {
                        this.hitWall = true;
                        if (!MoveIfThereHaveOnlyOneWay(walls))
                        {
                            AStarPathFinding(pacman, paths);
                        }
                        this.hitWall = false;
                    }
                    else if (CheckForTurns(paths))
                    {
                        AStarPathFinding(pacman, paths); 
                    }
                }
            }
        }

        public void RunAwayFromPacman(Character pacman, List<Wall> walls)
        {
            if (pacman.AttackMode && Character.maxAttackModeTime / 2 > pacman.attackModeTimer)
            {
                this.Blink = true;
            }
            else
            {
                this.Blink = false;
            }
            this.slowMotionMove = true;
            if (this.counterForSlowMotionMove == 1)
            {
                if (CheckForHitWall(walls))
                {
                    if (!MoveIfThereHaveOnlyOneWay(walls))
                    {
                        MoveRandomWhenHitWall();
                    }
                }
                else
                {
                    RunAwayByNormalCalculatingDistanceDifference(pacman);
                }
            }
        }

        public override void Update()
        {
            if (this.StartFollow && this.startPosition == false)
            {
                base.Update();
            }
        }

        public override void Move(Direction direction)
        {
            if (this.slowMotionMove)
            {
                if (counterForSlowMotionMove == 3)
                {
                    base.Move(direction);
                    counterForSlowMotionMove = 1;
                }
                else
                {
                    counterForSlowMotionMove++;
                }
            }
            else
            {
                base.Move(direction);
            }
        }

        public object Clone()
        {
            return new Opponent(this.Symbol, this.Position, this.smartPercentage);
        }

        public override void Reset()
        {
            this.Position = new MatrixCoords(Constant.OpponentRowStartPosition, Constant.OpponentColStartPosition);
            this.startPosition = true;
            this.slowMotionMove = false;
            this.StartFollow = false;
            this.counterForSlowMotionMove = 1;
            base.Reset();
        }

        /// <summary>
        /// If we are moving right and there have wall at right and bottom, only way to move is up
        /// </summary>
        /// <param name="paths"></param>
        /// <returns></returns>
        private bool MoveIfThereHaveOnlyOneWay(List<Wall> paths)
        {
            bool onlyWay = false;
            if (this.currentDirection == Direction.Up)
            {
                if (paths.FirstOrDefault(x => x.Position == this.Position.TopPosition) != null)
                {
                    if (paths.FirstOrDefault(x => x.Position == this.Position.LeftPosition) != null)
                    {
                        onlyWay = true;
                        this.waitingDirection = Direction.Right;
                        this.currentDirection = Direction.Right;
                    }
                    if (paths.FirstOrDefault(x => x.Position == this.Position.RightPosition) != null)
                    {
                        onlyWay = true;
                        this.waitingDirection = Direction.Left;
                        this.currentDirection = Direction.Left;
                    }
                }
            }
            if (this.currentDirection == Direction.Down)
            {
                if (paths.FirstOrDefault(x => x.Position == this.Position.BottomPosition) != null)
                {
                    if (paths.FirstOrDefault(x => x.Position == this.Position.LeftPosition) != null)
                    {
                        onlyWay = true;
                        this.waitingDirection = Direction.Right;
                        this.currentDirection = Direction.Right;
                    }
                    if (paths.FirstOrDefault(x => x.Position == this.Position.RightPosition) != null)
                    {
                        onlyWay = true;
                        this.waitingDirection = Direction.Left;
                        this.currentDirection = Direction.Left;
                    }
                }
            }
            if (this.currentDirection == Direction.Left)
            {
                if (paths.FirstOrDefault(x => x.Position == this.Position.LeftPosition) != null)
                {
                    if (paths.FirstOrDefault(x => x.Position == this.Position.TopPosition) != null)
                    {
                        this.waitingDirection = Direction.Down;
                        this.currentDirection = Direction.Down;
                        onlyWay = true;
                    }
                    if (paths.FirstOrDefault(x => x.Position == this.Position.BottomPosition) != null)
                    {
                        this.waitingDirection = Direction.Up;
                        this.currentDirection = Direction.Up;
                        onlyWay = true;
                    }
                }
            }
            if (this.currentDirection == Direction.Right)
            {
                if (paths.FirstOrDefault(x => x.Position == this.Position.RightPosition) != null)
                {
                    if (paths.FirstOrDefault(x => x.Position == this.Position.TopPosition) != null)
                    {
                        this.waitingDirection = Direction.Down;
                        this.currentDirection = Direction.Down;
                        onlyWay = true;
                    }
                    if (paths.FirstOrDefault(x => x.Position == this.Position.BottomPosition) != null)
                    {
                        this.waitingDirection = Direction.Up;
                        this.currentDirection = Direction.Up;
                        onlyWay = true;
                    }
                }
            }

            return onlyWay;
        }

        private bool CheckForTurns(List<Path> paths)
        {
            bool foundTurn = false;

            if (this.waitingDirection == Direction.Up || this.waitingDirection == Direction.Down)
            {
                if (paths.FirstOrDefault(x => x.Position == this.Position.LeftPosition || x.Position == this.Position.RightPosition) != null)
                {
                    foundTurn = true;
                }
            }
            else if (this.waitingDirection == Direction.Left || this.waitingDirection == Direction.Right)
            {
                if (paths.FirstOrDefault(x => x.Position == this.Position.TopPosition) != null)
                {
                    foundTurn = true;
                }
                if (paths.FirstOrDefault(x => x.Position == this.Position.BottomPosition) != null)
                {
                    foundTurn = true;
                }
            }

            return foundTurn;
        }

        private void AStarPathFinding(Character pacman, List<Path> paths)
        {
            MatrixCoords currPosition = new MatrixCoords(this.Position.Row, this.Position.Col);
            openList = new List<MatrixCoords>();
            closedList = new List<MatrixCoords>();
            firstEqualFoundedWays = new List<KeyValuePair<decimal, MatrixCoords>>();
            shortestDirections = new List<KeyValuePair<int, Direction>>();
            foundWay = false;
            int firstDirectionChoose = 0;
            int stepCounter = 0;
            FindPath(currPosition, paths, pacman, firstDirectionChoose, stepCounter);
        }

        public void FindPath(MatrixCoords currPosition, List<Path> paths, Character pacman, int firstChooseDirection, int stepCounter)
        {
            closedList.Add(new MatrixCoords(currPosition.Row, currPosition.Col));
            List<Path> fourPossiblePaths = FindPossibleWaysToMove(currPosition, paths);

            AddPathsToOpenList(fourPossiblePaths);
            RemoveClosedPathsFromOpenList();

            List<KeyValuePair<decimal, MatrixCoords>> foundedWays = new List<KeyValuePair<decimal, MatrixCoords>>();

            firstChooseDirection++;
            stepCounter++;

            foreach (var startPos in openList)
            {
                if (startPos == pacman.Position)
                {
                    ProceedWhenFindPacman(paths, pacman, firstChooseDirection, stepCounter);
                }

                decimal distanceDifference = CalculateDistanceDifference(startPos, pacman.Position);

                foundedWays.Add(new KeyValuePair<decimal, MatrixCoords>(distanceDifference, startPos));
            }

            if (firstChooseDirection == 1) // will be true only first time when this method is called
            {
                foundedWays = RemoveForbiddenDirections(foundedWays);
                this.firstEqualFoundedWays = CloneEqualWays(foundedWays);
            }

            foundedWays = foundedWays.OrderBy(x => x.Key).ToList();

            foreach (var distance in foundedWays)
            {
                if (foundWay)
                {
                    return;
                }
                else
                {
                    FindPath(distance.Value, paths, pacman, firstChooseDirection, stepCounter);
                }
            }
        }

        private bool CheckForHitWall(List<Wall> allWalls)
        {
            bool isHitted = false;

            if (this.currentDirection == Direction.Up)
            {
                if (allWalls.FirstOrDefault(x => x.Position == this.Position.TopPosition) != null)
                {
                    isHitted = true;
                }
            }
            if (this.currentDirection == Direction.Down)
            {
                if (allWalls.FirstOrDefault(x => x.Position == this.Position.BottomPosition) != null)
                {
                    isHitted = true;
                }
            }
            else if (this.currentDirection == Direction.Left)
            {
                if (allWalls.FirstOrDefault(x => x.Position == this.Position.LeftPosition) != null)
                {
                    isHitted = true;
                }
            }
            else if (this.currentDirection == Direction.Right)
            {
                if (allWalls.FirstOrDefault(x => x.Position == this.Position.RightPosition) != null)
                {
                    isHitted = true;
                }
            }

            return isHitted;
        }

        private void ProceedWhenFindPacman(List<Path> paths, Character pacman, int firstChooseDirection, int stepCounter)
        {
            foundWay = true;
            this.shortestDirections.Add(new KeyValuePair<int, Direction>(stepCounter, SetDirection(paths)));

            if (this.firstEqualFoundedWays.Count > 1) // if we have more than 1 equal distanceDifference ways
            {
                this.openList.Clear(); // clear both lists to start the search again with new position
                this.closedList.Clear();

                FindPath(firstEqualFoundedWays[1].Value, paths, pacman, firstChooseDirection, 0);

                this.firstEqualFoundedWays.Clear();
                    // clear because this is static variable and will not change and we will enter in this condition again and again..
            }

            this.waitingDirection = ChooseBestDirection(shortestDirections);
        }

        private Direction ChooseBestDirection(List<KeyValuePair<int, Direction>> shortestDirections)
        {
            KeyValuePair<int, Direction> shortestWay = new KeyValuePair<int, Direction>(shortestDirections[0].Key, shortestDirections[0].Value);

            foreach (var item in shortestDirections)
            {
                if (item.Key < shortestWay.Key)
                {
                    shortestWay = new KeyValuePair<int, Direction>(item.Key, item.Value);
                }
            }

            return shortestWay.Value;
        }

        private Direction SetDirection(List<Path> paths)
        {
            Direction direction = this.currentDirection;

            bool correctWay = CalculatePercentage();

            if (closedList.Count > 1)
            {
                if (closedList[0].Row == closedList[1].Row)
                {
                    if (closedList[0].Col > closedList[1].Col)
                    {
                        if (this.currentDirection != Direction.Right)
                        {
                            direction = correctWay ? Direction.Left : SetRandomPossibleDirection(Direction.Left, Direction.Right, paths);
                        }
                    }
                    else
                    {
                        if (this.currentDirection != Direction.Left)
                        {
                            direction = correctWay ? Direction.Right : SetRandomPossibleDirection(Direction.Right, Direction.Left, paths);
                        }
                    }
                }
                else
                {
                    if (closedList[0].Row > closedList[1].Row && correctWay)
                    {
                        if (this.currentDirection != Direction.Down)
                        {
                            direction = correctWay ? Direction.Up : SetRandomPossibleDirection(Direction.Up, Direction.Down, paths);
                        }
                    }
                    else
                    {
                        if (this.currentDirection != Direction.Up)
                        {
                            direction = correctWay ? Direction.Down : SetRandomPossibleDirection(Direction.Down, Direction.Up, paths);
                        }
                    }
                }
            }

            return direction;
        }

        private Direction SetRandomPossibleDirection(Direction direction, Direction secondDirection, List<Path> paths)
        {
            Direction randomDirection = direction;
            Random rand = new Random();

            Direction topDirection = paths.FirstOrDefault(x => x.Position == this.Position.TopPosition) != null ? Direction.Up : direction;
            Direction bottomDirection = paths.FirstOrDefault(x => x.Position == this.Position.BottomPosition) != null ? Direction.Down : direction;
            Direction leftDirection = paths.FirstOrDefault(x => x.Position == this.Position.LeftPosition) != null ? Direction.Left : direction;
            Direction rightDirection = paths.FirstOrDefault(x => x.Position == this.Position.RightPosition) != null ? Direction.Right : direction;

            while (direction == randomDirection || direction == secondDirection)
            {
                int randNum = rand.Next(1, 5);
                switch (randNum)
                {
                    case 1:
                        randomDirection = topDirection;
                        break;
                    case 2 :
                        randomDirection = bottomDirection;
                        break;
                    case 3:
                        randomDirection = leftDirection;
                        break;
                    case 4:
                        randomDirection = rightDirection;
                        break;
                }
            }

            return randomDirection;
        }

        private List<KeyValuePair<decimal, MatrixCoords>> CloneEqualWays(List<KeyValuePair<decimal, MatrixCoords>> foundedWays)
        {
            List<KeyValuePair<decimal, MatrixCoords>> copy = new List<KeyValuePair<decimal, MatrixCoords>>();

            if (foundedWays.Count > 1)
            {
                for (int i = 0; i < foundedWays.Count - 1; i++)
                {
                    for (int j = i + 1; j < foundedWays.Count; j++)
                    {
                        if (foundedWays[i].Key == foundedWays[j].Key)
                        {
                            copy.Add(foundedWays[j]);
                        }
                    }
                }
            }

            return copy;
        }

        /// <summary>
        /// Еliminates the directions which are contrary to my
        /// </summary>
        /// <param name="foundedWays"></param>
        /// <returns></returns>
        private List<KeyValuePair<decimal, MatrixCoords>> RemoveForbiddenDirections(List<KeyValuePair<decimal, MatrixCoords>> foundedWays)
        {
            List<KeyValuePair<decimal, MatrixCoords>> correctFoundedWays = new List<KeyValuePair<decimal, MatrixCoords>>();

            foreach (var way in foundedWays)
            {
                if (way.Value.Col > this.Position.Col)
                {
                    if (this.currentDirection == Direction.Left)
                    {
                        continue;
                    }
                }
                if (way.Value.Col < this.Position.Col)
                {
                    if (this.currentDirection == Direction.Right)
                    {
                        continue;
                    }
                }
                if (way.Value.Row > this.Position.Row)
                {
                    if (this.currentDirection == Direction.Up)
                    {
                        continue;
                    }
                }
                if (way.Value.Row < this.Position.Row)
                {
                    if (this.currentDirection == Direction.Down)
                    {
                        continue;
                    }
                }

                correctFoundedWays.Add(way);
            }

            return correctFoundedWays;
        }

        private void RemoveClosedPathsFromOpenList()
        {
            foreach (var close in closedList)
            {
                openList.RemoveAll(x => x == close);
            }
        }

        private void AddPathsToOpenList(List<Path> fourPossiblePaths)
        {
            foreach (var path in fourPossiblePaths)
            {
                openList.Add(path.Position);
            }
        }

        private List<Path> FindPossibleWaysToMove(MatrixCoords currPosition, List<Path> paths)
        {
            List<Path> fourPossiblePaths = new List<Path>();

            fourPossiblePaths.Add(paths.FirstOrDefault(x => currPosition.TopPosition == x.Position));
            fourPossiblePaths.Add(paths.FirstOrDefault(x => currPosition.BottomPosition == x.Position));
            fourPossiblePaths.Add(paths.FirstOrDefault(x => currPosition.LeftPosition == x.Position));
            fourPossiblePaths.Add(paths.FirstOrDefault(x => currPosition.RightPosition == x.Position));
            fourPossiblePaths.RemoveAll(x => x == null);

            foreach (var item in closedList)
            {
                fourPossiblePaths.RemoveAll(x => x.Position == item);
            }

            return fourPossiblePaths;
        }

        private bool CalculatePercentage()
        {
            Random rand = new Random();
            List<int> randNums = new List<int>();

            for (int i = 0; i < this.SmartPercentage / 10; i++)
            {
                int randNum = rand.Next(1, 11);

                if (randNums.Contains(randNum))
                {
                    i--;
                    continue;
                }
                randNums.Add(randNum);
            }

            if (randNums.Contains(1))
            {
                return true;
            }

            return false;
        }

        private decimal CalculateDistanceDifference(MatrixCoords startPosition, MatrixCoords endPosition)
        {
            decimal sideA = Math.Abs(startPosition.Row - endPosition.Row);
            decimal sideB = Math.Abs(startPosition.Col - endPosition.Col);

            decimal estimateDistance = 1 + (decimal)Math.Sqrt(Math.Pow((double)sideA, 2) + Math.Pow((double)sideB, 2));

            return estimateDistance;
        }

        private void MoveRandomWhenHitWall()
        {
            Random rand = new Random();
            if (this.currentDirection == Direction.Up || this.currentDirection == Direction.Down)
            {
                if (rand.Next(1, 3) == 1)
                {
                    this.currentDirection = Direction.Left;
                }
                else
                {
                    this.currentDirection = Direction.Right;
                }
            }
            if (this.currentDirection == Direction.Left || this.currentDirection == Direction.Right)
            {
                if (rand.Next(1, 3) == 1)
                {
                    this.currentDirection = Direction.Up;
                }
                else
                {
                    this.currentDirection = Direction.Down;
                }
            }
        }

        private void RunAwayByNormalCalculatingDistanceDifference(Character pacman)
        {
            if (this.Position.Col > pacman.Position.Col)
            {
                int colDifference = this.Position.Col - pacman.Position.Col;

                if (this.Position.Row > pacman.Position.Row)
                {
                    int rowDifference = this.Position.Row - pacman.Position.Row;

                    if (colDifference > rowDifference)
                    {
                        if (this.currentDirection != Direction.Up)
                        {
                            this.waitingDirection = Direction.Down;
                        }
                    }
                    else
                    {
                        if (this.currentDirection != Direction.Left)
                        {
                            this.waitingDirection = Direction.Right;
                        }
                    }
                }
                else
                {
                    int rowDifference = pacman.Position.Row - this.Position.Row;

                    if (colDifference > rowDifference)
                    {
                        if (this.currentDirection != Direction.Down)
                        {
                            this.waitingDirection = Direction.Up;
                        }
                    }
                    else
                    {
                        if (this.currentDirection != Direction.Left)
                        {
                            this.waitingDirection = Direction.Right;
                        }
                    }
                }
            }
            else
            {
                int colDifference = pacman.Position.Col - this.Position.Col;

                if (this.Position.Row > pacman.Position.Row)
                {
                    int rowDifference = this.Position.Row - pacman.Position.Row;

                    if (colDifference > rowDifference)
                    {
                        if (this.currentDirection != Direction.Up)
                        {
                            this.waitingDirection = Direction.Down;
                        }
                    }
                    else
                    {
                        if (this.currentDirection != Direction.Right)
                        {
                            this.waitingDirection = Direction.Left;
                        }
                    }
                }
                else
                {
                    int rowDifference = pacman.Position.Row - this.Position.Row;

                    if (colDifference > rowDifference)
                    {
                        if (this.currentDirection != Direction.Down)
                        {
                            this.waitingDirection = Direction.Up;
                        }
                    }
                    else
                    {
                        if (this.currentDirection != Direction.Right)
                        {
                            this.waitingDirection = Direction.Left;
                        }
                    }
                }

            }
        }
    }
}
