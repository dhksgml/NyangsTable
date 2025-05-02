using UnityEngine;

[CreateAssetMenu(fileName = "NewProduct", menuName = "Tycoon/Product")]
public class ProductData : ScriptableObject
{
    public string productName;       // 제품 이름
    public float productionTime;     // 생산 시간 (초)
    public int resourceAmount;       // 생산되는 자원 개수
    public int productPrice;         // 제품 가격
    public Sprite sprite;
}
