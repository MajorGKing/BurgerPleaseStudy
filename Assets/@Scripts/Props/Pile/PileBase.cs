using UnityEngine;
using System;
using System.Collections.Generic;
using DG.Tweening;

[RequireComponent(typeof(BoxCollider))]
public class PileBase : MonoBehaviour
{
    #region Fields
    [SerializeField]
    protected int _row = 2;
    [SerializeField]
    protected int _column = 2;
    [SerializeField]
    protected Vector3 _size = new Vector3(0.5f, 0.1f, 0.5f);
    [SerializeField]
    protected float _dropInterval = 0.05f;
    #endregion

    #region Piles
    protected Stack<GameObject> _objects = new Stack<GameObject>();

    public int ObjectCount => _objects.Count;
    #endregion

    #region Contents
    protected Define.EObjectType _objectType = Define.EObjectType.None;

    public void SpawnObject()
    {
        switch (_objectType)
        {
            case Define.EObjectType.Burger:
                {
                    GameObject go = Managers.Resource.Instantiate(Define.BURGEROBJECT);
                    AddToPile(go, false);
                }
                break;
            case Define.EObjectType.Money:
                {
                    GameObject go = Managers.Resource.Instantiate(Define.MONEYOBJECT);
                    AddToPile(go, false);
                }
                break;
            case Define.EObjectType.Trash:
                {
                    GameObject go = Managers.Resource.Instantiate(Define.TRASHOBJECT);
                    AddToPile(go, false);
                }
                break;
        }
    }

    public void SpawnObjectWithJump(Vector3 spawnPos)
    {
        switch (_objectType)
        {
            case Define.EObjectType.Burger:
                {
                    GameObject go = Managers.Resource.Instantiate(Define.BURGEROBJECT);
                    go.transform.position = spawnPos;
                    AddToPile(go, true);
                }
                break;
            case Define.EObjectType.Money:
                {
                    GameObject go = Managers.Resource.Instantiate(Define.MONEYOBJECT);
                    go.transform.position = spawnPos;
                    AddToPile(go, true);
                }
                break;
            case Define.EObjectType.Trash:
                {
                    GameObject go = Managers.Resource.Instantiate(Define.TRASHOBJECT);
                    go.transform.position = spawnPos;
                    AddToPile(go, true);
                }
                break;
        }
    }

    public void DespawnObject()
    {
        if (ObjectCount == 0)
            return;

        switch (_objectType)
        {
            case Define.EObjectType.Burger:
                {
                    GameObject go = RemoveFromPile();
                    Managers.Resource.Destroy(go);
                }
                break;
            case Define.EObjectType.Money:
                {
                    GameObject go = RemoveFromPile();
                    Managers.Resource.Destroy(go);
                }
                break;
            case Define.EObjectType.Trash:
                {
                    GameObject go = RemoveFromPile();
                    Managers.Resource.Destroy(go);
                }
                break;
        }
    }

    public void DespawnObjectWithJump(Vector3 destPos, Action onDespawnCallback = null)
    {
        if (ObjectCount == 0)
            return;

        switch (_objectType)
        {
            case Define.EObjectType.Burger:
                {
                    GameObject go = RemoveFromPile();
                    go.transform
                        .DOJump(destPos, 3, 1, 0.3f)
                        .OnComplete(() =>
                        {
                            Managers.Resource.Destroy(go);
                            onDespawnCallback?.Invoke();
                        });
                }
                break;
            case Define.EObjectType.Money:
                {
                    GameObject go = RemoveFromPile();
                    go.transform
                        .DOJump(destPos, 3, 1, 0.3f)
                        .OnComplete(() =>
                        {
                            Managers.Resource.Destroy(go);
                            onDespawnCallback?.Invoke();
                        });
                }
                break;
            case Define.EObjectType.Trash:
                {
                    GameObject go = RemoveFromPile();
                    go.transform
                        .DOJump(destPos, 3, 1, 0.3f)
                        .OnComplete(() =>
                        {
                            Managers.Resource.Destroy(go);
                            onDespawnCallback?.Invoke();
                        });
                }
                break;
        }
    }
    // Tray -> Pile
    public void TrayToPile(TrayController tray)
    {
        if (tray.CurrentTrayObjectType == Define.EObjectType.None)
			return;
		if (tray.CurrentTrayObjectType != Define.EObjectType.None && _objectType != tray.CurrentTrayObjectType)
			return;
        Transform t = tray.RemoveFromTray();
        if (t == null)
			return;

        t.rotation = Quaternion.identity;

		AddToPile(t.gameObject, jump: true); 
    }

    // Pile -> Tray
    public void PileToTray(TrayController tray)
    {
        if (_objectType == Define.EObjectType.None)
			return;
		if (tray.CurrentTrayObjectType != Define.EObjectType.None && _objectType != tray.CurrentTrayObjectType)
			return;

        GameObject go = RemoveFromPile();
		if (go == null)
			return;

		tray.AddToTray(go.transform);    
    }

    #endregion

    #region Pile
    private void AddToPile(GameObject go, bool jump = false)
    {
        // 스택에 추가한다.
		_objects.Push(go);

        // 위치를 조정한다.
		Vector3 pos = GetPositionAt(_objects.Count - 1);

        if (jump)
			go.transform.DOJump(pos, 3, 1, 0.3f);
		else
			go.transform.position = pos;
    }
    
    private GameObject RemoveFromPile()
    {
        if (_objects.Count == 0)
			return null;

		// 스택에서 제거한다.
		return _objects.Pop();
    }

    private Vector3 GetPositionAt(int pileIndex)
    {
        Vector3 offset = new Vector3((_row - 1) * _size.x / 2, 0, (_column - 1) * _size.z / 2);
		Vector3 startPos = transform.position - offset;

        int row = (pileIndex / _row) % _column;
        int column = pileIndex % _row;
		int height = pileIndex / (_row * _column);

        float x = startPos.x + column * _size.x;
		float y = startPos.y + height * _size.y;
		float z = startPos.z + row * _size.z;

		return new Vector3(x, y, z);
    }
    #endregion

    #region Editor
#if UNITY_EDITOR
    void OnDrawGizmosSelected()
    {
        Vector3 offset = new Vector3((_row - 1) * _size.x / 2, 0, (_column - 1) * _size.z / 2);
		Vector3 startPos = transform.position - offset; // 0번 칸의 위치.

        Gizmos.color = Color.yellow;

        for (int r = 0; r < _row; r++)
		{
			for (int c = 0; c < _column; c++)
			{
				Vector3 center = startPos + new Vector3(r * _size.x, _size.y / 2, c * _size.z);
				Gizmos.DrawWireCube(center, _size);
			}
		}
    }
#endif
    #endregion 
}
