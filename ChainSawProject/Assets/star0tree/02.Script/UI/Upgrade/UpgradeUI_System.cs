using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeUI_System : MonoBehaviour
{
    // Start is called before the first frame update
    private int RandomNomber;
    private void Awake()
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        
    }

    public GradeType gradeTypeProperty {  get; private set; }

    private void GradeDetermination()
    {
        RandomNomber = Random.Range(1, 100);
        if (RandomNomber == 100)
        {

        }
        else if (RandomNomber <10)
        {

        }
        else if (RandomNomber == 100)
        {

        }
    }
}
// 40, 30, 20, 9, 1