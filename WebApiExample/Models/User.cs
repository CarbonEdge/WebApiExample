namespace WebApiExample.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Points { get; set; }
    }

    public static class UsersSeedData
    {
        public static readonly List<User> Users = new List<User>
        {
            new User
            {
                Id = 1,
                Name = "jim",
                Points = 10
            },
            new User
            {
                Id = 2,
                Name = "ben",
                Points = 0.1
            }
        };
    }

}
