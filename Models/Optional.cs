using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace iserra_api.Models;

public sealed class Optional
{
    [Key] public Guid Id { get; set; }
    public string Nome { get; set; }
    [JsonIgnore]
    public ICollection<Advert> Anuncios { get; set; }
    
    private Optional() {}

    public Optional(string nome)
    {
        Id = Guid.NewGuid();
        Nome = nome;
    }
}