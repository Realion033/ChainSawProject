using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Shuffle : MonoBehaviour
{
    private List<Card> _cards = new List<Card>();
    [SerializeField] private GameObject _cardPrefab;
    private List<GameObject> _spawnedCards = new List<GameObject>();

    public int Card;

    private void Awake()
    {
        for (int i = 1; i < 7; i++)
        {
            string assetPath = $"Assets/Saironee/SO/{i}.asset";
            _cards.Add(AssetDatabase.LoadAssetAtPath<Card>(assetPath));
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
        float offset = 350f;  // 카드 간격
        float startX = -(cnt - 1) * offset / 2;  // 카드 배열 시작점

        for (int i = 0; i < cnt; i++)
        {
            // 카드 생성
            GameObject card = Instantiate(_cardPrefab, transform);

            // RectTransform을 통해 위치 설정
            RectTransform rectTransform = card.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector2(startX + i * offset, 0);

            // 생성된 카드 리스트에 추가
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
