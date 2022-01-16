using System;
using System.Collections.Generic;
using System.Text;

namespace Battleships
{
    class Game
    {

        private Random rnd = new Random();
        private int BoardSize { get; set; }
        private List<List<SquareType>> GameState = new List<List<SquareType>>();

        public Game(int _BoardSize)
        {
            BoardSize = _BoardSize;
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
            for (int i = 0; i < BoardSize; i++)
            {
                string row = "";
                for (int j = 0; j < BoardSize; j++)
                {
                    switch (GameState[i][j])
                    {
                        case SquareType.SHIP:
                            row += "S";
                            break;
                        case SquareType.NONE:
                            row += "N";
                            break;
                        case SquareType.MISS:
                            row += "M";
                            break;
                        case SquareType.HIT:
                            row += "H";
                            break;
                        default:
                            break;
                    }
                }
                Console.WriteLine(row);
            }
        }
    }
}
