using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private AnimationCurve _curveScale;
    [SerializeField] private float _age;

    private float _elapsedTime;

    public void Show(float range)
    {
        StartCoroutine(Animate(range));
    }

    private IEnumerator Animate(float range)
    {
        while(_elapsedTime < _age)
        {
            _elapsedTime += Time.deltaTime;
            transform.localScale = Vector3.one * (range * _curveScale.Evaluate(_elapsedTime));
            yield return null;
        }

        Destroy(gameObject);
    }
}
