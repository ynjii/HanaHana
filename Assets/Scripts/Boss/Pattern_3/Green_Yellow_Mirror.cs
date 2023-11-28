using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Green_Yellow_Mirror : MonoBehaviour
{
    [SerializeField] List<SpriteRenderer> _greenMirrorSprites;
    [SerializeField] List<SpriteRenderer> _yellowMirrorSprites;

    [SerializeField] List<Collider2D> _greenMirrorCols;
    [SerializeField] List<Collider2D> _yellowMirrorCols;
    GameObject _player;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {

        // �ʷ� �������� Ȱ��ȭ �ɶ�
        if (_player.GetComponent<SpriteRenderer>().flipX)
        {
            ActivateMirror(_greenMirrorSprites, _greenMirrorCols);
            DeActivateMirror(_yellowMirrorSprites, _yellowMirrorCols);
        }
        // ��� �������� Ȱ��ȭ �ɶ�
        else
        {
            ActivateMirror(_yellowMirrorSprites, _yellowMirrorCols);
            DeActivateMirror(_greenMirrorSprites, _greenMirrorCols);
        }
    }

    void ActivateMirror(List<SpriteRenderer> sprites, List<Collider2D> colliders)
    {
        foreach (SpriteRenderer sprite in sprites)
        {
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 1f);
        }
        foreach (Collider2D col in colliders)
        {
            col.isTrigger = false;
        }
    }

    void DeActivateMirror(List<SpriteRenderer> sprites, List<Collider2D> colliders)
    {
        foreach (SpriteRenderer sprite in sprites)
        {
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 0.5f);
        }
        foreach (Collider2D col in colliders)
        {
            col.isTrigger = true;
        }
    }
}
