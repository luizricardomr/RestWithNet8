using System.ComponentModel.DataAnnotations.Schema;

namespace RestWithNet8.Api.Model.Base
{
    public class BaseEntity
    {
        [Column("id")]
        public long Id { get; set; }
    }
}
