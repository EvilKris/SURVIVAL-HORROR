using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MSLimitPos : MonoBehaviour {

    public enum LimitAxis {
        LimitX, LimitY, LimitZ
    };
    public LimitAxis _limitAxis = LimitAxis.LimitX;
    public float min = 0;
    public float max = 1;

	void Start () {
        if (min > max) {
            MSLimitPos thisComponent = this;
            thisComponent.enabled = false;
            Debug.LogWarning("The minimum value cannot be greater than the maximum value.", transform.gameObject);
            return;
        }
    }

	void FixedUpdate () {
        //X AXIS
        if (_limitAxis == LimitAxis.LimitX) {
            if (transform.localPosition.x < min) {
                transform.localPosition = new Vector3(min, transform.localPosition.y, transform.localPosition.z);
            }
            if (transform.localPosition.x > max) {
                transform.localPosition = new Vector3(max, transform.localPosition.y, transform.localPosition.z);
            }
        }

        //Y AXIS
        if (_limitAxis == LimitAxis.LimitY) {
            if (transform.localPosition.y < min) {
                transform.localPosition = new Vector3(transform.localPosition.x, min, transform.localPosition.z);
            }
            if (transform.localPosition.y > max) {
                transform.localPosition = new Vector3(transform.localPosition.x, max, transform.localPosition.z);
            }
        }

        //Z AXIS
        if (_limitAxis == LimitAxis.LimitZ) {
            if (transform.localPosition.z < min) {
                transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, min);
            }
            if (transform.localPosition.z > max) {
                transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, max);
            }
        }
    }
}
