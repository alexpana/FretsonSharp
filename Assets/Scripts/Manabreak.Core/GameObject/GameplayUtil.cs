using UnityEngine;

[Core.Component]
public class GameplayUtil : MonoBehaviour
{
    /// <summary>
    ///     Mouse coordinates projected from screen into world space terrain
    /// </summary>
    public static Vector3 MouseProjectionTerrain;

    public static Camera MainCamera;

    private void Start()
    {
        MainCamera = Camera.main;
    }

    private void Update()
    {
        MouseProjectionTerrain = RayCastFromCameraToGround(Input.mousePosition);
    }

    public static Vector3 RayCastFromCameraToGround(Vector2 mousePosition)
    {
        return RayCastToGround(MainCamera.ScreenPointToRay(mousePosition));
    }

    public static Vector3 RayCastToGround(Ray ray)
    {
        var scale = ray.origin.y / Mathf.Abs(ray.direction.y);
        return ray.origin + ray.direction * scale;
    }

    public static Vector3 CameraNearPlaneToWorldPos(Vector2 mousePosition)
    {
        return MainCamera.ScreenToWorldPoint(mousePosition);
    }

    public static bool RayCastFromCameraToCustomLayer(Vector2 mousePosition, int layerMask, out Collider result)
    {
        RaycastHit hitInfo;
        if (Physics.Raycast(MainCamera.ScreenPointToRay(mousePosition),
                out hitInfo,
                float.MaxValue,
                layerMask
            ))
        {
            result = hitInfo.collider;
            return true;
        }

        result = null;
        return false;
    }

    public static Vector3 MouseProject()
    {
        return MouseProjectionTerrain;
    }

    public static int GetDownAlphaKey()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) return 1;

        if (Input.GetKeyDown(KeyCode.Alpha2)) return 2;

        if (Input.GetKeyDown(KeyCode.Alpha3)) return 3;

        if (Input.GetKeyDown(KeyCode.Alpha4)) return 4;

        if (Input.GetKeyDown(KeyCode.Alpha5)) return 5;

        if (Input.GetKeyDown(KeyCode.Alpha6)) return 6;

        if (Input.GetKeyDown(KeyCode.Alpha7)) return 7;

        if (Input.GetKeyDown(KeyCode.Alpha8)) return 8;

        if (Input.GetKeyDown(KeyCode.Alpha9)) return 9;

        if (Input.GetKeyDown(KeyCode.Alpha0)) return 0;

        return -1;
    }
}