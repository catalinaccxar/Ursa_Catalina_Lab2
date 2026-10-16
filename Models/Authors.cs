namespace Ursa_Catalina_Lab2.Models
{
    public class Authors
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public ICollection<Book>? Books { get; set; }   // navigation property

        public string FullName => FirstName + " " + LastName;
    }
}
