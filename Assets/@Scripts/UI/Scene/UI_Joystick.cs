using UnityEngine;
using UnityEngine.EventSystems;
using static Define;

public class UI_Joystick : UI_Scene
{
	enum GameObjects
	{
		JoystickBG,
		JoystickCursor,
	}

	private GameObject _background;
	private GameObject _cursor;
	private float _radius;
	private Vector2 _touchPos;

	protected override void Awake()
	{
		base.Awake();

		BindObjects(typeof(GameObjects));

		_background = GetObject((int)GameObjects.JoystickBG);
		_cursor = GetObject((int)GameObjects.JoystickCursor);
		_radius = _background.GetComponent<RectTransform>().sizeDelta.y / 3;

		gameObject.BindEvent(OnPointerDown, type: ETouchEvent.PointerDown);
		gameObject.BindEvent(OnPointerUp, type: ETouchEvent.PointerUp);
		gameObject.BindEvent(OnDrag, type: ETouchEvent.Drag);

		// GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceCamera;
		// GetComponent<Canvas>().worldCamera = Camera.main;
	}

	#region Event
	public void OnPointerDown(PointerEventData evt)
	{
		_background.transform.position = evt.position;
		_cursor.transform.position = evt.position;
		_touchPos = evt.position;
	}

	public void OnPointerUp(PointerEventData evt)
	{
		_cursor.transform.position = _touchPos;

		Managers.Game.JoystickDir = Vector2.zero;
	}

	public void OnDrag(PointerEventData evt)
	{
		Vector2 touchDir = (evt.position - _touchPos);

		float moveDist = Mathf.Min(touchDir.magnitude, _radius);
		Vector2 moveDir = touchDir.normalized;
		Vector2 newPosition = _touchPos + moveDir * moveDist;
		_cursor.transform.position = newPosition;

		Managers.Game.JoystickDir = moveDir;
	}
	#endregion
}
