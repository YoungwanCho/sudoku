using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelect : MonoBehaviour, IScene
{
    public SceneManager _sceneManager;

    private view.LevelSelect _levelSelect = null;

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
        _levelSelect = CreateLevelSelectView();
    } 

    // Use this for initialization
    void Start () {
        _levelSelect.Initialize(this.LevelSelectButton);

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void LevelSelectButton(GameObject obj)
    {
        Debug.Log(string.Format("LevelSelectButton", obj.name));
        _sceneManager.ChangeScene(SceneManager.SCENE.INGAME);
    }

    private view.LevelSelect CreateLevelSelectView()
    {
        GameObject prefab = Resources.Load<GameObject>(DefineData.PREFAB_VIEW_LEVELSELECT_PATH);
        GameObject obj = null;
        obj = Instantiate(prefab, this.transform);
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localRotation = Quaternion.identity;
        obj.transform.localScale = Vector3.one;
        obj.name = "LevelSelct";
        return obj.GetComponent<view.LevelSelect>();
    }
}
