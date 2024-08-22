using UnityEditor;
using UnityEngine;
using UnityEngine;
[CreateAssetMenu(fileName = "SO", menuName = "CreateSO / card")]
public class Card : ScriptableObject
{
    [SerializeField]
    private string name;

    public short rank;

    public float[] points;

    public Sprite[] icon;

    [HideInInspector]
    public CardBg bg;

    public string text1;
    public string text2;

    [HideInInspector] public string discription => $"{text1}{points[rank]}{text2}";

    private void OnValidate()
    {
        string assetPath = "Assets/Saironee/SO/BG.asset";
        bg = AssetDatabase.LoadAssetAtPath<CardBg>(assetPath);
    }
}
