using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forest : MonoBehaviour
{
    [SerializeField] private List<GameObject> _templetes;
    [SerializeField] private Surface _surface;
    [SerializeField] private GameObject _containerForest;
    [SerializeField] private int _minTrees, _maxTrees;
    [SerializeField] private MainMenuButton _mainMenuButton;

    private List<GameObject> _trees = new List<GameObject>();

    private void OnEnable()
    {
        _mainMenuButton.Opened += RemoveTrees;
    }

    private void OnDisable()
    {
        _mainMenuButton.Opened -= RemoveTrees;
    }

    public void CreateTrees()
    {
        int numberOfTrees = Random.Range(_minTrees, _maxTrees);

        for (int i = 0; i < numberOfTrees; i++)
        {
            int indexGround = Random.Range(0, _surface.GetMaxGround());
            Vector3 positionPoint = _surface.FindGround(indexGround).transform.position + new Vector3(0, _surface.FindGround(indexGround).HalfHeight, 0);
            GameObject tree = _templetes[Random.Range(0, _templetes.Count)];

            if (_surface.FindGround(indexGround).IsFree)
            {
                GameObject newTree = Instantiate(tree, positionPoint, Quaternion.identity, _containerForest.transform);
                _surface.FindGround(indexGround).MakeBusy();
                _trees.Add(newTree);
            }
            else
            {
                i--;
            }
        }
    }

    private void RemoveTrees()
    {
        foreach (var item in _trees)
        {
            Destroy(item.gameObject);
        }

        _trees.Clear();
    }
}
