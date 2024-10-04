using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Shuffle : MonoBehaviour
{
    private List<Card> _cards = new List<Card>();
    [SerializeField] private GameObject _cardPrefab;
    private List<GameObject> _spawnedCards = new List<GameObject>();

    public int Card;

    int i;

    private void Awake()
    {
        while(true)
        {
            string assetPath = $"Assets/Saironee/SO/{i}.asset";
            if (AssetDatabase.LoadAssetAtPath<Card>(assetPath) == null)
                break;
            _cards.Add(AssetDatabase.LoadAssetAtPath<Card>(assetPath));
            i++;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ClearCards();
            CardShuffle(Card);
        }
    }

    public void CardShuffle(int cnt)
    {
        float offset = 350f;  // ī�� ����
        float startX = -(cnt - 1) * offset / 2;  // ī�� �迭 ������

        for (int i = 0; i < cnt; i++)
        {
            // ī�� ����
            GameObject card = Instantiate(_cardPrefab, transform);

            // RectTransform�� ���� ��ġ ����
            RectTransform rectTransform = card.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector2(startX + i * offset, 0);

            // ������ ī�� ����Ʈ�� �߰�
            _spawnedCards.Add(card);
        }
    }

    private void ClearCards()
    {
        foreach (var card in _spawnedCards)
        {
            Destroy(card);
        }
        _spawnedCards.Clear();
    }
}
