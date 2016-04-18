using System.Reflection;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace Assets.Editor.Utility
{
    public class IconManager
    {
        public enum LabelIcon
        {
            Gray = 0,
            Blue,
            Teal,
            Green,
            Yellow,
            Orange,
            Red,
            Purple
        }

        public enum Icon
        {
            CircleGray = 0,
            CircleBlue,
            CircleTeal,
            CircleGreen,
            CircleYellow,
            CircleOrange,
            CircleRed,
            CirclePurple,
            DiamondGray,
            DiamondBlue,
            DiamondTeal,
            DiamondGreen,
            DiamondYellow,
            DiamondOrange,
            DiamondRed,
            DiamondPurple
        }

        private static GUIContent[] labelIcons;
        private static GUIContent[] largeIcons;

        public static void SetIcon(GameObject gObj, LabelIcon icon)
        {
#if UNITY_EDITOR
            if (labelIcons == null)
            {
                labelIcons = GetTextures("sv_label_", string.Empty, 0, 8);
            }

            SetIcon(gObj, labelIcons[(int)icon].image as Texture2D);
#endif
        }

        public static void SetIcon(GameObject gObj, Icon icon)
        {
#if UNITY_EDITOR
            if (largeIcons == null)
            {
                largeIcons = GetTextures("sv_icon_dot", "_pix16_gizmo", 0, 16);
            }

            SetIcon(gObj, largeIcons[(int)icon].image as Texture2D);
#endif
        }

        private static void SetIcon(GameObject gObj, Texture2D texture)
        {
#if UNITY_EDITOR
            var ty = typeof(EditorGUIUtility);
            var mi = ty.GetMethod("SetIconForObject", BindingFlags.NonPublic | BindingFlags.Static);
            mi.Invoke(null, new object[] { gObj, texture });
#endif
        }

        public static void ClearIcon(GameObject gObj)
        {
#if UNITY_EDITOR
            SetIcon(gObj, (Texture2D)null);
#endif
        }

        private static GUIContent[] GetTextures(string baseName, string postFix, int startIndex, int count)
        {
#if UNITY_EDITOR
            GUIContent[] guiContentArray = new GUIContent[count];

            var t = typeof(EditorGUIUtility);
            var mi = t.GetMethod("IconContent", BindingFlags.Public | BindingFlags.Static, null, new[] { typeof(string) }, null);

            for (int index = 0; index < count; ++index)
            {
                guiContentArray[index] = mi.Invoke(null, new object[] { baseName + (object)(startIndex + index) + postFix }) as GUIContent;
            }

            return guiContentArray;
#else
            return new GUIContent[count];
#endif
        }
    }
}
