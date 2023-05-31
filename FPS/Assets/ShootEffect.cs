using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootEffect : MonoBehaviour
{
    [SerializeField] float fSpeed;
    [SerializeField] float fDeletTime;
    
    public Vector3 destination;

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, destination, fSpeed);

        Invoke("DeleteThis", fDeletTime);
    }

    private void DeleteThis()
    {
        Destroy(gameObject);
    }
}
