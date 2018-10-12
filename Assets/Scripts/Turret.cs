using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : Placeable {
    public float attackRange;
    public PlayerProjectile projectile;
    public float reloadTime;
    private float currentReloadTime;
    private Animator anim;
    private bool enabled = false;
    private CircleCollider2D attackRangeCollider;
    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
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
        if (currentReloadTime <= 0 && enabled) {
            currentReloadTime = reloadTime;
            fire();
        }
	}

    private void fire() {
        Collider2D target = getTarget();
        if (target != null){
            anim.Play("Fire");
            PlayerProjectile p = Instantiate(projectile);
            p.init(transform.rotation.eulerAngles.z, transform.position, new Vector2(target.transform.position.x, target.transform.position.y - target.transform.localScale.y / 2));
        }
    }

    private Collider2D getTarget() {
            Collider2D[] colliders = new Collider2D[10];
            int count = attackRangeCollider.GetContacts(colliders);
            if (count != 0) {
                return colliders[0];
            }
            else {
                return null;
            }
        }

    public override void place(Transform parent) {
        disableDragMode();
        CreateJoints();
    }

    protected override void CreateJoints() {
        StructureBlock sb = GetClosestStructureBlockToPos(transform.position);
        if (Equals(sb, null)) {
            Destroy(gameObject);
            return;
        }
        sb.disableTurretAttatchPoint();
        HingeJoint2D fj = gameObject.AddComponent<HingeJoint2D>();
        fj.connectedBody = sb.GetComponent<Rigidbody2D>();
        transform.parent = sb.transform;
    }

    public override void move() {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0;
        transform.position = pos;
        GameObject closestAttatcher = ConstructionManager.instance.getClosestTurretAttatcherInRange(pos, 0.33f);
        if (Equals(closestAttatcher, null)) {
            transform.position = pos;
            transform.rotation = new Quaternion();
        } else {
            Transform parent = closestAttatcher.transform.parent.transform;
            transform.position = parent.position;
            transform.rotation = parent.rotation;
        }
    }
    

    public override void activateDragMode() {
        setSelectedAlpha(0.5f);
    }

    public override void disableDragMode() {
        setSelectedAlpha(1f);
        enabled = true;
    }

    private StructureBlock GetClosestStructureBlockToPos(Vector3 pos) {
        GameObject o = ConstructionManager.instance.getClosestTurretAttatcherInRange(pos, 0.1f);
        if (Equals(o, null))
            return null;
        return o.transform.GetComponentInParent<StructureBlock>();
    }



}
