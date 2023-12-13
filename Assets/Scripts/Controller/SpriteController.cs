using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteController : ParentObstacleController
{
    public enum ObType
    {
        ChangeAnimator,//애니메이터변경
        ChangeRendererOrder,//렌더러순서 변경
        ChangeSprite,//스프라이트변경
        FlipSprite//플립
    }
    public enum RendererOrder
    {
        Default,
        Layer1,
        Layer2,
        Layer3,
        UI
    }
    public enum FlipType 
    {
        X,
        Y
    }

    private Renderer renderer;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private ObType obType;
    
    /// <summary>
    /// ChangeAnimator변수
    /// </summary>
    [SerializeField] private Animator anim;
    [SerializeField] string path;
    /// <summary>
    /// RendererOrder변수
    /// </summary>
    [SerializeField] private RendererOrder rendererOrder;
    [SerializeField] private int orderNumber = 0;
    /// <summary>
    /// ChangeSprite 변수
    /// </summary>

    [SerializeField] Sprite inputImage;
    /// <summary>
    /// FlipSprite변수
    /// </summary>
    [SerializeField] private FlipType flipType;
    public override IEnumerator Activate()
    {
        switch (obType)
        {
            case ObType.ChangeAnimator:
                StartCoroutine(ChangeAnimator(anim,path));
                break;
            case ObType.ChangeRendererOrder:
                StartCoroutine(ChangeRendererOrder());
                break;
            case ObType.ChangeSprite:
                StartCoroutine(ChangeSprite());
                break;
            case ObType.FlipSprite:
                StartCoroutine(FlipSprite());
                break;
        }
        yield return base.Activate(); // 부모 클래스의 Activate 메서드 실행 
        //사실 ismoving과 별개로 움직이기 때문에 이걸 굳이 부모 activate를 실행하지 않아도 되지만 후에 test할때를 위해 그냥 실행하겠음. 
    }


    /// <summary>
    /// 애니메이터 바꿔주기
    /// path: 경로명
    /// anim: 바꿔줄 오브젝트의 애니메이터
    /// </summary>
    IEnumerator ChangeAnimator(Animator anim, string path)
    {
        path = path.Replace("Assets/Resources/","");
        path = path.Replace(".controller","");
        Debug.Log(path);
        anim.runtimeAnimatorController =
            (RuntimeAnimatorController)RuntimeAnimatorController.Instantiate(Resources.Load(path,
                typeof(RuntimeAnimatorController)));
        yield return null;
    }

    IEnumerator ChangeRendererOrder()
    {
        switch (rendererOrder)
        {
            case RendererOrder.Default:
                renderer.sortingLayerName = "Default";
                break;
            case RendererOrder.Layer1:
                renderer.sortingLayerName = "Layer1";
                break;
            case RendererOrder.Layer2:
                renderer.sortingLayerName = "Layer2";
                break;
            case RendererOrder.Layer3:
                renderer.sortingLayerName = "Layer3";
                break;
            case RendererOrder.UI:
                renderer.sortingLayerName = "UI";
                break;
        }

        renderer.sortingOrder = orderNumber;

        yield return null;
    }
    IEnumerator ChangeSprite()
    {
        if (GetComponent<SpriteRenderer>() != null)
        {
            spriteRenderer.sprite = inputImage;
        }
        
        yield return null;
    }
    IEnumerator FlipSprite()
    {
        if (flipType== FlipType.X)
        {
            spriteRenderer.flipX = true;
        }
        else if (flipType == FlipType.Y)
        {
            spriteRenderer.flipY = true;
        }
        yield return null;
    }

    private void Awake()
    {
        base.Awake();
        renderer = GetComponent<Renderer>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
}
