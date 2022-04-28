using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantRandomizer : MonoBehaviour
{

    public List<GameObject> prefabs;

    // Start is called before the first frame update
    void Start()
    {
        GameObject randomPrefab = prefabs[Random.Range(0,prefabs.Count-1)];
        Instantiate(randomPrefab, transform.position,transform.rotation,transform);
    }
}
