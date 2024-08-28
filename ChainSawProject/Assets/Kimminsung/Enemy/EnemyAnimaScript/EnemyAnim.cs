using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnim : MonoBehaviour
{
    Animator anim;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    public void KnifeEnemRun()
    {
        anim.SetBool("Knife_Run", true);
    }
    public void KnifeEnemRunf()
    {
        anim.SetBool("Knife_Run", false);
    }

    public void KnifeEnemDie()
    {
        anim.SetBool("Knife_Die", true);
    }
}
