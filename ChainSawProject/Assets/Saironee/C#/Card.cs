using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "SO", menuName = "CreateSO / card")]
public class Card : ScriptableObject
{
    [SerializeField]
    private string name;

    public short level;
    public short rank;

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
            case 0:
                assetPath = "Assets/Saironee/SO/blas/BG.asset";
                break;
            case 1:
                assetPath = "Assets/Saironee/SO/blas/BGBG.asset";
                break;
            case 2:
                assetPath = "Assets/Saironee/SO/blas/BGBGBGB.asset";
                break;

        }
        bg = AssetDatabase.LoadAssetAtPath<CardBg>(assetPath);
    }
}
