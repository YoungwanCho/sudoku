using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryManager : UnitySingleton<FactoryManager>
{
    public T InstantiateGameobject<T>(GameObject sourceObj, Transform parent, Vector3 localPos, Quaternion localRot, Vector3 localScale, string objName, string layerName)
    {
        GameObject obj = this.InstantiateGameobject(sourceObj, parent, localPos, localRot, localScale, objName, layerName);
        return obj.GetComponent<T>();
    }

    public T InstantiateGameObject<T>(string sourcePath, Transform parent, Vector3 localPos, Quaternion localRot, Vector3 localScale, string objName, string layerName)
    {
        GameObject obj = this.InstantiateGameobject(sourcePath, parent, localPos, localRot, localScale, objName, layerName);
        return obj.GetComponent<T>();
    }

    public GameObject InstantiateGameobject(GameObject sourceObj, Transform parent, Vector3 localPos, Quaternion localRot, Vector3 localScale, string objName, string layerName)
    {
        GameObject obj = Instantiate(sourceObj, parent);

        obj.transform.localPosition = localPos;
        obj.transform.localRotation = localRot;
        obj.transform.localScale = localScale;
        obj.layer = LayerMask.NameToLayer(layerName);

        if(objName != string.Empty)
        {
            obj.name = objName;
        }

        return obj;
    }

    public GameObject InstantiateGameobject(string sourcePath, Transform parent, Vector3 localPos, Quaternion localRot, Vector3 localScale, string objName, string layerName)
    {
        GameObject prefab = Resources.Load<GameObject>(sourcePath);
        return this.InstantiateGameobject(prefab, parent, localPos, localRot, localScale, objName, layerName);
    }

}
