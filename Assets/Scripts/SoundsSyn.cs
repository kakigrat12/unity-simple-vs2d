using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsSyn : MonoBehaviour
{
	[SerializeField] private AudioSource audioSource;
	[SerializeField] private PhotonView photonView;
	
	[Space]

	[SerializeField] private AudioClip[] sounds;

	public void ChangeSound(int orderSound)
    {
		photonView.RPC(nameof(ChangeSoundSin), RpcTarget.AllBuffered, orderSound);
	}

	public void ChangeSoundLocal(int orderSound)
    {
		ChangeSoundSin(orderSound);
	}

	public void ChangeClips(AudioClip[] newSounds)
    {
		sounds = newSounds;
		//ChangeSoundSin(0);
	}

	[PunRPC]
	private void ChangeSoundSin(int orderSound)
    {
		audioSource.Stop();
		if (orderSound >= 0 && sounds != null)
		{
			audioSource.clip = sounds[orderSound];
			audioSource.Play();
		}
        else
        {
			audioSource.clip = null;
		}
	}
}
