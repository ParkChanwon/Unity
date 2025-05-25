using UnityEngine;
using UnityEngine.UI;

public class ZombieManager : MonoBehaviour
{
    public static ZombieManager Instance; 
    public Text zombieCounterText;        
    private int zombieCount = 0;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        UpdateZombieCounterUI();
    }

    // 좀비 생성
    public void AddZombie()
    {
        zombieCount++;
        UpdateZombieCounterUI();
    }

    // 좀비 제거 
    public void RemoveZombie()
    {
        zombieCount--;
        UpdateZombieCounterUI();
    }

    //좀비 수 계산
    void UpdateZombieCounterUI()
    {
        if (zombieCounterText != null)
        {
            zombieCounterText.text = "Zombies: " + zombieCount;
        }
    }

    // 현재 좀비 수
    public int GetZombieCount()
    {
        return zombieCount;
    }
}