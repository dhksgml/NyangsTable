using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapFix : MonoBehaviour
{
    void Start() // ���� ������Ʈ�� ���۵� �� ����Ǵ� �Լ�
    {
        Tilemap tilemap = GetComponent<Tilemap>(); // Tilemap ������Ʈ ��������
        if (tilemap != null)
        {
            tilemap.RefreshAllTiles(); // ��� Ÿ���� ������ �ٽ� �ε�
        }
    }
}
