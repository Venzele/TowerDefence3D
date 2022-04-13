using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private Material _road;

    public bool IsFree { get; private set; }
    public float HalfHeight { get; private set; }

    private void Awake()
    {
        IsFree = true;
        HalfHeight = transform.localScale.y / 2;
    }

    public void ChangeOnRoad()
    {
        _meshRenderer.material = _road;
        IsFree = false;
    }

    public void MakeBusy()
    {
        IsFree = false;
    }
}
