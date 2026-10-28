using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Ursa_Catalina_Lab2.Models;

namespace Ursa_Catalina_Lab2.Pages.PublisherPages;

public class DeleteModel : PageModel
{
    private readonly Ursa_Catalina_Lab2Context _context;

    public DeleteModel(Ursa_Catalina_Lab2Context context)
    {
        _context = context;
    }

    [BindProperty]
    public Publisher Publisher { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var publisher = await _context.Publisher.FirstOrDefaultAsync(m => m.ID == id);
        if (publisher is null)
        {
            return NotFound();
        }
        else
        {
            Publisher = publisher;
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var publisher = await _context.Publisher
            .Include(p => p.Books)
            .FirstOrDefaultAsync(p => p.ID == id);

        if (publisher != null)
        {

            var bookIds = publisher.Books.Select(b => b.ID).ToList();
            var authorIds = publisher.Books.Select(b => b.AuthorID).Distinct().ToList();

            var authorsToDelete = await _context.Authors
                .Where(a => authorIds.Contains(a.ID)
                         && a.Books.All(b => bookIds.Contains(b.ID)))
                .ToListAsync();

            _context.Book.RemoveRange(publisher.Books);
            _context.Authors.RemoveRange(authorsToDelete);
            _context.Publisher.Remove(publisher);

            await _context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}
