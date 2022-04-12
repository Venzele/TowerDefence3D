using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSetter : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private GameObject _containerTowers;
    [SerializeField] private MainMenuButton _mainMenuButton;
    [SerializeField] private Player _player;

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
            //Ray ray = _mainCamera.ScreenPointToRay(Input.GetTouch(0).position);

            if (Physics.Raycast(ray, out RaycastHit raycastHit))
            {
                _installableTower.transform.position = raycastHit.point;
                _installableTower.ChangeColor(Color.red);
                raycastHit.collider.gameObject.TryGetComponent(out Ground ground);

                if (ground != null && ground.IsGround)
                {
                    _installableTower.transform.position = ground.transform.position + new Vector3(0, ground.ScaleSize, 0);
                    _installableTower.ChangeColor(Color.green);

                    if (Input.GetMouseButtonUp(0))
                    {
                        _installableTower.EnableTower(true);
                        _installableTower.ChangeColor(Color.white);
                        _installableTower.transform.parent = _containerTowers.transform;
                        _towers.Add(_installableTower);
                        _player.BuyTower(price);
                        _installableTower = null;
                        ground.ChangeGround();
                        break;
                    }

                    //if (Input.GetTouch(0).phase == TouchPhase.Ended)
                    //{
                    //    _installableTower.EnableTower(true);
                    //    _installableTower.ChangeColor(Color.white);
                    //    _installableTower.transform.parent = _containerTowers.transform;
                    //    _towers.Add(_installableTower);
                    //    _player.BuyTower(price);
                    //    _installableTower = null;
                    //    ground.ChangeGround();
                    //    break;
                    //}
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                Destroy(_installableTower.gameObject);
            }

            //if (Input.GetTouch(0).phase == TouchPhase.Ended)
            //{
            //    Destroy(_installableTower.gameObject);
            //}

            yield return null;
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