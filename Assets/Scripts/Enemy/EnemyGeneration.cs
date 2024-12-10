using UnityEngine;

public class EnemyGeneration : MonoBehaviour
{
    public GameObject[] enemyPrefab;
    public GameObject[] BossPrefabs;
    private float EnemyTime;
    private float BossTime;
    private int currentWaveSize; // 当前波次敌人数目
    private int lastKillNum;

    void Start()
    {
        EnemyTime = 0f;
        BossTime = 15.0f;
        currentWaveSize = 1; // 初始波次敌人数目设置为1

        lastKillNum = AttackPlayer.KillNumber;
        EnemyCreat();
    }

    void Update()
    {
        if ( AttackPlayer.KillNumber != lastKillNum)
        {
            // 检查是否达到生成下一波的条件（这里假设每4个敌人被击杀时升级）
            if ((AttackPlayer.KillNumber- lastKillNum) % (4*currentWaveSize) == 0)
            {
                NextWave();
            }
        }

        if (BossPrefabs != null)
        {
            BossGeneration();
        }
    }

    void NextWave()
    {
        lastKillNum = AttackPlayer.KillNumber;

        // 增加波次敌人数量，并确保不超过最大值10
        currentWaveSize += 1;
        currentWaveSize = Mathf.Min(currentWaveSize, 10);

        // 立即生成当前波次的所有敌人
        for (int i = 0; i < currentWaveSize; i++)
        {
            EnemyCreat();
        }

    }

    void EnemyCreat()
    {
        int num = Random.Range(0, enemyPrefab.Length);
        var enemy = Instantiate(enemyPrefab[num]);
        enemy.transform.parent = this.transform;
        enemy.transform.localPosition = Vector3.zero;
    }

    void BossGeneration()
    {
        if (BossTime > 0) { BossTime -= Time.deltaTime; }
        else
        {
            int num = Random.Range(0, BossPrefabs.Length);
            var boss = Instantiate(BossPrefabs[num]);
            boss.transform.parent = this.transform;
            boss.transform.localPosition = Vector3.zero;
            BossTime = Random.Range(15, 30);
        }
    }
}