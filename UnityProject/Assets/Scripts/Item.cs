using UnityEngine;

public class Item : MonoBehaviour
{
    [HideInInspector]
    public bool pass;

    [Header("道具音效")]
    public AudioClip sound;

    private Transform player;
    private AudioSource aud;

    private void Start()
    {
        Physics.IgnoreLayerCollision(10, 10, false);
        aud = GetComponent<AudioSource>();
        player = GameObject.Find("鼠王").transform;

        HandleCollision();
    }

    private void Update()
    {
        GoToPlayer();
    }

    private void HandleCollision()
    {
        Physics.IgnoreLayerCollision(10, 8);
        Physics.IgnoreLayerCollision(10, 9);
    }

    private void GoToPlayer()
    {
        if (pass)
        {
            Physics.IgnoreLayerCollision(10, 10);
            transform.position = Vector3.Lerp(transform.position, player.position, 0.8f * Time.deltaTime * 20);

            if (Vector3.Distance(transform.position, player.position) < 1.5f && !aud.isPlaying)
            {
                aud.PlayOneShot(sound, 0.3f);
                Destroy(gameObject, 0.3f);
            }
        }
    }
}
