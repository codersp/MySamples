using DTO.Employees;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
        string address = "http://www.angular.net/api/Employees";
        public MainWindow()
        {
            InitializeComponent();
            var tasks = new List<Task>();
            List<TodoItem> items = new List<TodoItem>();
            items.Add(new TodoItem() { Title = "Complete this WPF tutorial", Completion = 45 });
            items.Add(new TodoItem() { Title = "Learn C#", Completion = 80 });
            items.Add(new TodoItem() { Title = "Wash the car", Completion = 0 });
            //client.Timeout = TimeSpan.FromMilliseconds(2000);

            for (int i = 0; i < 10; i++)
            {
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    items.Add(GetTodoItem(i));
                }));
            }

            //then wait for all tasks to complete asyncronously
            Task.WaitAll(tasks.ToArray());
            ListBox1.ItemsSource = items;

            //then add the result of all the tasks to r in a treadsafe fashion
        }

        private TodoItem GetTodoItem(int i)
        {
            TodoItem item = new TodoItem();
            try
            {
                Employee employee = new Employee()
                {
                    FirstName = i.ToString(),
                    LastName = i.ToString(),
                    Email = string.Format("{0}@{0}.com", i),
                    Phone = "3333333333"
                };
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                Task<HttpResponseMessage> result = client.PostAsync(address, GetEmployee(employee));
                result.Result.EnsureSuccessStatusCode();
                string content = result.Result.Content.ReadAsStringAsync().Result;
                item.Title = content;
                item.Completion = i;
            }
            catch (Exception ex)
            {
                item.Title = ex.ToString();
                item.Completion = i;
            }

            return item;
        }

        public StringContent GetEmployee(Employee employee)
        {
            return new StringContent("{" + string.Format("\"firstName\": \"{0}\", \"lastName\": \"{1}\",\"email\": \"{2}\"", employee.FirstName, employee.LastName, employee.Email) + "}", Encoding.UTF8, "application/json");
        }
    }

    public class TodoItem
    {
        public string Title { get; set; }
        public int Completion { get; set; }
    }
}
