using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PlayerFire : MonoBehaviour
{
    public GUISkin skin;
    public Transform FreeZombie;   // 좀비 프리팹
    public Transform medpack;      // 힐팩 프리팹
    public GameObject bulletFactory; // 총알 프리팹
    public GameObject firePosition; // 총구 위치
    public GameObject bulletEffect; // 총알 효과
    public float bulletSpeed = 20f; // 총알 속도
    public Text timerText;
    private float timer = 30f;
    private bool isTimer = true;
    private Camera mainCamera;
    private ParticleSystem ps;
    private Vector3 lookDir;

    private float HP = 100f;
    private bool isDead = false;

    private int zombieCount = 0;    // 현재 생성된 좀비 수
    private int maxZombies = 25;     // 최대 생성할 좀비 수
    private int medpackCount = 0;   // 현재 생성된 힐팩 수
    private int maxMedpacks = 2;    // 최대 생성할 힐팩 수
    

    void Start()
    {
        mainCamera = Camera.main;
        ps = bulletEffect.GetComponent<ParticleSystem>();
        
        if (SceneManager.GetActiveScene().name == "GameScene2")
    {
        timer = 60f;  // Stage2 == 60
    }else{
        timer = 30f;  // Stage1 == 30
    }
    }

    void Update()
    {
        if (isDead) return;

        isDead = HP <= 0; // 체력이 0 이하이면 사망
        RotatePlayer();

        if (Input.GetMouseButtonDown(0)) // 마우스 왼쪽 클릭
        {
            Fire();
        }

        MakeZombieOrMedpack();

        if(isTimer && timer >0){
            timer -= Time.deltaTime;
        }
        if(timer <= 0){
            isTimer = false;
            LoadNextStage();
        }
        if (isTimer)
        {
            timerText.text = "Time : " + Mathf.Ceil(timer).ToString() + " (s)";
        }
        else
        {
            timerText.text = "Stage Cleared!";
        }
    }

    // 플레이어가 마우스 방향을 바라보게 회전
    void RotatePlayer()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
        {
            lookDir = hit.point;
            lookDir.y = 0;
            transform.LookAt(lookDir);
        }
    }

    // 총알 발사
    void Fire()
    {
        GameObject bullet = Instantiate(bulletFactory);
        bullet.transform.position = firePosition.transform.position;

        Vector3 aimDirection = GetAimDirection();
        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
        if (bulletRigidbody != null)
        {
            bulletRigidbody.velocity = aimDirection * bulletSpeed;
        }

        Destroy(bullet, 2f); // 2초 후 총알 삭제
    }

    // 에임 방향 계산
    Vector3 GetAimDirection()
    {
        Ray ray = mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            return (hit.point - firePosition.transform.position).normalized;
        }
        return mainCamera.transform.forward; 
    }

    void OnCollisionEnter(Collision coll)
    {
        if (isDead) return;

        if (coll.transform.CompareTag("ZOMBIE"))
        {
            HP -= 10;
        }
        else if (coll.transform.CompareTag("MEDPACK"))
        {
            HP = Mathf.Clamp(HP + 20, 0, 100);
            Destroy(coll.gameObject);
        }
    }

    void OnCollisionStay(Collision coll)
    {
        if (!isDead && coll.transform.CompareTag("ZOMBIE"))
        {
            HP -= 2f * Time.deltaTime;
        }
    }

    // 좀비 또는 힐팩 생성
    void MakeZombieOrMedpack()
    {
        if (zombieCount >= maxZombies && medpackCount >= maxMedpacks) return;
        if (Random.Range(0, 1000) < 980) return;

        Vector3 pos;
        bool isOffScreen;
        do
        {
            pos = new Vector3(
                Random.Range(-15f, 15f),
                0f,
                Random.Range(-15f, 15f)
            );
            Vector3 screenPoint = mainCamera.WorldToScreenPoint(pos);
            isOffScreen = screenPoint.x < 0 || screenPoint.x > Screen.width ||
                          screenPoint.y < 0 || screenPoint.y > Screen.height;
        } while (!isOffScreen);

        if (Random.Range(0, 500) < 200 && zombieCount < maxZombies)
        {
            Instantiate(FreeZombie, pos, Quaternion.identity);
            zombieCount++;
            ZombieManager.Instance.AddZombie();
        }
        else if (medpackCount < maxMedpacks)
        {
            Instantiate(medpack, pos, Quaternion.identity);
            medpackCount++;
        }
    }

    void OnGUI()
    {
        GUI.skin = skin;

        int w = Screen.width / 2;
        int h = Screen.height / 2;

        DrawProgress();

        if (!isDead)
        {
            Vector2 mousePosition = Input.mousePosition;
            mousePosition.y = Screen.height - mousePosition.y;

            GUI.DrawTexture(new Rect(mousePosition.x - 24, mousePosition.y - 24, 46, 48),
                Resources.Load("crosshair") as Texture2D);
            return;
        }
        if (ZombieManager.Instance.GetZombieCount() == 0){
            if (GUI.Button(new Rect(w - 60, h - 50, 120, 50), "Next Level")){
                    UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene2");  //다음 스테이지 이동 
            }
        }    
        
        if (GUI.Button(new Rect(w - 60, h - 50, 120, 50), "Play Game"))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene1");
        }

        if (GUI.Button(new Rect(w - 60, h + 50, 120, 50), "Quit Game"))
        {
            Application.Quit();
        }
    }
    private void LoadNextStage()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene2");  // 다음 씬으로 이동
    }
    void DrawProgress()
    {
        int w = Screen.width;
        float x = w * 0.81f;
        float bw = x * 0.22f;
        float pw = bw * HP / 100;

        GUI.DrawTexture(new Rect(x, 5, bw, 20), Resources.Load("Progress_back") as Texture2D);
        GUI.DrawTexture(new Rect(x, 5, pw, 20), Resources.Load("Progress_bar") as Texture2D);
    }
}