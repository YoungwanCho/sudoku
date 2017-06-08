using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using UnityEngine;

namespace controller
{
    class MapEditor
    {
        public void MapSave(model.SquareBoard board)
        {
            var mapData = new model.MapData();

            model.SquarePack targetPack = null;
            model.SquareCell targetCell = null;
            int column, row;
            for(int i=0; i<board.SquarePack.Length; i++)
            {
                targetPack = board.SquarePack[i];
                for(int j=0; j<targetPack.SquareCells.Length; j++)
                {
                    targetCell = targetPack.SquareCells[j];
                    column = targetCell.BoardCoorinate.column;
                    row = targetCell.BoardCoorinate.row;
                    mapData.mapData[column, row].column = column;
                    mapData.mapData[column, row].row = row;
                    mapData.mapData[column, row].column = targetCell.NumberValue;
                }
            }

            string str = JsonUtility.ToJson(mapData, prettyPrint: true);
            using (FileStream fs = new FileStream("Assets/Resources/MapData.txt", FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(fs))
                {
                    writer.Write(str);
                    writer.Close();
                    writer.Dispose();
                }
                fs.Close();
                fs.Dispose();
            }
        }
    }
}


