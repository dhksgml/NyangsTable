using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    public float speed = 1.0f;  // ����� �����̴� �ӵ�
    public bool moveHorizontally = true; // �¿� �̵� ����
    public bool moveVertically = false; // ���Ʒ� �̵� ����

    void Update()
    {
        Vector3 moveDirection = Vector3.zero;

        if (moveHorizontally)
            moveDirection.x = speed * Time.deltaTime;
        if (moveVertically)
            moveDirection.y = speed * Time.deltaTime;

        transform.position += moveDirection;
    }
}