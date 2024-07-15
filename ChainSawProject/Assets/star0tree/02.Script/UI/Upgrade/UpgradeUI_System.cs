using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeUI_System : MonoBehaviour
{

    [SerializeField] private Player player;

    private Basic Health_Up = new Health_Up();
    private Basic EnergyRecovery_Up = new EnergyRecovery_Up();
    private Basic EnergyChargeAmount_Up = new EnergyChargeAmount_Up();
    private Basic DashSpeed_Up = new DashSpeed_Up();
    private Basic Atk_UP = new Atk_UP();
    private Basic AtkSpeed_Up = new AtkSpeed_Up();

    private SpecialStat specialStat = null;
    private Special special = null;
    private int TypeNomber;
    [SerializeField] private Image form1;
    [SerializeField] private Image form2;
    [SerializeField] private Image form3;
    [SerializeField] private Image form4;
    [SerializeField] private Image form5;
    [SerializeField] private Sprite Health_UpImage;
    [SerializeField] private Sprite EnergyRecovery_UpImage;
    [SerializeField] private Sprite EnergyChargeAmount_UpImage;
    [SerializeField] private Sprite DashSpeed_UpImage;
    [SerializeField] private Sprite Atk_UPImage;
    [SerializeField] private Sprite AtkSpeed_UpImage;
    BasicType basicType;

    List<BasicType> BasicList = new List<BasicType>();
    // 선택된 사진
    private Sprite sprite;
    private UpgradeUI_Form upgradeUI_Form;

    private void Awake()
    {
        //gameObject.SetActive(false);
        BasicList.Add(BasicType.Health_Up);
        BasicList.Add(BasicType.EnergyRecovery_Up);
        BasicList.Add(BasicType.EnergyChargeAmount_Up);
        BasicList.Add(BasicType.DashSpeed_Up);
        BasicList.Add(BasicType.Atk_UP);
        BasicList.Add(BasicType.AtkSpeed_Up);


    }

    private void OnEnable()
    {

        RandBasic(form1, form2, form3);
            RandSpecialStat();
            RandSpecial();
    }

    private void RandBasic(Image form1, Image form2, Image form3)
    {
        for (int i = 0; i < 3;  i++) {
        TypeNomber = Random.Range(1, BasicList.Count);

            basicType = BasicList[TypeNomber];
            switch (basicType)
            {
                case BasicType.Health_Up:
                    sprite = Health_UpImage;
                    break;
                case BasicType.EnergyRecovery_Up:
                    sprite=EnergyRecovery_UpImage;
                    break ;
                case BasicType.EnergyChargeAmount_Up:
                    sprite=EnergyChargeAmount_UpImage;
                    break;
                case BasicType.DashSpeed_Up:
                    sprite=DashSpeed_UpImage;
                    break;
                case BasicType.Atk_UP:
                    sprite=Atk_UPImage;
                    break;
                case BasicType.AtkSpeed_Up:
                    sprite=AtkSpeed_UpImage;
                    break;
            }
            if (i == 1)
            {
                form1.sprite = sprite;
            }
            else if (i == 2)
            {
                form2.sprite = sprite;
            }
            else if(i == 3)
            {
                form3.sprite = sprite;
            }
        }
    }
    public void BasicChose()
    {
        switch (TypeNomber)
        {
            case 1:
                Health_Up.ChangeInformation(player,Health_Up.UpGradeCount);
                break;
            case 2:
                EnergyRecovery_Up.ChangeInformation(player, Health_Up.UpGradeCount);
                break;
            case 3:
                EnergyChargeAmount_Up.ChangeInformation(player, Health_Up.UpGradeCount);
                break;
            case 4:
                DashSpeed_Up.ChangeInformation(player, Health_Up.UpGradeCount);
                break;
            case 5:
                Atk_UP.ChangeInformation(player, Health_Up.UpGradeCount);
                break;
            case 6:
                AtkSpeed_Up.ChangeInformation(player, Health_Up.UpGradeCount);
                break;
        }
        gameObject.SetActive(false);
    }

    private void RandSpecialStat()
    {

    }

    private void RandSpecial()
    {

    }

    private void Color()
    {

    }

}
// 40, 30, 20, 9, 1