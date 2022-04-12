using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private Material _road;

    public bool IsGround { get; private set; }
    public float ScaleSize { get; private set; }

    private void Awake()
    {
        IsGround = true;
        ScaleSize = transform.localScale.y / 2;
    }

    public void ChangeGroundOnRoad()
    {
        _meshRenderer.material = _road;
        IsGround = false;
    }

    public void ChangeGround()
    {
        IsGround = false;
    }
}
