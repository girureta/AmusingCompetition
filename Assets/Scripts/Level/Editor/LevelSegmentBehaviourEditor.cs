using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LevelSegmentBehaviour))]
public class LevelSegmentBehaviourEditor : Editor
{
    private void OnSceneViewGUI(SceneView sv)
    {
        LevelSegmentBehaviour segment = target as LevelSegmentBehaviour;

        segment.startPoint.position = Handles.PositionHandle(segment.startPoint.position, Quaternion.identity);
        segment.endPoint.position = Handles.PositionHandle(segment.endPoint.position, Quaternion.identity);
        segment.startTangent.position = Handles.PositionHandle(segment.startTangent.position, Quaternion.identity);
        segment.endTangent.position = Handles.PositionHandle(segment.endTangent.position, Quaternion.identity);
    }

    [DrawGizmo(GizmoType.InSelectionHierarchy | GizmoType.NotInSelectionHierarchy)]
    static void DrawHandles(LevelSegmentBehaviour segment, GizmoType gizmoType)
    {
        GUIStyle style = new GUIStyle();
        style.normal.textColor = Color.blue;
        Handles.Label(segment.startPoint.position, segment.name, style);
        Handles.DrawBezier(segment.startPoint.position, segment.endPoint.position, segment.startTangent.position, segment.endTangent.position, Color.red, null, 2f);
    }

    void OnEnable()
    {
        SceneView.duringSceneGui += OnSceneViewGUI;
    }

    void OnDisable()
    {
        SceneView.duringSceneGui -= OnSceneViewGUI;
    }
}
