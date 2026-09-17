using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnLoot : MonoBehaviourPunCallbacks
{
    [SerializeField] private AssetsItemContainer assetsItemContainer;

    [SerializeField] private Transform[] spawnPoints;
    [Range(0, 100)]
    [SerializeField] private float generalPopulation;
    [SerializeField] private IItem[] spawnObjectInfos;
    private int spawnObject;

    //[SerializeField] private PhotonView pV;
    //private int playersOnScene;

    private void Start()
    {
        spawnObjectInfos = assetsItemContainer.assetItems;
        //pV.RPC(nameof(NewPlayerOnScene), RpcTarget.MasterClient);
        if (PhotonNetwork.IsMasterClient) H();
        //if (PhotonNetwork.IsMasterClient) StartCoroutine(Wait());
        //if (PhotonNetwork.IsMasterClient)
        //{
        //    Invoke("H", 1f);
        //}
    }

    //[PunRPC]
    //private void NewPlayerOnScene()
    //{
    //    playersOnScene++;
    //    if (PhotonNetwork.CurrentRoom.PlayerCount == playersOnScene) H();
    //}

    //private IEnumerator Wait()
    //{
    //    while (PhotonNetwork.AutomaticallySyncScene)
    //    {
    //        yield return null;
    //    }
    //    H();
    //}

    private void H()
    {
        float sumPopulations = 0;
        foreach (var s in spawnObjectInfos)
        {
            sumPopulations += s.Population;
        }


        float onePersent = sumPopulations / 100;

        spawnObjectInfos[0].Population /= onePersent;
        for (int i = 1; i < spawnObjectInfos.Length; i++)
        {
            spawnObjectInfos[i].Population = spawnObjectInfos[i].Population / onePersent + spawnObjectInfos[i - 1].Population;
        }

        Debug.Log(((float)spawnPoints.Length / 100) * generalPopulation);
        for (int i = 0; i < ((float)spawnPoints.Length / 100) * generalPopulation; i++) //Mathf.RoundToInt(spawnPoints.Length / 100 * generalPopulation)
        {
            int persentWeapon = Random.Range(0, 100);
            spawnObject = 0;
            for (int l = 1; l < spawnObjectInfos.Length; l++)
            {
                if (persentWeapon <= spawnObjectInfos[l].Population && persentWeapon > spawnObjectInfos[l - 1].Population)
                {
                    spawnObject = l;
                }
            }

            int numberSpawnPoints = Random.Range(0, spawnPoints.Length - 1 - i);
            object[] data = new object[3] { spawnObject, 0, 0 };
            PhotonNetwork.InstantiateRoomObject("Thing", spawnPoints[numberSpawnPoints].position + new Vector3(0f, 0f, -0.1f), spawnPoints[numberSpawnPoints].rotation, 0, data);
            //thing.GetComponent<Thing>().assetItem = spawnObject;
            //thing.GetComponent<PhotonView>().OnPreNetDestroy(null);
            //Transform vessel = spawnPoints[number];
            spawnPoints[numberSpawnPoints] = spawnPoints[spawnPoints.Length - 1 - i];
        }
    }
}
