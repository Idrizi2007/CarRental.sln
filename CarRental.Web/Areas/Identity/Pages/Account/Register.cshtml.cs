using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CarRental.Web.Areas.Identity.Pages.Account;

// Public registration is switched off: the site has exactly one seeded admin account.
// This override must stay. Deleting it would bring back the Identity UI package's
// built-in Register page, and anyone could create an account.
public class RegisterModel : PageModel
{
    public IActionResult OnGet() => NotFound();

    public IActionResult OnPost() => NotFound();
}
