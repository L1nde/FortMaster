using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(PlayerProjectile))]
public class PlayerProjectileEdiotr : Editor {

    public override void OnInspectorGUI() {
        PlayerProjectile myTarget = (PlayerProjectile)target;

        EditorGUILayout.LabelField("Basic", EditorStyles.boldLabel);
        myTarget.damage = EditorGUILayout.FloatField("Damage", myTarget.damage);
        myTarget.accuracySpread = EditorGUILayout.FloatField("Accuracy (lower is better)", myTarget.accuracySpread);
        myTarget.noGravityTime = EditorGUILayout.FloatField("No Gravity time", myTarget.noGravityTime);
        myTarget.deathDelay = EditorGUILayout.FloatField("Decay time", myTarget.deathDelay);
        EditorGUILayout.LabelField("Transform", EditorStyles.boldLabel);
        myTarget.posOffset = EditorGUILayout.Vector3Field("LaunchPosOffset", myTarget.posOffset);
        myTarget.shootingAngleOffset = EditorGUILayout.Slider("ShootingAngleOffset", myTarget.shootingAngleOffset, -180, 180);
        EditorGUILayout.LabelField("Other", EditorStyles.boldLabel);
        myTarget.stayStuck = EditorGUILayout.Toggle("StayStuck", myTarget.stayStuck);
        myTarget.parabola = EditorGUILayout.Toggle("Parabola", myTarget.parabola);
        myTarget.affectedByGravity = EditorGUILayout.Toggle("AffectedByGravity", myTarget.affectedByGravity);
        if (!myTarget.parabola)
            constSpeed(myTarget);
    }

    private void constSpeed(PlayerProjectile myTarget) {
        myTarget.speed = EditorGUILayout.FloatField("Speed", myTarget.speed);
    }
}
