using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPickable
{
    bool KeepWorldPosition { get; }

    GameObject PickUp();
}

public class Pickable : MonoBehaviour, IPickable
{
    [field: SerializeField]
    public bool KeepWorldPosition { get; private set; } = true;

    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public GameObject PickUp()
    {
        if (rb != null) rb.isKinematic = true;

        return gameObject;
    }
}
