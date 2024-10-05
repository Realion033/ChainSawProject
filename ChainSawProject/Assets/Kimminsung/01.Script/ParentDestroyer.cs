using UnityEngine;

public class ParentDestroyer : MonoBehaviour
{
    private GameObject parentObject; // �θ� ������Ʈ�� ������ ����

    private void Start()
    {
        // �θ� ������Ʈ ���� (�θ� ������Ʈ�� ���� ��쵵 ���)
        if (transform.parent != null)
        {
            parentObject = transform.parent.gameObject;
        }
        else
        {
            Debug.LogError("�θ� ������Ʈ�� �����ϴ�.");
        }
    }

    private void Update()
    {
        // �θ� ������Ʈ�� null(�ı���)�̸� �ڽŵ� �ı�
        if (parentObject == null)
        {
            Destroy(gameObject); // �θ� �ı��Ǹ� �ڽŵ� �ı�
        }
    }
}
