namespace Kathanika.Domain.Aggregates.BibRecordAggregate;

internal enum MaterialType
{
    /// <summary>Textual material, including books, pamphlets, and printed sheets.</summary>
    Book,

    /// <summary>Serial publications issued in successive parts.</summary>
    Serial,

    /// <summary>Cartographic materials, including maps, atlases, globes, etc.</summary>
    Map,

    /// <summary>Visual materials, including motion pictures, videorecordings, etc.</summary>
    VisualMaterial,

    /// <summary>Mixed materials, including archival and manuscript collections.</summary>
    MixedMaterial,

    /// <summary>Computer files, including software, numeric data, etc.</summary>
    ComputerFile,

    /// <summary>Musical materials, including scores, sound recordings, etc.</summary>
    Music
}