
using UnityEngine;

public class Product
{
    private string _productName;
    private float _productionTime;
    private int _resourceAmount;
    private float _productPrice;
    private Sprite _sprite;

    public string ProductName { get; set; }
    public float ProductionTime { get; set; }
    public int ResourceAmount { get; set; }
    public float ProductPrice { get; set; }

    public Sprite ProductSprite { get; set; }

    public void Setup(string productName, float productionTime, int resourceAmount, float productPrice, Sprite sprite = null)
    {
        ProductName = productName;
        ProductionTime = productionTime;
        ResourceAmount = resourceAmount;
        ProductPrice = productPrice;
        ProductSprite = sprite;
    }
    
}
