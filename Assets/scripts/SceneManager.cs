using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public GameObject sceneRoot_;

    public void Start()
    {
        this.Initialize();
    }

    public void Initialize()
    {
        Debug.Log("Scene Init");
        //일단 인게임 씬만 제어한다

        GameObject sceneObj = Instantiate(new GameObject(), sceneRoot_.transform) as GameObject;
        sceneObj.transform.localPosition = Vector3.zero;
        sceneObj.transform.localRotation = Quaternion.identity;
        sceneObj.transform.localScale = Vector3.one;
        sceneObj.name = "GameScene";

        sceneObj.transform.localPosition = Vector3.zero;
        IScene scene = sceneObj.AddComponent<scene.Game>() as IScene;
    }
}
