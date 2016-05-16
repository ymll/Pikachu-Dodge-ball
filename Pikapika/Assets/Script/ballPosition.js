#pragma strict
private var pokeball: GameObject[];
private var image: GameObject[] ;
private var viewPos: Vector3;

public class ballPosition extends NetworkBehaviour{
	function Start () {
		image = GameObject.FindGameObjectsWithTag("arrow");
	}

	function Update () {
		if (!isLocalPlayer){
			return;
		}
		pokeball = GameObject.FindGameObjectsWithTag("PlayPokeball");
		if (pokeball != null){
			viewPos = GetComponent.<Camera>().WorldToViewportPoint(pokeball[0].transform.position);
			if (viewPos.z < 0){
				if (viewPos.x < 0){
					//print("target is on the right side!");
					image[0].gameObject.GetComponent.<CanvasGroup>().alpha = 1f;
					image[1].gameObject.GetComponent.<CanvasGroup>().alpha = 0f;
				}
				else{
					//print("target is on the left side!");
					image[0].gameObject.GetComponent.<CanvasGroup>().alpha = 0f;
					image[1].gameObject.GetComponent.<CanvasGroup>().alpha = 1f;
				}
			}
			else{
				image[0].gameObject.GetComponent.<CanvasGroup>().alpha = 0f;
				image[1].gameObject.GetComponent.<CanvasGroup>().alpha = 0f;
			}
		}
	}
}