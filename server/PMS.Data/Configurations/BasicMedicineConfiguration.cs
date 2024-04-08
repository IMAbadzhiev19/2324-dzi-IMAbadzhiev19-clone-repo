using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using PMS.Data.Models.PharmacyEntities;

namespace PMS.Data.Configurations;

/// <summary>
/// Configuration class for the entity BasicMedicine.
/// </summary>
public class BasicMedicineConfiguration : IEntityTypeConfiguration<BasicMedicine>
{
    /// <summary>
    /// Configures the entity BasicMedicine.
    /// </summary>
    /// <param name="builder">The entity type builder.</param>
    public void Configure(EntityTypeBuilder<BasicMedicine> builder)
    {
        builder.HasKey(m => m.Id);

        builder
            .Property(m => m.Description)
            .IsRequired(false);

        builder.HasData(this.ExtractMedicines());
    }

    /// <summary>
    /// Extracts BasicMedicine entities from a JSON file.
    /// </summary>
    /// <returns>An array of BasicMedicine entities.</returns>
    private BasicMedicine[] ExtractMedicines()
    {
        string pathToResourceFile =
            Path.GetFullPath($@"{this.GetParentDirectory(Directory.GetCurrentDirectory(), "server")}\PMS.Data\SeedingResources\BasicMedicines.json");
        string json = File.ReadAllText(pathToResourceFile);

        var medicines = JsonConvert.DeserializeObject<BasicMedicine[]>(json);

        foreach (var medicine in medicines)
        {
            medicine.Id = Guid.NewGuid().ToString();
        }

        return medicines;
    }

    /// <summary>
    /// Gets the path to server from any path given.
    /// </summary>
    /// <param name="basePath"></param>
    /// <param name="targetDirectoryName"></param>
    /// <returns></returns>
    private string GetParentDirectory(string basePath, string targetDirectoryName)
    {
        DirectoryInfo directoryInfo = new DirectoryInfo(basePath);

        while (directoryInfo != null && directoryInfo.Name.ToLower() != targetDirectoryName.ToLower())
        {
            directoryInfo = directoryInfo.Parent!;
        }

        return directoryInfo?.FullName!;
    }
}