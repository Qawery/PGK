using UnityEngine;
using System.Collections;

/**
 * Opisuje typowy pocisk.
 * */
public class Bullet : MonoBehaviour 
{
	private GameObject owner;
	private string ownerTag;
	public float damage;
	public float AOE;
	public float speed;
	private float flyingTime;
	public float flyingTimeLimit;
	public ParticleSystem impactParticle;

	void Start () 
	{
		flyingTime = 0f;
	}

	void Update () 
	{
		flyingTime += Time.deltaTime;
		if(flyingTime >= flyingTimeLimit)
		{
			detonate();
		}
		else
		{
			Movement();
		}
	}

	void Movement()
	{
		transform.Translate(Vector3.forward * speed * Time.deltaTime);
	}

	void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject == owner)
		{
			//nie ma kolizji z samym sobą
			return;
		}
		if(collision.gameObject.tag == "projectile")
		{
			//przelatuje przez inne pociski
			return;
		}
		if(Team.isItMyAlly(ownerTag, collision.gameObject))
		{
			//przelatuje przez sojuszników
			return;
		}
		//detonuje
		detonate (collision.gameObject);
	}

	/**
	 * Detonuje pocisk.
	 * */
	private void detonate()
	{
		detonate (null);
	}

	private void detonate(GameObject collidedGameObject)
	{
		if(impactParticle != null)
		{
			impactParticle.Play();
		}

		if(collidedGameObject != null)
		{
			//zranienie trafionego celu
			if((owner != null && !Team.isItMyAlly(owner, collidedGameObject)))
			{
				if(collidedGameObject.GetComponent<Health>() != null)
				{
					collidedGameObject.GetComponent<Health>().ApplyChange(-damage);
				}
			}
			else if(owner == null && !Team.isItMyAlly(ownerTag, collidedGameObject))
			{
				if(collidedGameObject.GetComponent<Health>() != null)
				{
					collidedGameObject.GetComponent<Health>().ApplyChange(-damage);
				}
			}

			//wybuch AOE po zetknięciu z celem
			Collider[] collidersInRange = Physics.OverlapSphere(transform.position, AOE);
			if(collidersInRange != null && collidersInRange.Length > 0)
			{
				for(int i=0; i<collidersInRange.Length; i++)
				{
					if(owner != null && collidedGameObject != collidersInRange[i].gameObject && !Team.isItMyAlly(owner, collidersInRange[i].gameObject))
					{
						if(collidersInRange[i].gameObject.GetComponent<Health>() != null)
						{
							collidersInRange[i].gameObject.GetComponent<Health>().ApplyChange(-damage);
						}
					}
					else if(owner == null && collidedGameObject != collidersInRange[i].gameObject && !Team.isItMyAlly(ownerTag, collidersInRange[i].gameObject))
					{
						if(collidersInRange[i].gameObject.GetComponent<Health>() != null)
						{
							collidersInRange[i].gameObject.GetComponent<Health>().ApplyChange(-damage);
						}
					}
				}
			}
		}
		else
		{
			//wybuch bez zetknięcia z celem
			Collider[] collidersInRange = Physics.OverlapSphere(transform.position, AOE);
			if(collidersInRange != null && collidersInRange.Length > 0)
			{
				for(int i=0; i<collidersInRange.Length; i++)
				{
					if(owner != null && !Team.isItMyAlly(owner, collidersInRange[i].gameObject))
					{
						if(collidersInRange[i].gameObject.GetComponent<Health>() != null)
						{
							collidersInRange[i].gameObject.GetComponent<Health>().ApplyChange(-damage);
						}
					}
					else if(owner == null && !Team.isItMyAlly(ownerTag, collidersInRange[i].gameObject))
					{
						if(collidersInRange[i].gameObject.GetComponent<Health>() != null)
						{
							collidersInRange[i].gameObject.GetComponent<Health>().ApplyChange(-damage);
						}
					}
				}
			}
		}

		Destroy(gameObject); 
	}

	public void setOwner(GameObject newOwner)
	{
		owner = newOwner;
		setOwnerTag (owner.tag);
	}

	public void setOwnerTag(string newOwnerTag)
	{
		ownerTag = newOwnerTag;
	}

	public void setDamage(float newDamage)
	{
		damage = newDamage;
	}

	public void setAOE(float newAOE)
	{
		AOE = newAOE;
	}

	public void setSpeed(float newSpeed)
	{
		speed = newSpeed;
	}

	public float getSpeed()
	{
		return speed;
	}

	public void setFlyingTimeLimit(float newFlyingTimeLimit)
	{
		flyingTimeLimit = newFlyingTimeLimit;
	}
}
