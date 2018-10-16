using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class StructureBlock : Placeable {

    public float hp;
    public float jointBreakTorque;

    private GameObject[] attachPoints;
    private GameObject turretAttatchPoint;
    private float initialGravity;

    private bool isTurretAttatchPointFree;
    private bool canPlace;
    private bool isPlaced;

    // Use this for initialization
    void Start () {
        attachPoints = new GameObject[4];
        setAttatchPoints();
        isTurretAttatchPointFree = true;
        canPlace = true;
        isPlaced = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (hp <= 0)
            DestroySelf();
	}

    private void DestroySelf() {
        Destroy(gameObject);
    }

    private void setAttatchPoints() {
        attachPoints[0] = gameObject.transform.Find("StructureAttatchPoint1").gameObject;
        attachPoints[1] = gameObject.transform.Find("StructureAttatchPoint2").gameObject;
        attachPoints[2] = gameObject.transform.Find("StructureAttatchPoint3").gameObject;
        attachPoints[3] = gameObject.transform.Find("StructureAttatchPoint4").gameObject;

        turretAttatchPoint = gameObject.transform.Find("TurretAttatchPoint").gameObject;
    }

    public GameObject[] getAttatchPoints() {
        return attachPoints;
    }

    public GameObject getTurretAttatchPoint() {
        if (isTurretAttatchPointFree)
            return turretAttatchPoint;
        return null;
    }

    public void doDamage(float damage) {
        hp -= damage;
    }

    public bool isDead() {
        return hp <= 0;
    }

    public override void place(Transform parent) {
        if (canPlace) {
            disableDragMode();
            CreateJoints();
            transform.parent = parent;
            isPlaced = true;
        } else {
            DestroySelf();
        }
    }

    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.tag == "AttackRange" || collision.tag == "Projectile" || isPlaced)
            return;
        canPlace = false;
        setSelectedRedColor(0.5f);
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.tag == "AttackRange" || collision.tag == "Projectile" || isPlaced)
            return;
        canPlace = true;
        setSelectedRedColor(1f);
    }

    override
    public void move() {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0;
        transform.position = pos;
        GameObject closestAttatcher = ConstructionManager.instance.getClosestStructureAttatcherInRange(pos, 0.33f);
        if (Equals(closestAttatcher, null)) {
            transform.position = pos;
            transform.rotation = new Quaternion();
        } else {
            Transform parent = closestAttatcher.transform.parent.transform;
            float dir = Mathf.Deg2Rad * parent.rotation.eulerAngles.z + Mathf.Atan2(closestAttatcher.transform.localPosition.y, closestAttatcher.transform.localPosition.x);
            float dist = 0.5f;
            Vector3 pPos = parent.localPosition;
            pPos.x += dist * Mathf.Cos(dir);
            pPos.y += dist * Mathf.Sin(dir);
            transform.position = pPos;
            transform.rotation = parent.rotation;
        }
    }

    override
    public void activateDragMode()
    {
        gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
        gameObject.GetComponent<BoxCollider2D>().size = new Vector2(0.9f, 0.9f);
        initialGravity = gameObject.GetComponent<Rigidbody2D>().gravityScale;
        gameObject.GetComponent<Rigidbody2D>().gravityScale = 0f;
        setSelectedAlpha(0.5f);
    }

    override
    public void disableDragMode()
    {
        gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
        gameObject.GetComponent<BoxCollider2D>().size = new Vector2(1, 1);
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3();
        gameObject.GetComponent<Rigidbody2D>().gravityScale = initialGravity;
        setSelectedAlpha(1f);
    }

    override
    protected void CreateJoints()
    {
        for (int i = 0; i < attachPoints.Length; i++) {
            StructureBlock sb = GetClosestStructureBlockToPos(attachPoints[i].transform.position);
            if (Equals(sb, null))
                continue;
            FixedJoint2D fj = gameObject.AddComponent<FixedJoint2D>();
            fj.connectedBody = sb.GetComponent<Rigidbody2D>();
            Vector2 anchor = fj.connectedAnchor;
            anchor.x = Mathf.Round(anchor.x);
            anchor.y = Mathf.Round(anchor.y);
            fj.autoConfigureConnectedAnchor = false;
            fj.connectedAnchor = anchor;
            fj.dampingRatio = 1;
            fj.breakTorque = jointBreakTorque;
        }
    }

    private StructureBlock GetClosestStructureBlockToPos(Vector3 pos)
    {
        GameObject o = ConstructionManager.instance.getClosestStructureAttatcherInRange(pos, 0.1f);
        if (Equals(o, null))
            return null;
        return o.transform.GetComponentInParent<StructureBlock>();
    }

    public void setTurretAttatchPointFree() {
        isTurretAttatchPointFree = true;
    }

    public void disableTurretAttatchPoint() {
        isTurretAttatchPointFree = false;
    }



}

