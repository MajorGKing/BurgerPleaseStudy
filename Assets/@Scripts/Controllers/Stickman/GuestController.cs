
public class GuestController : StickmanController
{
    private Define.EGuestState _guestState = Define.EGuestState.None;
    public Define.EGuestState GuestState
	{
		get { return _guestState; }
		set 
		{ 
			_guestState = value;
			
			if (value == Define.EGuestState.Eating)
				State = Define.EAnimState.Eating;

			UpdateAnimation(); 
		}
	}

    public int CurrentDestQueueIndex;

    protected override void Awake()
	{
		base.Awake();		
	}

    protected override void Update()
    {
        base.Update();

        if (GuestState != Define.EGuestState.Eating)
        {
            if (HasArrivedAtDestination)
			{
				//_navMeshAgent.isStopped = true;
				State = Define.EAnimState.Idle;
			}
			else
			{
				State = Define.EAnimState.Move;
				LookAtDestination();
			}
        }
        else
        {
            //
        }
    }
}
