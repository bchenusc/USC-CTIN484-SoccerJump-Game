/* 
* Posted on June 3, 2013 by Jibran Syed
* at http://fortressfiasco.wordpress.com/2013/06/03/how-to-force-all-new-sounds-to-be-2d-in-unity/
*
*/

using UnityEditor;
using UnityEngine;

public class Force2DSound : AssetPostprocessor
{
	
	public void OnPreprocessAudio()
	{
		AudioImporter ai = assetImporter as AudioImporter;
		ai.threeD = false;
	}
}