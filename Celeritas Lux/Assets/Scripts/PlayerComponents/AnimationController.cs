using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{

    /// <summary> Is the sprite currently facing right? if false, then facing left. </summary>
    private bool facingRight = true;

    /// <summary> The sprite for the object. </summary>
    public MeshFilter sprite;

    public bool isFacingRight() => facingRight;

    bool opposite = true;

    public Animator animator;
    public SpriteRenderer spriterendered;

    public void Flip()
    {
        spriterendered.flipX = true;
        Vector3 workingScale = sprite.transform.localScale;
        workingScale.x *= -1f;
        sprite.transform.localScale = workingScale;
        facingRight = !facingRight;
        spriterendered.flipX = opposite;
        opposite = !opposite;
    }
}
