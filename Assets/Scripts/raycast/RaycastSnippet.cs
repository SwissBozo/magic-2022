using System.Collections;
using UnityEngine;
using UnityEngine.XR.MagicLeap;

public class RaycastSnippet : MonoBehaviour 
{

    public Transform camTransform;
    public GameObject [] prefabs;

    private MLRaycast.QueryParams _raycastParams = new MLRaycast.QueryParams();

    public LayerMask layerMask = ~0;
    Coroutine loop;
    void Start() 
    {
        // Start raycasting.
        MLRaycast.Start();
    }

    private void OnDestroy() 
    {
        // Stop raycasting.
        MLRaycast.Stop();
    }


    void OnEnable(){
        
      loop =  StartCoroutine(Loop());
    }
    void OnDisable(){

       StopCoroutine(loop); 
    }

    
     IEnumerator Loop(){

        while(true)
        {
            while(Physics.Raycast(camTransform.position,camTransform.forward,10,layerMask))
            {
                yield return null;
            }

            // Update the orientation data in the raycast parameters.
            _raycastParams.Position = camTransform.position;
            _raycastParams.Direction = camTransform.forward;
            _raycastParams.UpVector = camTransform.up;

            // Make a raycast request using the raycast parameters 
            MLRaycast.Raycast(_raycastParams, HandleOnReceiveRaycast);
            yield return null;
        }
     }
     
    void HandleOnReceiveRaycast( MLRaycast.ResultState state, 
                                UnityEngine.Vector3 point, Vector3 normal, 
                                float confidence) 
    {
        if (state ==  MLRaycast.ResultState.HitObserved) 
        {
            Debug.DrawLine(point,point+normal,Color.blue,1);
             // Rotate the prefab to match given normal.
        Quaternion rotation = Quaternion.FromToRotation(Vector3.up, normal);
        // Instantiate the prefab at the given point.
        // GameObject go = Instantiate(prefabs, point, rotation);


        int randomId = Random.Range(0,prefabs.Length-1);
        Instantiate(prefabs[randomId],point,rotation);
        
        }
    }
}