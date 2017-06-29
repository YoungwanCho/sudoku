using UnityEngine;
using System;
using UnityEngine.UI;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

namespace view
{
    public class SituationBoard : MonoBehaviour
    {
        public Image backGroundImage_ = null;
        public Text timeText = null;
        public Text emptyCellCountText_ = null;

        public void UpdatePlayerTime(string str)
        {
            timeText.text = str;
        }

        public void UpdateEmptyCellCount(int count)
        {
            emptyCellCountText_.text = count.ToString();
        }
    }
}

