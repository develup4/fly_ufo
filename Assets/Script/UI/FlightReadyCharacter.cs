using UnityEngine;
using System.Collections;

public class FlightReadyCharacter : MonoBehaviour
{
	public SkeletonDataAsset[] ARRAY_SKELETON;

	void Awake()
	{
		this.GetComponent<SkeletonAnimation> ().skeletonDataAsset = ARRAY_SKELETON [CharacterSelectManager.g_UFONumber];
	}
}
