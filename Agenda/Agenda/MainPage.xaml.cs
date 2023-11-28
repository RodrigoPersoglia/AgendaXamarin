using Agenda.Entidades;
using Agenda.Service;
using Agenda.Vistas;
using System;
using Xamarin.Forms;

namespace Agenda
{
    public partial class MainPage : ContentPage
    {
        public ContactoService servicioContacto;

        public MainPage()
        {
            InitializeComponent();
            servicioContacto = new ContactoService();
            CargarContactos();
        }

        public void CargarContactos()
        {
            listaContactos.ItemsSource = servicioContacto.ObtenerContactos();
        }

        async void OnContactoSeleccionado(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                var contactoSeleccionado = (Contacto)e.SelectedItem;
                await Navigation.PushAsync(new DetalleContactoPage(contactoSeleccionado));
                listaContactos.SelectedItem = null;
            }
        }

        async void OnAgregarContactoClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AgregarContactoPage(this));
        }
    }

}
