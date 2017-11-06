using System.ComponentModel.DataAnnotations.Schema;

namespace AlisBatchReporter.Models
{
    public class Distribution : Entity
    {
        [Index(IsUnique = true)]
        public string EmailAddress { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool DistFlag { get; set; }
        public string DisplayMember { get; set; }

        public override string ToString()
        {
            return $@"{FirstName} {LastName} ({EmailAddress})";
        }
    }
}
