using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class GetJSONModel : MonoBehaviour
{

    public JSONDataObject robots;

    string json, m_json;

    [SerializeField] GameObject[] prefabs;

    [SerializeField] Transform Grid;

    public List<GameObject> sceneObjects;

    public GameObject jo;

    public void SetObjectsToScene()
    {
        for (int i = 0; i < robots.pos.Count; i++)
        {
            GameObject go = (GameObject)Instantiate(prefabs[robots.number[i]], Grid);
            go.transform.localPosition = robots.pos[i];
            go.transform.localRotation = robots.rot[i];
            go.transform.localScale = new Vector3(3f, 3f, 3f);
            sceneObjects.Add(go);
            //go.transform.position += Vector3.right * 100f;
            jo.SetActive(false);
        }
        img.sprite = yes;
        return;
    }

    public void GetDataFromServer()
    {
        StartCoroutine(GetObjectJson());
        return;
    }

    [SerializeField] GameObject InternetAccess;

    [SerializeField] Image img;

    [SerializeField] Sprite yes, no;

    IEnumerator GetObjectJson()
    {
        robots.ClearAllFields();
        foreach(GameObject a in sceneObjects)
        {
            Destroy(a);
        }
        sceneObjects.Clear();
        yield return new WaitForEndOfFrame();
        string url = "http://e80659.hostde31.fornex.host/testarrobotrek.com/obj.json";

        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        if (request.error != null)
        {
            InternetAccess.SetActive(true);
            img.sprite = no;
        }
        else
        {
            if (request.isDone)
            {
                if (request.downloadHandler.text != null)
                {
                    robots = JsonUtility.FromJson<JSONDataObject>(request.downloadHandler.text);
                    
                }
                else
                {
                    // Msg NO ACCESSED FILE
                    yield break;
                }
            }
            yield return new WaitForEndOfFrame();
            SetObjectsToScene();
        }
    }
}
