using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace model
{
    [Serializable]
    public class CellData
    {
        public int column;
        public int row;
        public int number;

        public CellData(int column, int row, int number)
        {
            this.column = column;
            this.row = row;
            this.number = number;
        }

    }

    [Serializable]
    public class StageData
    {        
        public List<CellData> cellDataList = null;
        public StageData()
        {
            cellDataList = new List<CellData>();            
        }

    }
}
