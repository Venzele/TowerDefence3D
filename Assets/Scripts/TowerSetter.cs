using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSetter : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private GameObject _containerTowers;
    [SerializeField] private MainMenuButton _mainMenuButton;
    [SerializeField] private Bank _bank;

    private List<Tower> _towers = new List<Tower>();
    private Tower _installableTower;

    private void OnEnable()
    {
        _mainMenuButton.Opened += RemoveTowers;
    }

    private void OnDisable()
    {
        _mainMenuButton.Opened -= RemoveTowers;
    }

    public void StartSetTower(ItemData itemData)
    {
        _installableTower = Instantiate(itemData.Prefab);
        StartCoroutine(UpdateTowerPose(itemData.Price));
    }

    private IEnumerator UpdateTowerPose(int price)
    {
        while (_installableTower != null)
        {
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit raycastHit))
            {
                _installableTower.transform.position = raycastHit.point;
                _installableTower.ChangeColor(Color.red);
                raycastHit.collider.gameObject.TryGetComponent(out Ground ground);
                TrySetTower(ground, price);
            }

            yield return null;
        }
    }

    private void TrySetTower(Ground ground, int price)
    {
        if (ground != null && ground.IsFree)
        {
            _installableTower.transform.position = ground.transform.position + new Vector3(0, ground.HalfHeight, 0);
            _installableTower.ChangeColor(Color.green);

            if (Input.GetMouseButtonUp(0))
            {
                _installableTower.Enable();
                _installableTower.ChangeColor(Color.white);
                _installableTower.transform.parent = _containerTowers.transform;
                _towers.Add(_installableTower);
                _bank.BuyTower(price);
                _installableTower = null;
                ground.MakeBusy();
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            Destroy(_installableTower.gameObject);
        }
    }

    private void RemoveTowers()
    {
        foreach (var item in _towers)
        {
            Destroy(item.gameObject);
        }

        _towers.Clear();
    }
}
