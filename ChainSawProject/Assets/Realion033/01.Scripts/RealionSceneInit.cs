using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RealionSceneInit : MonoBehaviour
{
    public Sprite sprite; // 마우스를 따라다닐 스프라이트
    private Image image; // UI Image 컴포넌트

    private void Awake()
    {
        // Canvas의 자식으로 빈 게임 오브젝트를 만들고 Image 컴포넌트를 추가합니다.
        GameObject uiObject = new GameObject("MouseFollower");
        uiObject.transform.SetParent(GameObject.Find("Canvas").transform); // Canvas라는 이름의 UI Canvas가 있어야 합니다.

        // Image 컴포넌트를 추가하고 스프라이트를 설정합니다.
        image = uiObject.AddComponent<Image>();
        image.sprite = sprite;
        image.color = Color.red;

        // 마우스 커서를 숨깁니다.
        Cursor.visible = false;
    }

    private void Update()
    {
        // 마우스 위치를 가져옵니다.
        Vector2 mousePosition = Input.mousePosition;

        // UI 요소의 위치를 마우스 위치로 설정합니다.
        image.rectTransform.position = mousePosition;
    }

}
