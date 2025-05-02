using System.Collections;
using UnityEngine;
using System;
using Unity.VisualScripting;

public class Workstation : MonoBehaviour
{
    public GameObject timerUI;

    public bool isOccupied = false; // ��� ���� Ȯ��
    public Action<int> OnProduce;
    public void Occupy() => isOccupied = true;

    public void Release() => isOccupied = false;
    private Product product;  // ������ ��ǰ

    public void Init(Product product)
    {
        this.product = product;
        timerUI.SetActive(false);
    }

    public IEnumerator Produce()
    {
        if (product == null)
        {
            yield break;
        }

        float adjustedProductionTime = product.ProductionTime;

        //Debug.Log("������..");
        timerUI.SetActive(true);
        CooldownUI cooldownUI = timerUI.GetComponent<CooldownUI>();
        cooldownUI.SetMaxCooldown(adjustedProductionTime);
        cooldownUI.SetCurrentCooldown(adjustedProductionTime);
        yield return new WaitForSeconds(adjustedProductionTime);
        timerUI.SetActive(false);

        OnProduce?.Invoke(product.ResourceAmount);
    }

    public void InVisibleProduct()
    {
        GetComponent<SpriteRenderer>().enabled = false;
    }

    public void CreateProduct(Product product)
    {
        GameObject gameObject = new GameObject();
        gameObject.transform.position = transform.position;
        gameObject.name = product.ProductName;
        SpriteRenderer spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = product.ProductSprite;
    }
}
