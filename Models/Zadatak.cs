using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SuperHeroAPI.Models

{
    //[Column(name="id", TypeName ="jsonb")]
    public class Zadatak
    {
        [Key]
        [Column("tenant_id")]
        public Guid TenantId { get; set; }
        [Column("context")]
        public string Context { get; set; }
        [Column(name: "content", TypeName = "jsonb")]
        public Dictionary<string, string> Content { get; set; }
    }

    public class ZadatakCreateRequest
    {
        [Required]
        public string Context { get; set; }
        [Required]
        public Dictionary<string, string> Content { get; set; }
        [Required]
        public Guid TenantId { get; set;}
    }
}
//public Root()
//{
//Content.Add("ApiKey", "");
//    Content.Add("ApiSecret", "");
//    Content.Add("BaseUrl", "");
//}
//public class CreateRottDto
//{
//    [Required]
//    ContextBoundObject
//        zadatak

//}

