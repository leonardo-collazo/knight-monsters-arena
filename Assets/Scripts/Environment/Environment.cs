using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment : MonoBehaviour
{
    [SerializeField] private Transform leftLimit;
    [SerializeField] private Transform upperLimit;
    [SerializeField] private Transform rightLimit;
    [SerializeField] private Transform lowerLimit;
    [SerializeField] private Transform ground;

    public Transform LeftLimit { get => leftLimit; }
    public Transform UpperLimit { get => upperLimit; }
    public Transform RightLimit { get => rightLimit; }
    public Transform LowerLimit { get => lowerLimit; }
    public Transform Ground { get => ground; }
}
