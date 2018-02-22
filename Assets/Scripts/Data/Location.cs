using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Location {
    [SerializeField] private int level;
    [SerializeField] private Point point;

    public int Level {
        get {
            return level;
        }

        set {
            level = value;
        }
    }

    public Point Point {
        get {
            return point;
        }

        set {
            point = value;
        }
    }
}

[System.Serializable]
public struct Point {
    public int X { get; set; }
    public int Y { get; set; }
}