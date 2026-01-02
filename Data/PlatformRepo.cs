using PlatformService.Models;

namespace PlatformService.Data
{
    public class PlatformRepo(AppDbContext context) : IPlatformRepo
    {
        bool IPlatformRepo.SaveChanges()
        {
            return context.SaveChanges() >= 0;
        }

        IEnumerable<Platform> IPlatformRepo.GetAllPlatforms()
        {
            return context.Platforms.ToList();
        }

        Platform? IPlatformRepo.GetPlatformById(int id)
        {
            var platform = context.Platforms.FirstOrDefault(p => p.Id == id);

            if (platform is null)
            {
                throw new ArgumentNullException(nameof(platform));
            }
            return platform;
        }

        void IPlatformRepo.CreatePlatform(Platform platform)
        {
            if (platform is null)
            {
                throw new ArgumentNullException(nameof(platform));
            }
            context.Platforms.Add(platform);
        }
    }
}
