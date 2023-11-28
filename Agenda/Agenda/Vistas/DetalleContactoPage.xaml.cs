using Agenda.Entidades;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Agenda.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetalleContactoPage : ContentPage
    {
        public DetalleContactoPage(Contacto contacto)
        {
            InitializeComponent();
            lblNombre.Text = contacto.Nombre;
            lblTelefono.Text = contacto.NumeroTelefono;
            imgContacto.Source = ImageSource.FromStream(() => new MemoryStream(contacto.Imagen));
        }
    }
}