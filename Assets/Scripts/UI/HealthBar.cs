using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{ 

    MaterialPropertyBlock matBlock;
    MeshRenderer meshRenderer;
    Camera mainCamera;
    EnemyManager enemyManager;

    private void Awake() {
        meshRenderer = GetComponent<MeshRenderer>();
        matBlock = new MaterialPropertyBlock();
        // get the damageable parent we're attached to
        enemyManager = GetComponentInParent<EnemyManager>();
    }

    private void Start() {
        // Cache since Camera.main is super slow
        mainCamera = Camera.main;
    }

    private void Update() {
        // Only display on partial health
        if (enemyManager.healthEnemy < enemyManager.maxHealth) 
        {
            meshRenderer.enabled = true;
            AlignCamera();
            UpdateParams();
        }
        else
        {
            meshRenderer.enabled = false;
        }

        if (enemyManager.healthEnemy <= 0)
        { 
            meshRenderer.enabled = false;
        }
    }

    private void UpdateParams() {
        meshRenderer.GetPropertyBlock(matBlock);
        matBlock.SetFloat("_Fill", enemyManager.healthEnemy / (float)enemyManager.maxHealth);
        meshRenderer.SetPropertyBlock(matBlock);
    }

    private void AlignCamera() 
    {
        if (mainCamera != null) 
        {
            var camXform = mainCamera.transform;
            var forward = transform.position - camXform.position;
            forward.Normalize();
            var up = Vector3.Cross(forward, camXform.right);
            transform.rotation = Quaternion.LookRotation(forward, up);
        }
    } 
}
