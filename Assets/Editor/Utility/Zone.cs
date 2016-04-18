using UnityEngine;
using Assets.Editor.Utility;

public enum ZoneType
{
    Point,
    Box,
    Sphere
}

[ExecuteInEditMode]
public class Zone : MonoBehaviour
{
    public ZoneType ZoneType;
    public IconManager.LabelIcon IconColor;

    private BoxCollider boxCollider;
    private SphereCollider sphereCollider;

    private void CreateCollidersIfRequired()
    {
        if (boxCollider == null && ZoneType == ZoneType.Box)
        {
            if (sphereCollider != null)
            {
                UnityEditor.EditorApplication.delayCall += () =>
                {
                    DestroyImmediate(sphereCollider, true);
                };
                
            }
            boxCollider = gameObject.AddComponent<BoxCollider>();
        }

        if (sphereCollider == null && ZoneType == ZoneType.Sphere)
        {
            if (boxCollider != null)
            {
                UnityEditor.EditorApplication.delayCall += () =>
                {
                    DestroyImmediate(boxCollider, true);
                };
            }
            sphereCollider = gameObject.AddComponent<SphereCollider>();
        }

        if (ZoneType == ZoneType.Point)
        {
            if (boxCollider != null)
            {
                UnityEditor.EditorApplication.delayCall += () =>
                {
                    DestroyImmediate(boxCollider, true);
                };
            }
            if (sphereCollider != null)
            {
                UnityEditor.EditorApplication.delayCall += () =>
                {
                    DestroyImmediate(sphereCollider, true);
                };
            }
        }
    }

    void OnValidate()
    {
        CreateCollidersIfRequired();
        IconManager.SetIcon(gameObject, IconColor);
        
//        switch (AnnotationType)
//        {
//            case AnnotationType.Box:
//                sphereCollider.enabled = false;
//                boxCollider.enabled = true;
//                break;
//            case AnnotationType.Point:
//                sphereCollider.enabled = false;
//                boxCollider.enabled = false;
//                break;
//            case AnnotationType.Sphere:
//                sphereCollider.enabled = true;
//                boxCollider.enabled = false;
//                break;
//        }
         
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        switch (ZoneType)
        {
            case ZoneType.Sphere:
                if (sphereCollider != null)
                {
                    Gizmos.DrawWireSphere(transform.TransformPoint(sphereCollider.center), sphereCollider.radius);
                }
                break;
            case ZoneType.Box:
                if (boxCollider != null)
                {
                    Gizmos.DrawWireCube(transform.TransformPoint(boxCollider.center), boxCollider.size);
                }
                break;
        }
    }
}
