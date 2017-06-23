using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using UnityEngine;

namespace controller
{
    class StageData
    {
        private const string dir = "Assets/Resources/";

        public model.StageData LoadStageData(string stageName)
        {
            model.StageData stageData = new model.StageData();
            string str = JsonUtility.ToJson(stageData, prettyPrint: true);
            Debug.Log(str);

            TextAsset file = Resources.Load<TextAsset>("stage1");


            JsonUtility.FromJsonOverwrite(file.text, stageData);

            /*string sourcePath = string.Format("{0}{1}.json", dir, stageName);
            model.StageData stageData = new model.StageData();
            using (StreamReader r = new StreamReader(sourcePath))
            {
                string jsonStr = r.ReadToEnd();                
                JsonUtility.FromJsonOverwrite(jsonStr, stageData);
                //CheckMapData(mapData);
            }*/

            return stageData;
        }

        private void CheckMapData(model.StageData stageData)
        {
            for(int i=0; i<stageData.cellDataList.Count; i++)
            {
                Debug.Log(string.Format("[{0}, {1}] : {2}", stageData.cellDataList[i].column, stageData.cellDataList[i].row, stageData.cellDataList[i].number));
            }
        }
    }
    
        
}
