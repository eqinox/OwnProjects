namespace Pacman.Engines
{
    using Pacman.ConsoleThings;
    using Pacman.Constants;
    using Pacman.GameObjects;
    using Pacman.GameObjects.MovableObjects;
    using Pacman.GameObjects.Scores;
    using Pacman.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Linq;
    using Pacman.Enumerations;
    using System.Media;
    using Pacman.sound;

    class GameEngine
    {
        // If we create list, we should delete all dead (IsAlive == false) objects from it in RemoveAllDeadObjects() method
        private IUserInput _userInput;
        private IRenderer renderer;
        private List<GameObject> allObjects;
        private List<MovableObject> allMovableObjects;
        private Character pacman;
        private Map map;
        private Map copyMap;

        public GameEngine(IUserInput _userInput, IRenderer renderer, Map map)
        {
            this._userInput = _userInput;
            this.renderer = renderer;
            this.map = map;
            this.copyMap = map.GiveMeMapAgain() as Map;
            this.allObjects = new List<GameObject>();
            this.allMovableObjects = new List<MovableObject>();
        }

        public void AddObject(GameObject obj)
        {
            if (obj is MovableObject)
            {
                this.allMovableObjects.Add(obj as MovableObject);
            }
            if (obj is Character)
            {
                this.pacman = obj as Character;
            }
            this.allObjects.Add(obj);
        }

        public void Run()
        {
            while (this.pacman.IsAlive && this.map.GiveMeAllScores().Count > 0)
            {
                this.renderer.EnqueueForRendering(this.map.GiveMeMap());
                this.renderer.EnqueueForRendering(this.allObjects);

                this._userInput.ProcessInput();

                ProceedAllOpponentsMoves();

                foreach (var obj in this.allMovableObjects)
                {
                    obj.SetIfCanMoveToWaitingDirection(this.map.GiveMeAllPaths());
                    obj.Update();
                    if (CollisionDispatcher.SeeForCollisionWithWalls(obj, this.map.GiveMeAllWalls()))
                    {
                        obj.MoveBack();
                    }
                    CheckPacmanCollisionWithOpponent(this.allMovableObjects);
                }

                this.renderer.RenderAll();
                RenderScore();

                CheckForCollisionWithScores(this.pacman, this.allObjects);

                RemoveAllDeadObjects();

                AddBonusScores();

                this.renderer.ClearQueue();
                Thread.Sleep(150);
            }

            ProceedIfPacmanIsDead();
            ProceedIfWin();
        }

        private void AddBonusScores()
        {
            Random rand = new Random();

            if (rand.Next(1, 50) == 10)
            {
                
                SuperBonusScore newScore = new SuperBonusScore(Constant.SuperBonusScoreSymbol, new MatrixCoords(Constant.PacmanRowStartPosition, Constant.PacmanColStartPosition));
                if (this.allObjects.FirstOrDefault(x => x.Position == newScore.Position) == null)
                {
                    this.AddObject(newScore);
                }
            }

            if (rand.Next(1, 100) == 10)
            {
                BonusLifeScore newScore = new BonusLifeScore(Constant.BonusLifeSymbol, new MatrixCoords(Constant.PacmanRowStartPosition, Constant.PacmanColStartPosition));
                if (this.allObjects.FirstOrDefault(x => x.Position == newScore.Position) == null)
                {
                    this.AddObject(newScore);
                }
            }
        }

        private void ProceedIfWin()
        {
            if (this.pacman.IsAlive)
            {
                Console.Clear();
                string winMsg = "Congatulations, you WIN";
                string scoreMsg = string.Format("Your score: {0}", this.pacman.Scores);
                Console.SetCursorPosition(Console.WindowWidth / 2 - winMsg.Length / 2, Console.WindowHeight / 2);
                Console.WriteLine(winMsg);
                Console.SetCursorPosition(Console.WindowWidth / 2 - scoreMsg.Length / 2, Console.WindowHeight / 2 + 1);
                Console.WriteLine(scoreMsg);
                string userChooseMsg = "Press \" " + Constant.RestartCommand + "\" for new game and \" " + Constant.ExitCommand + "\" for exit";
                Console.SetCursorPosition(Console.WindowWidth / 2 - userChooseMsg.Length / 2, Console.WindowHeight / 2 + 2);
                Console.WriteLine(userChooseMsg);
                string userInputCommand = Console.ReadLine().ToUpper();
                ProceedUserChoise(userInputCommand);
            }
        }

        private void ProceedUserChoise(string userChooseMsg)
        {
            while (userChooseMsg != Constant.ExitCommand)
            {
                if (userChooseMsg == Constant.RestartCommand)
                {
                    foreach (var obj in this.allMovableObjects)
                    {
                        if (obj is Character)
                        {
                            (obj as Character).ResetEverything();
                        }
                        else
                        {
                            obj.Reset();
                        }
                    }

                    if (this.allObjects.FirstOrDefault(x => x is Character) == null)
                    {
                        Character pacman = new Character('@', new MatrixCoords(Constant.PacmanRowStartPosition, Constant.PacmanColStartPosition));
                        _userInput.OnUpPressed += (sender, eventArgs) => { pacman.waitingDirection = Direction.Up; };
                        _userInput.OnDownPressed += (sender, eventArgs) => { pacman.waitingDirection = Direction.Down; };
                        _userInput.OnLeftPressed += (sender, eventArgs) => { pacman.waitingDirection = Direction.Left; };
                        _userInput.OnRightPressed += (sender, eventArgs) => { pacman.waitingDirection = Direction.Right; };
                        this.AddObject(pacman);
                    }

                    this.map = this.copyMap.GiveMeMapAgain() as Map;

                    this.Run();
                }
                else
                {
                    Console.WriteLine("Invalid command");
                    userChooseMsg = Console.ReadLine().ToUpper();
                }
            }
        }

        private void ProceedIfPacmanIsDead()
        {
            if (!this.pacman.IsAlive)
            {
                MusicPlayer.killed.Play();
                Console.Clear();
                string gameOverMsg = "GAME OVER";
                string scoreMsg = string.Format("Your score: {0}", this.pacman.Scores);
                Console.SetCursorPosition(Console.WindowWidth / 2 - gameOverMsg.Length / 2, Console.WindowHeight / 2);
                Console.WriteLine(gameOverMsg);
                Console.SetCursorPosition(Console.WindowWidth / 2 - scoreMsg.Length / 2, Console.WindowHeight / 2 + 1);
                Console.WriteLine(scoreMsg);
                string userChooseMsg = "Press \"" + Constant.RestartCommand + "\" for new game and \"" + Constant.ExitCommand + "\" for exit";
                Console.SetCursorPosition(Console.WindowWidth / 2 - userChooseMsg.Length / 2, Console.WindowHeight / 2 + 2);
                Console.WriteLine(userChooseMsg);
                string userInputCommand = Console.ReadLine().ToUpper();
                ProceedUserChoise(userInputCommand);
            }
        }

        private void CheckPacmanCollisionWithOpponent(List<MovableObject> opponents)
        {
            MovableObject opponent = CollisionDispatcher.SeeForCollisionWithOpponent(this.pacman, opponents);
            if (opponent != null)
            {
                if (pacman.AttackMode)
                {
                    pacman.EatOpponent(opponent as Opponent);
                }
                else
                {
                    pacman.Lives--;

                    this.renderer.RenderAll();
                    MusicPlayer.killed.Play();
                    Thread.Sleep(1500);
                    ResetMovableObjectsToStartPosition();
                    if (pacman.Lives == 0)
                    {
                        pacman.IsAlive = false;
                    }
                }
            }
        }

        private void ResetMovableObjectsToStartPosition()
        {
            foreach (var movableObject in this.allMovableObjects)
            {
                movableObject.Reset();
            }
        }

        private void ProceedAllOpponentsMoves()
        {
            foreach (var movableObject in this.allMovableObjects)
            {
                if (movableObject is Opponent)
                {
                    Opponent opponent = movableObject as Opponent;
                    bool startFollow = new Random().Next(0, 13) == 1 ? true : false;
                    if (!opponent.StartFollow)
                    {
                        if (startFollow)
                        {
                            opponent.StartFollow = true;
                            break;
                        }
                    }
                    else
                    {
                        if (this.pacman.AttackMode == true)
                        {
                            opponent.RunAwayFromPacman(this.pacman, this.map.GiveMeAllWalls());
                        }
                        else
                        {
                            opponent.FollowPacman(this.pacman, this.map.GiveMeAllPaths(), this.map.GiveMeAllWalls());
                        }
                    }
                }
            }
        }

        private void RemoveAllDeadObjects()
        {
            this.allObjects.RemoveAll(x => x.IsAlive == false);
            this.allMovableObjects.RemoveAll(x => x.IsAlive == false);
            this.map.GiveMeMap().RemoveAll(x => x.IsAlive == false);
            this.map.GiveMeAllScores().RemoveAll(x => x.IsAlive == false);
        }

        private void CheckForCollisionWithScores(Character pacman, List<GameObject> allScores)
        {
            GameObject obj = CollisionDispatcher.SeeForCollisionWithObjects(pacman, allScores);
            if (obj is Score)
            {
                pacman.TakeScore(obj as Score);
            }

            Score score = CollisionDispatcher.SeeForCollisionWithScores(pacman, this.map.GiveMeAllScores());
            if (score != null)
            {
                pacman.TakeScore(score);
            }
        }

        private void RenderScore()
        {
            string scoreMsg = string.Format("Scores: {0}", this.pacman.Scores);
            string livesMSG = string.Format("Lives: {0}", this.pacman.Lives);
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(livesMSG);
            Console.SetCursorPosition(this.map.AllColls / 2 - scoreMsg.Length / 2, Console.CursorTop);
            Console.WriteLine(scoreMsg);
        }
    }
}
