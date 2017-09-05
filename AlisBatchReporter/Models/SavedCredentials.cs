using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlisBatchReporter.Models
{
    [Table("saved_credentials")]
    public class SavedCredentials
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public string Db { get; set; }
        public string Name { get; set; }
        public string ConnString { get; set; }
        public bool ChoseLast { get; set; }
        public bool Saved { get; set; }
    }
}
