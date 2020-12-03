using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(CinemachineBrain))]

public class CheckForCameraBlending : MonoBehaviour
{
    private CinemachineBrain cineMachineBrain;
    public CinemachineVirtualCamera VirtualCamera;

    

    private bool wasBlendingLastFrame;

    void Awake()
    {
        cineMachineBrain = GetComponent<CinemachineBrain>();
    }
    //IEnumerator Start()
   // {
    //    yield return null;
     //   VirtualCamera = cineMachineBrain.ActiveVirtualCamera as CinemachineVirtualCamera;
    //}

    void Update()
    {

    }    

}