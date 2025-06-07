using UnityEngine;

[RequireComponent(typeof(WorkerInteraction))]
public class Office : UnlockableBase
{
    private UI_UpgradeEmployeePopup _upgradeEmployeePopup;

    private void Start()
    {
        GetComponent<WorkerInteraction>().OnTriggerStart = OnEnterOffice;
        GetComponent<WorkerInteraction>().OnTriggerEnd = OnLeaveOffice;
    }

    public void OnEnterOffice(WorkerController wc)
    {
        _upgradeEmployeePopup = Managers.UI.ShowPopupUI<UI_UpgradeEmployeePopup>();
    }

    public void OnLeaveOffice(WorkerController wc)
    {
        if(_upgradeEmployeePopup == null)
            return;

        Managers.UI.ClosePopupUI(_upgradeEmployeePopup);
    }
}
