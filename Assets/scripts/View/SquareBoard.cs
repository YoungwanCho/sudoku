using UnityEngine;
using System.Collections;

namespace view
{
    public class SquareBoard : MonoBehaviour
    {
        private view.SquarePack[] _squarePacks = null;

        public void Awake()
        {
           _squarePacks = CreateSquarePack();
        }

        public void Initialize()
        {
        }

        private view.SquarePack[] CreateSquarePack()
        {
            view.SquarePack[] squarePack = new view.SquarePack[DefineData.MAX_PACK_COUNT];
            GameObject prefab = Resources.Load(DefineData.PREFAB_SQUAREPACK_PATH) as GameObject;
            GameObject obj = null;
            Vector3 pos = Vector3.zero;
            for (int i = 0; i < squarePack.Length; i++)
            {
                obj = Instantiate(prefab, this.transform) as GameObject;
                obj.transform.localPosition = pos;
                obj.transform.localRotation = Quaternion.identity;
                obj.transform.localScale = Vector3.one;
                obj.name = string.Format("Pack {0}", i);

                squarePack[i] = obj.GetComponent<view.SquarePack>();
                squarePack[i].Initialize(i);
            }
            return squarePack;
        }

    }
}