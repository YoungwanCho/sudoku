using UnityEngine;

namespace model
{
    public class SquareBoard
    {
        private model.SquarePack[] _squarePacks = new model.SquarePack[DefineData.MAX_PACK_COUNT];

        public SquareBoard()
        {
            CreatePacks();
        }

        public void Initialize()
        {
            Debug.Log("Square Board Model Init");
            for (int i = 0; i < _squarePacks.Length; i++)
            {
                _squarePacks[i].Initialize();
            }
        }

        public void CreatePacks()
        {
            for (int i = 0; i < _squarePacks.Length; i++)
            {
                _squarePacks[i] = new model.SquarePack(i);
            }
        }
    }
}
