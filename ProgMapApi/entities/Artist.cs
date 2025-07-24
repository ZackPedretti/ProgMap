namespace ProgMapApi.entities;

public record Artist(
    string Id,
    string Name,
    string Description,
    Position Position,
    string Country,
    string BeginArea,
    int BeginDate,
    Link[] Links,
    string Source
);