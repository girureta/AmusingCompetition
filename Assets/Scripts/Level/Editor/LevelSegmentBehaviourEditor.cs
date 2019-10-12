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

        segment.GetStartPoint().position = Handles.PositionHandle(segment.GetStartPoint().position, Quaternion.identity);
        segment.GetEndPoint().position = Handles.PositionHandle(segment.GetEndPoint().position, Quaternion.identity);
        segment.GetStartTangent().position = Handles.PositionHandle(segment.GetStartTangent().position, Quaternion.identity);
        segment.GetEndTangent().position = Handles.PositionHandle(segment.GetEndTangent().position, Quaternion.identity);
    }

    [DrawGizmo(GizmoType.InSelectionHierarchy | GizmoType.NotInSelectionHierarchy)]
    static void DrawHandles(LevelSegmentBehaviour segment, GizmoType gizmoType)
    {
        GUIStyle style = new GUIStyle();
        style.normal.textColor = Color.blue;
        Handles.Label(segment.GetStartPoint().position, segment.name, style);
        Handles.DrawBezier(segment.GetStartPoint().position, segment.GetEndPoint().position, segment.GetStartTangent().position, segment.GetEndTangent().position, Color.red, null, 2f);
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
