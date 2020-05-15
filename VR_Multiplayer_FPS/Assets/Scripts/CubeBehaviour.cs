using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bolt;

public class CubeBehaviour : Bolt.EntityEventListener<ICubeState>
{

    private float _resetColorTime;
    private Renderer _renderer;

    public override void Attached()
    {
        _renderer = GetComponent<Renderer>();

        state.SetTransforms(state.CubeTransform, transform);

        if (entity.IsOwner)
        {
            state.CubeColor = new Color(Random.value, Random.value, Random.value);
        }

        state.AddCallback("CubeColor", ColorChanged);
    }

    void ColorChanged()
    {
        GetComponent<Renderer>().material.color = state.CubeColor;
    }

    public override void SimulateOwner()
    {
        var speed = 4f;
        var movement = Vector3.zero;

        if (Input.GetKey(KeyCode.W)) { movement.z += 1; }
        if (Input.GetKey(KeyCode.S)) { movement.z -= 1; }
        if (Input.GetKey(KeyCode.A)) { movement.x -= 1; }
        if (Input.GetKey(KeyCode.D)) { movement.x += 1; }

        if (movement != Vector3.zero)
        {
            transform.position = transform.position + (movement.normalized * speed * BoltNetwork.FrameDeltaTime);
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            var flash = FlashColorEvent.Create(entity);
            flash.FlashColor = Color.red;
            flash.Send();
        }
    }

    public override void OnEvent(FlashColorEvent evnt)
    {
        _resetColorTime = Time.time + 0.2f;
        _renderer.material.color = evnt.FlashColor;
    }

    void Update()
    {
        if (_resetColorTime < Time.time)
        {
            _renderer.material.color = state.CubeColor;
        }
    }
}