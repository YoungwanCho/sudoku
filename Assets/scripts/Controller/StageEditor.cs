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

            string path = string.Format("{0}/{1}.json", UnityEngine.Application.streamingAssetsPath, stageName); //@TODO: file 붙이면 에러남" 유니티에디터에서만 사용가능 할듯;

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

        public void RotationStage(model.SquareBoard sourceBoard)
        {
            int[,] sourceNumberArr = new int[9, 9];
            int[,] resultNumberArr = new int[9, 9];

            model.SquarePack sourcePack = null;
            model.SquareCell sourceCell = null;

            int sourceColumn = 0;
            int sourceRow = 0;

            int maxIndex;
            int magicIndex;

            //@Brief Make sourceNumberArr
            for(int i=0; i<sourceBoard.SquarePack.Length; i++)
            {
                sourcePack = sourceBoard.SquarePack[i];
                for(int j=0; j<sourcePack.SquareCells.Length; j++)
                {
                    sourceCell = sourcePack.SquareCells[j];
                    sourceColumn = sourceCell.BoardCoorinate.column;
                    sourceRow = sourceCell.BoardCoorinate.row;
                    Debug.Log(string.Format("{0}, {1}", sourceRow, sourceColumn));
                    sourceNumberArr[sourceRow, sourceColumn] = sourceCell.NumberValue;
                }
            }

            //@Brief Log Print
            StringBuilder log = new StringBuilder();
            for (int i = 0; i < sourceNumberArr.GetLength(0); i++)
            {
                for (int j = 0; j < sourceNumberArr.GetLength(1); j++)
                {
                    log.AppendFormat("{0} ", sourceNumberArr[i, j]);
      
                }
                log.Append("\n");
            }
            Debug.Log(log);

            maxIndex = sourceNumberArr.GetLength(0) - 1; // maxIndex  = (9 - 1);

            //@Brief Right 90 Rotation
            for (int i=0; i<sourceNumberArr.GetLength(0); i++)
            {
                for(int j=0; j<sourceNumberArr.GetLength(1); j++)
                {
                    magicIndex = maxIndex - j;
                    resultNumberArr[i, j] = sourceNumberArr[magicIndex, i];
                }
            }

            //@Brief Log Print
            log = new StringBuilder();
            for (int i = 0; i < resultNumberArr.GetLength(0); i++)
            {
                for (int j = 0; j < resultNumberArr.GetLength(1); j++)
                {
                    log.AppendFormat("{0} ", resultNumberArr[i, j]);
                }
                log.Append("\n");
            }
            Debug.Log(log);

            //@Brief SourceBoard Update
            for (int i = 0; i < sourceBoard.SquarePack.Length; i++)
            {
                sourcePack = sourceBoard.SquarePack[i];
                for (int j = 0; j < sourcePack.SquareCells.Length; j++)
                {
                    sourceCell = sourcePack.SquareCells[j];
                    sourceColumn = sourceCell.BoardCoorinate.column;
                    sourceRow = sourceCell.BoardCoorinate.row;
                    sourceCell.Initialize(resultNumberArr[sourceRow, sourceColumn]);
                }
            }
        }
    }
}
