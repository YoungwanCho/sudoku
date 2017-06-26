using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace controller
{
    public class DoController
    {
        private Stack<model.DoAction> _undoStack = new Stack<model.DoAction>();
        private Stack<model.DoAction> _redoStack = new Stack<model.DoAction>();

        public void Initialize()
        {
            _undoStack.Clear();
            _redoStack.Clear();
        }

        public void UndoPush(model.DoAction undo)
        { 
            _undoStack.Push(undo);
            Debug.Log(string.Format("UndoStackCount : {0}", _undoStack.Count));
        }

        public model.DoAction UndoPop()
        {
            Debug.Log(string.Format("UndoStackCount : {0}", _undoStack.Count));
            return _undoStack.Pop();            
        }

        public int GetUndoStackCount()
        {
            return _undoStack.Count;
        }

        public void RedoPush(model.DoAction redo)
        {
            Debug.Log(string.Format("RedoStackCount : {0}", _redoStack.Count));
            _redoStack.Push(redo);
        }

        public model.DoAction RedoPop()
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
