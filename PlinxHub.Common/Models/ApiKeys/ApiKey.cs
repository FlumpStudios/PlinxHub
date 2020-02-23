using System.ComponentModel.DataAnnotations;
using System;
namespace PlinxHub.Common.Models.ApiKeys
{
    public class ApiKey
    {
        [Key]
        public int ApiKeyId { get; set; }
        public string Key {get; set;}
        public Guid UserId { get; set; }
    }

}