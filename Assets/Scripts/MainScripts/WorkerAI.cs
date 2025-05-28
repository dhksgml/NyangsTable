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
            // WorkSpaceManager�� ���� ��� ������ �۾� ���� ã��
            Workstation targetWorkstation = WorkSpaceManager.Instance.GetAvailableWorkstation(transform.position);

            if (targetWorkstation != null)
            {
                currentWorkstation = targetWorkstation;
                currentWorkstation.Occupy();

                currentWorkstation.OnProduce += CollectResource;

                // �۾� �������� �̵�
                SetWalkAnimation(true);

                yield return transform.DOMove(currentWorkstation.transform.position, WorkerManager.Instance.workerMoveSpeed)
                    .SetSpeedBased()
                    .WaitForCompletion();

                // ���ϴ� �ð�
                SetWalkAnimation(false);

                yield return currentWorkstation.Produce();

                currentWorkstation.OnProduce -= CollectResource;

                // �۾� �Ϸ� �� �ڸ� ����
                currentWorkstation.Release();

                // ���� �ڸ��� �̵�
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
                Debug.Log("�� �۾� ������ �����ϴ�. 1�� ��� �� �ٽ� �õ�");
                yield return new WaitForSeconds(1f); // 1�� ��� �� �ٽ� �õ�
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


    public GameObject sellEffectPrefab;     // ����Ʈ ������ (ex. +1G)
    public Transform effectSpawnPoint;      // ����Ʈ�� ������ ��ġ (��Ŀ ĳ���� �ڽ����� empty ����� ����)

    private void SellProduct()
    {
        // ����, �Ǹ��� ��ǰ ���� ��������
        Product productInfo = WorkSpaceManager.Instance.Product;

        // �ݾ��� ����ϰ�, �� ���� �ݾ׸�ŭ ��带 �߰�
        int goldAmount = (int)productInfo.ProductPrice * collectedResources;

        // �ݾ��� 0���� ū ��쿡�� ����Ʈ ������ ��� �߰�
        if (goldAmount > 0)
        {
            // ����Ʈ ����
            if (sellEffectPrefab != null && effectSpawnPoint != null)
            {
                // ����Ʈ�� �����ؼ�, ��Ŀ�� �Ӹ� ���� ǥ��
                GameObject effect = Instantiate(sellEffectPrefab, effectSpawnPoint.position, Quaternion.identity);

                // ��Ŀ ������ �� ������� �ϱ� ���� ��Ŀ �ڽ����� ���̱�
                effect.transform.SetParent(transform);

                // ���� ������ ���� (Ȥ�� ������ ũ�� ���ϱ� ��)
                effect.transform.localScale = Vector3.one;

                // ���� �ð� �� �ڵ� ���� (��: 1.5�� �� �ı�)
                Destroy(effect, 1.5f);
            }

            // ��� �߰�
            GoldManager.Instance.AddGold(goldAmount, transform.position);

            if (AudioManager.Instance != null)
            {
                // ���� ȿ���� ���
                AudioManager.Instance.PlayCoinSFX();
            }
        }

        // ���ҽ� �ʱ�ȭ
        collectedResources = 0;
    }
}