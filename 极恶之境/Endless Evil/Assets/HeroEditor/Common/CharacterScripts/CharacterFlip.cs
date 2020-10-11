using UnityEngine;

namespace Assets.HeroEditor.Common.CharacterScripts
{
    /// <summary>
    /// Makes character to look at cursor side (flip by X scale).
    /// </summary>
    public class CharacterFlip : MonoBehaviour
    {
        bool flip = false;
        public void Update()
        {
	        var scale = transform.localScale;

	        scale.x = Mathf.Abs(scale.x);

            if (GetComponent<CharacterController>().facingDirection == -1 && !flip) 
            { 
                scale.x *= -1;
                flip = !flip;
            } else if (GetComponent<CharacterController>().facingDirection == 1 && flip)
            {
                scale.x *= -1;
                flip = !flip;
            }

			transform.localScale = scale;
        }
    }
}