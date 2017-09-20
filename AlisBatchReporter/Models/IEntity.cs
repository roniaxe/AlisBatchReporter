using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlisBatchReporter.Models
{
    public interface IEntity
    {
        [Key, Column(Order = 0)]
        int Id { get; set; }
    }
}