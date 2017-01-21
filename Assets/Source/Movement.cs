using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour 
{
	public Collider2D mCollider;
	public int rays = 4;
	public bool grounded = false;
	float offset = 0.1f;

	public void  Move(Vector2 motion, out bool colx, out bool coly)
	{
		float mgnt = motion.magnitude;
		bool collisionX = false;
		bool collisionY = false;

		if(motion.x != 0)
		{
			Vector2 botSidepoint =  (Vector2)transform.position 
				+ Vector2.down * mCollider.bounds.size.y/2
				- Vector2.down * offset
				+ Vector2.right * Mathf.Sign(motion.x) * mCollider.bounds.size.x/2;
			Vector2 topSidePoint = botSidepoint 
				+ Vector2.up* mCollider.bounds.size.y
				-Vector2.up* offset*2;

			Vector2 xmov = new Vector2(motion.x, 0);
			float xmgnt = motion.x;

			for(int i= 0; i <= rays; i++)
			{
				Vector2 p = Vector2.Lerp(botSidepoint, topSidePoint, (float)i/rays);

				if(Physics2D.Raycast(p, xmov.normalized, xmgnt*1.1f, 1 << LayerMask.NameToLayer("Solid")))
				{
					Debug.DrawRay(p,xmov,Color.red);
					collisionX = true;
				}
				else
				{
					Debug.DrawRay(p,xmov,Color.green);	
				}
			}
		}
		if(motion.y != 0)
		{
			Vector2 leftpoint =  (Vector2)transform.position 
				+ Vector2.up * Mathf.Sign(motion.y)* mCollider.bounds.size.y/2 
				+ Vector2.left * mCollider.bounds.size.x/2;
			Vector2 rightPoint = leftpoint + Vector2.right* mCollider.bounds.size.x;

			Vector2 ymov = new Vector2(0,motion.y);
			float ymgnt = motion.y;

			for(int i= 0; i <= rays; i++)
			{
				Vector2 p = Vector2.Lerp(leftpoint, rightPoint, (float)i/rays);

				if(Physics2D.Raycast(p, ymov.normalized, ymgnt, 1 << LayerMask.NameToLayer("Solid")))
				{
					Debug.DrawRay(p,ymov,Color.red);
					collisionY = true;
				}
				else
				{
					Debug.DrawRay(p,ymov,Color.green);	
				}
			}
		}

		if(collisionX )
		{
			motion.x = 0;
		}
		if(collisionY)
		{
			grounded = motion.y > 0 ? false : true;
			motion.y = 0;
		}
		else
		{
			grounded = false;
		}
		transform.Translate(motion);
		colx = collisionX;
		coly = collisionY;
	}
}
