using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] Vector2 parallaxEffectMultiplier;
    private Transform cameraTransform;
    private Vector3 Lastcameraposition;

    [SerializeField] SpriteRenderer sprite;
    private float TextureUnitSizeX;

    // Start is called before the first frame update
    void Start()
    {
        cameraTransform = Camera.main.transform;
        Lastcameraposition = cameraTransform.position;
        Texture2D texture = sprite.sprite.texture;
        TextureUnitSizeX = texture.width / sprite.sprite.pixelsPerUnit;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 deltaMovement = cameraTransform.position - Lastcameraposition;
        transform.position += new Vector3(deltaMovement.x * parallaxEffectMultiplier.x, deltaMovement.y * parallaxEffectMultiplier.y);
        Lastcameraposition = cameraTransform.position;

        if (Mathf.Abs(cameraTransform.position.x - transform.position.x) >= TextureUnitSizeX)
        {
            float offsetPositionX = (cameraTransform.position.x - transform.position.x) % TextureUnitSizeX;
            transform.position = new Vector3(cameraTransform.position.x + offsetPositionX, transform.position.y);
        }
    }
}
