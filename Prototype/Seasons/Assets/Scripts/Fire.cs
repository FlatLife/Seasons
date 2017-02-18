using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour {
    public Canvas canvas;
    public int slotNum;
    public CookingUI cookingUI;
    public float burnTime = 20;
    GameObject fireSpark;

    //burnState = 1(small), 2(medium), 3(large)
    public int fireState;
    public int lastFrame;
    public float animationTime;

    GameObject player;
    GameObject warmth;
    float distanceFromPlayer;
    public int fireHeatRange = 5;

    public int frameIndex;
    // An array with the sprites used for animation
    public Sprite[] animSprites;

    // Reference to the renderer of the sprite
    // game object
    SpriteRenderer animRenderer;

    private float timeSinceLastFrame; 
    AudioSource sound;   

    // Use this for initialization
    void Awake () {
        player = GameObject.Find("Player");
        warmth = GameObject.Find("Warmth");
        animRenderer = GetComponent<Renderer>() as SpriteRenderer;
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        cookingUI = Instantiate(Resources.Load<CookingUI>("CookingUI"));
        cookingUI.name = "CookingUI";
        cookingUI.Initialize(slotNum);
        cookingUI.transform.SetParent(canvas.transform);
        // Get a reference to game object renderer and
        // cast it to a Sprite Renderer
        animRenderer = GetComponent<Renderer>() as SpriteRenderer;
        //Sets the animation to the first frame
        timeSinceLastFrame = 0;
        fireState = -1;
        sound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update () {
        //if the fire is not dead
        if(fireState > 0){
            if(!sound.isPlaying){
                sound.Play();
            }

            //adjusting the sound volume
            distanceFromPlayer = Vector3.Distance(player.transform.position, transform.position);
            sound.volume = (1 - distanceFromPlayer/5)/2;
            //adjusting the players warmth
            if(distanceFromPlayer < fireHeatRange){
                warmth.GetComponent<BarScript>().increment(0.001f);
            }

            //changing state of the fire as time goes on
            burnTime -= Time.deltaTime;
            if(burnTime < 0){
                //decrease fire state
                changeState(fireState--, 0);          
            }
            //if its time to change the frame
            if(timeSinceLastFrame > animationTime){
                animRenderer.sprite = animSprites[frameIndex];
                timeSinceLastFrame = 0;
                frameIndex++;
            } else{
                timeSinceLastFrame = timeSinceLastFrame + Time.deltaTime;
            }
            //if we are at the last animation frame, reset it back to the first frame
            if(frameIndex > lastFrame){
                frameIndex = lastFrame - 4;
            }
            
            for (int i = 0; i < slotNum; i++) {
            Slot currentSlot = cookingUI.cookSlots[i].GetComponent<Slot>();
            //if checking the log slot
            if(i == slotNum - 1){
                if(!currentSlot.isEmpty && fireState == 1){
                    //if its a stick increase the state by 1
                    if(currentSlot.CurrentItem.type == ItemType.STICK){
                        currentSlot.DestroyItem();
                        changeState(fireState++, 1);
                    }
                    //if its a log increase the state by 2
                    if(currentSlot.CurrentItem.type == ItemType.WOOD){
                        currentSlot.DestroyItem();
                        changeState(fireState++, 1);
                        changeState(fireState++, 1);
                    }
                }
                if(!currentSlot.isEmpty){
                    //if its a stick/wood increase the state by 1
                    if(currentSlot.CurrentItem.type == ItemType.STICK){
                        currentSlot.DestroyItem();
                        if(fireState + 1 <= 3){
                            changeState(fireState++, 1);
                        }
                        burnTime += 20;
                    }else if (currentSlot.CurrentItem.type == ItemType.WOOD){
                        currentSlot.DestroyItem();
                        if(fireState + 2 <= 3){
                            changeState(fireState+=2, 1);
						}else if(fireState + 1 <= 3){
								changeState(fireState++ , 1);
						}
                        burnTime += 40;
                    }
                }
            } else {
                //check food slots               
                if(!currentSlot.isEmpty) {
                    if (currentSlot.CurrentItem.isFood) {
                        if (currentSlot.isCooking == false) {
                            currentSlot.cookTime = currentSlot.CurrentItem.cookTime;
                            currentSlot.isCooking = true;
                        }

                        currentSlot.cookTime -= Time.deltaTime;

                        if (currentSlot.cookTime <= 0) {
                            currentSlot.ClearSlot();
                            Item item = Instantiate(Resources.Load<Item>(currentSlot.CurrentItem.nextItem));
                            currentSlot.AddItem(item);
                            item.transform.position = new Vector3 (0,20f,0);
                            currentSlot.isCooking = false;
                        }
                    }
                }
            }
        }
        //if fire is dead, display deadFire sprite    
        } else {
            if(sound.isPlaying){
                sound.Stop();
                fireState = -1;
            }
            animRenderer.sprite = animSprites[15];
        }		    
    }


    void changeState(int state, int increaseOrDecrease){
		if (burnTime <= 0) {
			burnTime = 20;
		}
        //increase means to add fuel to the fire, causing the lastFrame to be increased
        if(increaseOrDecrease == 1){
            lastFrame += 5;
        } else{
            lastFrame -= 5;
        }
        frameIndex = (state - 1) * 5; 
    }

    public void startFire(){
        fireState = 3;
        burnTime = 20;
        lastFrame = 14;
        frameIndex = 10;
    }

    public void sparkFire(){
        fireSpark = Instantiate(Resources.Load<GameObject>("sparkFire"));
        fireSpark.transform.position = transform.position;
    }

}