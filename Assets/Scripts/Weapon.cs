using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject bulletPrefab; // Префаб снаряда
    public Transform firePoint; // Точка выстрела
    public float fireForce = 20f; // Сила выстрела

    public void Fire(float fireRate, float nextFireTime) 
    {
        if (Time.time >= nextFireTime) // Проверяем, прошло ли достаточно времени с предыдущего выстрела
        {
            // Создаем снаряд и устанавливаем его позицию и поворот в соответствии с точкой выстрела
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            
            // Получаем компонент Rigidbody2D снаряда
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

            // Применяем силу к снаряду, направленную вперед (вдоль оси up точки выстрела)
            rb.AddForce(firePoint.up * fireForce, ForceMode2D.Impulse);
        }
    }
}
