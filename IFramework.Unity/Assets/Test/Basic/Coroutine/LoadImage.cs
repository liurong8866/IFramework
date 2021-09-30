using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
    
public class LoadImage : MonoBehaviour
{
    private RawImage rawImage;

    public Transform cube;

    private string url = "https://img.3dmgame.com/uploads/images/news/20210929/1632876123_323945.jpg";
    
    // Start is called before the first frame update
    void Start()
    {
        rawImage = gameObject.GetComponent<RawImage>();

        StartCoroutine(DownloadImage());
    }

    private void Update()
    {
        cube.Rotate(Vector3.up);
    }

    IEnumerator DownloadImage()
    {
        WWW www = new WWW(url);

        yield return www;

        rawImage.texture = www.texture;

    }
}
