using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace controller
{
    public class DoController
    {
        private Stack<model.Do> _undoStck = new Stack<model.Do>();

        public void UndoPush(model.Do undo)
        { 
            _undoStck.Push(undo);
            Debug.Log(string.Format("UndoStackCount : {0}", _undoStck.Count));
        }

        public model.Do UndoPeek()
        {
            return _undoStck.Peek();
            Debug.Log(string.Format("UndoStackCount : {0}", _undoStck.Count));
        }
    }
}
