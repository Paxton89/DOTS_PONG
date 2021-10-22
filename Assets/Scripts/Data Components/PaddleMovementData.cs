using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

[GenerateAuthoringComponent]
public struct PaddleMovementData : IComponentData
{
    public int Direction;
    public float Speed;
}
