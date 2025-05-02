using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerManager : Singleton<WorkerManager>
{
    [Header("Prefab")]
    [SerializeField] private WorkerAI workerPrefab;

    private List<WorkerAI> _workers;
    public List<WorkerAI> Workers => _workers;

    public float workerMoveSpeed = 0.8f; // 이동 속도

    private void Start()
    {
        _workers = new List<WorkerAI>();
        CreateWorker();
    }
    public void CreateWorker()
    {
        WorkerAI newWorker = Instantiate(workerPrefab, WorkSpaceManager.Instance.sellLocation.position, Quaternion.identity);
        _workers.Add(newWorker);
        newWorker.transform.SetParent(this.transform);
    }

    public void UpgradeSpeed(float speedMultiplier)
    {
        workerMoveSpeed += speedMultiplier;
    }
}
