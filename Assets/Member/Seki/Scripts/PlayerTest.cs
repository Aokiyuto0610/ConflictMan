using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTest : MonoBehaviour
{
    [SerializeField, Label("弱点範囲に入っているか")] bool _weakness=false;

    [SerializeField, Label("盾範囲に入ったのか")] bool _shield = false;
}
