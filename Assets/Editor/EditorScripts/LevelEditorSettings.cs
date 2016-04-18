using System;
using UnityEngine;

namespace Assets.Editor.EditorScripts
{
    [Serializable]
    public class LevelEditorSettings : ScriptableObject
    {
        public float GridSizeInMeters = 1.23f;
        public bool ShouldOverrideUnitySnapping;
        public bool ShowBasicSettings = true;
        public bool ShowAppearanceSettings;
        public bool ShowGrid = true;
        public int GridsToShow = 50;
        public Color GridColor = Color.white;
        public UnityEngine.Object SelectedPrefab;
    }
}
