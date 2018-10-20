using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadScript : MonoBehaviour {

    [SerializeField] private BasePlayerScript _player;

    [SerializeField] private float _timeStep;
    [SerializeField] private float _damp = 0.2f;
    [SerializeField] private float _freq = 1;

    private float _scale;
    private float _vel;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (_player.InputMovement.magnitude > 0)
            {
            _scale += Time.deltaTime * 2;
            }
        else
            {
            _scale = Spring(_scale, ref _vel, 0, _damp, _freq, _timeStep);
            }
        transform.localScale = Vector3.one + (Vector3.one * _scale);
	}

    float Spring(float x, ref float v, float xt, float zeta, float omega, float h)
        {
        float f = 1.0f + 2.0f * h * zeta * omega;
        float oo = omega * omega;
        float hoo = h * oo;
        float hhoo = h * hoo;
        float detInv = 1.0f / (f + hhoo);
        float detX = f * x + h * v + hhoo * xt;
        float detV = v + hoo * (xt - x);
        x = detX * detInv;
        v = detV * detInv;

        return x;
        }
    }
