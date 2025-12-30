using PlatformService.Models;

namespace PlatformService.Data
{
    public class PlatformRepo : IPlatformRepo
    {
        AppDbContext _context;

        public PlatformRepo(AppDbContext context)
        {
            _context = context;
        }

        bool IPlatformRepo.SaveChanges()
        {
            return _context.SaveChanges() >= 0;
        }

        IEnumerable<Platform> IPlatformRepo.GetAllPlatforms()
        {
            return _context.Platforms.ToList();
        }

        Platform IPlatformRepo.GetPlatformById(int id)
        {
            var platform = _context.Platforms.FirstOrDefault(p => p.Id == id);

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
            _context.Platforms.Add(platform);
        }
    }
}
