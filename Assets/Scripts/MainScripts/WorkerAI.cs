using UnityEngine;
using System.Collections;
using DG.Tweening;

public class WorkerAI : MonoBehaviour
{
    private Workstation currentWorkstation;
    private int collectedResources = 0;

    //animation related
    private Animator animator;
    private bool isWalking = false;
    private int animationParameterWalkHash;

    private ScaleEffect scaleEffect;

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        animationParameterWalkHash = Animator.StringToHash("IsWalking");

        scaleEffect = gameObject.GetComponent<ScaleEffect>();

        StartCoroutine(WorkRoutine());
    }


    IEnumerator WorkRoutine()
    {
        while (true)
        {
            // WorkSpaceManager를 통해 사용 가능한 작업 공간 찾기
            Workstation targetWorkstation = WorkSpaceManager.Instance.GetAvailableWorkstation(transform.position);

            if (targetWorkstation != null)
            {
                currentWorkstation = targetWorkstation;
                currentWorkstation.Occupy();

                currentWorkstation.OnProduce += CollectResource;

                // 작업 공간으로 이동
                SetWalkAnimation(true);

                yield return transform.DOMove(currentWorkstation.transform.position, WorkerManager.Instance.workerMoveSpeed)
                    .SetSpeedBased()
                    .WaitForCompletion();

                // 일하는 시간
                SetWalkAnimation(false);

                yield return currentWorkstation.Produce();

                currentWorkstation.OnProduce -= CollectResource;

                // 작업 완료 후 자리 비우기
                currentWorkstation.Release();

                // 원래 자리로 이동
                SetWalkAnimation(true);

                yield return transform.DOMove(WorkSpaceManager.Instance.sellLocation.position, WorkerManager.Instance.workerMoveSpeed)
                    .SetSpeedBased()
                    .WaitForCompletion();

                SellProduct();
                SetWalkAnimation(false);
                scaleEffect.ScaleUpAndDown();
            }
            else
            {
                Debug.Log("빈 작업 공간이 없습니다. 1초 대기 후 다시 시도");
                yield return new WaitForSeconds(1f); // 1초 대기 후 다시 시도
            }
        }
    }
    void SetWalkAnimation(bool isWalking)
    {
        this.isWalking = isWalking;
        animator.SetBool(animationParameterWalkHash, isWalking);
    }

    void CollectResource(int amount)
    {
        collectedResources += amount;
    }


    public GameObject sellEffectPrefab;     // 이펙트 프리팹 (ex. +1G)
    public Transform effectSpawnPoint;      // 이펙트가 생성될 위치 (워커 캐릭터 자식으로 empty 만들면 좋아)

    private void SellProduct()
    {
        // 먼저, 판매할 제품 정보 가져오기
        Product productInfo = WorkSpaceManager.Instance.Product;

        // 금액을 계산하고, 그 계산된 금액만큼 골드를 추가
        int goldAmount = (int)productInfo.ProductPrice * collectedResources;

        // 금액이 0보다 큰 경우에만 이펙트 생성과 골드 추가
        if (goldAmount > 0)
        {
            // 이펙트 생성
            if (sellEffectPrefab != null && effectSpawnPoint != null)
            {
                // 이펙트를 생성해서, 워커의 머리 위에 표시
                GameObject effect = Instantiate(sellEffectPrefab, effectSpawnPoint.position, Quaternion.identity);

                // 워커 움직일 때 따라오게 하기 위해 워커 자식으로 붙이기
                effect.transform.SetParent(transform);

                // 로컬 스케일 보정 (혹시 프리팹 크기 꼬일까 봐)
                effect.transform.localScale = Vector3.one;

                // 일정 시간 후 자동 제거 (예: 1.5초 뒤 파괴)
                Destroy(effect, 1.5f);
            }

            // 골드 추가
            GoldManager.Instance.AddGold(goldAmount, transform.position);

            if (AudioManager.Instance != null)
            {
                // 코인 효과음 재생
                AudioManager.Instance.PlayCoinSFX();
            }
        }

        // 리소스 초기화
        collectedResources = 0;
    }
}