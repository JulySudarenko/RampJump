using UnityEngine;

public class DetailCreatingInfo
{
    public string Key;
    public Vector3 Position;
    public Quaternion Rotation;
    private Vector3 LocalScale;

    public DetailCreatingInfo(string key, Vector3 position, Quaternion rotation, Vector3 localScale)
    {
        Key = key;
        Position = position;
        Rotation = rotation;
        LocalScale = localScale;
    }
}
