using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Data;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class GameManager
{
    public Vector2 JoystickDir { get; set; } = Vector2.zero;

    public long Money{get; set;}

    public GameObject SpawnGuest() { return Managers.Resource.Instantiate(Define.GUEST); }
}