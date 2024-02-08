using Newtonsoft.Json;

namespace praktos12;

public partial class ListView : ContentPage
{
    private List<NoteBook> noteBooks;
    public ListView()
    {
        InitializeComponent();
        string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Notebook.json");

        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            noteBooks = JsonConvert.DeserializeObject<List<NoteBook>>(json);
            CountriesListView.ItemsSource = noteBooks;
        }
    }

    private void Remove_Clicked(object sender, EventArgs e)
    {
        if (CountriesListView.SelectedItem != null)
        {
            noteBooks.Remove(CountriesListView.SelectedItem as NoteBook);
        }
        string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Notebook.json"); 
        string json = JsonConvert.SerializeObject(noteBooks);
        File.WriteAllText(filePath, json);
        Refresh();
    }

    private void Refresh()
    {
        string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Notebook.json");

        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            noteBooks = JsonConvert.DeserializeObject<List<NoteBook>>(json);
            CountriesListView.ItemsSource = noteBooks;
        }
    }

    private void Edit_Clicked(object sender, EventArgs e)
    {
        if (CountriesListView.SelectedItem != null)
        {
            MainPage mainPage = new MainPage();
            mainPage.Edit(CountriesListView.SelectedItem as NoteBook, noteBooks.IndexOf(CountriesListView.SelectedItem as NoteBook));
            Navigation.PushAsync(mainPage);
        }
    }
}