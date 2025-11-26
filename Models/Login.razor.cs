using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Proyecto.Pages
{
    public partial class Login : ComponentBase
    {
        [Inject]
        private NavigationManager Navigation { get; set; } = default!;

        [Inject]
        private IJSRuntime JS { get; set; } = default!;

        private LoginModel loginModel = new();
        private string errorMessage = "";
        private string successMessage = "";
        private bool isLoading = false;

        // Modelo para el resultado del JSInterop
        public class FirebaseLoginResult
        {
            public bool Success { get; set; }
            public string Uid { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
            public string Code { get; set; } = string.Empty; // Código de error de Firebase
            public string Message { get; set; } = string.Empty; // Mensaje de error
        }

        private async Task HandleLogin()
        {
            try
            {
                isLoading = true;
                errorMessage = "";
                successMessage = "";
                StateHasChanged();

                // 1. Llamar a la función JS que usa el SDK de Firebase
                var result = await JS.InvokeAsync<FirebaseLoginResult>("firebaseLogin",
                    loginModel.Username, loginModel.Password);

                if (result.Success)
                {
                    // LOGIN EXITOSO
                    successMessage = $"¡Bienvenido {result.Email}!";

                    // 2. Guardar información de sesión (UID de Firebase) en localStorage
                    await JS.InvokeVoidAsync("localStorage.setItem", "firebaseUid", result.Uid);
                    await JS.InvokeVoidAsync("localStorage.setItem", "firebaseEmail", result.Email);
                    await JS.InvokeVoidAsync("localStorage.setItem", "isAuthenticated", "true"); // Marcar como autenticado

                    StateHasChanged();

                    // 3. Redirigir
                    await Task.Delay(1000);
                    Navigation.NavigateTo("/tienda", true);
                }
                else
                {
                    // LOGIN FALLIDO
                    errorMessage = GetFriendlyErrorMessage(result.Code);
                }
            }
            catch (Exception ex)
            {
                errorMessage = $"Error interno: {ex.Message}";
            }
            finally
            {
                isLoading = false;
                StateHasChanged();
            }
        }

        private void GoToHome()
        {
            Navigation.NavigateTo("/tienda", true);
        }

        // Traduce los códigos de error de Firebase a mensajes amigables
        private string GetFriendlyErrorMessage(string errorCode)
        {
            return errorCode switch
            {
                "auth/user-not-found" => "El usuario no existe.",
                "auth/wrong-password" => "Contraseña incorrecta.",
                "auth/invalid-email" => "El formato del correo electrónico es inválido.",
                _ => $"Error de autenticación: {errorCode}",
            };
        }

        // MODELO DEL FORMULARIO (Username ahora representa el Correo Electrónico)
        public class LoginModel
        {
            [Required(ErrorMessage = "El correo electrónico es requerido")]
            [EmailAddress(ErrorMessage = "Debe ser un correo electrónico válido")]
            public string Username { get; set; } = ""; // Renombrado a Email en Firebase

            [Required(ErrorMessage = "La contraseña es requerida")]
            [MinLength(6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres")] // Requisito mínimo de Firebase
            public string Password { get; set; } = "";
        }
    }
}
