using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingTextManager : MonoBehaviour
{
    [SerializeField] private GameObject floatingTextParent;
    private Queue<GameObject> pool = new Queue<GameObject>();

    private void OnEnable()
    {
        GoldManager.Instance.OnGoldAdded += HandleGoldAdded;
    }

    private void OnDisable()
    {
        if (GoldManager.Instance != null)
            GoldManager.Instance.OnGoldAdded -= HandleGoldAdded;
    }

    public GameObject GetObject()
    {
        if (pool.Count > 0)
        {
            return pool.Dequeue();
        }
        else
        {
            return Instantiate(floatingTextParent);
        }
    }

    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
        pool.Enqueue(obj);
    }

    private void HandleGoldAdded(int goldAmount, Vector3 position)
    {
        ShowFloatingText(goldAmount.ToString(), position);
    }

    public void ShowFloatingText(string text, Vector3 worldPosition)
    {
        if (floatingTextParent)
        {
            GameObject prefab = GetObject();
            prefab.transform.position = worldPosition;
            prefab.transform.rotation = Quaternion.identity;
            prefab.SetActive(true);

            var textMesh = prefab.GetComponentInChildren<TextMesh>();
            if (textMesh != null)
                textMesh.text = text;

            StartCoroutine(ReturnAfterDelay(prefab, 1f));
        }
    }

    private IEnumerator ReturnAfterDelay(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);
        ReturnObject(obj);
    }

}
