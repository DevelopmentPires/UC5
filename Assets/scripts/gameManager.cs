using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour {

    public static gameManager gM;

    private int weapom;


    private void Awake()
    {
        if (gM = null)
        {
            gM = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }


    // Use this for initialization
    void Start () {

       
	}
	
	// Update is called once per frame
	void Update () {

    }


        public void setWeapon(int weapon)
        {
            this.weapom = weapon;
        }

        public int getWeapon()
        {
            return weapom;
        }
   
    

}
