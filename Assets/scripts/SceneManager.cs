using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public enum SCENE {MAINLOBBY = 0, LEVELSELECT, INGAME, RESULT, MAX_COUNT}

    private IScene[] _scenes = new IScene[(int)SCENE.MAX_COUNT];
    private GameObject[] _sceneObjects = new GameObject[(int)SCENE.MAX_COUNT];

    private scene.LevelSelect _levelSelectScene = null;
    private scene.MainLobby _mainLobbyScene = null;
    private scene.InGame _inGameScene = null;
    private scene.GameResult _gameResultScene = null;

    public void Awake()
    {
        CreateAllScene();
    }

    public void Start()
    {
        Initialize();
        ChangeScene(SCENE.MAINLOBBY);
    }

    public int GetSelectStageIndex()
    {
        return _levelSelectScene.LevelSelectIndex;
    }

    private void Initialize()
    { 
        for (int i=0; i<_scenes.Length; i++)
        {
            _scenes[i].Initialize(this);
        }
    }

    public void ChangeScene(SCENE current)
    {
        int sceneIndex = (int)current;
        for (int i = 0; i < _scenes.Length; i++)
        {
            _sceneObjects[i].SetActive(i == sceneIndex);
            if (i == sceneIndex)
            {
                _scenes[i].Initialize(this);
            }
        }
    }

    private void CreateAllScene()
    {
        string[] prefabPaths = {
            DefineData.PREFAB_SCENE_MAINLOBBY_PATH,
            DefineData.PREFAB_SCENE_LEVELSELECT_PATH,
            DefineData.PREFAB_SCENE_INGAME_PATH,
            DefineData.PREFAB_SCENE_GAMERESULT_PATH
        };
        for(int i=0; i< _scenes.Length; i++)
        {
            _sceneObjects[i] = InstantiateScene(prefabPaths[i], this.transform);
            _scenes[i] = _sceneObjects[i].GetComponent<IScene>();

            switch((SCENE)i)
            {
                case SCENE.MAINLOBBY:
                    _mainLobbyScene = _sceneObjects[i].GetComponent<scene.MainLobby>();
                    break;

                case SCENE.LEVELSELECT:
                    _levelSelectScene = _sceneObjects[i].GetComponent<scene.LevelSelect>();
                    break;

                case SCENE.INGAME:
                    _inGameScene = _sceneObjects[i].GetComponent<scene.InGame>();
                    break;

                case SCENE.RESULT:
                    _gameResultScene = _sceneObjects[i].GetComponent<scene.GameResult>();
                    break;
            }
        }
    }

    private GameObject InstantiateScene(string prefabPath, Transform parent)
    {
        Debug.Log(string.Format("InstantiateScene : {0}", prefabPath));
        GameObject prefab = Resources.Load<GameObject>(prefabPath) as GameObject;
        GameObject obj = Instantiate(prefab, parent);
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localRotation = Quaternion.identity;
        obj.transform.localScale = Vector3.one;
        obj.layer = LayerMask.NameToLayer("UI");
        return obj;
    }
}
