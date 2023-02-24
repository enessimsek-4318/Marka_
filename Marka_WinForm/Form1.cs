namespace Marka_WinForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5025/");
            HttpResponseMessage response = await client.GetAsync("api/User");
            string result = await response.Content.ReadAsStringAsync();
            List<User> users=Newtonsoft.Json.JsonConvert.DeserializeObject<List<User>>(result);
            listBox1.DataSource = users.Select(x => x.FullName).ToList();
        }
    }
}