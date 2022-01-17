using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Battleships
{
    class Game
    {

        private Random rnd = new Random();
        private int BoardSize { get; set; }
        private List<List<SquareType>> GameState = new List<List<SquareType>>();
        public bool GameFinished = false;
        private int[] LastAttackCoords = new int[2];
        public string LastAttackDetails;
        public bool isLastAttack { get; set; }
        public int health { get; set; }


        public Game(int _BoardSize)
        {
            BoardSize = _BoardSize;
            isLastAttack = false;
            health = 2;
            InitGame();
            DrawBoard();
        }

        private enum SquareType
        {
            SHIP = 1,
            NONE = 2,
            MISS = 3,
            HIT = 4
        }

        private enum Orientation
        {
            HORIZONTAL = 1,
            VERTICAL = 2
        }

        private void InitGame()
        {
            List<SquareType> row = new List<SquareType>();
            for (int i = 0; i < BoardSize; i++)
            {
                row.Add(SquareType.NONE);
            }
            for (int i = 0; i < BoardSize; i++)
            {
                List<SquareType> newList = new List<SquareType>(row);
                GameState.Add(newList);
            }
            int x = rnd.Next(0, BoardSize);
            int y = rnd.Next(0, BoardSize);
            GameState[x][y] = SquareType.SHIP;
            int o = rnd.Next(1, 3);
            switch ((Orientation)o)
            {
                case Orientation.HORIZONTAL:
                    if (x == 0)
                    {
                        GameState[++x][y] = SquareType.SHIP;
                    } else if (x == BoardSize - 1)
                    {
                        GameState[--x][y] = SquareType.SHIP;
                    } else
                    {
                        int randUpDown = rnd.Next(0, 2) * 2 - 1;
                        GameState[x + randUpDown][y] = SquareType.SHIP;
                    }
                    break;
                case Orientation.VERTICAL:
                    if (y == 0)
                    {
                        GameState[x][++y] = SquareType.SHIP;
                    }
                    else if (y == BoardSize - 1)
                    {
                        GameState[x][--y] = SquareType.SHIP;
                    } else
                    {
                        int randUpDown = rnd.Next(0, 2) * 2 - 1;
                        GameState[x][y + randUpDown] = SquareType.SHIP;
                    }
                    break;
                default:
                    break;
            }
        }

        private void DrawBoard()
        {
            Console.Clear();
            string headerRow = "  ";
            for (int i = 0; i < BoardSize; i++)
            {
                headerRow += ((char)(i + 65)).ToString() + " ";
            }
            Console.WriteLine(headerRow);
            for (int i = 0; i < BoardSize; i++)
            {
                string row = (i + 1).ToString() + " ";
                for (int j = 0; j < BoardSize; j++)
                {
                    switch (GameState[i][j])
                    {
                        case SquareType.SHIP:
                            row += "  ";
                            break;
                        case SquareType.NONE:
                            row += "  ";
                            break;
                        case SquareType.MISS:
                            row += "M ";
                            break;
                        case SquareType.HIT:
                            row += "H ";
                            break;
                        default:
                            break;
                    }
                }
                Console.WriteLine(row);
            }
        }

        private bool CheckFire(string coords)
        {
            if (coords.Length != 2)
            {
                return false;
            }
            int charCode = (int)coords[0] - 65;
            if (charCode < 0 || charCode >= BoardSize)
            {
                return false;
            }
            int y = 0;
            if (!int.TryParse(coords[1].ToString(), out y))
            {
                return false;
            }
            if (coords[1] >= 1 && coords[1] < BoardSize)
            {
                return false;
            }
            isLastAttack = true;
            LastAttackCoords[1] = charCode;
            LastAttackCoords[0] = y - 1;
            return true;
        }

        public void updateHealth()
        {
            health--;
            if (health == 0)
            {
                GameFinished = true;
            }
        }

        public void Fire(string coords)
        {
            string attackDetails = "";
            attackDetails += coords + " : ";
            if (!CheckFire(coords))
            {
                attackDetails += "Out of bounds.";
            } else
            {
                switch (GameState[LastAttackCoords[0]][LastAttackCoords[1]])
                {
                    case SquareType.SHIP:
                        GameState[LastAttackCoords[0]][LastAttackCoords[1]] = SquareType.HIT;
                        updateHealth();
                        attackDetails += "Ship HIT!";
                        break;
                    case SquareType.NONE:
                        GameState[LastAttackCoords[0]][LastAttackCoords[1]] = SquareType.MISS;
                        attackDetails += "MISS!";
                        break;
                    case SquareType.MISS:
                        attackDetails += "Already missed at this location!";
                        break;
                    case SquareType.HIT:
                        attackDetails += "Already hit a ship at this location!";

                        break;
                    default:
                        break;
                }
            }
            DrawBoard();
            LastAttackDetails = attackDetails;
        }

    }
}
