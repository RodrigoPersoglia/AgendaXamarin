using Agenda.Entidades;
using Agenda.Service;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Agenda.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AgregarContactoPage : ContentPage
    {
        MainPage mainPage;
        byte[] imagenBytes;
        private MediaFile foto;

        public AgregarContactoPage(MainPage mainPage)
        {
            InitializeComponent();
            this.mainPage = mainPage;
        }

        async void OnSeleccionarImagenClicked(object sender, EventArgs e)
        {
            await TomarFotoAsync();
        }

        async void OnGuardarClicked(object sender, EventArgs e)
        {
            var nuevoContacto = new Contacto
            {
                Nombre = entryNombre.Text,
                NumeroTelefono = entryTelefono.Text,
                Imagen = imagenBytes
            };

            var service = new ContactoService();
            mainPage.servicioContacto.AgregarContacto(nuevoContacto);
            mainPage.CargarContactos();

            await Navigation.PopAsync();
        }

        private async Task TomarFotoAsync()
        {
            try
            {
                await CrossMedia.Current.Initialize();

                if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                {
                    await DisplayAlert("Error", "La cámara no está disponible.", "OK");
                    return;
                }

                var opciones = new StoreCameraMediaOptions
                {
                    Directory = "Sample",
                    Name = "miImagen.jpg"
                };

                foto = await CrossMedia.Current.TakePhotoAsync(opciones);

                if (foto == null)
                    return;

                using (var memoryStream = new MemoryStream())
                {
                    await foto.GetStream().CopyToAsync(memoryStream);
                    foto.Dispose();

                    imagenBytes = memoryStream.ToArray();

                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Error: {ex.Message}", "OK");
            }
        }
    }
}