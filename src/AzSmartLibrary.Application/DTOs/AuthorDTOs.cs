namespace AzSmartLibrary.Application.DTOs
{
    
    public record AuthorDto(int Id, string Name);

    
    public record CreateAuthorDto(string Name);

    
    public record UpdateAuthorDto(int Id, string Name);
}
