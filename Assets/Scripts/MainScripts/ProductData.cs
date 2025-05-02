using UnityEngine;

[CreateAssetMenu(fileName = "NewProduct", menuName = "Tycoon/Product")]
public class ProductData : ScriptableObject
{
    public string productName;       // ��ǰ �̸�
    public float productionTime;     // ���� �ð� (��)
    public int resourceAmount;       // ����Ǵ� �ڿ� ����
    public int productPrice;         // ��ǰ ����
    public Sprite sprite;
}
