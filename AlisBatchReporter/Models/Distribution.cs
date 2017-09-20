using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Windows.Forms;

namespace AlisBatchReporter.Models
{
    public class Distribution : IEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Index(IsUnique = true)]
        public string EmailAddress { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool DistFlag { get; set; }
        public string DisplayMember { get; set; }

        public override string ToString()
        {
            //DisplayMember = $@"{FirstName} {LastName} ({EmailAddress})";
            return $@"{FirstName} {LastName} ({EmailAddress})";
        }
    }
}
