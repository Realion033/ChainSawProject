using UnityEngine;

public class ParentDestroyer : MonoBehaviour
{
    private GameObject parentObject; // 부모 오브젝트를 저장할 변수

    private void Start()
    {
        // 부모 오브젝트 설정 (부모 오브젝트가 없는 경우도 고려)
        if (transform.parent != null)
        {
            parentObject = transform.parent.gameObject;
        }
        else
        {
            Debug.LogError("부모 오브젝트가 없습니다.");
        }
    }

    private void Update()
    {
        // 부모 오브젝트가 null(파괴됨)이면 자신도 파괴
        if (parentObject == null)
        {
            Destroy(gameObject); // 부모가 파괴되면 자신도 파괴
        }
    }
}
