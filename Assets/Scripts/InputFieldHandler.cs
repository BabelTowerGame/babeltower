using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(InputField))]
public class InputFieldHandler : MonoBehaviour, ISelectHandler, IDeselectHandler {

    private InputField field;
    public string placeholder;

    public void Start() {
        this.field = this.gameObject.GetComponent<InputField>();
        this.field.text = placeholder;
    }

    public void OnDeselect(BaseEventData eventData) {
        //Debug.Log(field.text.Trim() + " " + string.IsNullOrEmpty(field.text.Trim()));
        if (string.IsNullOrEmpty(field.text.Trim())) {
            field.text = placeholder;
        }
    }

    public void OnSelect(BaseEventData eventData) {
        if (field.text == placeholder) {
            field.text = "";
        }
    }
}
