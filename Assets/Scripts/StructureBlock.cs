using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class StructureBlock : Placeable {

    public float hp;
    public float jointBreakTorque;

    protected Attatcher[] attatchPoints;
    private GameObject turretAttatchPoint;
    private float initialGravity;

    private bool isTurretAttatchPointFree;
    protected bool canPlace;
    protected bool isPlaced;

    // Use this for initialization
    void Start () {
        
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
        attatchPoints = GetComponentsInChildren<Attatcher>();

        turretAttatchPoint = gameObject.transform.Find("TurretAttatchPoint").gameObject;
    }

    public override Attatcher[] getAttatchPoints() {
        return attatchPoints;
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
        Attatcher closestAttatcher = ConstructionManager.instance.getClosestStructureAttatcherInRange(pos, 0.33f);
        if (Equals(closestAttatcher, null)) {
            transform.position = pos;
            transform.rotation = new Quaternion();
        } else {
            
            Transform parent = closestAttatcher.transform.parent.transform;

            float dir = Mathf.Deg2Rad * parent.rotation.eulerAngles.z;
            if (Mathf.Abs(closestAttatcher.transform.localPosition.x) > Mathf.Abs(closestAttatcher.transform.localPosition.y))
                dir += Mathf.Atan2(0, closestAttatcher.transform.localPosition.x);
            else
                dir += Mathf.Atan2(closestAttatcher.transform.localPosition.y, 0);

            Vector3 pPos = new Vector3();
            float dist = 0.25f;
            pPos.x = dist * Mathf.Cos(dir);
            pPos.y = dist * Mathf.Sin(dir);
            

            transform.position = closestAttatcher.transform.position + pPos;
            transform.rotation = parent.rotation;
        }
    }

    override
    public void activateDragMode()
    {
        gameObject.tag = "SelectedItem";
        gameObject.layer = 14; // SelectedItem
        GetComponent<SpriteRenderer>().sortingLayerName = "SelectedItem";
        gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
        gameObject.GetComponent<BoxCollider2D>().size = new Vector2(0.9f, 0.9f);
        initialGravity = gameObject.GetComponent<Rigidbody2D>().gravityScale;
        gameObject.GetComponent<Rigidbody2D>().gravityScale = 0f;
        setSelectedAlpha(0.5f);
    }

    override
    public void disableDragMode()
    {
        gameObject.tag = "StructureBlock";
        gameObject.layer = 11; // Destroyable
        GetComponent<SpriteRenderer>().sortingLayerName = "Default";
        gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
        gameObject.GetComponent<BoxCollider2D>().size = new Vector2(1, 1);
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3();
        gameObject.GetComponent<Rigidbody2D>().gravityScale = initialGravity;
        setSelectedAlpha(1f);
    }

    override
    protected void CreateJoints()
    {
        for (int i = 0; i < attatchPoints.Length; i++) {
            StructureBlock sb = GetClosestStructureBlockToPos(attatchPoints[i].transform.position);
            if (Equals(sb, null))
                continue;
            FixedJoint2D fj = gameObject.AddComponent<FixedJoint2D>();
            fj.connectedBody = sb.GetComponent<Rigidbody2D>();
            Vector2 anchor = fj.connectedAnchor;
            anchor.x = Mathf.Round(anchor.x*100)/100;
            anchor.y = Mathf.Round(anchor.y*100)/100;
            fj.autoConfigureConnectedAnchor = false;
            fj.connectedAnchor = anchor;
            fj.dampingRatio = 1;
            fj.breakTorque = jointBreakTorque;
        }
    }

    private StructureBlock GetClosestStructureBlockToPos(Vector3 pos)
    {
        Attatcher o = ConstructionManager.instance.getClosestStructureAttatcherInRange(pos, 0.1f);
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

