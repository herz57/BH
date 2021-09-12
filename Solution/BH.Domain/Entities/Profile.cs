namespace BH.Domain.Entities
{
    public class Profile
    {
        public int ProfileId { get; set; }

        public string UserId { get; set; }

        public long Balance { get; set; }


        public User User { get; set; }
    }
}
