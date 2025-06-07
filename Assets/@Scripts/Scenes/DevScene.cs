using UnityEngine;

public class DevScene : BaseScene
{
    private UI_GameScene _uiGameScene;
	private UI_Joystick _uiJoystick;

    protected override void Awake()
    {
        base.Awake();

#if UNITY_EDITOR
        gameObject.AddComponent<CaptureScreenShot>();
#endif
        SceneType = Define.EScene.DevScene;
        Managers.UI.CacheAllPopups();

        if (_uiGameScene == null)
		{
			_uiJoystick = Managers.UI.ShowSceneUI<UI_Joystick>();
			_uiGameScene = Managers.UI.ShowSceneUI<UI_GameScene>();
			_uiGameScene.GetComponent<Canvas>().sortingOrder = 1;
			Managers.UI.SceneUI = _uiGameScene;
			_uiGameScene.SetInfo();
		}

    }

    public override void Clear()
    {
        throw new System.NotImplementedException();
    }
}
