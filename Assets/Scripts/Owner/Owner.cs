using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Owner : MonoBehaviour
{
    public int touchEarnGold = 1;
    public int touchEarnSpecialGold = 5;

    private float specialProbability = 0.05f; //5% 확률

    public ScaleEffect scaleEffect;

    [Header("Effect Settings")]
    public GameObject clickEffectPrefab;
    public GameObject clickSpecialEffectPrefab;

    public Vector3 effectOffset = new Vector3(0, 0, 0.1f); // 위치 조정 (원하면)

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hitData = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hitData.collider != null)
            {
                if (hitData.collider.gameObject == gameObject)
                {
                    TryDoClick();
                }
            }
        }
    }

    void TryDoClick()
    {
        if (Random.value < specialProbability)
        {
            SpecialAction();
        }
        else
        {
            NomalAction();
        }
    }

    void SpecialAction()
    {
        GoldManager.Instance.AddGold(touchEarnSpecialGold);

        if (scaleEffect != null)
            scaleEffect.ScaleUpAndDown();

        FindObjectOfType<CameraZoomEffect>().ZoomToPosition(transform.position + new Vector3(0, 0.5f, 0));

        //  이펙트 프리팹 생성
        if (clickSpecialEffectPrefab != null)
        {
            if (AudioManager.Instance != null)
            {
                // 코인 효과음 재생
                AudioManager.Instance.PlaySpecialCoinSFX();

            }
            GameObject effect = Instantiate(clickSpecialEffectPrefab, transform.position + effectOffset, Quaternion.Euler(-90f, 0, 0));
            
        }
    }

    void NomalAction()
    {
        GoldManager.Instance.AddGold(touchEarnGold);

        if (scaleEffect != null)
            scaleEffect.ScaleUpAndDown();

        //  이펙트 프리팹 생성
        if (clickEffectPrefab != null)
        {
            if (AudioManager.Instance != null)
            {
                // 코인 효과음 재생
                AudioManager.Instance.PlayCoinSFX();

            }
            GameObject effect = Instantiate(clickEffectPrefab, transform.position + effectOffset, Quaternion.identity);
            Destroy(effect, 1f); // 1초 후 자동 삭제
        }
    }
}
