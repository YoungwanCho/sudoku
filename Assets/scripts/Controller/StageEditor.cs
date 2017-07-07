using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using UnityEngine;
using System.Text.RegularExpressions;

namespace controller
{
    class StageEditor
    {
        public void SaveStageAs(object stageName, model.SquareBoard board)
        {
            if(stageName.GetType() == typeof(String))
            {
                if(IsEnglish(stageName.ToString()))
                {
                    MapSave(stageName.ToString(), board);
                }
            }
        }

        private bool IsEnglish(string str)
        {
            Debug.Log(string.Format("IsEnglish : {0}", str));
            Regex engRegex = new Regex(@"[a-zA-Z]");
            return engRegex.IsMatch(str);
        }

        private void MapSave(string stageName, model.SquareBoard board)
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

            System.IO.FileInfo fi = new System.IO.FileInfo(path);
            if(fi.Exists)
            {
                Debug.Log("StageName Exists");
            }
            else
            {
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
}


