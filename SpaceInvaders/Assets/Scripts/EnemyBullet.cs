
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D bulletRB;

    [HideInInspector]
    public GameObject ObjectPulled;
    [HideInInspector]
    public List<GameObject> ObjectPulledList;

    [SerializeField]
    private Transform bulletTransform;
    [SerializeField]
    private TrailRenderer bulletTrail;

    private void OnEnable()
    {
        bulletRB.AddForce(Vector2.down * 6, ForceMode2D.Impulse);
        bulletTrail.Clear();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ObjectPulledList = ObjectPuller.current.GetBulletBurst();
        ObjectPulled = ObjectPuller.current.GetGameObjectFromPull(ObjectPulledList);
        ObjectPulled.transform.position = transform.position + new Vector3(0, -0.3f, 0); //взрыв пули нужен чуть ниже точки коллизии для хорошего эффекта
        ObjectPulled.SetActive(true);

        if (collision.gameObject.TryGetComponent<PlayerShip>(out PlayerShip playerShip))
        {
            playerShip.reduceTheLiveOfShip();
        }
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (bulletTransform.position.y < -GameManager.vertScreenSize - 1) gameObject.SetActive(false);
    }
}
