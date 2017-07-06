using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using UnityEngine;

namespace controller
{
    class StageEditor
    {
        public void MapSave(string stageName, model.SquareBoard board)
        {
            if (stageName == string.Empty)
                return;

            model.StageData stageData = new model.StageData();

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

                    stageData.cellDataList.Add(new model.CellData( column, row, targetCell.NumberValue));
                }
            }

            string str = JsonUtility.ToJson(stageData, prettyPrint: true);
            Debug.Log(str);

            string path = string.Format("Assets/StreamingAssets/{0}.json", stageName);

            using (FileStream fs = new FileStream(path, FileMode.Create))
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


