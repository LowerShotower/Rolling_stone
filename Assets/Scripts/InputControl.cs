using UnityEngine;
using System.Collections;

public class InputControl : MonoBehaviour {


	public static int x;
	public static int y;

	Vector2 touchOrigin;
	Vector2 touchEnd;

	void Awake (){
		
	}

	void Update (){
		//x = 0;
		//y = 0;

#if UNITY_STANDALONE || UNITY_EDITOR

		x = (int)Input.GetAxisRaw ("Horizontal");
		y = (int)Input.GetAxisRaw ("Vertical");





		#elif UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE
		
		x = 0;     //Used to store the horizontal move direction.
		y = 0;       //Used to store the vertical move direction.
		//Check if Input has registered more than zero touches
		if (Input.touchCount > 0)
		{
			//Store the first touch detected.
			Touch myTouch = Input.touches[0];
			
			//Check if the phase of that touch equals Began
			if (myTouch.phase == TouchPhase.Began)
			{
				//If so, set touchOrigin to the position of that touch
				touchOrigin = myTouch.position;
			}
			
			//If the touch phase is not Began, and instead is equal to Ended and the x of touchOrigin is greater or equal to zero:
			else if (myTouch.phase == TouchPhase.Ended && touchOrigin.x >= 0)
			{
				//Set touchEnd to equal the position of this touch
				Vector2 touchEnd = myTouch.position;
				
				//Calculate the difference between the beginning and end of the touch on the x axis.
				float vertical = touchEnd.x - touchOrigin.x;
				
				//Calculate the difference between the beginning and end of the touch on the y axis.
				float horizontal = touchEnd.y - touchOrigin.y;
				
				//Set touchOrigin.x to -1 so that our else if statement will evaluate false and not repeat immediately.
				touchOrigin.x = -1;
				
				//Check if the difference along the x axis is greater than the difference along the y axis.
				if ((horizontal>0&&vertical>0)||(vertical<0&&horizontal<0))
					//If x is greater than zero, set horizontal to 1, otherwise set it to -1
					x = horizontal > 0 ? 1 : -1;
				else
					//If y is greater than zero, set horizontal to 1, otherwise set it to -1
					y = vertical > 0 ? -1 : 1;
			}
		}
	
		#endif //End of mobile platform dependendent compilation section started above with #elif

		
	}

}	