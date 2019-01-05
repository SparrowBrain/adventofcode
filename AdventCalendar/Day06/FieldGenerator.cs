using System.Collections.Generic;

namespace AdventCalendar.Day06
{
    internal class FieldGenerator
    {
        private readonly IEnumerable<Source> _sources;

        public FieldGenerator(IEnumerable<Source> sources)
        {
            _sources = sources;
        }

        public Vector[][] CreateFields()
        {
            var boardFactory = new BoardFactory(_sources);
            var board = boardFactory.CreateBoard();

            var distance = 0;
            var modificationHappened = false;
            do
            {
                modificationHappened = false;
                foreach (var source in _sources)
                {
                    for (var deltaX = 0; deltaX <= distance; deltaX++)
                    {
                        var deltaY = distance - deltaX;

                        modificationHappened |= SetVector(board, source, distance, source.X + deltaX, source.Y + deltaY);
                        modificationHappened |= SetVector(board, source, distance, source.X + deltaX, source.Y - deltaY);
                        modificationHappened |= SetVector(board, source, distance, source.X - deltaX, source.Y + deltaY);
                        modificationHappened |= SetVector(board, source, distance, source.X - deltaX, source.Y - deltaY);
                    }
                }

                distance++;
            } while (modificationHappened);

            SetInfinity(board);

            return board;
        }

        private static void SetInfinity(Vector[][] board)
        {
            for (var x = 0; x < board.Length; x ++)
            {
                CheckBorder(board, x, 0);
                CheckBorder(board, x, board[0].Length - 1);
            }

            for (var y = 0; y < board[0].Length; y ++)
            {
                CheckBorder(board, 0, y);
                CheckBorder(board, board.Length - 1, y);
            }
        }

        private static void CheckBorder(Vector[][] board, int x, int y)
        {
            var vector = board[x][y];
            if (!vector.Equals(Vector.Conflict))
            {
                vector.Source.Infinity = true;
            }
        }

        private static bool SetVector(Vector[][] board, Source source, int distance, int xCoord, int yCoord)
        {
            if (xCoord > board.Length - 1 || yCoord > board[0].Length - 1 || xCoord < 0 || yCoord < 0)
            {
                return false;
            }

            if (board[xCoord][yCoord] == null)
            {
                board[xCoord][yCoord] = new Vector(source, distance);
                return true;
            }

            if (board[xCoord][yCoord].Distance == distance && !Equals(board[xCoord][yCoord].Source, source))
            {
                board[xCoord][yCoord] = Vector.Conflict;
                return true;
            }

            return false;
        }
    }
}