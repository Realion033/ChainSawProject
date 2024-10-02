using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject laserPrefab; // ������ ������
    public Transform firePoint; // �������� �߻��� ��ġ
    public float laserSpeed = 10f; // ������ �ӵ�
    public float fireRate = 1f; // �߻� �ӵ� (�ʴ� �߻� ��)
    private float nextFireTime; // ���� �߻� �ð�

    private void Update()
    {
        if (Time.time >= nextFireTime)
        {
                FireLaser();
                nextFireTime = Time.time + 1f / fireRate;
        }
    }

    void FireLaser()
    {
        if (laserPrefab && firePoint)
        {
            // ������ ����
            GameObject laser = Instantiate(laserPrefab, firePoint.position, firePoint.rotation);

            // �������� Rigidbody2D�� ������ �ӵ��� ����
            Rigidbody2D rb = laser.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = firePoint.right * laserSpeed; // �������� �߻� �������� �̵�
            }

            // �������� ���� �ֱ⸦ ���� (��: 3�� �� ����)
            Destroy(laser, 3f);
        }
    }
}
