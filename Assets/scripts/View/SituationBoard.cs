using UnityEngine;
using System;
using UnityEngine.UI;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

namespace view
{
    public class SituationBoard : MonoBehaviour
    {
        [SerializeField]
        private Image backGroundImage_ = null;
        [SerializeField]
        private Text timeText_ = null;
        [SerializeField]
        private Text emptyCellCountText_ = null;

        public void UpdatePlayerTime(string str)
        {
            timeText_.text = str;
        }

        public void UpdateEmptyCellCount(int count)
        {
            emptyCellCountText_.text = count.ToString();
        }
    }
}

