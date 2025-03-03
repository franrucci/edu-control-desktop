using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Controladora.Patron_Facade
{
    public class RecuperacionContraseñaFacade
    {
        private readonly EmailService.EmailService emailService;
        private readonly ControladoraUsuarios controladoraUsuarios;

        public RecuperacionContraseñaFacade()
        {
            emailService = new EmailService.EmailService();
            controladoraUsuarios = ControladoraUsuarios.Instancia;
        }


        public bool EnviarTokenRecuperacion(string email, out string mensajeError)
        {
            mensajeError = string.Empty;

            // Busca el usuario por su correo electrónico
            Usuario usuario = controladoraUsuarios.BuscarUsuarioPorEmail(email);

            if (usuario == null)
            {
                mensajeError = "Error: El correo electrónico no está registrado.";
                return false;
            }

            // Genera un token
            string token = controladoraUsuarios.GenerarToken();

            // Encripta el token usando el mismo Salt del usuario
            string tokenEncriptado = controladoraUsuarios.EncriptarToken(token, usuario.Salt);

            // Asigna el token encriptado al usuario
            usuario.Token = tokenEncriptado;
            controladoraUsuarios.ActualizarUsuario(usuario);

            // Envia el token por correo electrónico
            string asunto = "Recuperación de Contraseña";
            string mensaje = $"Hola {usuario.Usuario1},\n\nTu token de recuperación es: {token}\n\nUtiliza este token para ingresar al sistema y cambiar tu contraseña.";

            string error;
            emailService.EnviarCorreo(new StringBuilder(mensaje), DateTime.Now, "AcaDebe@irElEmail.com", usuario.Email, asunto, out error); // Aca utilizo mi correo para enviar el token. Aca debe ir el email.

            if (error != "Mensaje enviado correctamente.")
            {
                mensajeError = $"Error al enviar el correo: {error}";
                return false;
            }

            return true;
        }
    }
}
