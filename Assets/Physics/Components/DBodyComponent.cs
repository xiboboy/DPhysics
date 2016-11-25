﻿using UnityEngine;
using FixedPointMath;
using System.Collections;

/// <summary>
/// Monobehaviour component used to define a new physics object.
/// </summary>
public class DBodyComponent : MonoBehaviour {

    public float speed;

    public float mass;
    public float restitution;
    public float friction;

    private ColliderComponent colliderComponent;
    private DBody body;

    //TODO: remove this temporary code
    void Start() {
        this.colliderComponent = GetComponent<ColliderComponent>();
        body = new DBody(
            colliderComponent.RequireCollider(),
            new Vector2f(transform.position),
            intf.Create(mass),
            intf.Create(restitution),
            intf.Create(friction)
            );
        PhysicsEngine.Instance.AddObject(body);

        //update position
        StartCoroutine(UpdatePosition());
    }

    void Update() {
        Vector2 direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (direction != Vector2.zero) {
            direction *= 10;
            body.AddForce(new Vector2f(direction));
        }

        //commented this part because of the coroutine
        /*if (body.IsSleeping() || body.IsFixed())
            return;

        this.transform.position = body.InterpolatedPosition();
        */
    }

    void OnDrawGizmos() {
        /*if (physicsObject == null)
            return;
        Gizmos.color = (physicsObject.IsSleeping()) ? Color.green : Color.white;
        Gizmos.DrawCube(transform.position, Vector3.one * 2);*/
    }

    private IEnumerator UpdatePosition() {
        while (true) {
            if (body.IsSleeping() || body.IsFixed())
                yield return null;

            this.transform.position = body.InterpolatedPosition();
            yield return null;
        }
    }
}