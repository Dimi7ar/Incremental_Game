namespace Cosmos_Reset
{
    public class CosmicBonus
    {
        public int id;
        public int requirement;
        public string description;
        public bool obtained;

        public CosmicBonus(int id, int requirement, string description, bool obtained)
        {
            this.id = id;
            this.requirement = requirement;
            this.description = description;
            this.obtained = obtained;
        }
    }
}