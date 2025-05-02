using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Owner : MonoBehaviour
{
    public int touchEarnGold = 1;
    public ScaleEffect scaleEffect;

    [Header("Effect Settings")]
    public GameObject clickEffectPrefab; // ✨ 넣을 프리팹
    public Vector3 effectOffset = Vector3.zero; // 위치 조정 (원하면)

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
        }
    }
}
