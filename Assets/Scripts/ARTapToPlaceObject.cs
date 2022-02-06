using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARCore;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]

public class ARTapToPlaceObject : MonoBehaviour
{

    public GameObject colocarGameOjbect;

    private GameObject objetoColocado;
    private ARRaycastManager _arRayCastManager;
    private Vector2 posicaoTocada;

    static List<ARRaycastHit> hits = new List<ARRaycastHit>();


    private void Awake()
    {
        _arRayCastManager = GetComponent<ARRaycastManager>();
    }

    bool TryGetPosicaoToque(out Vector2 posicaoTocada)
    {
        if(Input.touchCount > 0)
        {
            posicaoTocada = Input.GetTouch(0).position;
            return true;
        }

        posicaoTocada = default;
        return false;

    }

    void Update()
    {
        if(!TryGetPosicaoToque(out Vector2 posicaoTocada))
        {   
            return;
        }
        if(_arRayCastManager.Raycast(posicaoTocada, hits, TrackableType.PlaneWithinPolygon))
        {
            var hitPose = hits[0].pose;

            if(objetoColocado == null)
            {
                objetoColocado = Instantiate(colocarGameOjbect, hitPose.position, hitPose.rotation);
            }
            else
            {
                objetoColocado.transform.position = hitPose.position;
            }
        }
    }
}
