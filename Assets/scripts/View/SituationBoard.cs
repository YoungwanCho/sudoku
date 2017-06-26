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
        private Stopwatch _stopWatch = new Stopwatch();

        public void Start()
        {
            _stopWatch.Start();
        }

        public void OnEnable()
        {
            _stopWatch.Reset();
        }

        public void Update()
        {
            timeText.text = string.Format("{0}:{1}:{2}", _stopWatch.Elapsed.Hours.ToString("00"), _stopWatch.Elapsed.Minutes.ToString("00"), _stopWatch.Elapsed.Seconds.ToString("00"));
        }

        public void UpdateEmptyCellCount(int count)
        {
            emptyCellCountText_.text = count.ToString();
        }
    }
}

