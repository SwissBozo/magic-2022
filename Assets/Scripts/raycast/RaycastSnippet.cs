using System.Collections;
using UnityEngine;
using UnityEngine.XR.MagicLeap;
using UnityEngine.InputSystem;

public class RaycastSnippet : MonoBehaviour 
{

    public Transform camTransform;
    public GameObject prefab;

    private AudioSource audioSource;
    public AudioClip[] shoot;
    private AudioClip shootClip;

    private MLRaycast.QueryParams _raycastParams = new MLRaycast.QueryParams();

    public LayerMask layerMask = ~0;
    Coroutine loop;

    public int plantId;
    public InputAction encoder;
    void Start() 
    {
        // Start raycasting.
        MLRaycast.Start();

        // Sound
        audioSource = gameObject.GetComponent<AudioSource>();
    }
    private void OnDestroy() 
    {
        // Stop raycasting.
        MLRaycast.Stop();
    }


    void OnEnable(){
        
        encoder.Enable();
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


        GameObject plantInstance = Instantiate(prefab,point,rotation);
        PlantRandomizer randomizer = plantInstance.GetComponent<PlantRandomizer>();
        float input = encoder.ReadValue<float>();
        float plantIdFloat =  Mathf.Lerp(0,randomizer.prefabs.Count,input);
        plantId = Mathf.RoundToInt(plantIdFloat);

        randomizer.plantId = plantId;

        int index = Random.Range(0, shoot.Length);
        shootClip = shoot[index];
        audioSource.clip = shootClip;
        audioSource.Play();
        
        }
    }
}