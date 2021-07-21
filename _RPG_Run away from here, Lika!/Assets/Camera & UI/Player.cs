using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageAble
{
    [SerializeField] int enemyLayer = 7;

    [SerializeField] float maxHealthPoints = 100f;
    [SerializeField] float currentHealthPoints =100f;   

    [SerializeField] float damagePerHit= 10f;
    [SerializeField] float hitDelay= 1.5f;
   // [SerializeField] float minAttackRange= 2f;
    [SerializeField] float maxAttackRange= 2f;
    

    GameObject currentTarget;
    CameraRaycaster cameraRaycaster;
    float distanceToEnemy;
    float lastHitTime;
    private void Start()
    {
        cameraRaycaster = FindObjectOfType<CameraRaycaster>();
        cameraRaycaster.notifyMouseClickObservers += OnMouseClick;
        currentHealthPoints = maxHealthPoints;
    }
    void OnMouseClick(RaycastHit raycastHit, int layerHit)
    {
        if (layerHit == enemyLayer)
        {
            var enemy = raycastHit.collider.gameObject;
            if ((Vector3.Distance(this.transform.position, enemy.transform.position)) > maxAttackRange)
            {
                return;
            }
            currentTarget = enemy;
            var enemyComponent = enemy.GetComponent<Enemy>();
            if (Time.time - lastHitTime > hitDelay)
            {
            enemyComponent.TakeDamage(damagePerHit);
            lastHitTime = Time.time;
            }
        }
    }
    void IDamageAble.TakeDamage(float damage) {
        currentHealthPoints = Mathf.Clamp(currentHealthPoints - damage,0f,maxHealthPoints);
    }
    public float healthAsPercentage { get { return currentHealthPoints / maxHealthPoints ; } }

}
