namespace controller
{
    public class InputController
    {
        public delegate void OnClick(int n, int n2);

        private controller.ModelController _modelCtrl = null;
        private controller.ViewController _viewCtrl = null;

        public InputController(controller.ModelController model, controller.ViewController view)
        {
            this._modelCtrl = model;
            this._viewCtrl = view;
        }

        public void OnClickCell(int column, int row)
        {
            UnityEngine.Debug.Log(string.Format("OnClick : [{0}, {1}]", column, row));
        }

    }
}
