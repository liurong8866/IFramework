using IFramework.Engine;
using UnityEngine;
using UnityEngine.Video;

public class NetAudioExample : MonoBehaviour
{
    private VideoPlayer videoPlayer;
    private AudioSource audioSource;
    ResourceLoader loader = new ResourceLoader();

    private void Awake() {
        videoPlayer = gameObject.GetComponent<VideoPlayer>();
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    void Start() {
        loader.AddToLoad<AudioClip>(
            // ResourcesUrlType.VIDEO + "https://vd3.bdstatic.com/mda-ka5ayxd86t7z2h1r/mda-ka5ayxd86t7z2h1r.mp4",
            ResourcesUrlType.AUDIO_MP3 + "https://mp32.9ku.com/upload/128/2017/06/14/862272.mp3", (result, res) => {
                if (result) {
                    audioSource.clip = res.Asset as AudioClip;
                    audioSource.Play();
                }
            });
        loader.LoadAsync();
    }
}
