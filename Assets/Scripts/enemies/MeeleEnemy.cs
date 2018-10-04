namespace Assets.Scripts.enemies
{
    public class MeeleEnemy : Enemy {



        // Use this for initialization
        new void Start () {
            base.Start();
        }
	
        // Update is called once per frame
        new void Update () {
            base.Update();
		
        }

        public override void attack()
        {
            throw new System.NotImplementedException();
        }
    }
}
