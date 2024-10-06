using UnityEngine;


[CreateAssetMenu(menuName = "CreateSO/LevelSO")]
public class LevelSO : ScriptableObject
{
    public GameObject level;
    
    public Vector3 SpawnPoints;
}
