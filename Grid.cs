// using System;
// using SplashKitSDK;

// namespace MyPathFinding
// {

//     public class Grid<T>
//     {
//         private int _column;
//         private int _row;
//         private double _cellSize;
//         private T[,] _gridArea;
//         private Point2D _startPoint;

//         public int Row
//         {
//             get
//             {
//                 return _row;
//             }
//         }

//         public int Column
//         {
//             get
//             {
//                 return _column;
//             }
//         }

//         public double CellSize
//         {
//             get
//             {
//                 return _cellSize;
//             }
//         }

//         public Grid(int column, int row, int singleCellSize)
//         {
//             _column = column;
//             _row = row;
//             _cellSize = singleCellSize;
//             _gridArea = new T[column, row];
//             _startPoint = Factory.CreatePoint(0, 0);
//         }
//         public Grid(int column, int row, int singleCellSize, Point2D startPoint, Func<Grid<T>, int, int, T> addGridObjFunc)
//         {
//             _column = column;
//             _row = row;
//             _cellSize = singleCellSize;
//             _gridArea = new T[column, row];
//             _startPoint = startPoint;

//             for (int x = 0; x < _gridArea.GetLength(0); x++)
//             {
//                 for (int y = 0; y < _gridArea.GetLength(1); y++)
//                 {
//                     _gridArea[x, y] = addGridObjFunc(this, x, y);
//                 }
//             }
//         }

//         public void PrintGrid()
//         {
//             for (int columnIndex = 0; columnIndex < _gridArea.GetLength(0); columnIndex++)
//             {
//                 for (int rowIndex = 0; rowIndex < _gridArea.GetLength(1); rowIndex++)
//                 {
//                     SplashKit.DrawLine(Color.Black, GetAPoint(columnIndex, rowIndex), GetAPoint(columnIndex, rowIndex + 1));
//                     SplashKit.DrawLine(Color.Black, GetAPoint(columnIndex, rowIndex + 1), GetAPoint(columnIndex + 1, rowIndex + 1));

//                 }
//                 SplashKit.DrawLine(Color.Black, GetAPoint(0, 0), GetAPoint(_column, 0));
//                 SplashKit.DrawLine(Color.Black, GetAPoint(_column, 0), GetAPoint(_column, _row));
//             }
//         }

//         public Point2D GetAPoint(int x, int y)
//         {
//             return Factory.CreatePoint(Convert.ToInt32(x * _cellSize + _startPoint.X), Convert.ToInt32(y * _cellSize + _startPoint.Y));
//         }
//         public static Point2D GetAPoint(int x, int y, int cellSize, Point2D startPoint)
//         {
//             return Factory.CreatePoint(Convert.ToInt32(x * cellSize + startPoint.X), Convert.ToInt32(y * cellSize + startPoint.Y));
//         }

//         public void GetRowColumnNumber(Point2D point, out double x, out double y)
//         {
//             x = GetColumnNumber(point);
//             y = GetRowNumber(point);
//         }

//         public int GetColumnNumber(Point2D point)
//         {
//             return Convert.ToInt32(Math.Floor((point.X - _startPoint.X) / _cellSize));
//         }
//         public int GetRowNumber(Point2D point)
//         {
//             return Convert.ToInt32(Math.Floor((point.Y - _startPoint.Y) / _cellSize));
//         }

//         public void SetValue(int columnIndex, int rowIndex, T value)
//         {
//             if (columnIndex >= 0 && columnIndex < _column && rowIndex >= 0 && rowIndex < _row)
//             {
//                 _gridArea[columnIndex, rowIndex] = value;
//             }
//         }

//         public void SetValue(Point2D point, T value)
//         {
//             SetValue(GetColumnNumber(point), GetRowNumber(point), value);

//         }

//         public T GetValue(int columnIndex, int rowIndex)
//         {
//             if (columnIndex >= 0 && columnIndex < _column && rowIndex >= 0 && rowIndex < _row)
//             {
//                 return _gridArea[columnIndex, rowIndex];
//             }
//             return default(T);
//         }
//         public T GetValue(Point2D point)
//         {
//             return GetValue(GetColumnNumber(point), GetRowNumber(point));
//         }
//     }
// }