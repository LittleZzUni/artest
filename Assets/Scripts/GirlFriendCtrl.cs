using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GirlFriendCtrl : MonoBehaviour {
	
	private Animator _animator;
	private AnimatorStateInfo _preStateInfo;
	private AnimatorStateInfo _curStateInfo;
	
	public float waitTime = 3f;
	public bool isRandom = true;
	private string imagePath;
	public AnimationClip[] _FaceClips;
	public string[] _FaceMotionName;

	public AudioClip[] _VoiceClips;
	public AudioClip[] _YadaVoice;
	public AudioClip[] _HourVoice;
	// Use this for initialization
	
//	private static readonly string YadaState = "BaseLayer.DAMAGED01";

	void Start () {
		_animator = GetComponent<Animator>();
		_curStateInfo = _animator.GetCurrentAnimatorStateInfo(0);
		_preStateInfo = _curStateInfo;

		_FaceClips = Resources.LoadAll<AnimationClip> ("FaceMotion");
		_VoiceClips = Resources.LoadAll<AudioClip> ("ChanVoice");
		_YadaVoice = Resources.LoadAll<AudioClip> ("YadaVoice");
		_HourVoice = Resources.LoadAll<AudioClip> ("HourVoice");
		_FaceMotionName = new string[_FaceClips.Length];

		for(int i = 0; i < _FaceClips.Length ; i++)
		{
			_FaceMotionName[i] = _FaceClips[i].name;
		}

		StartCoroutine (RandomChangeMotion());
	}
	
	// Update is called once per frame
	void Update () {

		RaycastHit hitInfo;
		if (Input.GetMouseButton (0)) {
			if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),out hitInfo, Mathf.Infinity)){
				if(hitInfo.collider.tag == "face"){
					ChangeFace();
				}
				else if(hitInfo.collider.tag == "xiong"){
					YadaVoice();
				}
			}
				
		}



		if (_animator.GetBool ("Next")) {
			_curStateInfo = _animator.GetCurrentAnimatorStateInfo(0);
			if (_preStateInfo.nameHash != _curStateInfo.nameHash) {
				_animator.SetBool ("Next", false);
				_preStateInfo = _curStateInfo;
			}
			
		}
	}

	private void ChangeFace()
	{
		_animator.SetLayerWeight (1, 1);
		int index = Random.Range (0, _FaceMotionName.Length);
		_animator.CrossFade (_FaceMotionName[index],0);
		if (audio.isPlaying)
		{
			audio.Stop();
		}
		audio.clip = _VoiceClips [index];
		audio.Play();
	}

	private void YadaVoice()
	{
		if (audio.isPlaying)
		{
			audio.Stop();
		}
		audio.clip = _YadaVoice [1] ;
		audio.Play();
//		AnimatorStateInfo stateInfo = _animator.GetCurrentAnimatorStateInfo(0);
//		if (!stateInfo.IsName(YadaState))
//		{
//			// 每次设置完参数之后，都应该在下一帧开始时将参数设置清空，避免连续切换
//			_animator.SetInteger (YadaState,0);
//		}


	}
	IEnumerator RandomChangeMotion()
	{
		while (true) 
		{
			if(isRandom)
			{
				_animator.SetBool("Next",true);
			}
			yield return new WaitForSeconds(waitTime);
		}
	}
	public void OnAskHour()
	{
		int hour = System.DateTime.Now.Hour;
		if (audio.isPlaying) 
		{
			audio.Stop();
		}
		audio.clip = _HourVoice [hour];
		audio.Play ();
	}


}
