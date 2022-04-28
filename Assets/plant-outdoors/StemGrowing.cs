using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StemGrowing : MonoBehaviour
{
    public List<MeshRenderer> stemGrowMeshes;
    public float timeToGrow = 5;
    public float refreshRate = 0.05f;
    [Range(0, 1)]
    public float minGrow = 0.2f;
    [Range(0, 1)]
    public float maxGrow = 0.97f;
    private List<Material> stemGrowMaterials = new List<Material>();
    private bool fullyGrown;

    public Material Grow;





    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < stemGrowMeshes.Count; i++)
        {
            for (int j = 0; j < stemGrowMeshes[i].materials.Length; j++)
            {
                if (stemGrowMeshes[i].materials[j].HasProperty("Grow_"))
                {


                    stemGrowMeshes[i].materials[j].SetFloat("Grow_", minGrow);
                    stemGrowMaterials.Add(stemGrowMeshes[i].materials[j]);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("clik space");
            Grow.SetFloat("Grow", 1);
            // for (int i = 0; i < stemGrowMeshes.Count; i++)
            // {
            //     StartCoroutine(StemGrow(stemGrowMaterials[i]));
            // }
        }
    }
    IEnumerator StemGrow(Material mat)
    {
        float growValue = mat.GetFloat("Grow_");
        if (!fullyGrown)
        {
            while (growValue < maxGrow)
            {
                growValue += 1 / (timeToGrow / refreshRate);
                mat.SetFloat("Grow_", growValue);
                yield return new WaitForSeconds(refreshRate);
            }

        }
        else
        {
            while (growValue > minGrow)
            {
                growValue -= 1 / (timeToGrow / refreshRate);
                mat.SetFloat("Grow_", growValue);
                yield return new WaitForSeconds(refreshRate);
            }
        }
        if (growValue >= maxGrow)
            fullyGrown = true;
        else
            fullyGrown = false;
    }
}
