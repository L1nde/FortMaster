using UnityEngine;
using System.Collections;
using UnityEditor;
using Assets.Scripts.Turrets;
using Assets.Scripts;

[CustomEditor(typeof(TurretData))]
public class TurretDataEditor : Editor {

    public override void OnInspectorGUI() {
        TurretData myTarget = (TurretData)target;

        myTarget.name = EditorGUILayout.TextField("Name", myTarget.name);
        myTarget.cost = EditorGUILayout.IntField("Cost", myTarget.cost);
        myTarget.reloadTime = EditorGUILayout.FloatField("Reload time", myTarget.reloadTime);
        myTarget.attackRange = EditorGUILayout.FloatField("Attack range", myTarget.attackRange);
        myTarget.minxRange = EditorGUILayout.FloatField("Min horizontal Range", myTarget.minxRange);
        myTarget.projectile = (PlayerProjectile) EditorGUILayout.ObjectField("Projectile", myTarget.projectile, typeof(PlayerProjectile), false);
        myTarget.aniController = (RuntimeAnimatorController)EditorGUILayout.ObjectField("Animator controller", myTarget.aniController, typeof(RuntimeAnimatorController), false);
        myTarget.fireSound = (AudioClipGroup)EditorGUILayout.ObjectField("Projectile", myTarget.fireSound, typeof(AudioClipGroup), false);
        EditorGUILayout.LabelField("Impact Dps", myTarget.dps.ToString());
        EditorGUILayout.LabelField("Dps per $", myTarget.DpsPerCost.ToString());

    }
}
