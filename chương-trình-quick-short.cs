using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thuyet_trinh
{
    // Lớp Timing dùng để đo thời gian chạy của thuật toán
    public class Timing
    {
        TimeSpan startingTime; // Thời gian bắt đầu đo
        TimeSpan duration; // Thời gian thực thi của thuật toán

        public Timing()
        {
            startingTime = new TimeSpan(0);
            duration = new TimeSpan(0);
        }

        // Dừng đo thời gian
        public void StopTime()
        {
            duration = Process.GetCurrentProcess().Threads[0].UserProcessorTime.Subtract(startingTime);
        }

        // Bắt đầu đo thời gian
        public void StartTime()
        {
            GC.Collect(); // Dọn dẹp bộ nhớ trước khi đo
            GC.WaitForPendingFinalizers();
            startingTime = Process.GetCurrentProcess().Threads[0].UserProcessorTime;
        }

        // Trả về thời gian thực thi của thuật toán
        public TimeSpan Result()
        {
            return duration;
        }
    }

    class Program
    {
        // Thuật toán Quick Sort
        static void QuickSort(int[] mang, int trai, int phai)
        {
            if (trai < phai)
            {
                // Phân hoạch mảng và lấy vị trí chốt
                int chiSoChot = PhanHoach(mang, trai, phai);

                // Đệ quy sắp xếp phần bên trái của chốt
                QuickSort(mang, trai, chiSoChot - 1);

                // Đệ quy sắp xếp phần bên phải của chốt
                QuickSort(mang, chiSoChot + 1, phai);
            }
        }

        // Hàm chia mảng thành hai phần theo chốt
        static int PhanHoach(int[] mang, int trai, int phai)
        {
            //3 trường hợp chốt
            // Chốt là phần tử cuối cùng
            int chot = mang[phai];

            // Chốt là phần tử đầu tiên 
             //HoanDoi(mang, trai, phai);
             //int chot = mang[phai];

            // Chốt là phần tử giữa 
             //int mid = trai + (phai - trai) / 2;
             //HoanDoi(mang, mid, phai);
             //int chot = mang[phai];

            int i = trai - 1;

            for (int j = trai; j < phai; j++)
            {
                if (mang[j] < chot)
                {
                    i++;
                    HoanDoi(mang, i, j);
                }
            }
            HoanDoi(mang, i + 1, phai);
            return i + 1;
        }

        // Hàm hoán đổi vị trí hai phần tử trong mảng
        static void HoanDoi(int[] mang, int a, int b)
        {
            int tam = mang[a];
            mang[a] = mang[b];
            mang[b] = tam;
        }

        // Hàm tạo mảng tăng dần 
        static int[] Mangxuoingaunhien(int size)
        {
            int[] array = new int[size];
            for (int i = 0; i < size; i++)
            {
                array[i] = i + 1;
            }
            return array;
        }

        // Hàm tạo mảng giảm dần 
        static int[] Mangnguocngaunhien(int size)
        {
            int[] array = new int[size];
            for (int i = 0; i < size; i++)
            {
                array[i] = size - i;
            }
            return array;
        }

        // Hàm tạo mảng ngẫu nhiên 
        static int[] Mangngaunhien(int size)
        {
            Random rand = new Random();
            int[] array = new int[size];
            for (int i = 0; i < size; i++)
            {
                array[i] = rand.Next(1, 100000);
            }
            return array;
        }

        // Hàm kiểm tra thời gian chạy của Quick Sort với từng loại mảng
        static void Thoigianchay(string testName, int[] array)
        {
            Timing timer = new Timing();
            double totalTime = 0;
            int iterations = 100;//Số lần lặp lại chương trình

            for (int i = 0; i < iterations; i++)
            {
                int[] testArray = (int[])array.Clone();
                timer.StartTime();
                QuickSort(testArray, 0, testArray.Length - 1);
                timer.StopTime();
                totalTime += timer.Result().TotalMilliseconds;
            }

            double averageTime = totalTime / iterations;
            Console.WriteLine($"{testName}: {averageTime} ms trung bình sau {iterations} lần chạy");
        }

        static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;
            int size = 10000;

            int[] sortedArray = Mangxuoingaunhien(size);
            int[] reverseSortedArray = Mangnguocngaunhien(size);
            int[] randomArray = Mangngaunhien(size);

            Console.WriteLine("Đo thời gian QuickSort (100 lần chạy mỗi loại):");

            Thoigianchay("Mảng tăng dần", sortedArray);
            Thoigianchay("Mảng giảm dần", reverseSortedArray);
            Thoigianchay("Mảng ngẫu nhiên", randomArray);
        }
    }
}
