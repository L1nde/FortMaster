using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class StructureBlock : MonoBehaviour {

    public float hp;
    private GameObject[] attachPoints;

    private float initialGravity;

	// Use this for initialization
	void Start () {
        attachPoints = new GameObject[4];
        setAttatchPoints();
	}
	
	// Update is called once per frame
	void Update () {
        if (hp <= 0)
            DestroySelf();
	}

    private void DestroySelf()
    {
        Destroy(gameObject);
    }

    private void setAttatchPoints()
    {
        attachPoints[0] = gameObject.transform.Find("StructureAttatchPoint1").gameObject;
        attachPoints[1] = gameObject.transform.Find("StructureAttatchPoint2").gameObject;
        attachPoints[2] = gameObject.transform.Find("StructureAttatchPoint3").gameObject;
        attachPoints[3] = gameObject.transform.Find("StructureAttatchPoint4").gameObject;
    }

    public GameObject[] getAttatchPoints()
    {
        return attachPoints;
    }

    public void doDamage(float damage)
    {
        hp -= damage;
    }

    public void activateDragMode()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        initialGravity = gameObject.GetComponent<Rigidbody2D>().gravityScale;
        gameObject.GetComponent<Rigidbody2D>().gravityScale = 0f;
        setSelectedAlpha(0.5f);
    }

    public void disableDragMode()
    {
        
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3();
        gameObject.GetComponent<Rigidbody2D>().gravityScale = initialGravity;
        setSelectedAlpha(1f);
    }

    private void setSelectedAlpha(float a)
    {
        Color c = gameObject.GetComponent<SpriteRenderer>().color;
        c.a = a;
        gameObject.GetComponent<SpriteRenderer>().color = c;
    }

    public void CreateJoints()
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
        }
    }

    private StructureBlock GetClosestStructureBlockToPos(Vector3 pos)
    {
        GameObject o = ConstructionManager.instance.getClosestAttatcherInRange(pos, 0.3f);
        if (Equals(o, null))
            return null;
        return o.transform.GetComponentInParent<StructureBlock>();
    }

}

