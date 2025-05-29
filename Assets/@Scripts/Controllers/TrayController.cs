using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;
using System.Linq;

public class TrayController : MonoBehaviour
{
    [SerializeField]
	private Vector2 _shakeRange = new Vector2(0.8f, 0.4f);

	[SerializeField]
	private float _bendFactor = 0.1f;

	[SerializeField]
	private float _itemHeight = 0.5f;

    private Define.EObjectType _objectType = Define.EObjectType.None;
    public Define.EObjectType CurrentTrayObjectType
	{
		get { return _objectType; }
		set 
		{ 
			_objectType = value;
			switch (value)
			{
				case Define.EObjectType.Trash:
					_itemHeight = 0.2f;
					break;
				case Define.EObjectType.Burger:
					_itemHeight = 0.5f;
					break;
			}
		}
	}

    private List<Transform> _items = new List<Transform>();
    public int ItemCount => _items.Count; // 쟁반 위에 들고 있는 아이템 개수.
    private HashSet<Transform> _reserved = new HashSet<Transform>();
    public int ReservedCount => _reserved.Count; // 쟁반 위로 이동중.
    public int TotalItemCount => _reserved.Count + _items.Count; // 쟁반 위로 이동중인 아이템을 포함한 전체 개수.

    private MeshRenderer _meshRenderer;
	//private StickmanController _owner;
	public bool IsPlayer = false;

    public bool Visible
	{
		set { if (_meshRenderer != null ) _meshRenderer.enabled = value; }//_owner?.UpdateAnimation(); }
		get { return (_meshRenderer != null) ? _meshRenderer.enabled : false; }
	}

    private void Start()
	{
		_meshRenderer = GetComponent<MeshRenderer>();
		// _owner = transform.parent.GetComponent<StickmanController>();
		Visible = false;
	}

    public void AddToTray(Transform child)
    {
        // 운반하는 물체 종류 추적을 위해.
		Define.EObjectType objectType = Utils.GetTrayObjectType(child);
		if (objectType == Define.EObjectType.None)
			return;

        // 다른 종류의 아이템이 있으면 수집 불가.
		if (CurrentTrayObjectType != Define.EObjectType.None && CurrentTrayObjectType != objectType)
			return;

        CurrentTrayObjectType = objectType;

        _reserved.Add(child);
        
        Vector3 dest = transform.position + Vector3.up * TotalItemCount * _itemHeight;
        child.DOJump(dest, 5, 1, 0.3f)
			.OnComplete(() =>
			{
				_reserved.Remove(child);
				_items.Add(child);
			});
    }

    public Transform RemoveFromTray()
	{
		if (ItemCount == 0 || ReservedCount > 0)
			return null;

		Transform item = _items.Last();
		if (item == null)
			return null;

		_items.RemoveAt(_items.Count - 1);

		// 운반하는 물체 종류 추적을 위해.
		if (TotalItemCount == 0)
			CurrentTrayObjectType = Define.EObjectType.None;

		return item;
	}
}
