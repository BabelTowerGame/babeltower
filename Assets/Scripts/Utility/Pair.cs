using System;
using UnityEngine;

[Serializable]
public class StringIntPair {

    [SerializeField] private string key;
    [SerializeField] private uint value;

    public string Key {
        get { return this.key; }
        set { this.key = value; }
    }

    public uint Value {
        get { return this.value; }
        set { this.value = value; }
    }

}





[Serializable]
public class Pair<T, U> {
    public Pair() {
    }

    public Pair(T first, U second) {
        this.First = first;
        this.Second = second;
    }

    public T First { get; set; }
    public U Second { get; set; }
};