using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class WorkSpaceManager : Singleton<WorkSpaceManager>
{
    public Transform sellLocation; // ���� �ڸ�

    [SerializeField] private Transform[] workstationSpawnLocation; // ���� �ڸ�
    [SerializeField] private Workstation workStationPrefab;
    [SerializeField] private ProductData productData;

    [HideInInspector] public List<Workstation> workstations; // ��� �۾� ���� ����Ʈ

    private Product product;
    private int workstationPosIndex = 0;


    public Product Product { get => product; set => product = value; }

    private void Start()
    {
        product = new Product();
        product.Setup(productData.productName, productData.productionTime, productData.resourceAmount, productData.productPrice, productData.sprite);
        CreateWorkstation();
    }

    // ��� ������ �۾� ���� ã�� (���� ����� ������ ����)
    public Workstation GetAvailableWorkstation(Vector3 workerPosition)
    {
        List<Workstation> availableWorkstations = workstations.Where(ws => !ws.isOccupied).ToList();

        if (availableWorkstations.Count > 0)
        {
            return availableWorkstations.OrderBy(ws => Vector3.Distance(workerPosition, ws.transform.position))
                                        .First();
        }

        return null; // ��� ������ �۾� ������ ������ null ��ȯ
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
