using UnityEngine;

[SelectionBase]
public class Portal : MonoBehaviour
{
    [SerializeField] private Portal _other;
    [SerializeField] private Camera _portalView;
    [SerializeField] private Transform _mainCameraTransform;

    private void Awake() 
    {
        _mainCameraTransform = Camera.main.transform;
    }

    private void Start() 
    {
        _other._portalView.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);    
        GetComponentInChildren<MeshRenderer>().sharedMaterial.mainTexture = _other._portalView.targetTexture;
    }

    private void Update() 
    {
        //Position
        Vector3 lookerPostion = _other.transform.worldToLocalMatrix.MultiplyPoint3x4(_mainCameraTransform.transform.position);
        lookerPostion = new Vector3(-lookerPostion.x, lookerPostion.y, -lookerPostion.z);

        _portalView.transform.localPosition = lookerPostion;

        //Rotation
        Quaternion difference = transform.rotation * Quaternion.Inverse(_other.transform.rotation * Quaternion.Euler(0, 180f, 0));
        _portalView.transform.rotation = difference * _mainCameraTransform.rotation;

        //Clipping
        _portalView.nearClipPlane = lookerPostion.magnitude;
    }
}
