using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using UnityEngine;
using System.Collections;

namespace controller
{
    class StageData
    {
        public model.StageData Data {get { return _stagedata; } }
        private model.StageData _stagedata = null;

        public IEnumerator LoadStageData(string stageName)
        {
            string dir = DefineData.StreamingAssetsPath;

            string sourcePath = string.Format("{0}{1}.json", dir, stageName);

            WWW www = new WWW(sourcePath);
            yield return www;

            Debug.Log(string.Format("{0} : {1}", sourcePath, www.isDone));

            string sourceStr = this.ByteToString(www.bytes);

            _stagedata = new model.StageData();
            JsonUtility.FromJsonOverwrite(sourceStr, _stagedata);
        }

        private void CheckMapData(model.StageData stageData)
        {
            for(int i=0; i<stageData.cellDataList.Count; i++)
            {
                Debug.Log(string.Format("[{0}, {1}] : {2}", stageData.cellDataList[i].column, stageData.cellDataList[i].row, stageData.cellDataList[i].number));
            }
        }

        // 바이트 배열을 String으로 변환 
        private string ByteToString(byte[] strByte)
        {
            string str = Encoding.ASCII.GetString(strByte);
            return str;
        }

        // String을 바이트 배열로 변환 
        private byte[] StringToByte(string str)
        {
            byte[] strByte = Encoding.ASCII.GetBytes(str);
            return strByte;
        }
    }

}
