using UnityEngine;

public class WorldCanvasPositioner : MonoBehaviour
{
    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
    }
    
    private void FixedUpdate()
    {
        transform.rotation = _camera.transform.rotation;
    }
}
