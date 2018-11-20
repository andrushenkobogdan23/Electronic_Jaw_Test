using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField]
    float distance = 5;

    void InstantiateRandomFigure(Vector3 position)
    {
        var prefab = UtillityMethods.GetRandomAB();
        Instantiate(prefab, position, Quaternion.identity);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray.origin, ray.direction, out hit, distance))
            {
                hit.transform.GetComponent<ColorableObject>().OnClick();
            }
            else
            {
                var spawnPoint = ray.origin + ray.direction * distance;
                InstantiateRandomFigure(spawnPoint);
            }
        }
    }
}
