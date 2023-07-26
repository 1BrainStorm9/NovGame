using UnityEngine;

public class ColliderCreationController : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private GameObject boxCollider;

    public void SetPostion(Vector3 position)
    {
        this.transform.position = position;
    }

    public void DeleteCollider()
    {
        if(boxCollider != null)
        Destroy(boxCollider);
    }

    public void AddCollider(Transform transform)
    {
        boxCollider = Instantiate(prefab,transform.position,Quaternion.identity);
    }

}
