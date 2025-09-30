using ImagesApi.DAL.Context;
using Microsoft.EntityFrameworkCore;

namespace ImagesApi.DAL.Repository
{
    public class ImageRepository (AppDbContext context)
    {
        private readonly AppDbContext _context = context;

        public async Task<ImagesApi.DAL.DbEntities.Image> AddImage(ImagesApi.DAL.DbEntities.Image image)
        {
            await _context.Images.AddAsync(image);
            await _context.SaveChangesAsync();
            return image;
        }

        public async Task<IEnumerable<ImagesApi.DAL.DbEntities.Image>> GetImages()
        {
            return await _context.Images.ToListAsync();
        }
        public async Task<ImagesApi.DAL.DbEntities.Image?> GetImage(int id)
        {
            return await _context.Images.FirstOrDefaultAsync(img => img.Id == id);
        }

        public async Task UpdateImage(ImagesApi.DAL.DbEntities.Image newImage)
        {
            var oldImage = await _context.Images.FirstOrDefaultAsync(g => g.Id == newImage.Id);
            if (oldImage != null)
            {
                _context.Entry(oldImage).CurrentValues.SetValues(newImage);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteImage(int id)
        {
            var image = await _context.Images.FirstOrDefaultAsync(g => g.Id == id);

            if (image == null) return;

            _context.Images.Remove(image);
            await _context.SaveChangesAsync();
        }
    }
}
