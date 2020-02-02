using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Trajectory))]
public class TrajectoryBuilder : Editor
{
    Trajectory lro;

    private void OnEnable()
    {
        lro = target as Trajectory;
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        GUILayout.Label("Build Outline", EditorStyles.boldLabel);
        if (GUILayout.Button("Build Outline"))
        {

            BuildOutline();
        }

    }

    void BuildOutline()
    {
        List<Vector3> poss = new List<Vector3>();
        foreach (GameObject go in lro.points)
        {
            poss.Add(go.transform.localPosition);
        }
        lro.lr.sortingOrder = 2;
        lro.lr.startWidth = 1f;
        lro.lr.endWidth = 1f;
        lro.lr.widthMultiplier = lro.width;
        lro.lr.useWorldSpace = false;
        lro.lr.positionCount = poss.Count;
        lro.lr.SetPositions(poss.ToArray());
        lro.lr.loop = false;
    }
}