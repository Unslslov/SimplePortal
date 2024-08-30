using System;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField] private Teleporter _other;
    [SerializeField] private  float offsetZ = 10f;
    public bool isTeleporting;


    private void OnTriggerStay(Collider col) 
    {
        if(_other.isTeleporting == false)
        {
            if(col.CompareTag("Player"))
            {
                float zPos = transform.worldToLocalMatrix.MultiplyPoint3x4(col.transform.position).z;

                if(zPos < 0)
                    Teleport(col.transform);
            }
        }
    }

    private void Teleport(Transform player)
    {
        //Position
        Vector3 localPos = transform.worldToLocalMatrix.MultiplyPoint3x4(player.position);
        // localPos = new Vector3(localPos.x, localPos.y, localPos.z + offsetZ);
        // localPos = new(-localPos.x, localPos.y, -localPos.z);

        player.position = _other.transform.localToWorldMatrix.MultiplyPoint3x4(localPos);

        //Rotation
        Quaternion difference = _other.transform.rotation * Quaternion.Inverse(transform.rotation * Quaternion.Euler(0, 180f, 0));
        player.rotation = difference * player.rotation;

        isTeleporting = true;
    }
}
