using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using static UnityEngine.InputManagerEntry;

public class EffectIcon : MonoBehaviour
{
    [SerializeField] private GameObject ImagePrefabs = null, EffectPanel;
    [SerializeField] private Sprite Health_UpImage;
    [SerializeField] private Sprite EnergyRecovery_UpImage;
    [SerializeField] private Sprite EnergyChargeAmount_UpImage;
    [SerializeField] private Sprite DashSpeed_UpImage;
    [SerializeField] private Sprite Atk_UPImage;
    [SerializeField] private Sprite AtkSpeed_UpImage;

    private Image image;
    private Image childImage;
    UpgradeUI_System upgradeSystem;

    private void Awake()
    {
        upgradeSystem = GameObject.Find("UpGradeUI").GetComponent<UpgradeUI_System>();
        image = ImagePrefabs.GetComponent<Image>();
    }
    private void OnEnable()
    {
        upgradeSystem.onDisplay += EffectStorage;
    }
    private void OnDisable()
    {
        upgradeSystem.onDisplay -= EffectStorage;
    }

    Dictionary<int,int> Effects = new Dictionary<int,int>();

    public void EffectStorage(int id, int grade)
    {
        Debug.Log("실행");
        if(Effects.ContainsKey(id))
        {
            Effects.Remove(id);
            Effects.Add(id, grade);
        }
        else
        {
            Effects.Add(id, grade);
        }
        AwakeEffectStorage();
    }
    private void AwakeEffectStorage()
    {
        Debug.Log("실행2");
        foreach (Transform child in EffectPanel.transform)
        {
            Destroy(child.gameObject);
        }
        foreach (var item in Effects.OrderByDescending(i => i.Value))
        {
            Debug.Log($"{item.Key},{item.Value}");
            EffectDisplay(item.Key, item.Value);
        }
    }
    private void EffectDisplay(int kind, int grade)
    {
        Sprite targetSprite = null;
        switch (kind)
        {
            case 1: targetSprite = Health_UpImage; break;
            case 2: targetSprite = EnergyRecovery_UpImage; break;
            case 3: targetSprite = EnergyChargeAmount_UpImage; break;
            case 4: targetSprite = DashSpeed_UpImage; break;
            case 5: targetSprite = Atk_UPImage; break;
            case 6: targetSprite = AtkSpeed_UpImage; break;
        }
        Debug.Log($"{targetSprite}");
        switch (grade)
        {
            case 2: image.color = Color.black; break;
            case 3: image.color = Color.blue; break;
            case 4: image.color = Color.magenta; break;
            case 5: image.color = Color.yellow; break;
            case 6: image.color = Color.red; break;
        }
        Debug.Log($"{image.color}");
        image.sprite = targetSprite;
        Debug.Log($"{image.sprite}");
        Debug.Log("아이콘 더 넣어진다.");
        Instantiate(ImagePrefabs, EffectPanel.transform);
    }
}
