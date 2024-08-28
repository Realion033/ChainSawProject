using UnityEngine;

public class Enemy_Knife : MonoBehaviour
{
    private StateMachine stateMachine;
    public Transform playerTransform;
    public EnemyAnim anim;
    
    
    [SerializeField] private float health = 100;

    void Awake()
    {
        stateMachine = GetComponent<StateMachine>();
       anim = GetComponent<EnemyAnim>();

        stateMachine.Initialize(this);
    }
}
