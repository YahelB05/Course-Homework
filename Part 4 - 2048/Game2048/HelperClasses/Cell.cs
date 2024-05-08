using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game2048
{
    internal class Cell
    {
        /*
         This class follows the Flyweight design pattern.
         By doing so, it helps with checking if a Cell object is in a Set. We can use .Contains() without needing to iterate
         over the set and check with .Equals() every object.
         */

        private static readonly Dictionary<string, Cell> _cells = new Dictionary<string, Cell>();

        public int Row {  get; set; }
        public int Column { get; set; }

        private Cell() { }

        private Cell(int row, int column)
        {
            Row = row;
            Column = column;
        }

        public static Cell CreateCell(int row, int column)
        {
            string rowColumn = row.ToString() + column.ToString();
            if (_cells.ContainsKey(rowColumn))
                return _cells[rowColumn];
            
            Cell cell = new Cell(row, column);
            _cells.Add(rowColumn, cell);
            return cell;
        } 
    }
}
