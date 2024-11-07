using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace iserra_api.Models;

public sealed class Optional
{
    [Key] public Guid Id { get; set; }
    public string Nome { get; set; }
    public ICollection<Advert> Anuncios { get; set; }
    
    private Optional() {}

    public Optional(string nome, ICollection<Advert> anuncios)
    {
        Id = new Guid();
        Nome = nome;
        Anuncios = anuncios;
    }
}