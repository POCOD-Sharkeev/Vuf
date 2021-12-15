using UnityEngine;
using System.Collections.Generic;
using UnityEditor;

[System.Serializable] 
public class JSONDataObject
{
    public List<Vector3> pos;
    public List<Quaternion> rot;
    public List<Color> color;
    public List<int> number;

    public void ClearAllFields()
    {
        if (pos.Count != 0)
        {
            pos.Clear();
            rot.Clear();
            color.Clear();
            number.Clear();
        }
        return;
    }
}
