using Newtonsoft.Json;

namespace praktos12
   
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }


        bool isEdit = false;
        private int indexNoteBook = -1;

        private void OnSaveClicked(object sender, EventArgs e)
        {
            try
            {
            if (isEdit)
            {
                string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Notebook.json");
                string updatejson = File.ReadAllText(filePath);
                List<NoteBook> existingNoteBook = new List<NoteBook>();
                existingNoteBook = JsonConvert.DeserializeObject<List<NoteBook>>(updatejson);
                NoteBook editedNoteBook = existingNoteBook[indexNoteBook];

                editedNoteBook.Name = entrName.Text;
                editedNoteBook.Number = int.Parse(entrNumber.Text);
                editedNoteBook.Lesson = entrAddress.Text;

                string updatedJson = JsonConvert.SerializeObject(existingNoteBook);
                File.WriteAllText(filePath, updatedJson);
                isEdit = false;
            }
            else
            {
                NoteBook newCountry = new NoteBook
                {
                    Lesson = entrAddress.Text,
                    Name = entrName.Text,
                    Number = int.Parse(entrNumber.Text),
                };

                string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Notebook.json");
                List<NoteBook> existingNoteBook = new List<NoteBook>();

                if (File.Exists(filePath))
                {
                    string json = File.ReadAllText(filePath);
                    existingNoteBook = JsonConvert.DeserializeObject<List<NoteBook>>(json);
                }

                existingNoteBook.Add(newCountry);

                string updatedJson = JsonConvert.SerializeObject(existingNoteBook);
                File.WriteAllText(filePath, updatedJson);

                entrAddress.Text = string.Empty;
                entrName.Text = string.Empty;
                entrNumber.Text = string.Empty;
            }
            }
            catch (Exception ex) { }
            
        }

        private void goToListViewClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ListView());
        }

        public void Edit(NoteBook noteBook, int index)
        {
            entrName.Text = noteBook.Name;
            entrNumber.Text = Convert.ToString(noteBook.Number);
            entrAddress.Text = Convert.ToString(noteBook.Lesson);
            isEdit = true;
            indexNoteBook = index;

        }

    }
    public class NoteBook
    {
        public string Name { get; set; }
        public string Lesson { get; set; }
        public int Number { get; set; }
    }

}
