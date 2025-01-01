// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;

namespace AutoService.Areas.Identity.Pages.Account.Manage
{
    public class EmailModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IEmailSender _emailSender;

        public EmailModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
        }

        /// <summary>
        ///     Цей API підтримує інфраструктуру за замовчуванням для ASP.NET Core Identity і не призначений для використання
        ///     безпосередньо в вашому коді. Цей API може змінюватися або бути видаленим у майбутніх випусках.
        /// </summary>
        [Display(Name = "Поточна електронна адреса")]
        public string Email { get; set; }

        /// <summary>
        ///     Цей API підтримує інфраструктуру за замовчуванням для ASP.NET Core Identity і не призначений для використання
        ///     безпосередньо в вашому коді. Цей API може змінюватися або бути видаленим у майбутніх випусках.
        /// </summary>
        public bool IsEmailConfirmed { get; set; }

        /// <summary>
        ///     Цей API підтримує інфраструктуру за замовчуванням для ASP.NET Core Identity і не призначений для використання
        ///     безпосередньо в вашому коді. Цей API може змінюватися або бути видаленим у майбутніх випусках.
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }

        /// <summary>
        ///     Цей API підтримує інфраструктуру за замовчуванням для ASP.NET Core Identity і не призначений для використання
        ///     безпосередньо в вашому коді. Цей API може змінюватися або бути видаленим у майбутніх випусках.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     Цей API підтримує інфраструктуру за замовчуванням для ASP.NET Core Identity і не призначений для використання
        ///     безпосередньо в вашому коді. Цей API може змінюватися або бути видаленим у майбутніх випусках.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     Цей API підтримує інфраструктуру за замовчуванням для ASP.NET Core Identity і не призначений для використання
            ///     безпосередньо в вашому коді. Цей API може змінюватися або бути видаленим у майбутніх випусках.
            /// </summary>
            [Required]
            [EmailAddress]
            [Display(Name = "Нова електронна адреса")]
            public string NewEmail { get; set; }
        }

        private async Task LoadAsync(IdentityUser user)
        {
            var email = await _userManager.GetEmailAsync(user);
            Email = email;

            Input = new InputModel
            {
                NewEmail = email,
            };

            IsEmailConfirmed = await _userManager.IsEmailConfirmedAsync(user);
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Не вдалося завантажити користувача з ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostChangeEmailAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Не вдалося завантажити користувача з ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var email = await _userManager.GetEmailAsync(user);
            if (Input.NewEmail != email)
            {
                var userId = await _userManager.GetUserIdAsync(user);
                var code = await _userManager.GenerateChangeEmailTokenAsync(user, Input.NewEmail);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                var callbackUrl = Url.Page(
                    "/Account/ConfirmEmailChange",
                    pageHandler: null,
                    values: new { area = "Identity", userId = userId, email = Input.NewEmail, code = code },
                    protocol: Request.Scheme);
                await _emailSender.SendEmailAsync(
                    Input.NewEmail,
                    "Підтвердження вашої електронної адреси",
                    $"Будь ласка, підтвердіть вашу обліковку, <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>натиснувши тут</a>.");

                StatusMessage = "Лист для підтвердження зміни електронної адреси надіслано. Будь ласка, перевірте вашу пошту.";
                return RedirectToPage();
            }

            StatusMessage = "Ваша електронна адреса не змінена.";
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostSendVerificationEmailAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Не вдалося завантажити користувача з ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var userId = await _userManager.GetUserIdAsync(user);
            var email = await _userManager.GetEmailAsync(user);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var callbackUrl = Url.Page(
                "/Account/ConfirmEmail",
                pageHandler: null,
                values: new { area = "Identity", userId = userId, code = code },
                protocol: Request.Scheme);
            await _emailSender.SendEmailAsync(
                email,
                "Підтвердження вашої електронної адреси",
                $"Будь ласка, підтвердіть вашу обліковку, <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>натиснувши тут</a>.");

            StatusMessage = "Лист для підтвердження надіслано. Будь ласка, перевірте вашу пошту.";
            return RedirectToPage();
        }
    }
}
