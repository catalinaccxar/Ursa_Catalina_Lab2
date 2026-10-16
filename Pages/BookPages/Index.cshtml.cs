using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Ursa_Catalina_Lab2.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ursa_Catalina_Lab2.Pages.BookPages;

public class IndexModel : PageModel
{
    private readonly Ursa_Catalina_Lab2Context _context;

    public IndexModel(Ursa_Catalina_Lab2Context context)
    {
        _context = context;
    }

    public IList<Book> Book { get; set; } = default!;
    [BindProperty(SupportsGet = true)]
    public int? AuthorID { get; set; }

    public SelectList Authors { get; set; } = default!;

    public async Task OnGetAsync()
    {
        Authors = new SelectList(_context.Authors, "ID", "FullName");

        var books = _context.Book
            .Include(b => b.Publisher)
            .Include(b => b.Author)
            .AsQueryable();

        if (AuthorID.HasValue)
        {
            books = books.Where(b => b.AuthorID == AuthorID);
        }

        Book = await books.ToListAsync();
    }
}
