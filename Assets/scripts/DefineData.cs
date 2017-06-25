using UnityEngine;

public static class DefineData
{
    public const int MAX_CELL_COUNT = 9;
    public const int MAX_PACK_COUNT = 9;
    public const int MAX_ROW_COUNT = 3;
    public const int MAX_COLUMN_COUNT = 3;
    public const int MAX_NUMBER_VALUE = 9;

    public const string PREFAB_SQUARECELL_PATH = "Prefab/SquareCell";
    public const string PREFAB_SQUAREPACK_PATH = "Prefab/SquarePack";
    public const string PREFAB_SQUAREBOARD_PATH = "Prefab/SquareBoard";
    public const string PREFAB_SITUATIONBOARD_PATH = "Prefab/SituationBoard";

    public const string PREFAB_INPUT_NUMBER_PAD_PATH = "Prefab/InputNumberButton";
    public const string PREFAB_DEFAULT_BUTTON_PATH = "Prefab/DefaultButton";

    public const string PREFAB_VIEW_MAINLOBBY_PATH = "Prefab/MainLobby";
    public const string PREFAB_VIEW_LEVELSELECT_PATH = "Prefab/LevelSelect";
    public const string PREFAB_VIEW_GAMERESULT_PATH = "Prefab/GameResult";

    public const string PREFAB_SCENE_MAINLOBBY_PATH = "Prefab/Scene/MainLobby";
    public const string PREFAB_SCENE_LEVELSELECT_PATH = "Prefab/Scene/LevelSelect";
    public const string PREFAB_SCENE_INGAME_PATH = "Prefab/Scene/InGame";
    public const string PREFAB_SCENE_GAMERESULT_PATH = "Prefab/Scene/GameResult";



    public static Vector2 CELLSIZE = new Vector2(100, 100);
    public static Vector2 NUMBERSIZE = new Vector2(100, 100);

    public static Vector2 CELL_INTERVAL = new Vector2(1, 1);

    public static Vector2 PACKSIZE = new Vector2(
        CELLSIZE.x * MAX_COLUMN_COUNT + (CELL_INTERVAL.x * 2),
        CELLSIZE.y * MAX_ROW_COUNT + (CELL_INTERVAL.y * 2));

    public static Vector2 PACK_INTERVAL = new Vector2(2, 2);

    public static Vector2 BOARDSIZE = new Vector2(
    PACKSIZE.x * MAX_COLUMN_COUNT + (PACK_INTERVAL.x * 2),
    PACKSIZE.y * MAX_ROW_COUNT + (PACK_INTERVAL.y * 2));

    public static string StreamingAssetsPath
    {
        get
        {
            string StreamingPath = string.Empty;
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
    StreamingPath = "file://" + UnityEngine.Application.streamingAssetsPath + "/";
#elif UNITY_ANDROID
	StreamingPath = UnityEngine.Application.streamingAssetsPath + "/";
#elif UNITY_IOS
	StreamingPath = "file://" + UnityEngine.Application.streamingAssetsPath + "/";
#endif
            UnityEngine.Debug.Log(string.Format("streamingAssetsPath : {0}", StreamingPath));
            return StreamingPath;
        }
   }
}