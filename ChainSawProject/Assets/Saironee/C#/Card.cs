using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "SO", menuName = "CreateSO / card")]
public class Card : ScriptableObject
{
    [System.Serializable]
    public enum RANK
    {
        COMMON,
        UNIQUE,
        UNLOCK,
    }

    [SerializeField]
    private string name;

    public short level;
    public RANK rank;

    public float[] points;

    public Sprite[] icon;

    [HideInInspector]
    public CardBg bg;

    public string text1;
    public string text2;

    private string assetPath;

    [HideInInspector] public string discription => $"{text1}{points[level]}{text2}";

    private void OnValidate()
    {
        switch (rank)
        {
            case RANK.COMMON:
                assetPath = "Assets/Saironee/SO/blas/BG.asset";
                break;
            case RANK.UNIQUE:
                assetPath = "Assets/Saironee/SO/blas/BGBG.asset";
                break;
            case RANK.UNLOCK:
                assetPath = "Assets/Saironee/SO/blas/BGBGBGB.asset";
                break;

        }
        bg = AssetDatabase.LoadAssetAtPath<CardBg>(assetPath);
    }
}
