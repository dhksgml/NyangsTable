using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    public float speed = 1.0f;  // 배경이 움직이는 속도
    public bool moveHorizontally = true; // 좌우 이동 여부
    public bool moveVertically = false; // 위아래 이동 여부

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