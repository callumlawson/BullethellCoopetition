using Assets.Editor.Utility;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor.EditorScripts
{
    public class LevelEditorWindow : EditorWindow
    {
        private LevelEditorSettings settings;

        [MenuItem("Veonix/LevelEditor")]
        public static void ShowWindow()
        {
            GetWindow(typeof(LevelEditorWindow));
        }

        [UsedImplicitly]
        void OnEnable()
        {
            settings = EditorWindowSettings.GetSettings<LevelEditorSettings>();
            SceneView.onSceneGUIDelegate += OnSceneGUI;
        }

        [UsedImplicitly]
        void OnGUI()
        {
            EditorGUILayout.HelpBox("Welcome to the Veonix Level Editor", MessageType.None);

            settings.ShowBasicSettings = EditorGUILayout.Foldout(settings.ShowBasicSettings, "Basic Configuration Settings");
            if (settings.ShowBasicSettings)
            {
                settings.GridSizeInMeters = EditorGUILayout.FloatField("Grid Size In Meters", settings.GridSizeInMeters);
                settings.ShouldOverrideUnitySnapping = EditorGUILayout.Toggle("Override Unity Snapping", settings.ShouldOverrideUnitySnapping);
                if (settings.ShouldOverrideUnitySnapping)
                {
                    EditorGUILayout.HelpBox("Unity editor snapping is overriden. If you hold control GameObjects will snap to a fixed grid of " +
                                            "the size you specified.", MessageType.Info);
                }
            }

            settings.ShowAppearanceSettings = EditorGUILayout.Foldout(settings.ShowAppearanceSettings, "Appearance Settings");
            if (settings.ShowAppearanceSettings)
            {
                settings.ShowGrid = EditorGUILayout.BeginToggleGroup("Show snapping grid", settings.ShowGrid);

                settings.GridsToShow = EditorGUILayout.IntSlider("Grids To Show", settings.GridsToShow, 10, 100);
                settings.GridColor = EditorGUILayout.ColorField("Grid Color", settings.GridColor);

                EditorGUILayout.EndToggleGroup();
            }

            if (GUI.changed)
            {
                SceneView.RepaintAll();
            }
        }

        void OnSceneGUI(SceneView sceneview)
        {
            SnapToGrid();
            if (settings.ShowGrid)
            {
                DrawGridLines();
            }
        }

        private void DrawGridLines()
        {
            Handles.color = settings.GridColor;

            for (var x = -settings.GridsToShow; x <= settings.GridsToShow; x++)
            {
                Handles.DrawLine(new Vector3(x*settings.GridSizeInMeters, 0, -settings.GridsToShow*settings.GridSizeInMeters),
                    new Vector3(x*settings.GridSizeInMeters, 0, settings.GridsToShow*settings.GridSizeInMeters));
            }

            for (var z = -settings.GridsToShow; z <= settings.GridsToShow; z++)
            {
                Handles.DrawLine(new Vector3(settings.GridsToShow*settings.GridSizeInMeters, 0, z*settings.GridSizeInMeters),
                    new Vector3(-settings.GridsToShow*settings.GridSizeInMeters, 0, z*settings.GridSizeInMeters));
            }
        }

        private void SnapToGrid()
        {
            if (Event.current.modifiers == EventModifiers.Control && settings.ShouldOverrideUnitySnapping)
            {
                var selectedGameObjects = Selection.transforms;

                foreach (var selectedGameObject in selectedGameObjects)
                {
                    selectedGameObject.transform.position = RoundTransform(selectedGameObject.transform.position, 2.5f);
                }
            }
        }

        private Vector3 RoundTransform(Vector3 v, float snapValue)
        {
            return new Vector3
            (
                snapValue * Mathf.Round(v.x / snapValue),
                snapValue * Mathf.Round(v.y / snapValue),
                snapValue * Mathf.Round(v.z / snapValue)
            );
        }

        [UsedImplicitly]
        void OnDisable()
        {
            EditorWindowSettings.SaveSettings(settings);
            SceneView.onSceneGUIDelegate -= OnSceneGUI;
        }

        [UsedImplicitly]
        void OnDestroy()
        {
            EditorWindowSettings.SaveSettings(settings);
        }

        [UsedImplicitly]
        void OnLostFocus()
        {
            EditorWindowSettings.SaveSettings(settings);
        }
    }
}