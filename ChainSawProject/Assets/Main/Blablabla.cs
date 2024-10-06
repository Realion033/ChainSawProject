using UnityEngine;

public class Blablabla : MonoBehaviour
{
    public Collision2D GroundChecker;
    public LayerMask GroundLayer;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == GroundLayer)
        {
            //더 이상 "낙하"하지 않는다!!
        }
    }
}
