using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iserra_api.Models;

public sealed class Photo
{
    [Key] public Guid Id { get; set; }
    public string Url { get; set; }
    public string Chave { get; set; }
    public Guid AnuncioId { get; set; }
    [ForeignKey("AdvertId")] public Advert Anuncio { get; set; }
    
    private Photo() {}

    public Photo(string url, string chave, Guid anuncioId, Advert anuncio)
    {
        Id = new Guid();
        Url = url;
        Chave = chave;
        AnuncioId = anuncioId;
        Anuncio = anuncio;
    }
}