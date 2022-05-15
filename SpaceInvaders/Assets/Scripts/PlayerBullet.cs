using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D bulletRB;

    [HideInInspector]
    public GameObject ObjectPulled;
    [HideInInspector]
    public List<GameObject> ObjectPulledList;
    [SerializeField]
    private Transform bulletTransform;

    private void OnEnable()
    {
        bulletRB.AddForce(Vector2.up*7, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ObjectPulledList = ObjectPuller.current.GetBulletBurst();
        ObjectPulled = ObjectPuller.current.GetGameObjectFromPull(ObjectPulledList);
        ObjectPulled.transform.position = transform.position + new Vector3(0, 0.3f, 0); //взрыв пули нужен чуть выше точки коллизии для хорошего эффекта
        ObjectPulled.SetActive(true);

        if (collision.gameObject.TryGetComponent<EnemyShip>(out EnemyShip enenmyShip))
        {
            enenmyShip.reduceTheLiveOfShip();
        }
        gameObject.SetActive(false);

    }

    private void Update()
    {
        if (bulletTransform.position.y>GameManager.vertScreenSize+1) gameObject.SetActive(false);
    }
}
