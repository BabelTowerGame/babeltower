using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Location {
	public int level;
	public Point point;


}

struct Point{
	public int X { get; set;}
	public int Y { get; set;}
}