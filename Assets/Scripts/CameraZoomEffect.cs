using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoomEffect : MonoBehaviour
{
    public float zoomSize = 2f; // 줌인할 크기
    public float zoomDuration = 0.2f; // 줌인 시간
    public float holdDuration = 0.1f; // 줌 유지 시간
    public float returnDuration = 0.3f; // 복귀 시간

    private Camera cam;
    private Coroutine zoomCoroutine;
    private float originalSize;
    private Vector3 originalPos;

    private bool isZooming = false;

    void Awake()
    {
        cam = Camera.main;

        originalSize = cam.orthographicSize;
        originalPos = cam.transform.position;
    }

    public void ZoomToPosition(Vector3 targetWorldPos)
    {
        if (isZooming) return;

        zoomCoroutine = StartCoroutine(ZoomRoutine(targetWorldPos));
    }

    private void ResetCamera()
    {
        cam.orthographicSize = originalSize;
        cam.transform.position = originalPos;
    }

    IEnumerator ZoomRoutine(Vector3 targetPos)
    {
        isZooming = true;

        Vector3 zoomTargetPos = new Vector3(targetPos.x, targetPos.y, originalPos.z); // Z는 유지

        // 1. 줌인
        yield return ZoomCamera(zoomTargetPos, zoomSize, zoomDuration);

        // 2. 잠깐 유지
        yield return new WaitForSeconds(holdDuration);

        // 3. 원래 위치로 복귀
        yield return ZoomCamera(originalPos, originalSize, returnDuration);

        isZooming = false;
    }


    IEnumerator ZoomCamera(Vector3 targetPos, float targetSize, float duration)
    {
        Vector3 startPos = cam.transform.position;
        float startSize = cam.orthographicSize;

        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / duration;
            cam.transform.position = Vector3.Lerp(startPos, targetPos, t);
            cam.orthographicSize = Mathf.Lerp(startSize, targetSize, t);
            yield return null;
        }
    }
}
