using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roach : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Animator animator;

    [SerializeField] private GameObject dieParticle;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hitData = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hitData.collider != null)
            {
                if (hitData.collider.gameObject == gameObject)
                {
                    Die();
                }
            }
        }
    }

    private void Die()
    {
        Debug.Log("Die");
        animator.SetTrigger("Die");
        Instantiate(dieParticle, transform.position, Quaternion.identity);
        Camera.main.GetComponent<CameraZoomEffect>().ZoomToPosition(gameObject.transform.position);
        StartCoroutine(ColorFlesh());
    }

    IEnumerator ColorFlesh()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = Color.white;
    }

    private void DestroyRoach()
    {
        Destroy(this.gameObject);
    }

    private void OnDestroy()
    {
        RandomPrefabSpawner spawner = FindObjectOfType<RandomPrefabSpawner>();
        if(spawner != null)
        {
            spawner.DecreaseSpawnCount();
        }
    }
}
