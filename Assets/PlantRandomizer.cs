using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlantRandomizer : MonoBehaviour
{
    public int plantId;

    public List<GameObject> prefabs;

    // Start is called before the first frame update
    void Start()
    {
        GameObject randomPrefab = prefabs[plantId];
        Instantiate(randomPrefab, transform.position,transform.rotation,transform);
    }
}
