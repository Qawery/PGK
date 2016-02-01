using UnityEngine;
using System.Collections;

public class PlayerAnimation : MonoBehaviour {

    private Animator anim;
    private Vector3 prevPosition;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        prevPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Animation();
        prevPosition = transform.position;
    }

    private void Animation()
    {
        Vector3 diffPosition = transform.position - prevPosition;
        if (anim != null)
        {
            if (diffPosition != Vector3.zero)
                anim.SetBool("walking", true);
            else
                anim.SetBool("walking", false);

            bool right = anim.GetBool("right");
            if (diffPosition.x > 0 && !right ||
                (diffPosition.x < 0 && right))
            {
                Vector3 theScale = transform.localScale;
                theScale.x *= -1;
                transform.localScale = theScale;
                anim.SetBool("right", !right);
            }
        }
    }
}
