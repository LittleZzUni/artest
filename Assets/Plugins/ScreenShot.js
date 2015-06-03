#pragma strict

import System.Runtime.InteropServices;
import System.IO; 

#if UNITY_IPHONE && !UNITY_EDITOR

@DllImportAttribute("__Internal") static private function _ScreenshotWriteToAlbum(path : String) {}

static function Capture() {
   var width = Screen.width;
		var height = Screen.height;
		
		yield WaitForEndOfFrame(); //去掉协程试试看会发生什么。
		
		var tex:Texture2D = new Texture2D(width,height,TextureFormat.RGB24,false);//设置Texture2D
		
		tex.ReadPixels(new Rect(0,0,width,height),0,0);//获取Pixels           
		tex.Apply();//应用改变
		
		var bytes = tex.EncodeToPNG();//转换为byte[]
		Destroy(tex);
		
		File.WriteAllBytes(Application.persistentDataPath + "/Screenshot.png", bytes);  
		
//		Debug.Log(string.Format("截屏了一张图片: {0}", filename));     
    (new GameObject()).AddComponent.<ScreenShot>();
}

function Start() {
    yield;
    _ScreenshotWriteToAlbum(Application.persistentDataPath + "/Screenshot.png");
    Destroy(gameObject);
}

#else

static function Capture() {
    var width = Screen.width;
		var height = Screen.height;
		
		yield WaitForEndOfFrame(); //去掉协程试试看会发生什么。
		
		var tex:Texture2D = new Texture2D(width,height,TextureFormat.RGB24,false);//设置Texture2D
		
		tex.ReadPixels(new Rect(0,0,width,height),0,0);//获取Pixels           
		tex.Apply();//应用改变
		
		var bytes = tex.EncodeToPNG();//转换为byte[]
		Destroy(tex);
		
		File.WriteAllBytes(Application.persistentDataPath + "/Screenshot.png", bytes);  
		Debug.Log(Application.dataPath + "/Screenshot.png");  
		Debug.Log(Application.persistentDataPath + "/Screenshot.png");     
}

#endif


//static function IEScreenShot()
//{
//		var width = Screen.width;
//		var height = Screen.height;
//		
//		yield WaitForEndOfFrame(); //去掉协程试试看会发生什么。
//		
//		var tex:Texture2D = new Texture2D(width,height,TextureFormat.RGB24,false);//设置Texture2D
//		
//		tex.ReadPixels(new Rect(0,0,width,height),0,0);//获取Pixels           
//		tex.Apply();//应用改变
//		
//		var bytes = tex.EncodeToPNG();//转换为byte[]
//		Destroy(tex);
//		
//		File.WriteAllBytes(Application.persistentDataPath + "/Screenshot.png", bytes);  
//		
////		Debug.Log(string.Format("截屏了一张图片: {0}", filename));     
//		
//}