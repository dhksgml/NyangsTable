using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapFix : MonoBehaviour
{
    void Start() // 게임 오브젝트가 시작될 때 실행되는 함수
    {
        Tilemap tilemap = GetComponent<Tilemap>(); // Tilemap 컴포넌트 가져오기
        if (tilemap != null)
        {
            tilemap.RefreshAllTiles(); // 모든 타일을 강제로 다시 로드
        }
    }
}
