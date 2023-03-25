// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EasyAcme.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        public RegisterModel()
        {
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>

        public Task OnGetAsync()
        {
            return Task.CompletedTask;
        }

        public Task<IActionResult> OnPostAsync()
        {
            return Task.FromResult<IActionResult>(BadRequest());
        }
    }
}