using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainLobby : MonoBehaviour, IScene
{
    private view.MainLobby _mainLobby = null;
    private SceneManager _sceneManager = null;

    public void Enter()
    {
    }

    public void Exit()
    {
    }

    public void Initialize(SceneManager manager)
    {
        _sceneManager = manager;
    }

    public void Awake()
    {
        _mainLobby = CreateMainLobbyView();
    }

    public void Start()
    {
        _mainLobby.Initialize(this.NewGameStart);
    }

    public void NewGameStart(GameObject obj)
    {
        _sceneManager.ChangeScene(SceneManager.SCENE.LEVELSELECT);
    }

    private view.MainLobby CreateMainLobbyView()
    {
        GameObject prefab = Resources.Load<GameObject>(DefineData.PREFAB_VIEW_MAINLOBBY_PATH);
        GameObject obj = null;
        obj = Instantiate(prefab, this.transform);
        obj.transform.localPosition = Vector3.one;
        obj.transform.localRotation = Quaternion.identity;
        obj.transform.localScale = Vector3.one;
        obj.name = "MainLobby";
        return obj.GetComponent<view.MainLobby>();
    }
}
