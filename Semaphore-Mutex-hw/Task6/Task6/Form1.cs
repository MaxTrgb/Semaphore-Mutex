using System.Windows.Forms;

namespace Task6
{
    public partial class Form1 : Form
    {
        private Mutex mutex = new Mutex();

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            runThreads();
        }

        private void runThreads()
        {
            Thread thread1 = new Thread(ascendingOrder);
            Thread thread2 = new Thread(descendingOrder);

            thread1.Start();
            thread2.Start();
        }
        private void ascendingOrder()
        {
            mutex.WaitOne();
            Invoke(new Action(() => listView1.Items.Add("Thread 1 started")));

            for (int i = 0; i <= 20; i++)
            {
                int value = i;
                Invoke(new Action (()=> listView1.Items.Add(value.ToString())));
                Thread.Sleep(200);
            }

            Invoke(new Action(() => listView1.Items.Add("Thread 1 end")));
            mutex.ReleaseMutex();
        }
        private void descendingOrder()
        {
            mutex.WaitOne();
            Invoke(new Action(() => listView1.Items.Add("Thread 2 started")));

            for (int i = 10; i >= 0; i--)
            {
                int value = i; 
                Invoke(new Action(() => listView1.Items.Add(value.ToString())));
                Thread.Sleep(200);
            }

            Invoke(new Action(() => listView1.Items.Add("Thread 2 end")));
            mutex.ReleaseMutex();
        }
    }
}
