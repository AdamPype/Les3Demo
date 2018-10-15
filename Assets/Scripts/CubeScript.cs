using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeScript : MonoBehaviour {

    private bool _push = false;
    private Rigidbody _rb;
    private Transform _player;
    private MeshRenderer _rend;
    private Vector3 _startScale;

	// Use this for initialization
	void Start () {
        _rb = GetComponent<Rigidbody>();
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _rend = GetComponent<MeshRenderer>();
        _startScale = transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {

        _rb.AddForce(Quaternion.LookRotation(_player.position - transform.position) * transform.forward, ForceMode.Impulse);
        _rb.velocity = Vector3.ClampMagnitude(_rb.velocity, 3);

        if (_push)
            {
            float sizeIncrease = 1.5f;
            transform.localScale = Vector3.MoveTowards(transform.localScale, _startScale * sizeIncrease, 0.1f);
            if (Vector3.Distance(transform.localScale, _startScale * sizeIncrease) <= 0)
                {
                _push = false;
                }
            }
        else
            {
            transform.localScale = Vector3.Lerp(transform.localScale, _startScale, 0.1f);
            }

        float h;
        float s;
        float v;
        Color.RGBToHSV(_rend.material.color, out h, out s, out v);
        h += Time.deltaTime * 5;
        if (h > 1)
            {
            h = 0;
            }
        _rend.material.color = Color.HSVToRGB(h, 1, 1);
	}

    private void OnCollisionEnter(Collision collision)
        {
        if (!_push)
            _push = true;
        }

    public void Grow()
        {
        _push = true;
        }

    }
