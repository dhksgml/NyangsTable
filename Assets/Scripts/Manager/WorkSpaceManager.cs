using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class WorkSpaceManager : Singleton<WorkSpaceManager>
{
    public Transform sellLocation; // 원래 자리

    [SerializeField] private Transform[] workstationSpawnLocation; // 원래 자리
    [SerializeField] private Workstation workStationPrefab;
    [SerializeField] private ProductData productData;

    [HideInInspector] public List<Workstation> workstations; // 모든 작업 공간 리스트

    private Product product;
    private int workstationPosIndex = 0;


    public Product Product { get => product; set => product = value; }

    private void Start()
    {
        product = new Product();
        product.Setup(productData.productName, productData.productionTime, productData.resourceAmount, productData.productPrice, productData.sprite);
        CreateWorkstation();
    }

    // 사용 가능한 작업 공간 찾기 (가장 가까운 순으로 정렬)
    public Workstation GetAvailableWorkstation(Vector3 workerPosition)
    {
        List<Workstation> availableWorkstations = workstations.Where(ws => !ws.isOccupied).ToList();

        if (availableWorkstations.Count > 0)
        {
            return availableWorkstations.OrderBy(ws => Vector3.Distance(workerPosition, ws.transform.position))
                                        .First();
        }

        return null; // 사용 가능한 작업 공간이 없으면 null 반환
    }

    public void CreateWorkstation()
    {
        if (workstationSpawnLocation.Length < workstationPosIndex) return;

        Workstation newWorkStation = Instantiate(workStationPrefab, workstationSpawnLocation[workstationPosIndex].position , Quaternion.identity);
        newWorkStation.transform.SetParent(workstationSpawnLocation[workstationPosIndex]);
        newWorkStation.Init(Product);
        newWorkStation.GetComponent<SpriteRenderer>().sprite = product.ProductSprite;
        workstations.Add(newWorkStation);
        workstationPosIndex++;
    }

}
