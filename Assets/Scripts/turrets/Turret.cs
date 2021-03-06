﻿using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Turret : Placeable {
    public float attackRange;
    public PlayerProjectile projectile;
    public float reloadTime;
    public float minxRange;
    public AudioClipGroup fireSound;
    private float currentReloadTime;
    private Animator anim;
    private bool isEnabled = false;
    private CircleCollider2D attackRangeCollider;
    


    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Start () {
        researched = true;
        currentReloadTime = reloadTime;
        foreach (var child in GetComponentsInChildren<CircleCollider2D>()) {
                if (child.gameObject.tag == "AttackRange") {
                    attackRangeCollider = child;
                    attackRangeCollider.radius = attackRange;
                    break;
                }
            }
    }
	
	// Update is called once per frame
	void Update () {
        currentReloadTime -= Time.deltaTime;
        if (currentReloadTime <= 0 && isEnabled) {
            currentReloadTime = reloadTime;
            fire();
        }
	}

    private void fire() {
        Collider2D target = getTarget();
        if (target != null){
            if (minxRange < Mathf.Abs(transform.position.x - target.transform.position.x)) {
                
                anim.Play("Fire");
                if (fireSound != null)
                    fireSound.play();
                PlayerProjectile p = Instantiate(projectile);
                p.init(transform.rotation.eulerAngles.z, transform.position, new Vector2(target.transform.position.x, target.transform.position.y - target.transform.localScale.y / 2));
                transform.rotation = p.transform.rotation;
            }
        }
    }

    private Collider2D getTarget() {
        Collider2D[] colliders = new Collider2D[10];
        int count = attackRangeCollider.GetContacts(colliders);
        if (count != 0) {
            Collider2D closest = colliders[0];
            float closestDist = 10000;
            foreach (Collider2D collider in colliders) {
                if (collider == null)
                    continue;
                float d = Vector3.Distance(transform.position, collider.transform.position);
                if (d < closestDist) {
                    closest = collider;
                    closestDist = d;
                }
            }
            return closest;
        }
        else {
            return null;
        }
    }

    public override void place(Transform parent) {
        if (GameController.instance.canAfford(cost)) {
            disableDragMode();
            CreateJoints();
        } else {
            Destroy(gameObject);
        }
    }

    
    public override void placeFree(Transform parent) {
        CreateJoints();
        isEnabled = true;
    }

    public override Attacher[] getAttachPoints()
    {
        return null;
    }

    protected override void CreateJoints() {
        StructureBlock sb = GetClosestStructureBlockToPos(transform.position);
        if (Equals(sb, null)) {
            Destroy(gameObject);
            return;
        }
        UIController.Instance.tutorialPart(4);
        sb.placeSound.play();
        GameController.instance.removeGold(cost);
        sb.disableTurretAttachPoint();
        HingeJoint2D fj = gameObject.AddComponent<HingeJoint2D>();
        fj.connectedBody = sb.GetComponent<Rigidbody2D>();
        transform.parent = sb.transform;
    }

    public override void move() {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0;
        transform.position = pos;
        GameObject closestAttacher = ConstructionManager.instance.getClosestTurretAttacherInRange(pos, 0.33f);
        if (Equals(closestAttacher, null)) {
            setSelectedRedColor(0.5f);
            transform.position = pos;
            transform.rotation = new Quaternion();
        } else {
            setSelectedRedColor(1f);
            Transform parent = closestAttacher.transform.parent.transform;
            transform.position = parent.position;
            transform.rotation = parent.rotation;
        }
    }
    

    public override void activateDragMode() {
        setSelectedAlpha(0.5f);
        GetComponent<SpriteRenderer>().sortingLayerName = "SelectedItem";
    }

    public override void disableDragMode() {
        setSelectedAlpha(1f);
        GetComponent<SpriteRenderer>().sortingLayerName = "Foreground";
        isEnabled = true;
    }

    private StructureBlock GetClosestStructureBlockToPos(Vector3 pos) {
        GameObject o = ConstructionManager.instance.getClosestTurretAttacherInRange(pos, 0.1f);
        if (Equals(o, null))
            return null;
        return o.transform.GetComponentInParent<StructureBlock>();
    }

    public void setAnimController(RuntimeAnimatorController controller)
    {
        anim.runtimeAnimatorController = controller;
    }

}
