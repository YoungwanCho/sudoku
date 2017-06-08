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

        //public CellData()
        //{
        //    UnityEngine.Debug.Log("CellData Construct");
        //}        
    }

    [Serializable]
    class MapData
    {
        public CellData[,] mapData;
        public MapData()
        {
            UnityEngine.Debug.Log("MapData Construct");
            mapData = new CellData[9, 9];

            for(int i=0; i<9; i++)
            {
                for(int j=0; j<9; j++)
                {
                    mapData[i, j] = new CellData();
                }
            }
        }

    }
}
