using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class UpgradeUI_System : MonoBehaviour
{
    public delegate void OnDisplay(int BasicType, int count);
    public OnDisplay onDisplay;

    [SerializeField] private Player player;

    private readonly Dictionary<BasicType, Basic> upgrades = new Dictionary<BasicType, Basic>
    {
        { BasicType.Health_Up, new Health_Up() },
        { BasicType.EnergyRecovery_Up, new EnergyRecovery_Up() },
        { BasicType.EnergyChargeAmount_Up, new EnergyChargeAmount_Up() },
        { BasicType.DashSpeed_Up, new DashSpeed_Up() },
        { BasicType.Atk_UP, new Atk_UP() },
        { BasicType.AtkSpeed_Up, new AtkSpeed_Up() }
    };

    private BasicType? lastSelectedType = null;

    [SerializeField] private Image form1;
    [SerializeField] private Image form2;
    [SerializeField] private Sprite Health_UpImage;
    [SerializeField] private Sprite EnergyRecovery_UpImage;
    [SerializeField] private Sprite EnergyChargeAmount_UpImage;
    [SerializeField] private Sprite DashSpeed_UpImage;
    [SerializeField] private Sprite Atk_UPImage;
    [SerializeField] private Sprite AtkSpeed_UpImage;
    [SerializeField] private Sprite None;

    [SerializeField] private TMP_Text form1_Text;
    [SerializeField] private TMP_Text form2_Text;

    private List<BasicType> BasicList = new List<BasicType>();
    private Sprite sprite;
    private string effectName;

    private void Awake()
    {
        gameObject.SetActive(false);
        BasicList.AddRange(upgrades.Keys);
    }

    private void OnEnable()
    {
        RandBasic(form1, form1_Text);
        RandBasic(form2, form2_Text, lastSelectedType);
    }

    private void RandBasic(Image form, TMP_Text formText, BasicType? excludeType = null)
    {
        if (BasicList.Count < 1)
        {
            form.sprite = None;
            form.color = Color.white;
            formText.text = "모든 업그레이드가 다 되었습니다.";
        }
        else
        {
            List<BasicType> availableTypes = new List<BasicType>(BasicList);
            if (excludeType.HasValue && BasicList.Count > 1)
            {
                availableTypes.Remove(excludeType.Value);
            }
            int typeIndex = Random.Range(0, availableTypes.Count);
            Debug.Log(availableTypes[typeIndex]);
            if (upgrades[availableTypes[typeIndex]].UpGradeCount > 5)
            {
                form.sprite = None;
                form.color = Color.white;
                formText.text = "모든 업그레이드가 다 되었습니다.";
            }
            else
            {
                BasicType selectedType = availableTypes[typeIndex];
                lastSelectedType = selectedType;
                switch (selectedType)
                {
                    case BasicType.Health_Up:
                        sprite = Health_UpImage;
                        effectName = "체력 증가";
                        break;
                    case BasicType.EnergyRecovery_Up:
                        sprite = EnergyRecovery_UpImage;
                        effectName = "에너지 회복\n속도 증가";
                        break;
                    case BasicType.EnergyChargeAmount_Up:
                        sprite = EnergyChargeAmount_UpImage;
                        effectName = "에너지 충전량 증가";
                        break;
                    case BasicType.DashSpeed_Up:
                        sprite = DashSpeed_UpImage;
                        effectName = "대쉬 거리와\n대쉬 속도 증가";
                        break;
                    case BasicType.Atk_UP:
                        sprite = Atk_UPImage;
                        effectName = "공격력 증가";
                        break;
                    case BasicType.AtkSpeed_Up:
                        sprite = AtkSpeed_UpImage;
                        effectName = "공격 속도 증가";
                        break;
                }
                BasicColor(selectedType, form);
                form.sprite = sprite;
                formText.text = effectName;
            }
        }
    }
    public void BasicChose()
    {
        GameObject clickTarget = EventSystem.current.currentSelectedGameObject;
        switch (clickTarget.name)
        {
            case "Panel":
                ApplyEffect(form1_Text.text);
                break;
            case "Panel (1)":
                ApplyEffect(form2_Text.text);
                break;
        }
        gameObject.SetActive(false);
    }
    private void ApplyEffect(string text)
    {
        Debug.Log($"{text} 효과작동 적용");
        switch (text)
        {
            case "체력 증가":
                ApplyUpgrade(BasicType.Health_Up, 1);
                break;
            case "에너지 회복\n속도 증가":
                ApplyUpgrade(BasicType.EnergyRecovery_Up, 2);
                break;
            case "에너지 충전량 증가":
                ApplyUpgrade(BasicType.EnergyChargeAmount_Up, 3);
                break;
            case "대쉬 거리와\n대쉬 속도 증가":
                ApplyUpgrade(BasicType.DashSpeed_Up, 4);
                break;
            case "공격력 증가":
                ApplyUpgrade(BasicType.Atk_UP, 5);
                break;
            case "공격 속도 증가":
                ApplyUpgrade(BasicType.AtkSpeed_Up, 6);
                break;
            default:
                break;
        }
    }
    private void ApplyUpgrade(BasicType type, int id)
    {
        upgrades[type].ChangeInformation(player);
        onDisplay?.Invoke(id, upgrades[type].UpGradeCount);
        if (upgrades[type].UpGradeCount == 6)
        {
            Debug.Log(BasicList.Count);
            BasicList.Remove(type);
        }
    }
    private void BasicColor(BasicType type, Image Icon)
    {
        int Grade = upgrades[type].UpGradeCount;
        switch (Grade)
        {
            case 1:
                Icon.color = Color.black;
                break;
            case 2:
                Icon.color = Color.blue;
                break;
            case 3:
                Icon.color = Color.magenta;
                break;
            case 4:
                Icon.color = Color.yellow;
                break;
            case 5:
                Icon.color = Color.red;
                break;
        }
    }
}
