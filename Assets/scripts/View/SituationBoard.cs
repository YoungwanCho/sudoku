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

        public void Update()
        {
            timeText.text = _stopWatch.Elapsed.ToString();
        }

        public void UpdateEmptyCellCount(int count)
        {
            emptyCellCountText_.text = count.ToString();
        }
    }
}

