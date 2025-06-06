using UnityEngine;
using UnityEngine.EventSystems;

public class UI_UpgradeEmployeePopup : UI_Popup
{
    private enum Buttons
    {
        CloseButton,
    }

    private enum GameObjects
    {
        MoveSpeed,
        Capacity,
        Hire,
    }

    private UI_UpgradeEmployeePopupItem _moveSpeedItem;
    private UI_UpgradeEmployeePopupItem _capacityItem;
    private UI_UpgradeEmployeePopupItem _hireItem;

    protected override void Awake()
    {
        base.Awake();

        BindButtons(typeof(Buttons));
        BindObjects(typeof(GameObjects));

        _moveSpeedItem = GetObject((int)GameObjects.MoveSpeed).GetComponent<UI_UpgradeEmployeePopupItem>();
        _capacityItem = GetObject((int)GameObjects.Capacity).GetComponent<UI_UpgradeEmployeePopupItem>();
        _hireItem = GetObject((int)GameObjects.Hire).GetComponent<UI_UpgradeEmployeePopupItem>();

        GetButton((int)Buttons.CloseButton).gameObject.BindEvent(OnClickCloseButton);

        _hireItem.SetInfo(Define.EUpgradeEmployeePopupItemType.Hire, 1000, this);
    }

    private void OnClickCloseButton(PointerEventData evt)
    {
        Managers.UI.ClosePopupUI(this);
    }
}
