namespace NamedHttpClient.API.DataTransferObjects.Cep;

public sealed record ViaCepResponse(
    string Cep,
    string Logradouro,
    string? Complemento,
    string Bairro,
    string Localidade,
    string Uf,
    string Estado);
