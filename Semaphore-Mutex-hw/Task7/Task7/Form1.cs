namespace Task7
{
    public partial class Form1 : Form
    {
        private Mutex mutex = new Mutex(false);

        private int[] nums = { 1, 2, 3, 4, 5 };

        private Random rand = new Random();

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
            int num = rand.Next(1, 5);
            Invoke(new Action(() => listView1.Items.Add("Array: ")));

            for (int i = 0; i < nums.Length; i++)
            {
                Invoke(new Action(() => listView1.Items.Add(nums[i].ToString())));
            }

            Invoke(new Action(() => listView1.Items.Add("Increment: " + num)));

            Thread thread1 = new Thread(() => increaseValue(nums, num));
            Thread thread2 = new Thread(() => findMax(nums, num));

            thread1.Start();
            thread2.Start();

            Invoke(new Action(() => listView1.Items.Add("New array: ")));
            for (int i = 0; i < nums.Length; i++)
            {
                Invoke(new Action(() => listView1.Items.Add(nums[i].ToString())));
            }
        }
        private void increaseValue(int[] nums, int num)
        {
            mutex.WaitOne();
            Invoke(new Action(() => listView1.Items.Add("Thread 1 start")));

            for (int i = 0; i < nums.Length; i++)
            {
                nums[i] += num;
            }

            Invoke(new Action(() => listView1.Items.Add("Thread 1 end")));
            mutex.ReleaseMutex();
        }
        private void findMax(int[] nums, int num)
        {
            mutex.WaitOne();
            Invoke(new Action(() => listView1.Items.Add("Thread 2 start")));

            Invoke(new Action(() => listView1.Items.Add("Max num: " + nums.Max())));

            Invoke(new Action(() => listView1.Items.Add("Thread 2 end")));
            mutex.ReleaseMutex();
        }

    }
}
