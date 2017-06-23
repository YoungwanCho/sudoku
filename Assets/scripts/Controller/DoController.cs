using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace controller
{
    public class DoController
    {
        private Stack<model.Do> _undoStack = new Stack<model.Do>();
        private Stack<model.Do> _redoStack = new Stack<model.Do>();

        public void Initialize()
        {
            _undoStack.Clear();
            _redoStack.Clear();
        }

        public void UndoPush(model.Do undo)
        { 
            _undoStack.Push(undo);
            Debug.Log(string.Format("UndoStackCount : {0}", _undoStack.Count));
        }

        public model.Do UndoPop()
        {
            Debug.Log(string.Format("UndoStackCount : {0}", _undoStack.Count));
            return _undoStack.Pop();            
        }

        public int GetUndoStackCount()
        {
            return _undoStack.Count;
        }

        public void RedoPush(model.Do redo)
        {
            Debug.Log(string.Format("RedoStackCount : {0}", _redoStack.Count));
            _redoStack.Push(redo);
        }

        public model.Do RedoPop()
        {
            Debug.Log(string.Format("RedoStackCount : {0}", _redoStack.Count));
            return _redoStack.Pop();
        }

        public int GetRedoStackCount()
        {
            return _redoStack.Count;
        }
    }
}
